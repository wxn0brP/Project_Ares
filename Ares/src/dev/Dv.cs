namespace Ares;

public class DvC{
    
    /// <summary>
	/// ładuje plik dll i uruchamia z podanego namescape i class (ta sama nazwa) metode Init (Main)
	/// </summary>
    public static void runDll(string path, string name, string plusParam){
        System.Threading.Thread t1 = new System.Threading.Thread(() => {
            ModuleC.loadModule(path, name, plusParam);
		});
		t1.SetApartmentState(ApartmentState.STA);
		t1.Start();
    }
}