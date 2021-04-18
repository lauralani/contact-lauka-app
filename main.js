function loadtextcaptcha() {
    fetch('https://api.textcaptcha.com/lauka.app.json', { mode: "no-cors" })
        .then((data) => {
            console.log(data);
        });
}
