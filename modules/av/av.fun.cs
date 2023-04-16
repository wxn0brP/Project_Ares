namespace Ares;

public partial class av{
    private void plikB_Click(object sender, EventArgs en){
        try{
            OpenFileDialog fd = new OpenFileDialog();
            DialogResult result = fd.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK){
                string res = changeNumToText(avObj.Scan(fd.FileName));
                MessageBox.Show("Plik: "+res);
            }
        }catch(Exception e){
            MessageBox.Show("Wystąpił błąd! \n\nInformacja Deweloperska:\n"+e);
        }
    }

    private void pokazB_Click(object sender, EventArgs en){
        MessageBox.Show(this.pliki.GetItemText(this.pliki.SelectedItem)+"");
    }

    private void Anuluj_Click(object sender, EventArgs en){
        continueScan = false;
    }

    private void dirB_Click(object sender, EventArgs en){
        ScanDir();
    }
    private void dirPB_Click(object sender, EventArgs en){
        ScanDir(true);
    }

    private async void ScanDir(bool podDir=false){
        continueScan = false;
        try{
            FolderBrowserDialog dd = new FolderBrowserDialog();
            DialogResult result = dd.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK){
                this.Text = "Ares - Anty Wirus - Skanowanie";
                string[] files;
                if(podDir){
                    files = Directory.GetFiles(dd.SelectedPath, "*.*", SearchOption.AllDirectories);
                }else{
                    files = Directory.GetFiles(dd.SelectedPath);
                }
                if(files.Length > 1_000){
                    this.Text = "Ares - Anty Wirus";
                    MessageBox.Show(
                        "Niestety, ale ten antywirus jest jeszcze zbyt słaby, aby przeskanować taką ilość plików! "+
                        "Aktualnie maksymalna ilość plików to 1000."
                    );
                    return;
                }
                pBar.Value = 0;
                this.pliki.Items.Clear();
                this.pliki.Items.Add("✔ - Nie znaleziono zagrożenia ❌- Plik jest Wirusem!");
                this.pliki.Items.Add(dd.SelectedPath);
                List<string> virus = new List<string>();
                continueScan = true;
                this.pBar.Maximum = files.Length;
                for(int i=0; i<files.Length; i++){
                    if(!continueScan) break;
                    int res = 0;
                    await Task.Run(() => {
                        res = avObj.Scan(files[i]);
                    });
                    if(res != 0 && res != 2){
                        virus.Add(changeNumToText(res) + " " + files[i].Replace(dd.SelectedPath+"\\", ""));
                    }
                    this.pliki.Items.Add(
                        changeNumToText(res, false) + " " + files[i].Replace(dd.SelectedPath+"\\", "")
                    );
                    pBar.Value = i;
                }
                pBar.Value = pBar.Maximum;
                this.Text = "Ares - Anty Wirus";
                MessageBox.Show("Wykryto "+virus.Count+" wirusów lub zagrożeń!\n"+string.Join("\n", virus.ToArray()));
                pBar.Value = 0;
            }
        }catch(Exception e){
            MessageBox.Show("Wystąpił błąd! \n\nInformacja Deweloperska:\n"+e);
        }
    }
}