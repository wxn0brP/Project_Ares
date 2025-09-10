using System.Dynamic;
namespace Ares;
using System.Reflection;

public partial class kd : Form{
    public void Init(string plusParams, string config){
		Application.Run(this);
    }

    public Jint.Engine engine;
    public dynamic configWin = new ExpandoObject();
	
    public kd(){
        configWin.bc="#222222"; configWin.fc="#aaaaaa";
        configWin.ebc="#333333"; configWin.efc="#aaaaaa"; configWin.bl="true";
		// this.Load += new System.EventHandler(Ares_Load);
        bool def = true;
        string[] line = new string[0];
        if(File.Exists("./Ares_lib.dll")){
            byte[] DllData = File.ReadAllBytes("./Ares_lib.dll");
            var DLL = Assembly.Load(DllData);
            Type? theType = DLL.GetType("Ares.szablonInit");
            if(theType == null) throw new Exception("Get Type null");
            object objE = Activator.CreateInstance(theType);

            string[] retVal = (string[])theType.GetMethod("Init").Invoke(objE, new object[]{"kd", "KD"});
            if(retVal[0] == "false"){
                line = retVal[1].Split("\n");
                engine = (Jint.Engine)theType.GetMethod("GetEngine").Invoke(objE, new object[]{""});
                configWin = (dynamic)theType.GetMethod("setConfig").Invoke(objE, new object[]{line[0]});
                def = false;
            }
        }

        InitializeComponent(def, line);
	}

	public static System.Drawing.Color eleBackColor;
	public static System.Drawing.Color eleFontColor;

    public void exe(string ed){
        try{
            engine.Execute(ed);
        }catch{}
    }




	// private void Ares_Load(object sender, System.EventArgs e){}

	private void ok_Click(object sender, EventArgs e){//przycisk "ok" kliknięty
		convert();
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData){//"ENTER" kilknięty
		if(keyData == Keys.Return){
			convert();
			return true;
		}
		return base.ProcessCmdKey(ref msg, keyData);
	}

	public char[] alfabet = {' ','A','a','B','b','C','c','D','d','E','e','F','f','G','g','H','h','I','i','J','j','K','k','L','l','M','m','N','n','O','o','P','p','Q','q','R','r','S','s','T','t','U','u','V','v','W','w','X','x','Y','y','Z','z','1','2','3','4','5','6','7','8','9','0','!','@','#','$','%','^','&','*','(',')','.',',','/','?','<','>','[',']','{','}','+','-','_','=',':',';','`','~','\\','|','"','Ą','Ę','Ł','Ó','Ś','Ż','Ź','Ć','Ń','ą','ę','ł','ó','ś','ż','ź','ć','ń','\''};
	Random ra = new Random();

	private void convert(){//główna funkcja kodujaca i dekodująca
		int index = 0;
		int enigma = 0;
		string result = "";
		string content_path = contentsTB.Text;
		string kd = this.kdWybor.Name;//kdTB.Text;
		
		kd = kd.ToLower();

		if(File.Exists(content_path)){
			content_path = File.ReadAllText(content_path);
		}

		if(kd == "k"){
			content_path = content_path.Replace("\n", "/r/n/m");
		}

		if(kd == "k"){
			char[] c = content_path.ToCharArray();
			enigma = ra.Next(3, alfabet.Length-1);
			result = enigma+"";
			if(enigma<10) result = "0"+result;
			if(enigma<100) result = "0"+result;
			for(int i = 0; i < c.Length; i++){
				enigma++;
				index = Wylicz(c[i]) + enigma;
				for(;index > alfabet.Length-1;){
					index -= alfabet.Length;
				}
				result += alfabet[index];
			}
			result = Zmien(result);
		}
		if(kd == "d"){
			if(content_path.Length <= 3){
				resultTB.Text = "invalid text";
				return;
			}
			string temp = Zmien(content_path);
			char[] c = temp.ToCharArray();
			string enigmaS = temp.Remove(3);
			if(!int.TryParse(enigmaS, out enigma)){
				resultTB.Text = "invalid text "+enigmaS;
				return;
			}
			for(int i = 3; i < c.Length; i++){
				enigma++;
				index = Wylicz(c[i]) - enigma;
				for(;index < 0;){
					index += alfabet.Length;
				}
				result += alfabet[index];
			}
		}

		if(kd == "d"){
			result = result.Replace("/r/n/m", "\r\n");
		}

		resultTB.Text = ""+result;
	}

	public int Wylicz(char c){//wylicz numer znaku
		int index = 0;
		while(c != alfabet[index]){
			index++;
			if(index > alfabet.Length-1){
				index = 0;
				break;
			}
		}
		return index;
	}

	public string Zmien(string text){//zmiana kolejności
		int len = text.Length;
		if(len < 5)return text;
		char[] c = text.ToCharArray();

		c = ZamienIndex(c, 0, 2);
		c = ZamienIndex(c, 1, 3);

		for(int i=5; i<len; i+=3){
			c = ZamienIndex(c, i, i-1);
		}

		return string.Join("", c);
	}

	public char[] ZamienIndex(char[] text, int index1, int index2){//zamiania index'sy w tablicy
		char temp = text[index1];
		text[index1] = text[index2];
		text[index2] = temp;
		return text;
	}
}