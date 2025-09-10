namespace Ares;
using System;
using System.Windows.Forms;

public partial class Ares : Form{
	ContextMenuStrip contextMenu;
	ToolStripDropDownButton tld_db_mod;
	ToolStripDropDownButton tld_db_ref;

	public void IconContext(){
		contextMenu = new ContextMenuStrip();
		this.malaIkonka.ContextMenuStrip = contextMenu;

		ToolStripButton tlB_Open = new ToolStripButton();
		tlB_Open.Text = LanguageC.Get("Otwórz");
		tlB_Open.Click += new EventHandler(malaIkonka_Click);
		contextMenu.Items.Add(tlB_Open);

		tld_db_mod = new ToolStripDropDownButton(LanguageC.Get("Moduły"), null, new ToolStripButton[]{});
		contextMenu.Items.Add(tld_db_mod);


		ToolStripButton tlB_odsM = new ToolStripButton();
		tlB_odsM.Text = LanguageC.Get("Listę modułów");
		tlB_odsM.Click += new EventHandler(odswiezModB_Click);
		contextMenu.Items.Add(tlB_odsM);

		ToolStripButton tlB_Akt = new ToolStripButton();
		tlB_Akt.Text = LanguageC.Get("Aktualizacje");
		tlB_Akt.Click += new EventHandler(odswiezB_Click);
		contextMenu.Items.Add(tlB_Akt);


		tld_db_ref = new ToolStripDropDownButton(LanguageC.Get("Odśwież"), null, new ToolStripButton[]{
            tlB_odsM,
            tlB_Akt
        });
		contextMenu.Items.Add(tld_db_ref);
        

		ToolStripButton tlB_Info = new ToolStripButton();
		tlB_Info.Text = LanguageC.Get("Info");
		tlB_Info.Click += new EventHandler(infoB_Click);
		contextMenu.Items.Add(tlB_Info);

		ToolStripButton tsB_Exit = new ToolStripButton();
		tsB_Exit.Text = LanguageC.Get("Wyjście");
		tsB_Exit.Click += new EventHandler(wyJavaScriptEngineCieB_Click);
		contextMenu.Items.Add(tsB_Exit);
	}
}