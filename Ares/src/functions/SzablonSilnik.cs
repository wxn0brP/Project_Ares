using System.Dynamic;
namespace Ares;
using System.IO;

public class szablonInit{
	public static bool blackTitle = true;
	public static System.Drawing.Color eleBackColor;
	public static System.Drawing.Color eleFontColor;

	public static string[] Init(string mod, string modName){
		string[] szablony = Directory.GetFiles("szablony", mod+"_*.js");
        
		if(szablony.Length > 0){
			for(int i=0; i<szablony.Length; i++){
				szablony[i] = szablony[i].Replace("szablony\\"+mod+"_", "").Replace(".js", "");
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
					string dataS = File.ReadAllText("szablony/"+mod+"_"+szablony[i]+".js")+"";
                    return new string[]{"false", dataS};
				}
			}
		}
		dalej:
		return new string[]{"true"};
	}

    public static Jint.Engine GetEngine(string pl){
        Jint.Engine engine = new Jint.Engine();

        engine.SetValue("FromHtml", new Func<string, Color>(System.Drawing.ColorTranslator.FromHtml));
        engine.SetValue("Size", (string dbw) => {
            string[] arr = dbw.Split(",");
            return new System.Drawing.Size(int.Parse(arr[0]), int.Parse(arr[1]));
        });
        engine.SetValue("imgImport", new Func<string, Image>(Image.FromFile));

        return engine;
    }

    public static dynamic setConfig(string dc){
        dynamic retVal = new ExpandoObject();
        retVal.bc="#222222"; retVal.fc="#aaaaaa"; retVal.ebc="#333333"; retVal.efc="#aaaaaa"; retVal.bl="true";
        string[] arr = dc.Replace("///", "").Split(" ");
        if(arr.Length != 5) return retVal;
        if(arr[0] != ";") retVal.bc = "#"+arr[0];
        if(arr[1] != ";") retVal.fc = "#"+arr[1];
        if(arr[2] != ";") retVal.ebc = "#"+arr[2];
        if(arr[3] != ";") retVal.efc = "#"+arr[3];
        if(arr[4] != ";") retVal.bl = arr[4];
        return retVal;
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
		this.ClientSize = new System.Drawing.Size(400, 170);
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