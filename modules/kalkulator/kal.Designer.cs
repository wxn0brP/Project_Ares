using System.Net.Mime;
namespace Ares;
using System;
using System.Runtime.InteropServices;

public partial class kalkulator{
    private System.ComponentModel.IContainer components = null;
    
    public Button przyciskInit(int x, int y, string text, int w, int h, string name=""){
        Button btn = new Button();
        btn.Location = new System.Drawing.Point(x, y);
        btn.Size = new System.Drawing.Size(w, h);
        btn.Font = new Font("Consolas", 11);
        btn.Text = text;
        btn.UseVisualStyleBackColor = true;
        btn.BackColor = eleBackColor;
        btn.ForeColor = eleFontColor;
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        return btn;
    }
  
    TextBox resTB;
    Button 
        Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8, Button9, Button0,
        Button_plus, Button_minus, Button_rownasie, Button_razy, Button_podzielic,
        Button_nawias1, Button_nawias2,
        Button_pow, Button_sqrt, 
        Button_sin, Button_cos, Button_tan, Button_ctan,
        Button_wyczysc, Button_cofnij,
        Button_PI,
        Button_przecinek, Button_kropka;

	Label[] labelL;

    private void InitializeComponent(bool def, string[] line){
        int x = 100;
        int y = 60;
        int klaw = 200;
        int WierszY1 = klaw;
        int WierszX1 = 5;
        int WierszY2 = klaw+y+5;
        int WierszX2 = 5+x+5;
        int WierszY3 = klaw+2*y+2*5;
        int WierszX3 = 5+2*x+2*5;
        int WierszY4 = klaw+3*y+3*5;
        int WierszX4 = 5+3*x+3*5;
        int WierszY5 = klaw+4*y+4*5;
        int WierszY6 = klaw+5*y+5*5;
        int WierszY7 = klaw+6*y+6*5;
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(5+4*x+4*5, WierszY7+y+5);
        this.Text = "Ares - Kalkulator";
        this.Icon = new System.Drawing.Icon("logo.ico");
        this.MaximizeBox = false;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;

        this.BackColor = System.Drawing.ColorTranslator.FromHtml(configWin.bc);
		this.ForeColor = System.Drawing.ColorTranslator.FromHtml(configWin.fc);
		eleBackColor = System.Drawing.ColorTranslator.FromHtml(configWin.ebc);
		eleFontColor = System.Drawing.ColorTranslator.FromHtml(configWin.efc);
		if(def){
			UseImmersiveDarkMode(Handle, true);
		}else{
			if(configWin.bl+"" == "true"){
				UseImmersiveDarkMode(Handle, true);
			}
		}

        this.resTB = new TextBox();
		this.resTB.Location = new System.Drawing.Point(5,5);
		this.resTB.Size = new System.Drawing.Size(4*x+15, klaw - 10);
		this.resTB.Font = new Font("Consolas", 17);
		this.resTB.AutoSize = false;
		this.resTB.Multiline = true;
        this.resTB.BackColor = eleBackColor;
        this.resTB.ForeColor = eleFontColor;
        this.Controls.Add(this.resTB);


        //wiersz 1
        Button_wyczysc = przyciskInit(WierszX1, WierszY1, "C", x, y);
        this.Button_wyczysc.Click += new System.EventHandler(this.Button_wyczysc_Click);
        this.Controls.Add(Button_wyczysc);

        Button_cofnij = przyciskInit(WierszX2, WierszY1, "<-", x, y);
        this.Button_cofnij.Click += new System.EventHandler(this.Button_cofnij_Click);
        this.Controls.Add(Button_cofnij);

        Button_nawias1 = przyciskInit(WierszX3, WierszY1, "(", x, y);
        this.Button_nawias1.Click += (object sender, EventArgs en) => {this.resTB.Text += "(";};
        this.Controls.Add(Button_nawias1);

        Button_nawias2 = przyciskInit(WierszX4, WierszY1, ")", x, y);
        this.Button_nawias2.Click += (object sender, EventArgs en) => {this.resTB.Text += ")";};
        this.Controls.Add(Button_nawias2);

        //wiersz 2
        Button1 = przyciskInit(WierszX1, WierszY2, "1", x, y);
        this.Button1.Click += (object sender, EventArgs en) => {this.resTB.Text += "1";};
        this.Controls.Add(Button1);

        Button2 = przyciskInit(WierszX2, WierszY2, "2", x, y);
        this.Button2.Click += (object sender, EventArgs en) => {this.resTB.Text += "2";};
        this.Controls.Add(Button2);

        Button3 = przyciskInit(WierszX3, WierszY2, "3", x, y);
        this.Button3.Click += (object sender, EventArgs en) => {this.resTB.Text += "3";};
        this.Controls.Add(Button3);

        Button_plus = przyciskInit(WierszX4, WierszY2, "+", x, y);
        this.Button_plus.Click += (object sender, EventArgs en) => {this.resTB.Text += "+";};
        this.Controls.Add(Button_plus);

        //wiersz 3
        Button4 = przyciskInit(WierszX1, WierszY3, "4", x, y);
        this.Button4.Click += (object sender, EventArgs en) => {this.resTB.Text += "4";};
        this.Controls.Add(Button4);

        Button5 = przyciskInit(WierszX2, WierszY3, "5", x, y);
        this.Button5.Click += (object sender, EventArgs en) => {this.resTB.Text += "5";};
        this.Controls.Add(Button5);

        Button6 = przyciskInit(WierszX3, WierszY3, "6", x, y);
        this.Button6.Click += (object sender, EventArgs en) => {this.resTB.Text += "6";};
        this.Controls.Add(Button6);

        Button_minus = przyciskInit(WierszX4, WierszY3, "-", x, y);
        this.Button_minus.Click += (object sender, EventArgs en) => {this.resTB.Text += "-";};
        this.Controls.Add(Button_minus);

        //wiersz 4
        Button7 = przyciskInit(WierszX1, WierszY4, "7", x, y);
        this.Button7.Click += (object sender, EventArgs en) => {this.resTB.Text += "7";};
        this.Controls.Add(Button7);

        Button8 = przyciskInit(WierszX2, WierszY4, "8", x, y);
        this.Button8.Click += (object sender, EventArgs en) => {this.resTB.Text += "8";};
        this.Controls.Add(Button8);

        Button9 = przyciskInit(WierszX3, WierszY4, "9", x, y);
        this.Button9.Click += (object sender, EventArgs en) => {this.resTB.Text += "9";};
        this.Controls.Add(Button9);

        Button_razy = przyciskInit(WierszX4, WierszY4, "*", x, y);
        this.Button_razy.Click += (object sender, EventArgs en) => {this.resTB.Text += "*";};
        this.Controls.Add(Button_razy);

        //wiersz 5
        Button_rownasie = przyciskInit(WierszX1, WierszY5, "=", x, y);
        this.Button_rownasie.Click += new System.EventHandler(this.Button_rownasie_Click);
        this.Controls.Add(Button_rownasie);

        Button0 = przyciskInit(WierszX2, WierszY5, "0", x, y);
        this.Button0.Click += (object sender, EventArgs en) => {this.resTB.Text += "0";};
        this.Controls.Add(Button0);

        Button_kropka = przyciskInit(WierszX3, WierszY5, ".", x, y);
        this.Button_kropka.Click += (object sender, EventArgs en) => {this.resTB.Text += ".";};
        this.Controls.Add(Button_kropka);

        Button_podzielic = przyciskInit(WierszX4, WierszY5, "/", x, y);
        this.Button_podzielic.Click += (object sender, EventArgs en) => {this.resTB.Text += "/";};
        this.Controls.Add(Button_podzielic);

        //wiersz 6
        Button_pow = przyciskInit(WierszX1, WierszY6, "potęga\n(x, y)", x, y);
        this.Button_pow.Click += (object sender, EventArgs en) => {this.resTB.Text += "pow(";};
        this.Controls.Add(Button_pow);

        Button_sqrt = przyciskInit(WierszX2, WierszY6, "√(x, y)", x, y);
        this.Button_sqrt.Click += (object sender, EventArgs en) => {this.resTB.Text += "sqrt(";};
        this.Controls.Add(Button_sqrt);

        Button_przecinek = przyciskInit(WierszX3, WierszY6, ",", x, y);
        this.Button_przecinek.Click += (object sender, EventArgs en) => {this.resTB.Text += ", ";};
        this.Controls.Add(Button_przecinek);

        Button_PI = przyciskInit(WierszX4, WierszY6, "π", x, y);
        this.Button_PI.Click += (object sender, EventArgs en) => {this.resTB.Text += "π";};
        this.Controls.Add(Button_PI);

        //wiersz 7
        Button_sin = przyciskInit(WierszX1, WierszY7, "sin\n(x)", x, y);
        this.Button_sin.Click += (object sender, EventArgs en) => {this.resTB.Text += "sin(";};
        this.Controls.Add(Button_sin);

        Button_cos = przyciskInit(WierszX2, WierszY7, "con\n(x)", x, y);
        this.Button_cos.Click += (object sender, EventArgs en) => {this.resTB.Text += "con(";};
        this.Controls.Add(Button_cos);

        Button_tan = przyciskInit(WierszX3, WierszY7, "tan\n(x)", x, y);
        this.Button_tan.Click += (object sender, EventArgs en) => {this.resTB.Text += "tan(";};
        this.Controls.Add(Button_tan);

        Button_ctan = przyciskInit(WierszX4, WierszY7, "ctan\n(x)", x, y);
        this.Button_ctan.Click += (object sender, EventArgs en) => {this.resTB.Text += "ctan(";};
        this.Controls.Add(Button_ctan);

        if(!def){
            Func<string, int> f = (db) => {
                System.Net.WebClient wc = new System.Net.WebClient();
                var data = wc.DownloadData(db);
                var image = Image.FromStream(new MemoryStream(data));

                if(image.Width < this.ClientSize.Width || image.Height < this.ClientSize.Height){
                    var newWidth = Math.Max(image.Width, this.ClientSize.Width);
                    var newHeight = Math.Max(image.Height, this.ClientSize.Height);

                    var newImage = new Bitmap(newWidth, newHeight);
                    var graphics = Graphics.FromImage(newImage);
                    graphics.Clear(this.BackColor);
                    graphics.DrawImage(image, (this.ClientSize.Width - image.Width) / 2,
                        WierszY1+5, image.Width, image.Height);
                    this.BackgroundImage = newImage;
                }else{
                    this.BackgroundImage = image;
                }
            
                foreach(Control control in this.Controls){
                    if(control is Button button){
                        button.BackColor = Color.FromArgb(128, button.BackColor.R, 
                            button.BackColor.G, button.BackColor.B);
                    }
                }
                return 0; 
            };
            engine.SetValue("tapeta", f);

            engine.SetValue("objT", this);
            for(int i=1; i<line.Length; i++)
                exe(line[i]);
        }
        
    }
    
    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, 
    ref int attrValue, int attrSize);
    private const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;
    private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
    internal static bool UseImmersiveDarkMode(IntPtr handle, bool enabled){
        if(IsWindows10OrGreater(17763)){
            var attribute = DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
            if(IsWindows10OrGreater(18985)){
                attribute = DWMWA_USE_IMMERSIVE_DARK_MODE;
            }
            int useImmersiveDarkMode = enabled ? 1 : 0;
            return DwmSetWindowAttribute(handle, attribute, ref useImmersiveDarkMode, sizeof(int)) == 0;
        }
        return false;
    }
    private static bool IsWindows10OrGreater(int build = -1){
        return Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= build;
    }

   
}