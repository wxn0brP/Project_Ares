namespace Ares;

public class ReputationScoreC{
    public static long Rep = 0;
    
    public static void Init(){
        try{
            string repS = ReadRep();
            if(repS != "")
                Rep = long.Parse(repS);
        }catch{}

        #region ilosc wejsc
        if(File.Exists("dane/i.txt")){
            string ilosc = File.ReadAllText("dane/i.txt");
            ilosc = EncryptC.XORCipher(ilosc, "46");
            int wejsc = 0;
            if(int.TryParse(ilosc, out wejsc)){
                wejsc++;
            }
            if(wejsc > 100){
                wejsc = 0;
                Rep++;
                WriteRep();
            }
            File.WriteAllText("dane/i.txt", EncryptC.XORCipher(wejsc.ToString(), "46")+"");
        }else{
            File.WriteAllText("dane/i.txt", EncryptC.XORCipher("1", "46")+"");
        }
        #endregion
        
    }

    public static void WriteRep(){
        if(!Directory.Exists("dane")) Directory.CreateDirectory("dane");
        string temp = EncryptC.XORCipher(Rep.ToString(), "25");
        File.WriteAllText("dane/rep.txt", temp);
    }

    public static string ReadRep(){
        if(!File.Exists("dane/rep.txt")) return "";
        string[] dane = File.ReadAllLines("dane/rep.txt");
        string rep = EncryptC.XORCipher(dane[0], "25");
        return rep;
    }
}