namespace Ares;
using System.Dynamic;
using Newtonsoft.Json;

public class ConfigAresToModuleC{

    public static string Get(){
        dynamic data = new ExpandoObject();

        data.pro = ProC.Zalogowano;
        
        return JsonConvert.SerializeObject(data);
    }
}