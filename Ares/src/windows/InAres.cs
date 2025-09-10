namespace Ares;
using Newtonsoft.Json;

public class InAresC : Form{
    public InAresC(){
        JavaScriptEngineC.Init();
        InitializeComponent();
        JavaScriptEngineC.engine.SetValue("tObj", this);
    }

    private void powiedzB_Click(object sender, EventArgs en){
        try{
            JavaScriptEngineC.Js(this.TMBox.Text);
        }catch(Exception e){
            ErrorC.InfDev("Błąd wykonywania \n"+e.ToString(), "inAres"); 
        }
    }

    private System.ComponentModel.IContainer components = null;
	protected override void Dispose(bool disposing){
		if(disposing && (components != null)){
            components.Dispose();
		}
		base.Dispose(disposing);
	}
    Button powiedzB;
    TextBox TMBox;
    Label TekstL;

    private void InitializeComponent(){
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(600, 200);
        this.Text = "Ares - inAres";
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

        TekstL = new Label{
            Location = new System.Drawing.Point(5, 6),
            Size = new System.Drawing.Size(295, 30),
            Font = new Font("Consolas", 11),
            Text = "Kod:",
            ForeColor = eleFontColor
        };
        this.Controls.Add(TekstL);

        powiedzB = new Button{
            Location = new System.Drawing.Point(495, 5),
            Size = new System.Drawing.Size(100, 30),
            Font = new Font("Consolas", 11),
            Text = "Wykonaj",
            BackColor = eleBackColor,
            ForeColor = eleFontColor,
            FlatStyle = FlatStyle.Flat,
            FlatAppearance = { 
                BorderSize = 0 
            }
        };
        powiedzB.Click += new System.EventHandler(this.powiedzB_Click);
        this.Controls.Add(powiedzB);

        this.TMBox = new TextBox{
            Location = new System.Drawing.Point(5, 45),
            Size = new System.Drawing.Size(590, 140),
            Font = new Font("Consolas", 10),
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            AcceptsReturn = true,
            AcceptsTab = true,
            WordWrap = true,
            BackColor = eleBackColor,
            ForeColor = eleFontColor
        };
        this.Controls.Add(this.TMBox);
    }
}