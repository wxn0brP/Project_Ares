namespace Ares;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

public partial class av : Form{
    private System.ComponentModel.IContainer components = null;

    MenuStrip menu;

    ToolStripMenuItem plikB;
    ToolStripMenuItem dirMenu;
    ToolStripMenuItem dirB;
    ToolStripMenuItem dirPB;
    ToolStripMenuItem Anuluj;
    ToolStripMenuItem pokazB;
    ProgressBar pBar;
    ListBox pliki;

    // ToolStripMenuItem subMenu;

    private void InitializeComponent(){
        szablonInit.Init("av", "Anty Virus");
		bool def = szablonInit.deflaut;

        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(460, 170);
        this.Text = "Ares - Anty Wirus";
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

        menu = new MenuStrip();
        this.menu.Location = new System.Drawing.Point(0, 0);
        this.menu.Size = new System.Drawing.Size(460, 5);
        this.menu.BackColor = eleBackColor;
        this.Controls.Add(menu);
        // subMenu = new ToolStripMenuItem("Akcje");
        // menu.Items.Add(subMenu);

        plikB = new ToolStripMenuItem();
        this.plikB.Font = new Font("Consolas", 11);
        this.plikB.Text = "Plik";
        this.plikB.Click += new System.EventHandler(this.plikB_Click);
        this.plikB.BackColor = eleBackColor;
        this.plikB.ForeColor = eleFontColor;
        menu.Items.Add(plikB);

        dirMenu = new ToolStripMenuItem("Folder");
        this.dirMenu.ForeColor = eleFontColor;
        this.dirMenu.Font = new Font("Consolas", 11);

        dirB = new ToolStripMenuItem();
        this.dirB.Font = new Font("Consolas", 11);
        this.dirB.Text = "Folder";
        this.dirB.Click += new System.EventHandler(this.dirB_Click);
        this.dirB.BackColor = eleBackColor;
        this.dirB.ForeColor = eleFontColor;

        dirPB = new ToolStripMenuItem();
        this.dirPB.Font = new Font("Consolas", 11);
        this.dirPB.Text = "Folder i Podfoldery";
        this.dirPB.Click += new System.EventHandler(this.dirPB_Click);
        this.dirPB.BackColor = eleBackColor;
        this.dirPB.ForeColor = eleFontColor;

        Anuluj = new ToolStripMenuItem();
        this.Anuluj.Font = new Font("Consolas", 11);
        this.Anuluj.Text = "Anuluj";
        this.Anuluj.Click += new System.EventHandler(this.Anuluj_Click);
        this.Anuluj.BackColor = eleBackColor;
        this.Anuluj.ForeColor = eleFontColor;

        this.dirMenu.DropDownItems.AddRange(new ToolStripItem[]{
             this.dirB,
             this.dirPB,
             this.Anuluj
        });
        menu.Items.Add(dirMenu);

        pokazB = new ToolStripMenuItem();
        this.pokazB.Font = new Font("Consolas", 11);
        this.pokazB.Text = "Pokaż";
        this.pokazB.Click += new System.EventHandler(this.pokazB_Click);
        this.pokazB.BackColor = eleBackColor;
        this.pokazB.ForeColor = eleFontColor;
        menu.Items.Add(pokazB);

        pBar = new ProgressBar();
        this.pBar.Location = new System.Drawing.Point(5, 30);
        this.pBar.Size = new System.Drawing.Size(450, 20);
        this.pBar.Visible = true;
        this.pBar.Minimum = 0;
        this.pBar.Step = 1;
        this.pBar.BackColor = eleBackColor;
        this.pBar.ForeColor = eleFontColor;
        this.Controls.Add(pBar);

        pliki = new ListBox();
        this.pliki.Location = new System.Drawing.Point(5, 55);
        this.pliki.Size = new System.Drawing.Size(450, 120);
        this.pliki.Font = new Font("Consolas", 11);
        this.pliki.BackColor = eleBackColor;
        this.pliki.ForeColor = eleFontColor;
        this.Controls.Add(pliki);
        // this.subMenu.DropDownItems.AddRange(new ToolStripItem[]{
        //     this.Anuluj
        // });


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
