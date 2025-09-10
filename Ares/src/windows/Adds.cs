namespace Ares;
using Newtonsoft.Json;
using System.Diagnostics;

public class AddsC : Form{
    public static void Display(int x, int y, string url){
        System.Threading.Thread t1 = new System.Threading.Thread(() => {
            Application.Run(new AddsC(x, y, url));
		});
		t1.SetApartmentState(ApartmentState.STA);
		t1.Start();
    }
    public AddsC(int x, int y, string url){
        InitializeComponent(x, y, url);
    }

    private System.ComponentModel.IContainer components = null;
	protected override void Dispose(bool disposing){
		if(disposing && (components != null)){
            components.Dispose();
		}
		base.Dispose(disposing);
	}
    WebBrowser? webBrowser;

    private void InitializeComponent(int x, int y, string url){
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(x, y);
        this.Text = "Ares - "+LanguageC.Get("reklama");
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

        webBrowser = new WebBrowser{
            Location = new System.Drawing.Point(0, 0),
            Size = new System.Drawing.Size(x, y),
            ScrollBarsEnabled = false
        };
		this.webBrowser.Navigate(url);
		this.webBrowser.Navigating += new WebBrowserNavigatingEventHandler(webBrowser1_Navigating);
		this.Controls.Add(webBrowser);

    }

    public void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e){
        e.Cancel = true;
        
        ProcessStartInfo startInfo = new ProcessStartInfo();
		startInfo.FileName = "powershell";
		startInfo.Arguments = "start "+e.Url.ToString();
        startInfo.UseShellExecute = false;
		startInfo.CreateNoWindow = true;
		startInfo.WindowStyle = ProcessWindowStyle.Hidden;

        Process processTemp = new Process();
		processTemp.StartInfo = startInfo;
		processTemp.Start();
    }
}