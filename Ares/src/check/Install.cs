using System.Dynamic;
namespace Ares;
using System;
using System.IO;
using System.Diagnostics;

public partial class InstallC : Form{
    public int postepOkienko = 0;
    public string romDir = "";
    public string deskDir = "";

    public dynamic install = new ExpandoObject();

    /// <summary>
	/// okienko instalacji
	/// </summary>
    public static void InstallForm(){
        if(!NetC.PingUrl(true)) return;
        NetC.DownloadFile(ConfigData.url+"Ares/logo.ico", "logo.ico");
        System.Threading.Thread t1 = new System.Threading.Thread(() => {
            try{
			    Application.Run(new InstallC());
            }catch(Exception err){
                MessageBox.Show(err+"", "Critical error!!!");
            }
            
		});
		t1.SetApartmentState(ApartmentState.STA);
		t1.Start();
        t1.Join();
    }

    public InstallC(){
        InitializeComponent();
        deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)+"\\";
        install.nor = true;
        install.dir = "";
        window_1();
        // window_2();
    }

    /// <summary>
	/// instaluje Aresa
	/// </summary>
    public void Install(bool nor, string dir){
        if(!NetC.PingUrl(true)) return;
        ChangeProgres(20, "Create Directory");
        romDir = (nor ?
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+"\\" : 
            dir
        );
        Mkdir(romDir+"Ares");

        ChangeProgres(50, "Copy files...");
        Copy(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName, romDir+"Ares/Ares_Launcher.exe");
        Thread.Sleep(1000);
        Copy("logo.ico", romDir+"Ares/logo.ico");

        if(nor){
            ChangeProgres(85, "Create Icon");
            CreateAppShortcutToDesktop(deskDir, romDir);
        }
        try{
            string temp = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(romDir+"Ares");
            UpdateC.CheckFiles();
            MessC.ShowBalloonTip(
                "",
                "Project Ares has been successfully installed!",
                logopath: romDir+"Ares\\logo.ico"
            );
            Directory.SetCurrentDirectory(temp);
        }catch{}

        File.WriteAllText(romDir+"Ares/instaled.txt", (nor ? "local" : "move"));
        
        ChangeProgres(100, "Done!");
        this.okB.Enabled = true;
    }

    public void ChangeProgres(int per, string txt){
        pbBar.Value = per;
        labelInfo.Text = txt;
    }

    private void okB_Click(object sender, EventArgs en){
        switch(postepOkienko){
            case 0:
                window_2();
                postepOkienko = 1;
            break;
            case 1:
                window_3();
                postepOkienko = 2;
                Install(install.nor, install.dir);
            break;
            case 2:
                Thread.Sleep(500);
                if(File.Exists(romDir+"Ares\\Ares_Launcher.exe")){
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.WorkingDirectory = romDir+"Ares\\";
                    startInfo.FileName = romDir+"Ares\\Ares_Launcher.exe";
                    Process.Start(startInfo);
                }
                this.Close();
            break;
        }
    }
    
    private void przenosnieB_Click(object sender, EventArgs en){
        window_2();
        postepOkienko = 1;
        string dir = Directory.GetCurrentDirectory()+"\\";
        try{
            FolderBrowserDialog dd = new FolderBrowserDialog();
            dd.SelectedPath = deskDir;
            DialogResult result = dd.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK){
                dir = dd.SelectedPath + "\\";
            }
        }catch(Exception e){
            ErrorC.InfDev(e.ToString(), "install");
        }
        install.nor = false;
        install.dir = dir;
    }


    /// <summary>
	/// kopjuje plik
	/// </summary>
    /// <param name="z">plik do skopjowania</param>
    /// <param name="do">skopjowany plik</param>
    public static void Copy(string z, string d){
		if(File.Exists(d)) File.Delete(d);
		File.Copy(z, d);
	}

    public static void MoveFile(string z, string d){
        if(!File.Exists(z)) return;
        if(File.Exists(d)) return;
        try{
            File.Move(z, d);
        }catch{}
    }

    /// <summary>
	/// tworzy folder
	/// </summary>
    /// <param name="folder">scieżka</param>
	public static void Mkdir(string path){
		if(!Directory.Exists(path)) Directory.CreateDirectory(path);
	}

    /// <summary>
	/// tworzy ikonke na pulpicie
	/// </summary>
    /// <param name="desk">ścieżka do pilpitu</param>
    /// <param name="rom">ścieżka do aplikacji</param>
	public static void CreateAppShortcutToDesktop(string desk, string rom){
		string s = 
			"Set oWS = WScript.CreateObject(\"WScript.Shell\")\n"+
			"sLinkFile = \""+desk+"Ares.lnk\"\n"+
			"Set oLink = oWS.CreateShortcut(sLinkFile)\n"+
			"oLink.TargetPath = \""+rom+"Ares\\Ares_Launcher.exe\"\n"+
			"oLink.WorkingDirectory = \""+rom+"Ares\"\n"+
			"oLink.Description = \"Ares\"\n"+
			"oLink.Save";
		File.WriteAllText(rom+"Ares\\short.vbs", s);
		Process scriptProc = new Process();
		scriptProc.StartInfo.FileName = "cscript"; 
		scriptProc.StartInfo.Arguments = rom+"Ares\\short.vbs";
		scriptProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        scriptProc.StartInfo.UseShellExecute = false;
		scriptProc.StartInfo.CreateNoWindow = true;
		scriptProc.Start();
		scriptProc.WaitForExit();
		scriptProc.Close();
		File.Delete(rom+"Ares\\short.vbs");
	}
}