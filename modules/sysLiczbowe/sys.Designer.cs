namespace Ares;
using System;
using System.Runtime.InteropServices;

public partial class sysLiczbowe{
    System.Drawing.Color eleBackColor;
    System.Drawing.Color eleFontColor;
    private System.ComponentModel.IContainer components = null;
    public Button przyciskInit(int x, int y, string text, int w, int h, string name=""){
        Button btn = new Button();
        btn.Location = new System.Drawing.Point(x, y);
        btn.Size = new System.Drawing.Size(w, h);
        btn.Font = new Font("Consolas", 11);
        btn.Text = text;
        btn.UseVisualStyleBackColor = true;
        btn.BackColor = this.eleBackColor;
        btn.ForeColor = this.eleFontColor;
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        return btn;
    }
    public TextBox TBInit(int x, int y, string text, int w, int h, string name=""){
        TextBox textBox = new TextBox();
        textBox.Location = new System.Drawing.Point(x, y);
        textBox.Size = new System.Drawing.Size(w, h);
        textBox.Font = new Font("Consolas", 11);
        textBox.Text = text;
		textBox.AutoSize = false;
		textBox.Multiline = true;
        textBox.BackColor = this.eleBackColor;
        textBox.ForeColor = this.eleFontColor;
        textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        return textBox;
    }
  
    TextBox out16TB, out10TB, out8TB, out2TB, outMyTB, inMyTB;
    Button Button_16, Button_10, Button_8, Button_2, Button_my;

    Button 
        Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8, Button9, Button0,
        ButtonA, ButtonB, ButtonC, ButtonD, ButtonE, ButtonF;

	Label[] labelL;

    private void InitializeComponent(){
        szablonInit.Init("sysLiczbowe", "sysLiczbowe");
		bool def = szablonInit.deflaut;

        int x = 120;
        int y = 60;
        int klaw = 250;
        int WierszY1 = klaw;
        int WierszX1 = 5;
        int WierszY2 = klaw+y+5;
        int WierszX2 = 5+x+5;
        int WierszY3 = klaw+2*y+2*5;
        int WierszX3 = 5+2*x+2*5;
        int WierszY4 = klaw+3*y+3*5;
        int WierszX4 = 5+3*x+3*5;

        int w = 5+4*x+4*5;

        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(w, WierszY4+y+5);
        this.Text = "Ares - konwerter systemów liczbowych";
        this.Icon = new System.Drawing.Icon("logo.ico");
        this.MaximizeBox = false;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;

        this.BackColor = System.Drawing.ColorTranslator.FromHtml("#222222");//# color
		this.ForeColor = System.Drawing.ColorTranslator.FromHtml("#aaaaaa");//# color
		System.Drawing.Color eleBackColor = System.Drawing.ColorTranslator.FromHtml("#333333");
		System.Drawing.Color eleFontColor = System.Drawing.ColorTranslator.FromHtml("#aaaaaa");
		if(def){
			UseImmersiveDarkMode(Handle, true);
		}else{
            szablonChangeOkienko();
			eleBackColor = szablonInit.eleBackColor;
			eleFontColor = szablonInit.eleFontColor;
			if(szablonInit.blackTitle){
				UseImmersiveDarkMode(Handle, true);
			}
		}
        this.eleBackColor = eleBackColor;
        this.eleFontColor = eleFontColor;

        int outw = (w-10)/2-5;
        int outx = w/2+5;
        int outh = klaw/5-10;
        int outy = klaw/5-5;

        this.out16TB = TBInit(5, 5, "", outw, outh);
        this.out16TB.Click += new System.EventHandler(this.ChangeCursor);
        this.Controls.Add(this.out16TB);

        Button_16 = przyciskInit(outx, 5, "HEX/16", outw, outh);
        this.Button_16.Click += new System.EventHandler(this.Button_16_Click);
        this.Controls.Add(Button_16);
        //--------


        this.out10TB = TBInit(5, outy+2*5, "", outw, outh);
        this.out10TB.Click += new System.EventHandler(this.ChangeCursor);
        this.Controls.Add(this.out10TB);

        Button_10 = przyciskInit(outx, outy+2*5, "DEC/10", outw, outh);
        this.Button_10.Click += new System.EventHandler(this.Button_10_Click);
        this.Controls.Add(Button_10);
        //-------


        this.out8TB = TBInit(5, outy*2+3*5, "", outw, outh);
        this.out8TB.Click += new System.EventHandler(this.ChangeCursor);
        this.Controls.Add(this.out8TB);

        Button_8 = przyciskInit(outx, outy*2+3*5, "OCT/8", outw, outh);
        this.Button_8.Click += new System.EventHandler(this.Button_8_Click);
        this.Controls.Add(Button_8);
        //-------

        this.out2TB = TBInit(5, outy*3+4*5, "", outw, outh);
        this.out2TB.Click += new System.EventHandler(this.ChangeCursor);
        this.Controls.Add(this.out2TB);

        Button_2 = przyciskInit(outx, outy*3+4*5, "BIN/2", outw, outh);
        this.Button_2.Click += new System.EventHandler(this.Button_2_Click);
        this.Controls.Add(Button_2);
        //-------

        this.outMyTB = TBInit(5, outy*4+5*5, "", outw, outh);
        this.outMyTB.Click += new System.EventHandler(this.ChangeCursor);
        this.Controls.Add(this.outMyTB);

        this.inMyTB = TBInit(outx, outy*4+5*5, "", outw/2-5, outh);
        this.inMyTB.Click += new System.EventHandler(this.ChangeCursor);
        this.Controls.Add(this.inMyTB);

        Button_my = przyciskInit(outx+outw/2, outy*4+5*5, "<--(2-36)", outw/2, outh);
        this.Button_my.Click += new System.EventHandler(this.Button_my_Click);
        this.Controls.Add(Button_my);



        //wiersz 1
        ButtonA = przyciskInit(WierszX1, WierszY1, "A", x, y);
        this.ButtonA.Click += new System.EventHandler(this.ButtonA_Click);
        this.Controls.Add(ButtonA);

        Button1 = przyciskInit(WierszX2, WierszY1, "1", x, y);
        this.Button1.Click += new System.EventHandler(this.Button1_Click);
        this.Controls.Add(Button1);

        Button2 = przyciskInit(WierszX3, WierszY1, "2", x, y);
        this.Button2.Click += new System.EventHandler(this.Button2_Click);
        this.Controls.Add(Button2);

        Button3 = przyciskInit(WierszX4, WierszY1, "3", x, y);
        this.Button3.Click += new System.EventHandler(this.Button3_Click);
        this.Controls.Add(Button3);

        //wiersz 2
        ButtonB = przyciskInit(WierszX1, WierszY2, "B", x, y);
        this.ButtonB.Click += new System.EventHandler(this.ButtonB_Click);
        this.Controls.Add(ButtonB);

        Button4 = przyciskInit(WierszX2, WierszY2, "4", x, y);
        this.Button4.Click += new System.EventHandler(this.Button4_Click);
        this.Controls.Add(Button4);

        Button5 = przyciskInit(WierszX3, WierszY2, "5", x, y);
        this.Button5.Click += new System.EventHandler(this.Button5_Click);
        this.Controls.Add(Button5);

        Button6 = przyciskInit(WierszX4, WierszY2, "6", x, y);
        this.Button6.Click += new System.EventHandler(this.Button6_Click);
        this.Controls.Add(Button6);

        //wiersz 3
        ButtonC = przyciskInit(WierszX1, WierszY3, "C", x, y);
        this.ButtonC.Click += new System.EventHandler(this.ButtonC_Click);
        this.Controls.Add(ButtonC);

        Button7 = przyciskInit(WierszX2, WierszY3, "7", x, y);
        this.Button7.Click += new System.EventHandler(this.Button7_Click);
        this.Controls.Add(Button7);

        Button8 = przyciskInit(WierszX3, WierszY3, "8", x, y);
        this.Button8.Click += new System.EventHandler(this.Button8_Click);
        this.Controls.Add(Button8);

        Button9 = przyciskInit(WierszX4, WierszY3, "9", x, y);
        this.Button9.Click += new System.EventHandler(this.Button9_Click);
        this.Controls.Add(Button9);

        //wiersz 4
        ButtonD = przyciskInit(WierszX1, WierszY4, "D", x, y);
        this.ButtonD.Click += new System.EventHandler(this.ButtonD_Click);
        this.Controls.Add(ButtonD);

        ButtonE = przyciskInit(WierszX2, WierszY4, "E", x, y);
        this.ButtonE.Click += new System.EventHandler(this.ButtonE_Click);
        this.Controls.Add(ButtonE);

        Button0 = przyciskInit(WierszX3, WierszY4, "0", x, y);
        this.Button0.Click += new System.EventHandler(this.Button0_Click);
        this.Controls.Add(Button0);

        ButtonF = przyciskInit(WierszX4, WierszY4, "F", x, y);
        this.ButtonF.Click += new System.EventHandler(this.ButtonF_Click);
        this.Controls.Add(ButtonF);



        if(!def){
			dynamic[] labelArray = szablonChangeC.dobjA["label"].ToObject<dynamic[]>();
			this.labelL = new Label[labelArray.Length];

			for(int i=0; i<labelArray.Length; i++){
				this.labelL[i] = new Label();
				this.labelL[i].Location = 
					new System.Drawing.Point(labelArray[i].x.ToObject<int>(), labelArray[i].y.ToObject<int>());
				this.labelL[i].Size = 
					new System.Drawing.Size(labelArray[i].w.ToObject<int>(), labelArray[i].h.ToObject<int>());
				this.labelL[i].Font = new Font("Consolas", labelArray[i].fontSize.ToObject<int>());
				this.labelL[i].Text = labelArray[i].txt.ToObject<string>();
				this.labelL[i].ForeColor = eleFontColor;
				this.Controls.Add(this.labelL[i]);
			}
		} 
    }

    public void szablonChangeOkienko(){
		szablonChangeC.SetProp<string>(this, "Text", (string)szablonChangeC.dobjA["txt"]);
		szablonChangeC.SetProp<Color>(this, "BackColor", szablonChangeC.stringToColor((string)szablonChangeC.dobjA["BackColor"]));
		szablonChangeC.SetProp<Color>(this, "ForeColor", szablonChangeC.stringToColor((string)szablonChangeC.dobjA["FontColor"]));
		szablonInit.eleBackColor = szablonChangeC.stringToColor((string)szablonChangeC.dobjA["eleBackColor"]);
		szablonInit.eleFontColor = szablonChangeC.stringToColor((string)szablonChangeC.dobjA["eleFontColor"]);
		szablonInit.blackTitle = (bool)szablonChangeC.dobjA["blackTitle"];
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