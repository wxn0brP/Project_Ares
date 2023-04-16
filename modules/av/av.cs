namespace Ares;
using System;
using System.IO;
using System.Management;
using System.Diagnostics;

//Jest tanio - jest tanio
//Jest dobrze - jest tanio

public partial class av : Form{
	ScanClass avObj = new ScanClass(@"C:\Program Files\Windows Defender\MpCmdRun.exe");
	bool continueScan = true;
	
	public void Init(string plusParams, string config){
        MessageBox.Show(
            "Ten Anty Virus jest w fazie testów! Prosimy o nie sugerowanie nim w kwestji bezpieczeństwa komputer.",
            "Uwaga!",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
        );

		if(AntivirusInstalled()){
			MessageBox.Show(
				"Aktualny komputer lub system operacyjny nie jest wspierany przez moduł av! :("
			);
			return;
		}
        
		try{
			if(plusParams == "") throw new Exception("uruchom av");
			string[] args = plusParams.Split("/;/");
			
			if(args.Length == 0) throw new Exception("ilosc arg");

			if(args[0] == "-f"){
				string res;
				if(args.Length == 1+2)
					res = changeNumToText(avObj.Scan(args[1], int.Parse(args[2])));
				else
					res = changeNumToText(avObj.Scan(args[1]));
				MessageBox.Show("Plik: "+res);
			}
		}catch{
			Application.Run(new av());
		}
	}
	
	public av(){
		InitializeComponent();
	}

	private string changeNumToText(int code, bool s=true){
		if(code == 0){
			return (s ? "✔ - Plik jest czysty!" : "✔");
		}else
		if(code == 1){
			return (s ? "❌ - Plik jest Wirusem!!!": "❌");
		}else
		if(code == 2){
			return "Plik nie istnieje.";
		}else
		if(code == 3){
			return "Błąd.";
		}else
		if(code == 4){
			return "Timeout - przekroczono limit czasu.";
		}else
		if(code == 5){
			return "Plik jest za duży.";
		}
		return "";
	}

	public bool AntivirusInstalled(){
        if(Environment.OSVersion.Version.Major < 10) return true;
        string[] res = new string[0];
        try{
            var path = string.Format(@"\\{0}\root\SecurityCenter2", Environment.MachineName);
            var searcher = new ManagementObjectSearcher(path, "SELECT * FROM AntivirusProduct");
            res = searcher.Get().Cast<ManagementObject>().Select(x => (string)x.GetPropertyValue("displayName")).ToArray();
        }catch(Exception e){
            return true;
        }
        bool retVal =
            (res.Length == 0) ||
            (res.Length == 1 && res.Contains("Windows Defender"))
        ;
        return !retVal;
	} 
}

public class ScanClass{
	private readonly string pathToWD;
	public ScanClass(string pathToWD){
		if(!File.Exists(pathToWD)){
			MessageBox.Show("Bląd krytyczny!!!");
			throw new FileNotFoundException();
		}
		this.pathToWD = new FileInfo(pathToWD).FullName;
	}
	/// <summary>
	/// Skanuje plik 0-brak zagorżeń 1-zagrożenie 2-plik nie istnieje 3-błąd 4-timeout 5-plik za duży
	/// </summary>
	/// <param name="file">path</param>
	/// <param name="timeoutInMs">max time deflaut 30s</param>
	/// <returns>code</returns>
	public int Scan(string file, int timeoutInMs = 0){
		if(!File.Exists(file)){
			return 2;//exists
		}
		var fileInfo = new FileInfo(file);
		var process = new Process();
		int timeout = timeoutInMs;
		if(timeout < 5_000){//if not set timeout or set < 5s
			timeout = (int)fileInfo.Length + 5_000;//1B = 1ms, +5s
		}
		if(fileInfo.Length > 200 * 1024 * 1024){// > 200MB return timeout
			return 5;
		}

		var startInfo = new ProcessStartInfo(this.pathToWD){
			Arguments = $"-Scan -ScanType 3 -File \"{fileInfo.FullName}\" -DisableRemediation",
			CreateNoWindow = true,
			ErrorDialog = false,
			WindowStyle = ProcessWindowStyle.Hidden,
			UseShellExecute = false
		};
		process.StartInfo = startInfo;
		process.Start();
		process.WaitForExit(timeout);
		if(!process.HasExited){
			process.Kill();
			return 4;
		}
		switch(process.ExitCode){
			case 0:
				return 0;
			case 2:
				return 1;
			default:
				return 3;
		}
	}
}
