namespace Ares;
using System.Runtime.InteropServices;

public partial class kd{
	private System.ComponentModel.IContainer components = null;
	protected override void Dispose(bool disposing){
		if(disposing && (components != null)){
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	Button ok;
	TextBox resultTB;
	TextBox contentsTB;

	RadioButton kdWyborK;
	RadioButton kdWyborD;
	RadioButton kdWybor;


	private void InitializeComponent(bool def, string[] line){//Inicjalizuje grafike
		this.components = new System.ComponentModel.Container();
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(770, 355);
		this.Text = "Ares - KD";
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

		//wiersz 1
		//redio kd - k
		this.kdWyborK = new System.Windows.Forms.RadioButton();
		this.kdWyborK.Location = new System.Drawing.Point(10, 5);
		this.kdWyborK.Size = new System.Drawing.Size(90, 30);
		this.kdWyborK.Text = "Koduj";
		this.kdWyborK.Name = "k";
		this.kdWyborK.Font = new Font("Consolas", 11);
		this.kdWyborK.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
		this.Controls.Add(this.kdWyborK);
		this.kdWybor = this.kdWyborK;
        if(!def) engine.SetValue("kdWyborK", kdWyborK);

		//redio kd - d
		this.kdWyborD = new System.Windows.Forms.RadioButton();
		this.kdWyborD.Location = new System.Drawing.Point(105, 5);
		this.kdWyborD.Size = new System.Drawing.Size(120, 30);
		this.kdWyborD.Text = "DeKoduj";
		this.kdWyborD.Name = "d";
		this.kdWyborD.Font = new Font("Consolas", 11);
		this.kdWyborD.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
		this.Controls.Add(this.kdWyborD);
        if(!def) engine.SetValue("kdWyborD", kdWyborD);

		//ok
		ok = new Button();
		this.ok.Location = new System.Drawing.Point(260, 5);
		this.ok.Size = new System.Drawing.Size(100, 25);
		this.ok.Font = new Font("Consolas", 11);
		this.ok.Text = "ok";
		this.ok.UseVisualStyleBackColor = true;
		this.ok.Click += new System.EventHandler(this.ok_Click);
		this.ok.BackColor = eleBackColor;
		this.ok.ForeColor = eleFontColor;
		this.ok.FlatStyle = FlatStyle.Flat;
		this.ok.FlatAppearance.BorderSize = 0;
		this.Controls.Add(ok);
        if(!def) engine.SetValue("ok", ok);

		//wiersz 2
		//contentsTB
		this.contentsTB = new TextBox();
		this.contentsTB.Location = new System.Drawing.Point(5, 40);
		this.contentsTB.Size = new System.Drawing.Size(760, 150);
		this.contentsTB.Font = new Font("Consolas", 11);
		this.contentsTB.PlaceholderText = "Treść/Ścieżka";
		this.contentsTB.AutoSize = false;
		this.contentsTB.Multiline = true;
		this.contentsTB.BackColor = eleBackColor;
		this.contentsTB.ForeColor = eleFontColor;
		this.Controls.Add(this.contentsTB);
        if(!def) engine.SetValue("contentsTB", contentsTB);

		//wiersz 3
		//resultTB
		resultTB = new TextBox();
		this.resultTB.Location = new System.Drawing.Point(5, 195);
		this.resultTB.Size = new System.Drawing.Size(760, 150);
		this.resultTB.Font = new Font("Consolas", 11);
		this.resultTB.PlaceholderText = "Wynik";
		this.resultTB.AutoSize = false;
		this.resultTB.Multiline = true;
		this.resultTB.BackColor = eleBackColor;
		this.resultTB.ForeColor = eleFontColor;
		this.Controls.Add(resultTB);
        if(!def) engine.SetValue("resultTB", resultTB);

        if(!def){
            engine.SetValue("objT", this);
            for(int i=1; i<line.Length; i++)
                exe(line[i]);
        }
	}

	
	private void radioButton_CheckedChanged(object sender, EventArgs e){
		RadioButton rb = sender as RadioButton;
		if(rb == null){
			MessageBox.Show("Błąd");
			return;
		}
		if(rb.Checked){
			this.kdWybor = rb;
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