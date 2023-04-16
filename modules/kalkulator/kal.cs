using System.Dynamic;
namespace Ares;
using System;
using Newtonsoft.Json;
using System.Reflection;
using Newtonsoft.Json.Linq;

public partial class kalkulator : Form{
    public void Init(string plusParams, string config){
        Application.Run(this);
    }

	public Jint.Engine engine;
    public dynamic configWin = new ExpandoObject();
	
	public Jint.Engine engineKal;
    public kalkulator(){
        configWin.bc="#222222"; configWin.fc="#aaaaaa";
        configWin.ebc="#333333"; configWin.efc="#aaaaaa"; configWin.bl="true";
        
        engineKal = new Jint.Engine();
        engineKal.Execute(@"
            function sqrt(x, y){ return Math.pow(x, 1/y); }
            function ctan(x)   { return 1 / Math.tan(x); }
        ");

		// this.Load += new System.EventHandler(Ares_Load);
        bool def = true;
        string[] line = new string[0];
        if(File.Exists("./Ares_lib.dll")){
            byte[] DllData = File.ReadAllBytes("./Ares_lib.dll");
            var DLL = Assembly.Load(DllData);
            Type? theType = DLL.GetType("Ares.szablonInit");
            if(theType == null) throw new Exception("Get Type null");
            object objE = Activator.CreateInstance(theType);

            string[] retVal = (string[])theType.GetMethod("Init").Invoke(objE, new object[]{"kalkulator", "kalkulator"});
            if(retVal[0] == "false"){
                line = retVal[1].Split("\n");
                engine = (Jint.Engine)theType.GetMethod("GetEngine").Invoke(objE, new object[]{""});
                configWin = (dynamic)theType.GetMethod("setConfig").Invoke(objE, new object[]{line[0]});
                def = false;
            }
        }

        InitializeComponent(def, line);
	}

	public static System.Drawing.Color eleBackColor;
	public static System.Drawing.Color eleFontColor;

    public void exe(string ed){
        try{
            engine.Execute(ed);
        }catch{}
    }

    public string licz(string dataP){
        string data = dataP
            .Replace("π", "Math.PI")
            .Replace("pow", "Math.pow")
            .Replace("sin", "Math.sin")
            .Replace("cos", "Math.cos")
            .Replace("tan", "Math.tan")
        ;
        return data;
    }

    private void Button_rownasie_Click(object sender, EventArgs en){
        try{
            this.resTB.Text = engineKal.Execute(licz(""+this.resTB.Text)).GetCompletionValue().ToString();
        }catch{}
    }

    private void Button_wyczysc_Click(object sender, EventArgs en){
        this.resTB.Text = "";
    }

    private void Button_cofnij_Click(object sender, EventArgs en){
        if(this.resTB.Text.Length > 0)
            this.resTB.Text = this.resTB.Text.Remove(this.resTB.Text.Length-1);
    }
}
