namespace Ares;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
public partial class AMowi{
    private System.ComponentModel.IContainer components = null;

    Button powiedzB;
    TextBox TMBox;
    Label TekstL;

    private void InitializeComponent(){//Inicjalizuje grafike3
        szablonInit.Init("AMowi", "Ares mówi");
		bool def = szablonInit.deflaut;

        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(400, 80);
        this.Text = "Ares - Ares Mówi";
        this.Icon = new System.Drawing.Icon("logo64.ico");

        this.BackColor = System.Drawing.ColorTranslator.FromHtml("#222222");//# color
		this.ForeColor = System.Drawing.ColorTranslator.FromHtml("#aaaaaa");//# color
		System.Drawing.Color eleBackColor = System.Drawing.ColorTranslator.FromHtml("#333333");
		System.Drawing.Color eleFontColor = System.Drawing.ColorTranslator.FromHtml("#aaaaaa");
		if(def){
			UseImmersiveDarkMode(Handle, true);
		}else{
            szablonChangeC.szablonChangeOkienko(this);
			eleBackColor = szablonInit.eleBackColor;
			eleFontColor = szablonInit.eleFontColor;
			if(szablonInit.blackTitle){
				UseImmersiveDarkMode(Handle, true);
			}
		}

        TekstL = new Label();
        this.TekstL.Location = new System.Drawing.Point(5, 6);
        this.TekstL.Size = new System.Drawing.Size(295, 30);
        this.TekstL.Font = new Font("Consolas", 11);
        this.TekstL.Text = "Tu wpisz co mam powiedzieć: ";
        this.TekstL.ForeColor = eleFontColor;
        this.Controls.Add(TekstL);

        powiedzB = new Button();
        this.powiedzB.Location = new System.Drawing.Point(305, 5);
        this.powiedzB.Size = new System.Drawing.Size(90, 25);
        this.powiedzB.Font = new Font("Consolas", 11);
        this.powiedzB.Text = "Powiedz";
        this.powiedzB.UseVisualStyleBackColor = true;
        this.powiedzB.Click += new System.EventHandler(this.powiedzB_Click);
        this.powiedzB.BackColor = eleBackColor;
        this.powiedzB.ForeColor = eleFontColor;
        this.powiedzB.FlatStyle = FlatStyle.Flat;
        this.powiedzB.FlatAppearance.BorderSize = 0;
        this.Controls.Add(powiedzB);

        this.TMBox = new TextBox();
        this.TMBox.Location = new System.Drawing.Point(5, 40);
        this.TMBox.Size = new System.Drawing.Size(390, 25);
        this.TMBox.Font = new Font("Consolas", 11);
        this.TMBox.BackColor = eleBackColor;
        this.TMBox.ForeColor = eleFontColor;
        this.Controls.Add(this.TMBox);
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
