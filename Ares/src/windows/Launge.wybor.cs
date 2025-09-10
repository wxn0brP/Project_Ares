namespace Ares;

public class LanguageCWybor : Form{
	public string retVal = null;

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
			this.retVal = currLang;
			goto exit;
		}
		this.retVal = moduleName+"";

		exit:
		this.DialogResult = DialogResult.OK;
		this.Close();
	}

	private void ok_Click(object sender, EventArgs e) => convert();
	private void Form_Closing(object sender, FormClosedEventArgs e){
		if(string.IsNullOrWhiteSpace(this.retVal)) this.retVal = currLang;
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
    private string currLang = "pl";
	public LanguageCWybor(string[] opcje){
        currLang = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        bool znalezionoJezyk = false;
        foreach(string lang in opcje){
            if(lang == currLang){
                znalezionoJezyk = true;
                break;
            }
        }
        if(string.IsNullOrEmpty(currLang) || !znalezionoJezyk) currLang = "pl";

		this.components = new System.ComponentModel.Container();
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(400, 170);
		this.Text = "Ares - Language?";
		this.Icon = new System.Drawing.Icon("logo.ico");
		this.MaximizeBox = false;
		this.FormBorderStyle = FormBorderStyle.FixedSingle;
		this.FormClosed += new FormClosedEventHandler(this.Form_Closing);
        this.BackColor = System.Drawing.ColorTranslator.FromHtml("#222222");//# color
        this.ForeColor = System.Drawing.ColorTranslator.FromHtml("#aaaaaa");//# color
        Color eleBackColor = System.Drawing.ColorTranslator.FromHtml("#333333");
        Color eleFontColor = System.Drawing.ColorTranslator.FromHtml("#aaaaaa");
        ChangeTitleColorC.UseImmersiveDarkMode(Handle, true);

		kdL = new Label{
            Location = new System.Drawing.Point(5, 7),
            Size = new System.Drawing.Size(295, 25),
            Font = new Font("Consolas", 11),
            Text = "Choose Language?"
        };
		this.Controls.Add(kdL);

		ok = new Button{
            Location = new System.Drawing.Point(300, 5),
            Size = new System.Drawing.Size(95, 25),
            Font = new Font("Consolas", 11),
            Text = "ok",
            UseVisualStyleBackColor = true,
            FlatStyle = FlatStyle.Flat,
            BackColor = eleBackColor,
            ForeColor = eleFontColor,
            FlatAppearance = {
                BorderSize = 0
            }
        };
        this.ok.Click += new System.EventHandler(this.ok_Click);
		this.Controls.Add(ok);

		modules = new ListBox{
            Location = new System.Drawing.Point(5, 35),
            Size = new System.Drawing.Size(390, 130),
            Font = new Font("Consolas", 11),
            BackColor = eleBackColor,
            ForeColor = eleFontColor
        };
		this.Controls.Add(modules);

		foreach(string a in opcje){
			modules.Items.Add(a);
		}
	}
}