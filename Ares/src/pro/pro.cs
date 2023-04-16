namespace Ares;

public class ProC{
    public static bool Zalogowano = false;

    public static void Init(){
        Zalogowano = LicenseC.quicVery();
        if(!Zalogowano) return;

        // JavaScriptEngineC.Init();
		DeveloperC.Init();
    }

    public static void Login() => Zalogowano = LicenseC.run();
}