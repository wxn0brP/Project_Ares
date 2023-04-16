namespace Ares;

public class ConfigData{
	public static string tWersja = "0.2.6.1";
    public static string configDom = "https://aresconfig.ct8.pl/";

    public static void Init(){
        string configS = "";
        try{
            configS = NetC.DownloadString(configDom+"domena.txt");
        }catch{ return; }
        
        string[] config = configS.Split("\n");
        protocol = config[0];
        domena = config[1];
        url = protocol + "://" + domena + "/";
        blog = protocol + "://blog." + domena + "/";
        api = url + config[2];
    }

    public static string protocol = "https";
    public static string domena = "projektares.tk";
    public static string url = protocol+"://"+domena+"/";
    public static string blog = "";
    public static string api = url+"panel/api/";
}