namespace Ares;
using System.Threading;
using System.Diagnostics;

public class parot{
    public void Init(string pp){
        System.Threading.Thread t1 = new System.Threading.Thread(() => {
            Process.Start("curl", "parrot.live");
		});
		t1.SetApartmentState(ApartmentState.STA);
		t1.Start();
    }
}
