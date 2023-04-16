namespace Ares;
using System.Reflection;
using Newtonsoft.Json;

public class ModuleC{
    /// <summary>
	/// ładuje plik dll i uruchamia z podanego namescape i class (ta sama nazwa) metode Init (Main)
	/// </summary>
    public static void loadModule(string path, string name, string plusParam=""){
        if(!File.Exists(path)){
            ErrorC.InfDev(
                "Plik nie istnieje "+path, "Ładowanie modułu f",
                "'txt': '"+LanguageC.Get("Wystąpił problem z załadowaniem \nmodułu")+"'"
            );
            return;
        }
        try{
            byte[] DllData = File.ReadAllBytes(path);
            var DLL = Assembly.Load(DllData);
            Type? theType = DLL.GetType(name);
            if(theType == null) throw new Exception("Get Type null");
            var function = Activator.CreateInstance(theType);
            MethodInfo? method = theType.GetMethod("Init");
            if(method == null) throw new Exception("Get Method null");
            method.Invoke(function, new object[]{
                plusParam,
                "{\"config\": \"" + ConfigAresToModuleC.Get() + "\"}"
            });
        }catch(Exception e){
            ErrorC.InfDev(
                "Load error: path = "+path+", name = "+name+" :: \n\n"+e, "Ładowanie modułu e",
                "'txt': '"+LanguageC.Get("Wystąpił problem z załadowaniem \nmodułu")+"'"
            );
        }
    }

	/// <summary>
	/// metoda uruchamiająca moduł wybrany z listy
	/// </summary>
	public static void moduleStart(string moduleName){
		string mnm = remName(moduleName);
        if(mnm == ":err"){
            ErrorC.InfDev(":err", "module Start");
            return;
        }

		System.Threading.Thread t1 = new System.Threading.Thread(() => {
			loadModule(
				"./modules/"+moduleName+".ares",
				"Ares."+mnm
			);
		});
		t1.SetApartmentState(ApartmentState.STA);
		t1.Start();
	}

    public static string[] GetFiles(string path, string ext){
        if(!Directory.Exists(path)) return new string[0];
        string[] files = Directory.GetFiles(path);
        for(int i = 0; i < files.Length; i++){
            files[i] = files[i]
            .Replace(path, "")
            .Replace(ext, "");
        }
        return files;
    }

    /// <returns>string[] moduły</returns>
    public static string[] GetModules(){
        return GetFiles("./modules/", ".ares");
    }

    /// <returns>string[] szablony</returns>
    public static string[] GetSzablony(){
        return GetFiles("./szablony/", ".js");
    }

    /// <returns>string[] jeżyki</returns>
    public static string[] GetLang(){
        return GetFiles("./lang/", ".json");
    }

    /// <returns>plik modułu -- wersja</returns>
	public static string remName(string a){
        try{
            string b = a;
            int index = a.IndexOf('-');
            b = b.Remove(index);
            return b;
        }catch{
            return ":err";
        }
	}

    /// <returns>plik modułu -- nazwa</returns>
	public static string remVer(string a){
        try{
            string b = a;
            int index = a.IndexOf('-');
            b = b.Remove(0, index+1);
            return b;
        }catch{
            return ":err";
        }
	}

    public static bool checkErrRemName(string a, string b){
        return remName(a) == ":err" || remName(b) == ":err";
    }
    
    /// <summary>
	/// aktualizuje moduły
	/// </summary>
    public static void aktualizujModuly(){
        string[][] mod = getModuleInServer();
        foreach(string inM in mod[0]){
			foreach(string serM in mod[1]){
                if(checkErrRemName(inM, serM)) continue;
				if(remName(inM).StartsWith(remName(serM))){
                    if(new Version(remVer(inM)) < new Version(remVer(serM))){
                        File.Delete("./modules/"+inM+".ares");
                        NetC.DownloadFile(ConfigData.url+"Modules/"+remName(serM)+".dll", "./modules/"+serM+".ares");
                    }
				}
			}
		}
    }

    /// <summary>
	/// aktualizuje szablony
	/// </summary>
    public static void aktualizujSzablony(){
        string[][] mod = getSzablonyInServer();
        foreach(string inM in mod[0]){
			foreach(string serM in mod[1]){
				if(remName(inM).StartsWith(remName(serM))){
                    if(checkErrRemName(inM, serM)) continue;
                    if(new Version(remVer(inM)) < new Version(remVer(serM))){
                        File.Delete("./szablony/"+inM+".js");
                        NetC.DownloadFile(ConfigData.url+"szablony/"+remName(serM)+".js", "./szablony/"+serM+".js");
                    }
				}
			}
		}
    }

    /// <summary>
	/// aktualizuje języki
	/// </summary>
    public static void aktualizujLang(){
        string[][] mod = getLangInServer();
        foreach(string inM in mod[0]){
			foreach(string serM in mod[1]){
				if(remName(inM).StartsWith(remName(serM))){
                    if(checkErrRemName(inM, serM)) continue;
                    if(new Version(remVer(inM)) < new Version(remVer(serM))){
                        File.Delete("./lang/"+inM+".json");
                        NetC.DownloadFile(ConfigData.url+"lang/"+remName(serM)+".json", "./lang/"+serM+".json");
                    }
				}
			}
		}
    }

    public static string[][] getInServer(string url, string key, string[] installModules){
        string? dataString = NetC.DownloadString(ConfigData.url+url);
        if(dataString == null) return new string[0][];
        dynamic? a = JsonConvert.DeserializeObject<dynamic>(dataString);
        if(a == null) return new string[0][];
		
		dynamic[] modulesServer = a[key].ToObject<dynamic[]>();
		List<string> serverModulesAll = new List<string>();

		for(int i=0; i<modulesServer.Length; i++){
			if(modulesServer[i].jest.ToString()+"" != "true") continue;
            if(modulesServer[i].mod.ToString()+"" == "true" && !DeveloperC.mod) continue;

            serverModulesAll.Add(
                (modulesServer[i].name.ToString()+"-"+modulesServer[i].v.ToString()).ToString()
            );
		}
		string[] serverModules = serverModulesAll.ToArray();
		serverModules = serverModules.Except(installModules).ToArray();
        return new string[][]{
            installModules,
            serverModules
        };
    }
    
    /// <returns>[0] - install modules, [1] - server modules -- install module</returns>
    public static string[][] getModuleInServer() => getInServer("Modules/modules.json", "modules", GetModules());

    /// <returns>[0] - install szablon, [1] - server szablon -- install szablon</returns>
    public static string[][] getSzablonyInServer() => getInServer("szablony/szablony.json", "szablony", GetSzablony());

    /// <returns>[0] - install lang, [1] - server lang -- install lang</returns>
    public static string[][] getLangInServer() => getInServer("lang/lang.json", "lang", GetLang());

    public static void installAddons(){
        string[][] modules = getModuleInServer();
        foreach(string mod in modules[1]){
            try{
                NetC.DownloadFile(ConfigData.url+"Modules/"+remName(mod)+".dll", "./modules/"+mod+".ares");
            }catch{}
        }
        string[][] szablony = getSzablonyInServer();
        foreach(string szab in szablony[1]){
            try{
                NetC.DownloadFile(ConfigData.url+"szablony/"+remName(szab)+".js", "./szablony/"+szab+".js");
            }catch{}
        }
        string[][] lang = getLangInServer();
        foreach(string szab in lang[1]){
            try{
                NetC.DownloadFile(ConfigData.url+"lang/"+remName(szab)+".json", "./lang/"+szab+".json");
            }catch{}
        }
    }
}