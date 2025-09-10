namespace Ares;
using Newtonsoft.Json;

public class loginForm : Form{

    public loginForm(){
        InitializeComponent();
        string[] dane = LicenseC.ReadLogin();
        if(dane[0] == "" || dane[1] == "") return;
        this.loginTB.Text = dane[0];
        this.hasloTB.Text = dane[1];
    }

    public bool Verify = false;
    public long Time = 0;

    private void okB_Click(object sender, EventArgs en){
        try{
            string login = this.loginTB.Text;
            string haslo = this.hasloTB.Text;
            if(login == "" || login == null || haslo == "" || haslo == null) return;

            bool[] respone = LicenseC.Verify(login, haslo, ref Time);
            if(respone[0]){
                LicenseC.WriteLogin(login, haslo);
            }else{
                this.loginL.Text = LanguageC.Get("Login")+": - "+LanguageC.Get("Nieudane logowanie")+"!";
                return;
            }

            if(!respone[1]){
                this.loginL.Text = LanguageC.Get("Login")+": - "+LanguageC.Get("Brak licencji")+"!";
                return;
            }

            if(respone[0] && respone[1]){
                this.loginL.Text = LanguageC.Get("Login")+": - OK";
                LicenseC.WriteLicenseC(Time);
                Thread.Sleep(1000);
                Verify = true;
                this.Close();
            }

        }catch(Exception e){
            ErrorC.InfDev("Błąd logowania "+e.ToString(), "login"); 
        }
    }

    private System.ComponentModel.IContainer components = null;
	protected override void Dispose(bool disposing){
		if(disposing && (components != null)){
            components.Dispose();
		}
		base.Dispose(disposing);
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData){//"ENTER" kilknięty
		if(keyData == Keys.Return){
			okB_Click(new object(), new EventArgs());
			return true;
		}
		return base.ProcessCmdKey(ref msg, keyData);
	}

    Button okB;
    TextBox loginTB;
    Label loginL;
    TextBox hasloTB;
    Label hasloL;

    private void InitializeComponent(){
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(400, 200);
        this.Text = "Ares - "+LanguageC.Get("logowanie");
        this.Icon = new System.Drawing.Icon("logo.ico");
        this.MaximizeBox = false;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;

        this.BackColor = System.Drawing.ColorTranslator.FromHtml("#222222");//# color
        this.ForeColor = System.Drawing.ColorTranslator.FromHtml("#aaaaaa");//# color
        System.Drawing.Color eleBackColor = System.Drawing.ColorTranslator.FromHtml("#333333");
        System.Drawing.Color eleFontColor = System.Drawing.ColorTranslator.FromHtml("#aaaaaa");
        if(File.Exists("config.json")){
            string? dataS = File.ReadAllText("config.json")+"";
            if(dataS == null) return;
            dynamic? a = JsonConvert.DeserializeObject<dynamic>(dataS);
            if(a == null) return;
            this.BackColor = System.Drawing.ColorTranslator.FromHtml(a.BackColor.ToString());//# color
            this.ForeColor = System.Drawing.ColorTranslator.FromHtml(a.FontColor.ToString());//# color
            eleBackColor = System.Drawing.ColorTranslator.FromHtml(a.eleBackColor.ToString());
            eleFontColor = System.Drawing.ColorTranslator.FromHtml(a.eleFontColor.ToString());
            if(a.blackTitle.ToString() == "true"){
                ChangeTitleColorC.UseImmersiveDarkMode(Handle, true);
            }
        }

        loginL = new Label{
            Location = new System.Drawing.Point(5, 6),
            Size = new System.Drawing.Size(295, 30),
            Font = new Font("Consolas", 11),
            Text = LanguageC.Get("Login")+":",
            ForeColor = eleFontColor
        };
        this.Controls.Add(loginL);

        this.loginTB = new TextBox{
            Location = new System.Drawing.Point(5, 40),
            Size = new System.Drawing.Size(390, 30),
            Font = new Font("Consolas", 11),
            BackColor = eleBackColor,
            ForeColor = eleFontColor
        };
        this.Controls.Add(this.loginTB);

        hasloL = new Label{
            Location = new System.Drawing.Point(5, 85),
            Size = new System.Drawing.Size(295, 30),
            Font = new Font("Consolas", 11),
            Text = LanguageC.Get("Hasło")+":",
            ForeColor = eleFontColor
        };
        this.Controls.Add(hasloL);

        this.hasloTB = new TextBox{
            Location = new System.Drawing.Point(5, 120),
            Size = new System.Drawing.Size(390, 30),
            Font = new Font("Consolas", 11),
            BackColor = eleBackColor,
            ForeColor = eleFontColor,
            UseSystemPasswordChar = true
        };
        this.Controls.Add(this.hasloTB);

        okB = new Button{
            Location = new System.Drawing.Point(5, 160),
            Size = new System.Drawing.Size(390, 30),
            Font = new Font("Consolas", 11),
            Text = LanguageC.Get("Zaloguj"),
            UseVisualStyleBackColor = true,
            BackColor = eleBackColor,
            ForeColor = eleFontColor,
            FlatStyle = FlatStyle.Flat,
            FlatAppearance = {
                BorderSize = 0
            }
        };
        this.okB.Click += new System.EventHandler(this.okB_Click);
        this.Controls.Add(okB);

    }
}