namespace Ares;
using Newtonsoft.Json;

public class LanguageC{
    public static void Init(){
        worlds = null;
        lang = "";
        string[] opcjeF = Directory.GetFiles("lang/", "*.json");
        string[] opcje = new string[opcjeF.Length+1];
        for(int i=0; i<opcjeF.Length; i++){
            opcjeF[i] = opcjeF[i].Replace("lang/", "").Replace(".json", "");
            opcje[i+1] = ModuleC.remName(opcjeF[i]);
        }
        opcje[0] = "pl";
        bool okienko = false; 

        if(File.Exists("lang/cur_lang.txt")){
            lang = File.ReadAllText("lang/cur_lang.txt");
            bool znalezionoJezyk = false;
            foreach(string la in opcje){
                if(la == lang){
                    znalezionoJezyk = true;
                    break;
                }
            }
            if(string.IsNullOrWhiteSpace(lang) || !znalezionoJezyk) okienko = true;
        }else{
            okienko = true;
        }
        if(okienko){
            var form = new LanguageCWybor(opcje);
            var result = form.ShowDialog();
            lang = form.retVal;
            File.WriteAllText("lang/cur_lang.txt", lang);
        }
        if(lang == "pl") return;

        int index = 0;
        for(int i=0; i<opcje.Length; i++){
            if(opcjeF[i].StartsWith(lang)){
                index = i;
                break;
            }
        }

        string dataS = File.ReadAllText("lang/"+opcjeF[index]+".json");
        string[][] data = JsonConvert.DeserializeObject<string[][]>(dataS).ToArray();
        if(data == null) return;
        worlds = data;
    }

    public static string[][] worlds = null;
    public static string lang = "pl";

    public static string Get(string world){
        if(worlds == null || lang == "pl") return world;
        for(int i=0; i<worlds.Length; i++){
            if(world == worlds[i][0]){
                return worlds[i][1];
            }
        }
        return world;
    }
}