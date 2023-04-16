namespace Ares;
using System.IO;
using Newtonsoft.Json;

partial class Ares{
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

    MenuStrip menu;
    PictureBox logo;

    ToolStripMenuItem akcjeMenu;

    ToolStripMenuItem odswiezMenu;

    Button okB;
    ListBox modules;

    System.Drawing.Color eleBackColor;
    System.Drawing.Color eleFontColor;

    FormWindowState? LastWindowState = null;
    int FormWidthInMax = 0;

    public void InitializeComponent(){
        FormWidthInMax = Screen.FromControl(this).Bounds.Width;
        // szablonChangeC.dobjA = JsonConvert.DeserializeObject<JObject>(File.ReadAllText("test.json"));

        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(320, 160);
        this.Text = "Ares (beta) - "+LanguageC.lang;
        this.Icon = new System.Drawing.Icon("logo.ico");
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.FormClosing += new FormClosingEventHandler(this.Ares_Closing);

        this.MaximizeBox = false;
        // this.Resize += new EventHandler(this.Form1_Resize);

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

    public void MenuInit(bool max=false){
        menu = new MenuStrip{
            Location = new System.Drawing.Point(0, 0),
            Size = new System.Drawing.Size((max ? FormWidthInMax : this.ClientSize.Width), 5),
            BackColor = eleBackColor
        };
        this.Controls.Add(menu);

        //ustawienia
        akcjeMenu = new ToolStripMenuItem(LanguageC.Get("Inne")){
            ForeColor = eleFontColor,
            Font = new Font("Consolas", 10)
        };

        this.akcjeMenu.DropDownItems.AddRange(new ToolStripItem[]{
            ZwyklyToolStrip(LanguageC.Get("Info"), new System.EventHandler(this.infoB_Click)),
            ZwyklyToolStrip(LanguageC.Get("Wyjście"), new System.EventHandler(this.wyJavaScriptEngineCieB_Click)),
            KontoBtn(),
            ZwyklyToolStrip("Change lang", new System.EventHandler(this.zmienLangB_Click))
        });
        menu.Items.Add(akcjeMenu);

        //odśwież
        odswiezMenu = new ToolStripMenuItem(LanguageC.Get("Odśwież")){
            ForeColor = eleFontColor,
            Font = new Font("Consolas", 10)
        };
        
        this.odswiezMenu.DropDownItems.AddRange(new ToolStripItem[]{
            ZwyklyToolStrip(LanguageC.Get("Listę modułów"), new System.EventHandler(this.odswiezModB_Click)),
            ZwyklyToolStrip(LanguageC.Get("Aktualizacje"), new System.EventHandler(this.odswiezB_Click))
        });

        menu.Items.Add(odswiezMenu);
    }

    public void OkiekoMale(){
        // logo
        logo = new PictureBox{
            Name = "logo",
            Location = new System.Drawing.Point(10, 30),
            Size = new System.Drawing.Size(65, 65)
        };
        SpecialLogoInEvent(ref logo);
		this.logo.Click += new System.EventHandler(this.logo_Click);
        this.Controls.Add(logo);

        //ok - załaduj moduł
        okB = new Button{
            Name = "okB",
            Location = new System.Drawing.Point(10, 95),
            Size = new System.Drawing.Size(64, 60),
            Font = new Font("Consolas", 11),
            Text = "OK",
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
        
        //modules - lista modułów
        modules = new ListBox{
            Location = new System.Drawing.Point(80, 35),
            Size = new System.Drawing.Size(235, 130),
            Font = new Font("Consolas", 10),
            Name = "modules",
            BackColor = eleBackColor,
            ForeColor = eleFontColor,
            ScrollAlwaysVisible = false,
            BorderStyle = BorderStyle.FixedSingle
        };
        this.modules.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckEnterKeyPress);
        this.Controls.Add(modules);
    }

    WebBrowser webBrowser1;

    public void OkienkoDuze(){
        webBrowser1 = new WebBrowser();
        webBrowser1.Dock = DockStyle.Fill;
        // webBrowser1.ScriptErrorsSuppressed = true;
        Controls.Add(webBrowser1);
        webBrowser1.Navigate("http://localhost:14882/ainw/");
    }


    private void Form1_Resize(object sender, EventArgs e){
        if(WindowState != LastWindowState){
            LastWindowState = WindowState;
            if(WindowState == FormWindowState.Maximized){
                this.Controls.Clear();
                OkienkoDuze();
                MenuInit();
                // odswiezModuly();
            }
            if(WindowState == FormWindowState.Normal){
                this.Controls.Clear();
                OkiekoMale();
                MenuInit();
                odswiezModuly();
            }
        }
        

    }


    public ToolStripMenuItem ZwyklyToolStrip(string txt, System.EventHandler evt){
        ToolStripMenuItem temp = new ToolStripMenuItem{
            Font = new Font("Consolas", 10),
            Text = txt,
            BackColor = this.eleBackColor,
            ForeColor = this.eleFontColor
        };
        temp.Click += evt;
        return temp;
    }

    public ToolStripMenuItem KontoBtn(){
        if(ProC.Zalogowano){
            return ZwyklyToolStrip(
                LanguageC.Get("Wyloguj się"),
                new System.EventHandler(this.wylogujB_Click));
        }else{
            return ZwyklyToolStrip(
                LanguageC.Get("Zaloguj"),
                new System.EventHandler(this.zalogujB_Click));
        }
    }
}