var captchahash = null;

function loadtextcaptcha() {
    //fetch('http://api.textcaptcha.com/lauka.app.json', { mode: "no-cors" })
    //    .then((data) => {
    //        data.json()
    //    })
    //    .then((response) => {
    //        console.log(response)
    //    });

    var options = {
        method: 'GET',
        redirect: 'follow',
        mode: "same-origin"
    };

    fetch("/api/captcha", options)
        .then(response => response.json())
        .then(result => {
            document.getElementById("label-captcha").innerHTML = result.q;
            captchahash = result.a;
        })
        .catch(error => {
            console.log('error', error);
            document.getElementById("label-captcha").innerHTML = "Couldn't get CAPTCHA text. Please try again later!";
            setmessage("error", "Couldn't get CAPTCHA text. Please try again later!");
        }
        );
}

function getformdata() {
    var data = {
        name: document.getElementById("name").value,
        email: document.getElementById("email").value,
        message: document.getElementById("message").value,
        captcha: document.getElementById("captcha").value,
        captchahash: captchahash
    };
    return data;
}

function sendform(data) {
    let url = "/api/contact"
    var options = {
        method: 'POST',
        mode: "same-origin",
        body: JSON.stringify(data)
    }
    fetch(url, options)
        .then(data => data.text())
        .then(response => handleformresponse(response))
        .catch((error) => {
            handleformresponse("UndefinedError");
            console.log("Error:", error);
        });
}

function formsubmit() {
    setsubmitbuttondisabled(true);
    setformstatedisabled(true);
    document.getElementById("submit").innerHTML = "Please wait...";

    var data = getformdata();
    sendform(data);
}

function handleformresponse(status) {
    switch (status) {
        case "OK":
            setmessage("success", "Your message has been sent!");
            document.getElementById("submit").innerHTML = "Thanks!";
            break;

        case "InvalidCaptcha":
            console.log("Captcha Invalid");
            setmessage("error", "The CAPTCHA response is invalid! Please refresh and try again!");
            break;

        case "InvalidRequest":
            console.log("Request Invalid");
            setmessage("error", "Somehow the request received by our backend was malformed...");
            break;

        case "MailSendError":
            console.log("Mail Backend failed");
            setmessage("error", "The email could not be sent in our backend. Please try again later!");
            break;

        case "UndefinedError":
            console.log("Something REALLY went wrong there...");
            break;
        default:
            break;
    }
}

function setmessage(type, message) {
    switch (type) {
        case "info":
            document.getElementById("info-message").innerHTML = message;
            break;
        case "success":
            document.getElementById("success-message").innerHTML = message;
            break;
        case "error":
            document.getElementById("error-message").innerHTML = message;
            break;

        default:
            break;
    }
}

function setformstatedisabled(bool) {
    document.getElementById("name").disabled = bool;
    document.getElementById("email").disabled = bool;
    document.getElementById("message").disabled = bool;
    document.getElementById("captcha").disabled = bool;
}

function setsubmitbuttondisabled(bool) {
    document.getElementById("submit").disabled = bool;
}
