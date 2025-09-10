namespace update;
using System.IO;
using System.Diagnostics;

public class update{
	public void Init(string plusParams){
        if(File.Exists("Ares_lib2.dll")){
            File.Delete("Ares_lib.dll");
            File.Move("Ares_lib2.dll", "Ares_lib.dll");
            Process.Start("Ares_Launcher.exe", plusParams);
        }else{
            MessageBox.Show("Update Error!");
        }
    }
}
