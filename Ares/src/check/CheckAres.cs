namespace Ares;
using System;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;

public class CheckAresC{
    public static System.Timers.Timer Timer_RAM_Control = new System.Timers.Timer();

	/// <summary>
	/// main Aresa + try
	/// </summary>
	/// <param name="param">dodatkowe parametry</param>
	public void Init(string plusParams){
		try{
			Start(plusParams);
		}catch(Exception e){
			ErrorC.InfDev(e+"", "Start");
			return;
		}
	}

	/// <summary>
	/// main Aresa
	/// </summary>
	/// <param name="param">dodatkowe parametry</param>
	public static void Start(string plusParams){
        if(RamControlC.ControlR()) return;
        Timer_RAM_Control.Interval = 10 * 1000;
		Timer_RAM_Control.Elapsed += (object s, System.Timers.ElapsedEventArgs e) => {
            RamControlC.ControlR();
        };
		Timer_RAM_Control.AutoReset = true;
		Timer_RAM_Control.Enabled = true;

        ConfigData.Init();
		string[] args = plusParams.Split("/;/");
		if(args.Length == 0) goto run;
        if(args.Length == 1){
            if(!File.Exists(args[0])) goto swi;
            string
                path = args[0],
                temp = args[0],
                ex = Path.GetExtension(args[0])
            ;
            if(ex.IndexOf(".ares") > -1 || ex.IndexOf(".dll") > -1){
                temp = Path.GetFileName(temp).Replace(".ares", "").Replace(".dll", "");
                string apppath = Directory.GetCurrentDirectory()+"\\modules\\"+temp+".ares";
                if(!File.Exists(apppath)) File.Move(path, apppath);
                ModuleC.moduleStart(temp);
                goto exit;
            }else goto swi;
        }

        swi:
        if(StartParamsC.Start(args)) goto run;
        else goto exit;

		run:
		RunAres();
		exit:
		return;
	}

    public static void CheckPathAndAddToEnvironmentVariable(){
        if(File.Exists("instaled.txt") && File.ReadAllText("instaled.txt") == "local"){
            string sciezkaDoFolderu = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Ares";
            string Zmienna = "Path";
            if(Directory.Exists(sciezkaDoFolderu)){
                string wartoscZmiennej = Environment.GetEnvironmentVariable(Zmienna);
                if(wartoscZmiennej == null || !wartoscZmiennej.Contains(sciezkaDoFolderu)){
                    wartoscZmiennej = wartoscZmiennej == null ? sciezkaDoFolderu : wartoscZmiennej.TrimEnd(';') + ";" + sciezkaDoFolderu;
                    MessageBox.Show(Zmienna + "\n" + wartoscZmiennej+ "\n" + EnvironmentVariableTarget.User);
                    
                    // Environment.SetEnvironmentVariable(Zmienna, wartoscZmiennej, EnvironmentVariableTarget.User);
                }
            }
        }
    }


	/// <summary>
	/// uruchom Aresa
	/// </summary>
    public static void RunAres(){
        if(!UpdateC.CheckInstallAndUpdate()) return;
        try{
            if(!OneAresRunInComputer()) return;
        }catch{
            ErrorC.InfDev("OneAresRunInComputer");
            goto exit;
        }
        if(RamControlC.ControlR()) return;
        LanguageC.Init();
        ReputationScoreC.Init();
        ProC.Init();
        
        System.Threading.Thread t1 = new System.Threading.Thread(() => {
            Application.Run(new Ares());
        });
        t1.SetApartmentState(ApartmentState.STA);
        t1.Start();
        t1.Join();

        exit:
        return;
    }

    
    /// <summary>
	/// więcej niż 1 Ares uruchomiony
	/// </summary>
    public static bool OneAresRunInComputer(){
        if(System.Diagnostics.Debugger.IsAttached) return true;
        if(Process.GetProcessesByName("Ares_Launcher").Length > 1){
            Random ra = new Random();
            string rand = "";
            string fileContent = File.ReadAllText("random.txt");

            rendNumber:
            rand = ra.Next(0, ra.Next(100, 1000))+"";
            if(rand == fileContent) goto rendNumber;

            File.WriteAllText("random.txt", rand);
            return false;
        }
        return true;
    }
    
    /// <summary>
	/// tworzy Ares.vbs do uruchamiania aresa z zewnątz
	/// </summary>
    public static void CreateAresVbs(){
        string data =
            "Set WshShell = CreateObject(\"WScript.Shell\")\n"+
            "WshShell.CurrentDirectory = WshShell.ExpandEnvironmentStrings(\"%APPDATA%\") & \"\\Ares\"\n"+
            "WshShell.Run chr(34) & \"Ares_Launcher.exe\" & Chr(34), 0\n"+
            "Set WshShell = Nothing";
        File.WriteAllText("Ares.vbs", data);
    }
    
    /// <summary>
	/// dodaje .ares do rejestru
	/// </summary>
    public static void CreateAresExtension(){
        string path = "SOFTWARE\\Classes\\";
        string ext = ".ares";
        if(Registry.CurrentUser.OpenSubKey(path+ext, false) != null) return;

        string aresDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+"\\Ares";
        string aresPath = aresDir + "\\Ares_Launcher.exe";
        if(!File.Exists(aresPath)) return;
        try{

            RegistryKey key = Registry.CurrentUser.CreateSubKey(path+ext);
            key.SetValue("", "Projekt Ares - moduł");
            key.Close();
            
            key = Registry.CurrentUser.CreateSubKey(path+ext + "\\Shell\\Open\\command");
            key.SetValue("", "\"" + aresPath + "\" \"" + aresDir + "\" \"%L\"");
            key.Close();
            
            key = Registry.CurrentUser.CreateSubKey(path+ext + "\\DefaultIcon");
            key.SetValue("", Directory.GetCurrentDirectory() + "\\logo.ico");
            key.Close();
        }catch(Exception e){
            ErrorC.InfDev(e.ToString(), "Start");
        }
    }
}
