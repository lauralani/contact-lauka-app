function loadtextcaptcha() {
    fetch('https://api.textcaptcha.com/lauka.app.json', { mode: "no-cors" })
        .then((response) => { console.log(response)});
        //.then(response => response.json())
        //.then(data => console.log(data));
}
