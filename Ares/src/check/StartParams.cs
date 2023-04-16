namespace Ares;

public class StartParamsC{
    public static bool end = true;
    public static bool Start(string[] args){
        if(args.Length == 0) return true;
        end = true;
        List<StartParamsObjC> objs = new List<StartParamsObjC>();

        objs.Add(new StartParamsObjC(new string[]{"exit", "-exit"},() => {}, "Exit."));

        objs.Add(new StartParamsObjC(new string[]{"-update"}, () => {
            UpdateC.UpdateAres(false);
        }, "Update Ares (not window)"));

        objs.Add(new StartParamsObjC(new string[]{"-v"}, () => {
            MessageBox.Show("Version: "+ConfigData.tWersja);
        }, "Display version"));

        objs.Add(new StartParamsObjC(new string[]{"-su"}, () => {
            UpdateC.CheckInstallAndUpdate();
        }, "Check install and update Ares"));

        objs.Add(new StartParamsObjC(new string[]{"-installForm"}, () => {
            InstallC.InstallForm();
        }, "Display install Form window."));

        objs.Add(new StartParamsObjC(new string[]{"-inAres"}, () => {
            if(RamControlC.ControlR()) return;
            System.Threading.Thread t1 = new System.Threading.Thread(() => {
                Application.Run(new InAresC());
            });
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
            t1.Join();
        }, "Display inAres window."));

        objs.Add(new StartParamsObjC(new string[]{"-u"}, () => {
            if(args.Length < 2){
                end = true;
                return;
            }
            if(args.Length == 2)
                ModuleC.loadModule(args[1], args[2]);
            else if(args.Length > 2)
                ModuleC.loadModule(args[1], args[2], string.Join("/;/", args.Skip(3).ToArray()));
        }, "Run dll in ares format (params: path, namescape.class, optional: plus params)"));

        objs.Add(new StartParamsObjC(new string[]{"-n"}, () => {
            if(args.Length < 2){
                end = true;
                return;
            }
            string moduleName = args[1];

            string mnm = ModuleC.remName(moduleName);
            if(mnm == ":err"){
                string[] modules = ModuleC.GetModules();
                string modLower = moduleName.ToLower();
                for(int i=0; i<modules.Length; i++){
                    if(modules[i].ToLower().IndexOf(modLower) > -1){
                        moduleName = modules[i];
                        break;
                    }
                }
            }
            mnm = ModuleC.remName(moduleName);
            if(mnm == ":err"){
                ErrorC.InfDev(":err", "module Start");
                end = true;
                return;
            }
            if(args.Length == 1)
                ModuleC.loadModule(
                    "./modules/"+moduleName+".ares",
                    "Ares."+mnm
                );
            else if(args.Length > 1)
                ModuleC.loadModule(
                    "./modules/"+moduleName+".ares",
                    "Ares."+mnm,
                    string.Join("/;/", args.Skip(3).ToArray())
                );
        }, "Run modules (params: name, optional: plus params)."));

        objs.Add(new StartParamsObjC(new string[]{"-add_.ares_to_reg"}, () => {
            UpdateC.CheckInstallAndUpdate(); 
            ProC.Init();
            if(!ProC.Zalogowano){
                MessageBox.Show("Aby dodać roszerzenie wymagane jest wersja Pro.");
                end = true;
                return;
            }
            CheckAresC.CreateAresExtension();
            DialogResult dialogResult = MessageBox.Show(
                "Czy uruchomić ponownie teraz?", "Teraz??",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question
            );
            if(dialogResult == DialogResult.Yes){
                System.Diagnostics.Process.Start("powershell", "shutdown /r");
            }
        }, "Add .ares extension to reg."));

        objs.Add(new StartParamsObjC(new string[]{"-h", "help", "-help" ,"--help", "/?"}, () => {
            System.Threading.Thread t1 = new System.Threading.Thread(() => {
                Application.Run(new HelpDisplayFormC(objs));
            });
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
            t1.Join();
        }, "Display help."));
        
        foreach(StartParamsObjC obj in objs){
            foreach(string s in obj.cas){
                if(s == args[0]){
                    end = false;
                    obj.act();
                    goto exit;
                    break;
                }
            }
        }
        exit:
        return end;
    }

    private class StartParamsObjC{
        public string[] cas = new string[0];
        public Action act = () => {};
        public string description = "";

        public StartParamsObjC(string[] s, Action a, string des=""){
            this.cas = s;
            this.act = a;
            this.description = (des == "" ? "Brak opisu." : des);
        }
    }

    private class HelpDisplayFormC : Form{
        private System.ComponentModel.IContainer components = null;
        public HelpDisplayFormC(List<StartParamsObjC> objs){
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800 + 60, 500);
            this.Icon = new System.Drawing.Icon("logo.ico");
            this.Text = "Ares - help";

            DataGridView dataGridView1 = new DataGridView();
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.ColumnCount = 2;
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns[0].Name = "Param";
            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[1].Name = "Descriptions";
            dataGridView1.Columns[1].Width = 600;

            foreach(StartParamsObjC obj in objs){
                string arr = string.Join(", ", obj.cas);
                dataGridView1.Rows.Add(arr, obj.description);
            }

            this.Controls.Add(dataGridView1);
        }
    }
}