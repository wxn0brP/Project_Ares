namespace Ares;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class szablonInit{
	public static bool deflaut = true;
	public static bool blackTitle = true;
	public static System.Drawing.Color eleBackColor;
	public static System.Drawing.Color eleFontColor;

	public static void Init(string mod, string modName){
		string[] szablony = Directory.GetFiles("szablony", mod+"_*.json");
		if(szablony.Length > 0){
			for(int i=0; i<szablony.Length; i++){
				szablony[i] = szablony[i].Replace("szablony\\"+mod+"_", "").Replace(".json", "");
			}
			string[] doSzab = new string[szablony.Length+1];
			doSzab[0] = "bez szablonu";
			for(int i=1; i<doSzab.Length; i++){
				doSzab[i] = remName(szablony[i-1]);
			}
			string szabName = pytanieM(modName, doSzab, "");
			if(szabName == "" || szabName == "bez szablonu") goto dalej;
			for(int i=0; i<szablony.Length; i++){
				if(szablony[i].StartsWith(szabName)){
					string dataS = File.ReadAllText("szablony/"+mod+"_"+szablony[i]+".json")+"";
					szablonChangeC.dobjA = JsonConvert.DeserializeObject<dynamic>(dataS);
					deflaut = false; 
					break;
				}
			}
		}
		dalej:
		return;
	}
    
	public static string remName(string a){
		string b = a;
		int index = a.IndexOf('-');
		b = b.Remove(index);
		return b;
	}

	public static string pytanieM(string p, string[] opcje, string alter){
		string input = alter;
		var form = new szablonWybor(p, opcje);
		var result = form.ShowDialog();
		if(result == DialogResult.OK){
			input = form.retVal;
		}
		if(string.IsNullOrEmpty(input)){
			return alter;
		}
		return input;
	}
}
public class szablonWybor : Form{
	public string retVal {get;set;} 

	private System.ComponentModel.IContainer components = null;
	protected override void Dispose(bool disposing){
		if(disposing && (components != null)){
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void convert(){
		string moduleName = this.modules.GetItemText(this.modules.SelectedItem);
		if(moduleName == "" || moduleName == null){
			this.retVal = "";
			goto exit;
		}
		this.retVal = moduleName+"";

		exit:
		this.DialogResult = DialogResult.OK;
		this.Close();
	}

	private void ok_Click(object sender, EventArgs e) => convert();
	private void Form_Closing(object sender, FormClosedEventArgs e){
		if(this.retVal == "" || this.retVal == null) this.retVal = "";
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData){//"ENTER" kilknięty
		if(keyData == Keys.Return){
			convert();
			return true;
		}
		return base.ProcessCmdKey(ref msg, keyData);
	}

	Label kdL;
	Button ok;
	ListBox modules;
	public szablonWybor(string p, string[] opcje){
		this.components = new System.ComponentModel.Container();
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(400, 160);
		this.Text = "Ares - "+p;
		this.Icon = new System.Drawing.Icon("logo.ico");
		this.MaximizeBox = false;
		this.FormBorderStyle = FormBorderStyle.FixedSingle;
		this.FormClosed += new FormClosedEventHandler(this.Form_Closing);

		kdL = new Label();
		this.kdL.Location = new System.Drawing.Point(5, 7);
		this.kdL.Size = new System.Drawing.Size(295, 25);
		this.kdL.Font = new Font("Consolas", 11);
		this.kdL.Text = "Jaki szablon chcesz odpalić?";
		this.Controls.Add(kdL);

		ok = new Button();
		this.ok.Location = new System.Drawing.Point(300, 5);
		this.ok.Size = new System.Drawing.Size(95, 25);
		this.ok.Font = new Font("Consolas", 11);
		this.ok.Text = "ok";
		this.ok.UseVisualStyleBackColor = true;
		this.ok.Click += new System.EventHandler(this.ok_Click);
		this.ok.FlatStyle = FlatStyle.Flat;
		this.ok.FlatAppearance.BorderSize = 0;
		this.Controls.Add(ok);

		modules = new ListBox();
		this.modules.Location = new System.Drawing.Point(5, 35);
		this.modules.Size = new System.Drawing.Size(390, 130);
		this.modules.Font = new Font("Consolas", 11);
		this.modules.Name = "Moduły";
		this.Controls.Add(modules);

		foreach(string a in opcje){
			modules.Items.Add(a);
		}
	}
}

public static class szablonChangeC{
	public static JObject dobjA;
	public static JObject dobj;
	public static void szablonChange<T>(ref T data, string name, bool txt=true){
		dobj = (JObject)dobjA[name];
		
		SetProp<Point>(data, "Location", new Point((int)dobj["x"], (int)dobj["y"]));
		SetProp<Size>(data, "Size", new Size((int)dobj["w"], (int)dobj["h"]));
		SetProp<Font>(data, "Font", new Font("Consolas", (int)dobj["fontSize"]));
		if(txt){ 
			SetProp<string>(data, "Text", (string)dobj["txt"]);
		}else{
			SetProp<string>(data, "PlaceholderText", (string)dobj["txt"]);
		}
	}

	public static void szablonChangeOkienko(object data){
		dobj = dobjA;
		
		SetProp<Size>(data, "ClientSize", new Size((int)dobj["x"], (int)dobj["y"]));
		SetProp<string>(data, "Text", (string)dobj["txt"]);
		SetProp<Color>(data, "BackColor", stringToColor((string)dobj["BackColor"]));
		SetProp<Color>(data, "ForeColor", stringToColor((string)dobj["FontColor"]));
		szablonInit.eleBackColor = stringToColor((string)dobj["eleBackColor"]);
		szablonInit.eleFontColor = stringToColor((string)dobj["eleFontColor"]);
		szablonInit.blackTitle = (bool)dobj["blackTitle"];
	}

	public static Color stringToColor(string data){
		return System.Drawing.ColorTranslator.FromHtml(data);
	}

	public static void SetProp<T>(object obj, string name, T data){
		obj.GetType().GetProperty(name).SetValue(obj, data, null);
	}
}