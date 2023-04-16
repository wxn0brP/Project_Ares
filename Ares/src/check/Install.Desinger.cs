namespace Ares;
using System.IO;
using Newtonsoft.Json;

public partial class InstallC : Form{
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	///  Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing){
		if(disposing && (components != null)){
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	System.Drawing.Color eleBackColor;
	System.Drawing.Color eleFontColor;

	PictureBox logo;
	Button okB;
	ProgressBar pbBar;
	Label labelInfo;

    WebBrowser webBrowser;

	public void InitializeComponent(){
		this.components = new System.ComponentModel.Container();
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(410, 160);
		this.Text = "Ares - Installer";
		this.Icon = new System.Drawing.Icon("logo.ico");
		this.MaximizeBox = false;
		this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.FormClosing += new FormClosingEventHandler(this.Ares_Closing);

		this.BackColor = System.Drawing.ColorTranslator.FromHtml("#222222");//# color
		this.ForeColor = System.Drawing.ColorTranslator.FromHtml("#aaaaaa");//# color
		this.eleBackColor = System.Drawing.ColorTranslator.FromHtml("#333333");
		this.eleFontColor = System.Drawing.ColorTranslator.FromHtml("#aaaaaa");
		if(File.Exists("config.json")){
			string dataS = File.ReadAllText("config.json")+"";
			dynamic a = JsonConvert.DeserializeObject<dynamic>(dataS);
			this.BackColor = System.Drawing.ColorTranslator.FromHtml(a.BackColor.ToString());//# color
			this.ForeColor = System.Drawing.ColorTranslator.FromHtml(a.FontColor.ToString());//# color
			this.eleBackColor = System.Drawing.ColorTranslator.FromHtml(a.eleBackColor.ToString());
			this.eleFontColor = System.Drawing.ColorTranslator.FromHtml(a.eleFontColor.ToString());
			if(a.blackTitle.ToString() == "true"){
				ChangeTitleColorC.UseImmersiveDarkMode(Handle, true);
			}
		}

	}

    private void Ares_Closing(object sender, FormClosingEventArgs e){
        if(File.Exists("logo.ico")) File.Delete("logo.ico");
        if(File.Exists("Ares_lib.dll")) File.Delete("Ares_lib.dll");
    }

	public void window_1(){
		this.Controls.Clear();
		this.ClientSize = new System.Drawing.Size(615, 80);
		// logo
        logo = new PictureBox{
            Name = "logo",
            Location = new System.Drawing.Point(15, 5),
            Size = new System.Drawing.Size(65, 65)
        };
        this.logo.Load(ConfigData.url+"Ares/logo64.ico");
        this.Controls.Add(logo);

		//dalej
        okB = new Button{
            Name = "okB",
            Location = new System.Drawing.Point(105, 5),
            Size = new System.Drawing.Size(250, 60),
            Font = new Font("Consolas", 12),
            Text = "Install in PC",
            UseVisualStyleBackColor = true,
            BackColor = this.eleBackColor,
            ForeColor = this.eleFontColor,
            FlatStyle = FlatStyle.Flat,
            FlatAppearance = {
                BorderSize = 0
            }
        };
        this.okB.Click += new System.EventHandler(this.okB_Click);
        this.Controls.Add(okB);

		//dalej
        Button LokalnieokB = new Button{
            Location = new System.Drawing.Point(360, 5),
            Size = new System.Drawing.Size(250, 60),
            Font = new Font("Consolas", 12),
            Text = "Install in...",
            UseVisualStyleBackColor = true,
            BackColor = this.eleBackColor,
            ForeColor = this.eleFontColor,
            FlatStyle = FlatStyle.Flat,
            FlatAppearance = {
                BorderSize = 0
            }
        };
        LokalnieokB.Click += new System.EventHandler(this.przenosnieB_Click);
        this.Controls.Add(LokalnieokB);
	}

    public void window_2(){
		this.Controls.Clear();
        int x = 500;
        int y = 300;
        int but = 50;
		this.ClientSize = new System.Drawing.Size(x, y+but);

        webBrowser = new WebBrowser{
            Location = new System.Drawing.Point(0, 0),
            Size = new System.Drawing.Size(x, y),
            ScrollBarsEnabled = true
        };
		this.webBrowser.Navigate(ConfigData.url+"polityka/regulamin/index.html");
		this.webBrowser.Navigating += (object sender, WebBrowserNavigatingEventArgs e) => {
            e.Cancel = true;
        };
		this.Controls.Add(webBrowser);

        Button cancleB = new Button{
            Location = new System.Drawing.Point(5, y+5),
            Size = new System.Drawing.Size(200, 40),
            Font = new Font("Consolas", 12),
            Text = "I disagree",
            UseVisualStyleBackColor = true,
            BackColor = this.eleBackColor,
            ForeColor = this.eleFontColor,
            FlatStyle = FlatStyle.Flat,
            FlatAppearance = {
                BorderSize = 0
            }
        };
        cancleB.Click += (object sender, EventArgs ex) => {
            this.Close();
        };
        this.Controls.Add(cancleB);

        okB = new Button{
            Name = "okB",
            Location = new System.Drawing.Point(x-205, y+5),
            Size = new System.Drawing.Size(200, 40),
            Font = new Font("Consolas", 12),
            Text = "I agree",
            UseVisualStyleBackColor = true,
            BackColor = this.eleBackColor,
            ForeColor = this.eleFontColor,
            FlatStyle = FlatStyle.Flat,
            FlatAppearance = {
                BorderSize = 0
            }
        };
        this.okB.Click += new System.EventHandler(this.okB_Click);
        this.Controls.Add(okB);
    }

	public void window_3(){
		this.Controls.Clear();
		this.ClientSize = new System.Drawing.Size(430, 120);
		
		//progres Bar instalacji
		pbBar = new ProgressBar{
            Location = new System.Drawing.Point(5, 5),
            Size = new System.Drawing.Size(420, 30)
        };
	 	this.Controls.Add(pbBar);
		this.pbBar.Maximum = 100;
		this.pbBar.Minimum = 0;
		this.pbBar.Value = 0;

		//dalej
        okB = new Button{
            Name = "okB",
            Location = new System.Drawing.Point(5, 85),
            Size = new System.Drawing.Size(150, 30),
            Font = new Font("Consolas", 11),
            Text = "OK",
            UseVisualStyleBackColor = true,
            BackColor = this.eleBackColor,
            ForeColor = this.eleFontColor,
            FlatStyle = FlatStyle.Flat,
            FlatAppearance = {
                BorderSize = 0
            },
		    Enabled = false
        };
        this.okB.Click += new System.EventHandler(this.okB_Click);
        this.Controls.Add(okB);
        
        //progres label
		labelInfo = new Label{
            Location = new System.Drawing.Point(5, 40+10),
            Size = new System.Drawing.Size(420, 30),
            Font = new Font("Consolas", 11),
            Text = "(Progres)",
            ForeColor = eleFontColor,
        };
        this.Controls.Add(labelInfo);
	}
}