namespace Ares;

public partial class Ares : Form{
	private void infoB_Click(object sender, EventArgs en) => RunModInAresLib("info");
    
	/// <summary>
	/// przycisk wyJavaScriptEngineCie
	/// </summary>
	private void wyJavaScriptEngineCieB_Click(object sender, EventArgs en){
		close = true;
        this.Close();
	}

	/// <summary>
	/// przycisk odśwież moduły, odświeża moduły z listy
	/// </summary>
	private void odswiezModB_Click(object sender, EventArgs en){
		odswiezModuly();
	}

	/// <summary>
	/// przycisk umożliwiający zmianę języka
	/// </summary>
	private void zmienLangB_Click(object sender, EventArgs en){
		if(File.Exists("lang/cur_lang.txt")) File.WriteAllText("lang/cur_lang.txt", "");
        przeladujOkienko();
	}

	/// <summary>
	/// przycisk modules w menu
	/// </summary>
	private void menuModules_Click(object sender, EventArgs en){
		ToolStripButton se = (ToolStripButton)sender;
        ModuleC.moduleStart(se.Text);
	}

	/// <summary>
	/// przycisk odśwież Aresa, sprawdza i aktualizuje Aresa i moduły
	/// </summary>
	private void odswiezB_Click(object sender, EventArgs en){
		if(!UpdateC.UpdateAres()) return;
		odswiezModuly();
		MessageBox.Show(LanguageC.Get("Wszystko już jest aktualne")+"!");
	}

	/// <summary>
	/// przycisk ok, uruchamia moduł
	/// </summary>
	private void okB_Click(object sender, EventArgs en){
		string moduleName = this.modules.GetItemText(this.modules.SelectedItem);
		if(moduleName == "" || moduleName == null) return;
		ModuleC.moduleStart(moduleName);
	}

	/// <summary>
	/// przycisk modules w menu
	/// </summary>
	private void wylogujB_Click(object sender, EventArgs en){
        if(!Directory.Exists("dane")){
            Directory.CreateDirectory("dane");
        }

		DialogResult respone = MessageBox.Show(
			LanguageC.Get("Czy napewno chcesz się wylogować")+"?",
			"",
			MessageBoxButtons.YesNo
		);
		if(respone == DialogResult.Yes){
            try{
                if(File.Exists("dane/LicenseC.txt")) File.Delete("dane/LicenseC.txt");
            }catch{;
                return;
            }
		} else return;
		
		respone = MessageBox.Show(
			LanguageC.Get("Czy chcesz też usunąć dane logowania")+"?",
			"",
			MessageBoxButtons.YesNo
		);
		if(respone == DialogResult.Yes){
            try{
			    if(File.Exists("dane/dane.txt")) File.Delete("dane/dane.txt");
            }catch{
                return;
            }
		}

        Ares.close = true;
        this.Close();
        CheckAresC.RunAres();
	}

	/// <summary>
	/// zaloguj się btn
	/// </summary>
	private void zalogujB_Click(object sender, EventArgs e){
		ProC.Login();
        przeladujOkienko();
	}


	/// <summary>
	/// obrazek logo click - uruchamia moduł pisany
	/// </summary>
	private void logo_Click(object sender, EventArgs e){
        if(Control.ModifierKeys == (Keys.Control | Keys.Shift)){
            if(!reSizeEgg){
                reSizeEgg = true;
                this.Resize += new EventHandler(this.Form1_Resize);
                this.MaximizeBox = true;
                // File.WriteAllText("en.txt", "");
                // DeveloperC.PlaySong();
            }else return;
        }
        // return;
		System.Threading.Thread t1 = new System.Threading.Thread(() => {
			// Application.Run(new hik());
		});
		t1.SetApartmentState(ApartmentState.STA);
		t1.Start();
	}
}