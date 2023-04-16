namespace Ares;
using System.Net;
using System.Net.NetworkInformation;

public class NetC{

    /// <summary>
	/// url => file
	/// </summary>
	/// <param name="serverFile">server + plik</param>
	/// <param name="path_to_file">plik lokalny (brak -> utworzyć)</param>
    public static void DownloadFile(string server_file, string path_to_file){
        if(File.Exists(path_to_file)){
            File.Delete(path_to_file);
        }
        WebClient webClient = new WebClient();
        webClient.DownloadFile(server_file, path_to_file);
    }

    /// <summary>
	/// url => string
	/// </summary>
	/// <param name="serverFile">server + plik</param>
	/// <returns>treść</returns>
    public static string DownloadString(string server_file){
        WebClient webClient = new WebClient();
        return webClient.DownloadString(server_file);
    }

    /// <summary>
	/// sprawdza połączenie z podaną stroną
	/// </summary>
	/// <param name="server">domena</param>
	/// <returns>true - połącznie udane, false - brak połącznia</returns>

    public static bool Ping(string server){
        try{
            Ping pinger = new Ping();
            PingReply reply = pinger.Send(server);
            return reply.Status == IPStatus.Success;
        }catch{
            return false;
        }
    }

    /// <summary>
	/// sprawdza połączenie ze stroną PA
	/// </summary>
	/// <param name="msg">pokaż komunikat</param>
	/// <returns>true - połącznie udane, false - brak połącznia</returns>
    public static bool PingUrl(bool msg=false){
        if(Ping(ConfigData.domena)){
            return true;
        }else{
            if(msg) MessageBox.Show("Nie można połączyć się z serwerem. Sprawdź połączenie z internetem.");
            return false;
        }
    }
    
}