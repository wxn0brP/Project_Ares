namespace Ares;
using Jint;
using System;
using System.IO;

public class JavaScriptEngineC{
    public static Engine engine = new Engine();
    public static bool initTrue = false;
    public static void Init(){
        if(initTrue) return;
        initTrue = true;
        //akcje
        engine.SetValue("msg", new Action<string>(msg));

        //klasy Aresa
        engine.SetValue("CheckAresC", new CheckAresC());
        engine.SetValue("InstallC", new InstallC());
        engine.SetValue("StartParamsC", new StartParamsC());
        engine.SetValue("UpdateC", new UpdateC());
        engine.SetValue("DeveloperC", new DeveloperC());
        engine.SetValue("DvC", new DvC());
        engine.SetValue("ConverterSys", new ConverterSys());
        engine.SetValue("EncryptC", new EncryptC());
        engine.SetValue("ErrorC", new ErrorC());
        engine.SetValue("JSE", new JavaScriptEngineC());
        engine.SetValue("MessC", new MessC());
        engine.SetValue("NetC", new NetC());
        engine.SetValue("RamControlC", new RamControlC());
        engine.SetValue("VersionC", new VersionC());
        engine.SetValue("ModuleC", new ModuleC());
        engine.SetValue("LicenseC", new LicenseC());
        engine.SetValue("ProC", new ProC());
        engine.SetValue("ReputationScoreC", new ReputationScoreC());
        engine.SetValue("ConfigAresToModuleC", new ConfigAresToModuleC());
        engine.SetValue("AddsC", new AddsC(0, 0, ""));
        engine.SetValue("ConfigData", new ConfigData());
        engine.SetValue("LanguageC", new LanguageC());


        // string index = "./js/index.js";
        // try{
        //     if(File.Exists(index)) JsFile("./js/index.js");
        // }catch(Exception e){
        //     ErrorC.InfDev("Błąd wykonywania "+index+".\n"+e.ToString(), "JSEC - index.js"); 
        // }
    }

    private static void msg(string a){
        MessageBox.Show(a+"");
    }

    public static void Js(string a){
        engine.Execute(a);
    }

    public static void JsFile(string a){
        if(File.Exists(a)){
            engine.Execute(File.ReadAllText(a));
        }else{
            ErrorC.InfDev("Plik nie istnieje: "+a, "js");
        }
    }
}