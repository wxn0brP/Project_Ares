namespace Ares;
using System.IO;
using Newtonsoft.Json;

public class ErrorWindow : Form{
    private System.ComponentModel.IContainer components = null;
    public string Err = "";
    public dynamic Obj = null;

    public ErrorWindow(string mieJavaScriptEngineCe, dynamic obj=null){
        this.Obj = obj;
        InitializeComponent(mieJavaScriptEngineCe);
    }

    private void errorL_Click(object sender, EventArgs en){
        MessageBox.Show(Err, "Ares - "+LanguageC.Get("Informacja deweloperska"), MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    Label? nazwaL;
    Label? errorL;

    public void InitializeComponent(string mieJavaScriptEngineCe){
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(510, 140);
        this.Text = "Ares - "+LanguageC.Get("Błąd")+"!";
        this.Icon = new System.Drawing.Icon("logo.ico");
        this.MaximizeBox = false;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;

        this.BackColor = System.Drawing.ColorTranslator.FromHtml("#222222");//# color
        this.ForeColor = System.Drawing.ColorTranslator.FromHtml("#aaaaaa");//# color
        System.Drawing.Color eleBackColor = System.Drawing.ColorTranslator.FromHtml("#333333");
        System.Drawing.Color eleFontColor = System.Drawing.ColorTranslator.FromHtml("#aaaaaa");
        if(File.Exists("config.json")){
            string dataS = File.ReadAllText("config.json")+"";
            dynamic a = JsonConvert.DeserializeObject<dynamic>(dataS);
            this.BackColor = System.Drawing.ColorTranslator.FromHtml(a.BackColor.ToString());//# color
            this.ForeColor = System.Drawing.ColorTranslator.FromHtml(a.FontColor.ToString());//# color
            eleBackColor = System.Drawing.ColorTranslator.FromHtml(a.eleBackColor.ToString());
            eleFontColor = System.Drawing.ColorTranslator.FromHtml(a.eleFontColor.ToString());
            if(a.blackTitle.ToString() == "true"){
                ChangeTitleColorC.UseImmersiveDarkMode(Handle, true);
            }
        }

        string temp = objExists<string>("txt", LanguageC.Get("Wystąpił problem")+"!");
        
        nazwaL = new Label{
            Location = new System.Drawing.Point(5, 5),
            Size = new System.Drawing.Size(500, 75),
            Text = temp,
            Font = new Font("Consolas", (int)objExists<long>("fz", (temp.IndexOf("\n") != -1 ? 15 : 20))),
            ForeColor = eleFontColor
        };
        this.Controls.Add(nazwaL);

        errorL = new Label{
            Location = new System.Drawing.Point(5, 85),
            Size = new System.Drawing.Size(500, 60),
            Font = new Font("Consolas", 11),
            Text = LanguageC.Get("Informacja deweloperska")+": \n("+mieJavaScriptEngineCe+")",
            ForeColor = eleFontColor
        };
        this.errorL.Click += new System.EventHandler(this.errorL_Click);
        this.Controls.Add(errorL);

    }

    public T objExists<T>(string branch, T defl){
        if(
            this.Obj == null ||
            branch == null ||
            branch == ""
        ) return defl;
        IDictionary<string, object> temp = (IDictionary<string, object>)this.Obj;
        if(temp.ContainsKey(branch)){
            return (T)temp[branch];
        }
        return defl;
    }
}