namespace Ares;
using System;

public class LicenseC{
	// [System.Runtime.InteropServices.DllImport("kernel32.dll")]
	// private static extern bool AllocConsole();

    public static bool run(){
        if(quicVery()) return true;

        bool verify = false;
        System.Threading.Thread t1 = new System.Threading.Thread(() => {
            loginForm log = new loginForm();
            Application.Run(log);
            verify = log.Verify;
		});
		t1.SetApartmentState(ApartmentState.STA);
		t1.Start();
        t1.Join();
        return verify;
    }

    public static bool quicVery(){
        return ReadLicenseC() > DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }

	public static bool[] Verify(string login, string haslo, ref long lg){
        try{
            string zapytanie = "konto-key/?l="+login+"&h="+haslo;
            string res = NetC.DownloadString(ConfigData.api+zapytanie);
            Newtonsoft.Json.Linq.JObject obj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(res);
            
            if((string)obj["err"] != "0") return new[]{ false, false };
            long buf = (long)obj["akt"];
            lg = buf;
            return new[]{
                true,
                (buf > DateTimeOffset.Now.ToUnixTimeMilliseconds())
            };
        }catch{
            return new[]{ false, false };
        }
	}

    public static void WriteLogin(string login, string haslo){
        if(!Directory.Exists("dane")) Directory.CreateDirectory("dane");
        login = EncryptC.XORCipher(login, "013");
        haslo = EncryptC.XORCipher(haslo, "014");
        File.WriteAllText("dane/dane.txt", login+"\n"+haslo);
    }

    public static string[] ReadLogin(){
        if(!File.Exists("dane/dane.txt")) return new[]{"", ""};
        string[] dane = File.ReadAllLines("dane/dane.txt");
        string login = EncryptC.XORCipher(dane[0], "013");
        string haslo = EncryptC.XORCipher(dane[1], "014");
        return new[]{ login, haslo };
    }

    public static void WriteLicenseC(long time){
        if(!Directory.Exists("dane")) Directory.CreateDirectory("dane");
        string temp = EncryptC.XORCipher(time.ToString(), "015");
        File.WriteAllText("dane/LicenseC.txt", temp);
    }

    public static long ReadLicenseC(){
        if(!File.Exists("dane/LicenseC.txt")) return 0;
        string dane = File.ReadAllText("dane/LicenseC.txt");
        string time = EncryptC.XORCipher(dane, "015");
        try{
            return long.Parse(time);
        }catch{
            return 0;
        }
    }
}