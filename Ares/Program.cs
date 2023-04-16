namespace Ares;

static class Program{
    [STAThread]
    static void Main(string[] args){
        ApplicationConfiguration.Initialize();

        string meta = "";

        if(args.Length == 0){
            if(!File.Exists("meta.txt")) File.Create("meta.txt");
            meta = File.ReadAllText("meta.txt").Replace(" ", "/;/");
        }else{
            meta = string.Join("/;/", args);
        }

        new CheckAresC().Init(meta+"");
    }
}//this.Invoke((MethodInvoker)(() => {}));