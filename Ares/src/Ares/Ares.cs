namespace Ares;
using System;

public partial class Ares : Form{
    public static bool close = false;
    public static bool reSizeEgg = false;
    // public bool closeBallonTip = false;

	public Ares(){
        AresInit();
    }
    
    public void AresInit(){
		System.Diagnostics.Debug.WriteLine("start");
		InitializeComponent();
        MenuInit();
        icon();
        OkiekoMale();
		odswiezModuly();
        changer();
		// logo_Click(new object(), new EventArgs());
	}

    public void przeladujOkienko(){
        LanguageC.Init();
        this.Controls.Clear();
        InitializeComponent();
        if(WindowState == FormWindowState.Maximized) OkienkoDuze();
        else if(WindowState == FormWindowState.Normal) OkiekoMale();
        MenuInit();
        IconContext();
        odswiezModuly();
    }

    public void changer(){
        if(System.Diagnostics.Debugger.IsAttached) return;
        try{
            var watcher = new FileSystemWatcher("./");
            watcher.Filter = "random.txt";
            watcher.IncludeSubdirectories = false;
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Attributes;
            watcher.Changed += OnChangedFile;
            watcher.EnableRaisingEvents = true;
        }catch{}
    }

    public void OnChangedFile(object sender, FileSystemEventArgs ed){
        malaIkonka_Click(new object(), new EventArgs());
    }

    public static void acc(){
        MessageBox.Show("Ahoj Ci!");
    }

	/// <summary>
	/// odświeża moduły wyświetlane w liscie
	/// </summary>
	public void odswiezModuly(){
		this.modules.Items.Clear();
        this.tld_db_mod.DropDownItems.Clear();
		string[] modules = ModuleC.GetModules();
		for(int i=0; i<modules.Length; i++){
            if(modules[i].IndexOf("-") == -1) continue;
			this.modules.Items.Add(modules[i]);
            ToolStripButton tl = new ToolStripButton();
            tl.Text = modules[i];
            tl.Click += new EventHandler(menuModules_Click);
			this.tld_db_mod.DropDownItems.Add(tl);
		}
	}

	/// <summary>
	/// metoda wywoływana podczas zamykania okienka
	/// </summary>
    private void Ares_Closing(object sender, FormClosingEventArgs e){
        if(Control.ModifierKeys == Keys.Shift){
            goto exitApp;
        }
        if(!close){
            e.Cancel = true;
            this.Hide();
			// malaIkonka.Visible = true;
            // if(closeBallonTip){
            //     MessC.ShowBalloonTipIcon(
            //         "Projekt Ares został zminimalizowany.",
            //         "Możesz otworzyć go ponownie klikając na ikonkę w prawym dolnym rogu ekranu.",
            //         malaIkonka,
            //         new EventHandler(malaIkonka_Click)
            //     );
            //     closeBallonTip = false;
            // }
            return;
        }

        exitApp:
        malaIkonka.Dispose();
        ReputationScoreC.WriteRep();
    }

	/// <summary>
	/// key press handler, enter - uruchamia moduł
	/// </summary>
	private void CheckEnterKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e){
		if(e.KeyChar == (char)Keys.Return){
            okB_Click(new object(), new EventArgs());
        }
	}

	/// <summary>
	/// uruchamia wbudowane zakładki
	/// </summary>
	private void RunModInAresLib(string name, bool inter = false){
		if(inter && !NetC.PingUrl(true)) return;
		System.Threading.Thread t1 = new System.Threading.Thread(() => {
			switch(name){
				case "info":
					Application.Run(new InfoC());
				break;
			}
		});
		t1.SetApartmentState(ApartmentState.STA);
		t1.Start();
		// t1.Join();
		// odswiezModuly();
	}

    public static void SpecialLogoInEvent(ref PictureBox lo){
        string act = NetC.DownloadString(ConfigData.url+"Ares/evtLogo.txt");
        if(act == ""){
            lo.ImageLocation = "logo64.ico";
            return;
        }
        if(!File.Exists("logo/"+act+".ico")){
            NetC.DownloadFile(ConfigData.url+"Ares/logo/"+act+".ico", "logo/"+act+".ico");
        }
        lo.ImageLocation = "logo/"+act+".ico";
    }
}
