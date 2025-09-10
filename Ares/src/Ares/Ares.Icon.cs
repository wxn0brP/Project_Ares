namespace Ares;
using System.Windows.Forms;

public partial class Ares : Form{
	System.Windows.Forms.NotifyIcon malaIkonka = new System.Windows.Forms.NotifyIcon();

	public void icon(){
		malaIkonka = new System.Windows.Forms.NotifyIcon(components);
		malaIkonka.Icon = new System.Drawing.Icon("logo.ico");
		malaIkonka.Visible = true;
		malaIkonka.Text = "Ares";

        IconContext();
	}

	private void malaIkonka_Click(object sender, EventArgs e){
        this.Show();
        WindowState = FormWindowState.Normal;
	}
}