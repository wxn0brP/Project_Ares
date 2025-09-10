var pathed = ".";
console.log("%cElo witam w debuggerze", "color: red;");

function engine_start(path){
    pathed = path;
	var menu = __("#menu");
	var data = "";
	data += __.httpReq(path+"/head.html");
	data += __.httpReq(path+"/nav.html");
	menu.html(data);

    __("#footer").html(__.httpReq(path+"/footer.html"));
    
    setPageStart();
}

function del(){}

var loadPage = {};
var page = __("#page")
var anim = __("#anim");

function setPage(strona, ani=true){
    del();
    del = function(){}
    var t;
    if(!loadPage[strona]){
        let temp = __.httpReq(pathed+"/pages/"+strona+".html");
        if(temp) t = loadPage[strona] = temp;
        else{
            setPage("main");
            return;
        }
    }else t = loadPage[strona];

    t = t.replaceAll("###path###", pathed);
    let script_path = t.substring(t.indexOf("###script###")+12, t.indexOf("###scripd###")).replaceAll("###path###", pathed);

    function str(){
        page.html(t);
        let script = document.createElement("script")
        script.src = script_path;
        page.add(script);
        changeLang();
    }

    if(ani){
        anim.g().style.animation = "animLoad 1s linear";
        setTimeout(() => {
            str();
        }, 700);
        setTimeout(()=>{anim.g().style.animation=""}, 1000);
    }else{
        str();
    }
    window.history.replaceState('', '', updateURLParameter(window.location.href, "p", strona));
}

function setPageStart(){
    var urlParams = new URLSearchParams(window.location.search);
    setPage((urlParams.get('p') || "main"), false);
    changeLang(urlParams.get('l'));
}