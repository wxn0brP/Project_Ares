namespace Ares;

public class VersionC{
    public static bool GetUpdates(){
        string wersja = NetC.DownloadString(ConfigData.url+"Ares/v.txt");
        return Ver1mniejszaVer2(ConfigData.tWersja, wersja);
    }

    public static bool Ver1mniejszaVer2(string versja1, string versja2){
        List<string> verA = versja1.Split(".").ToList();
        List<string> verB = versja2.Split(".").ToList();
        if(verA.Count == 0 || verB.Count == 0) return false;
        if(verA.Count != verB.Count){
            if(verA.Count < verB.Count){
                int il = verB.Count-verA.Count;
                for(int i=0; i<il; i++) verA.Add("0");
            }else{
                int il = verA.Count-verB.Count;
                for(int i=0; i<il; i++) verB.Add("0");
            }
        }

        for(int i=0; i<verA.Count; i++){
            int a = 0, b = 0;
            int.TryParse(verA[i], out a);
            int.TryParse(verB[i], out b);
            if(a < b) return true;
            if(a > b) return false;
        }
        return false;
    }
}