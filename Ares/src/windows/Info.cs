namespace Ares;
using Newtonsoft.Json;
using System.Diagnostics;

public class InfoC : Form{
    public InfoC(){
        InitializeComponent();
    }

    private System.ComponentModel.IContainer components = null;
	protected override void Dispose(bool disposing){
		if(disposing && (components != null)){
            components.Dispose();
		}
		base.Dispose(disposing);
	}
    WebBrowser? webBrowser;

    private void InitializeComponent(){
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(500, 300);
        this.Text = "Ares - "+LanguageC.Get("informacje - wersja")+": "+ConfigData.tWersja;
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
        
        string n = "<br />";
        string head = 
            "<style>body{background-color: "+
            colorToString(this.BackColor)+
            ";color: "+
            colorToString(eleFontColor)+
            ";}"+
            "a{color:"+
            colorToString(eleFontColor)+
            ";}</style>"
        ;
        string github = "https://github.com/wxn0brP/Project_Ares";
        string body = 
            LanguageC.Get("autor")+": <b>wxn0brP</b>"+n+
            LanguageC.Get("współautorzy")+":"+n+
            "- "+LanguageC.Get("aplikacja")+": <b>Kierownik</b>"+n+
            "- "+LanguageC.Get("grafika")+": <b>Siege</b>"+n+
            "- "+LanguageC.Get("media")+": <b>Slit</b>"+n+
            "<hr>"+
            LanguageC.Get("strona")+": <a href="+ConfigData.url+">"+removeProtocol(ConfigData.url)+"</a>"+n+
            LanguageC.Get("blog")+": <a href="+ConfigData.blog+">"+removeProtocol(ConfigData.blog)+"</a>"+n+
            LanguageC.Get("github")+": <a href="+github+">"+removeProtocol(github)+"</a>"+n+
            LanguageC.Get("pełna lista osób")+": <a href="+ConfigData.url+"strona/?p=lista"+"> -> link <- </a>"
        ;

        webBrowser = new WebBrowser{
            Location = new System.Drawing.Point(0, 0),
            Size = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height),
            ScrollBarsEnabled = false,
		    DocumentText = "<!DOCTYPE html><html><head><meta charset=\"UTF-8\">"+
                head+"</head><body>"+body+"</body></html>"
        };
		this.webBrowser.Navigating += new WebBrowserNavigatingEventHandler(webBrowser1_Navigating);
		this.Controls.Add(webBrowser);

    }

    public void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e){
        e.Cancel = true;
        
        ProcessStartInfo startInfo = new ProcessStartInfo();
		startInfo.FileName = "powershell";
		startInfo.Arguments = "start "+e.Url.ToString();

        Process processTemp = new Process();
		processTemp.StartInfo = startInfo;
		processTemp.Start();
    }

    public string removeProtocol(string data){
        return data.Replace("https", "").Replace("http", "").Replace("://", "");
    }

    public string colorToString(Color a){
        return System.Drawing.ColorTranslator.ToHtml(a);
    }
}