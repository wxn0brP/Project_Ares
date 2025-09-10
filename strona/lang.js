const langs = ["pl", "en"];
const defLang = navigator.language;
const preferLang = langs.includes(defLang) ? defLang : "pl";
var currlang = preferLang;

function changeLang(lang=null){
    if(lang==null) lang = currlang;
    else currlang = lang;
    
    if(!langs.includes(lang)) lang = preferLang;
    [...document.querySelectorAll('langTag')].forEach(el => {
        el.classList.add('noActiveLang');
    });
    [...document.querySelectorAll('[lang="'+lang+'"]')].forEach(el => {
        el.classList.remove('noActiveLang');
    });
}