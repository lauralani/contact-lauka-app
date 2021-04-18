function loadtextcaptcha() {
    fetch('https://api.textcaptcha.com/lauka.app.json', { mode: "no-cors" })
        .then(response => response.text())
        .then(data => {
            console.log("test");
            console.log(data);
        });
}
