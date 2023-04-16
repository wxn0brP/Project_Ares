using System.Diagnostics;
namespace Ares;

public class UpdateC{
    /// <summary>
	/// sprawdza instalacje Aresa, jeżeli nie - zainstaluj
	/// </summary>
	/// <returns>true - zainstalowny, false - zainstaluj i przerwij uruchamianie</returns>
	public static bool CheckInstallAndUpdate(){
		if(!CheckInstallAres()) return false;
		if(!UpdateAres()) return false;
        return true;
	}

	/// <summary>
	/// sprawdza instalacje Aresa, jeżeli nie - zainstaluj
	/// </summary>
	/// <returns>true - zainstalowny, false - zainstaluj i przerwij uruchamianie</returns>
	public static bool CheckInstallAres(){
		try{
			if(!File.Exists("instaled.txt")){
				InstallC.InstallForm();
				return false;
			}
			CheckFiles();
			return true;
		}catch(Exception e){
			ErrorC.InfDev(e.ToString(), "check Install Ares");
			return false;
		}
	}

	

	/// <summary>
	/// sprawdza aktualizacje i aktualizuje Aresa
	/// </summary>
	/// <returns>true - brak aktualizacji, false - aktualizacja lub błąd</returns>
	public static bool UpdateAres(bool open=true){
		if(!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable()) return true;
		try{
            if(VersionC.GetUpdates()){
				try{
                    string lib = NetC.DownloadString(ConfigData.configDom+"lib.txt");
                    NetC.DownloadFile(lib, "Ares_lib2.dll");
                    Thread.Sleep(50);
                    File.Delete("Ares_Lib.dll");
                    File.Move("Ares_Lib2.dll", "Ares_Lib.dll");
                    Thread.Sleep(100);
                    if(open) Process.Start("Ares_Launcher.exe");
					Application.Exit();
					Environment.Exit(1);
                    return false;
				}catch{}
				return false;
			}
			ModuleC.aktualizujModuly();
			ModuleC.aktualizujSzablony();
			ModuleC.aktualizujLang();
			ModuleC.installAddons();
		}catch{}
		return true;
	}

	/// <summary>
	/// sprawdza instnienie wymaganych plików i usuwa ban pliki
	/// </summary>
	public static void CheckFiles(){
		if(!Directory.Exists("modules")) Directory.CreateDirectory("modules");
		if(!Directory.Exists("szablony")) Directory.CreateDirectory("szablony");
		if(!Directory.Exists("logo")) Directory.CreateDirectory("logo");
		if(!Directory.Exists("lang")) Directory.CreateDirectory("lang");
		if(!Directory.Exists("dane")) Directory.CreateDirectory("dane");
		if(!Directory.Exists("js")) Directory.CreateDirectory("js");
        try{
		    if(!File.Exists("random.txt")) File.WriteAllText("random.txt", "0");
        }catch{}
		if(!File.Exists("Ares.vbs")) CheckAresC.CreateAresVbs();
		if(!NetC.PingUrl()) return;
		string[] filesReq = NetC.DownloadString(ConfigData.url+"Ares/files.txt").Split("\n");

		string[] localFiles = Directory.GetFiles("./");
		for(int i=0; i<localFiles.Length; i++) localFiles[i] = localFiles[i].Replace("./", "").Trim();
		string[] filesToDownload = filesReq.Except(localFiles).ToArray();
		foreach(string link in filesToDownload){
			if(link == "" || link == null) continue;
			try{
				NetC.DownloadFile(ConfigData.url+"Ares/"+link, link);
			}catch{}
		}

        foreach(string mod in Directory.GetFiles("./modules/", "*.dll")){
            string d = mod.Replace(".dll", ".ares");
            File.Move(mod, d);
        }

		string[] banedModules = NetC.DownloadString(ConfigData.url+"Ares/baned.txt").Split("\n");
		string[] modules = ModuleC.GetModules();
		
		foreach(string bm in banedModules){
			foreach(string lm in modules){
				if(bm == ModuleC.remName(lm)){
					File.Delete("./modules/"+lm+".ares");
				}
			}
		}

	}
}