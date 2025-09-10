namespace Ares;

public class RamControlC{
    public const long MaxMemoryUsage = 104857600; // 100 MB

    public static bool ControlR(){
        if(GC.GetTotalMemory(true) > MaxMemoryUsage){
            System.Windows.Forms.MessageBox.Show("RAM is eat");
            Application.Exit();
			Environment.Exit(0);
            return true;
        }
        return false;
    }
}