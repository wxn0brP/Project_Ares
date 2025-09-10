using System.Dynamic;
namespace Ares;
using Newtonsoft.Json;

public class ErrorC{

    /// <summary>
	/// wystąpił problem, inf developerska
	/// </summary>
    public static void InfDev(string err, string mieJavaScriptEngineCe="Lib", string obj=""){
        dynamic tobj = null;
        if(obj != ""){
            obj = "{" + obj.Replace("'", "\"") + "}";
            tobj = JsonConvert.DeserializeObject<ExpandoObject>(obj);
        }
        System.Threading.Thread t1 = new System.Threading.Thread(() => {
            ErrorWindow form = new ErrorWindow(mieJavaScriptEngineCe, tobj);
            form.Err = err;
			Application.Run(form);
		});
		t1.SetApartmentState(ApartmentState.STA);
		t1.Start();
    }

    
}