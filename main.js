function loadtextcaptcha() {
    //fetch('http://api.textcaptcha.com/lauka.app.json', { mode: "no-cors" })
    //    .then((data) => {
    //        data.json()
    //    })
    //    .then((response) => {
    //        console.log(response)
    //    });


    var requestOptions = {
        method: 'GET',
        redirect: 'follow',
        mode: "same-origin"
    };

    fetch("/api/captcha", requestOptions)
        .then(response => response.json())
        .then(result => {
            document.getElementById("label-captcha").innerHTML = result.q;
            document.getElementById('captcha-hash').value = JSON.stringify(result.a);
        })
        .catch(error => console.log('error', error));
}
