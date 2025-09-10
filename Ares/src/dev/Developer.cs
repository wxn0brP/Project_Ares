namespace Ares;
using System.Diagnostics;

public class DeveloperC{
    public static bool mod = false;

    /// <summary>
	/// inicjuje metody dla developers
	/// </summary>
    public static void Init(){
        mod = File.Exists("mod.txt");
    }

    public static System.Timers.Timer Timer_1 = new System.Timers.Timer();

	/// <summary>
	/// sprawdza esteer egga
	/// </summary>
	public static void PlaySong(){
		if(!File.Exists("en.txt")) return;
        Timer_1.Interval = 1000;
		Timer_1.Elapsed += Timer__1;
		Timer_1.AutoReset = true;
		Timer_1.Enabled = true;
        File.Delete("en.txt");
		if(!File.Exists("szablony/player.json")) NetC.DownloadFile(ConfigData.url+"/szablony/player.json", "szablony/player.json");
		string file = Path.GetFullPath("szablony/player.json");
		ProcessStartInfo startInfo = new ProcessStartInfo();
		startInfo.FileName = "powershell";
		startInfo.Arguments = "-c (New-Object Media.SoundPlayer \""+file+"\").PlaySync()";
		startInfo.RedirectStandardOutput = true;
		startInfo.RedirectStandardError = true;
		startInfo.UseShellExecute = false;
		startInfo.CreateNoWindow = true;

		System.Threading.Thread t1 = new System.Threading.Thread(() => {
			Thread.Sleep(5000);
			for(int i=0; i<5; i++){
				Process processTemp = new Process();
				processTemp.StartInfo = startInfo;
				processTemp.EnableRaisingEvents = true;
				processTemp.Start();
				processTemp.WaitForExit();
			}
            Timer_1.Enabled = false;
            File.Create("en.txt");
		});
		t1.SetApartmentState(ApartmentState.STA);
		t1.Start();
	}

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

    static void Timer__1(object s, System.Timers.ElapsedEventArgs e){
        keybd_event((byte)Keys.VolumeUp, 0, 0, 0);
        Process[] procs = Process.GetProcessesByName("taskmgr");
        foreach(Process p in procs) { p.Kill(); }
	}
    
}