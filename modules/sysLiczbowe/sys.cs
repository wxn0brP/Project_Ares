namespace Ares;
using System;

public partial class sysLiczbowe : Form{
	public void Init(string plusParams, string config){
        try{
            if(plusParams == "") throw new Exception("uruchom sys");
			string[] args = plusParams.Split("/;/");
			
			if(args.Length == 0) throw new Exception("ilosc arg");

            if(args[0] == "help" || args[0] == "/?"){
                MessageBox.Show("pod(2-36) pod(2-36) liczba");
                return;
            }

            if(args.Length < 3) throw new Exception("ilosc arg");

            string[] odp = convertN(int.Parse(args[0]), int.Parse(args[1]), args[2]);
            MessageBox.Show("16: "+odp[0]+"\n10: "+odp[1]+"\n8:   "+odp[2]+"\n2:   "+odp[3]+"\n"+args[1]+":  "+odp[4]);
        }catch{
		    Application.Run(this);
        }
	}

	public sysLiczbowe(){
		InitializeComponent();
		this.inTB = this.out10TB;
		this.out10TB.Select();
		changeOnButtons(out10TB);
	}

	public void convert(int sys, string wartosc){
        int my = 36;
        try{
            my = int.Parse(this.inMyTB.Text);
        }catch{
            this.inMyTB.Text = my+"";
        }

		try{
			string[] o = convertN(sys, my, wartosc);
            this.out16TB.Text = o[0];
			this.out10TB.Text = o[1];
			this.out8TB.Text = o[2];
			this.out2TB.Text = o[3];
			this.outMyTB.Text = o[4];
		}catch{
			this.out10TB.Text = 
			this.out16TB.Text =
			this.out8TB.Text =
			this.out2TB.Text =
			this.outMyTB.Text =
			"0";
		}
	}

	public string[] convertN(int z, int pod, string wartosc){
        string[] ret = new string[]{"0", "0", "0", "0", "0"};
		long imput = 0;
        if(wartosc == null || wartosc == "") imput = 0;
        else
        imput = ArbitraryToDecimalSystem(wartosc, z);
        ret[0] = DecimalToArbitrarySystem(imput, 16);
        ret[1] = DecimalToArbitrarySystem(imput, 10);
        ret[2] = DecimalToArbitrarySystem(imput, 8);
        ret[3] = DecimalToArbitrarySystem(imput, 2);
        ret[4] = DecimalToArbitrarySystem(imput, pod);
        return ret;
	}

	public static string DecimalToArbitrarySystem(long decimalNumber, int radix){
		const int BitsInLong = 64;
		const string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		if(radix == 0) radix = Digits.Length;
		if(radix < 2 || radix > Digits.Length)
			throw new ArgumentException("The radix must be >= 2 and <= " + Digits.Length.ToString());

		if(decimalNumber == 0)
			return "0";

		int index = BitsInLong - 1;
		long currentNumber = Math.Abs(decimalNumber);
		char[] charArray = new char[BitsInLong];

		while(currentNumber != 0){
			int remainder = (int)(currentNumber % radix);
			charArray[index--] = Digits[remainder];
			currentNumber = currentNumber / radix;
		}

		string result = new String(charArray, index + 1, BitsInLong - index - 1);
		if(decimalNumber < 0){
			result = "-" + result;
		}

		return result;
	}

    
    public static long ArbitraryToDecimalSystem(string number, int radix=0){
		const string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		if(radix == 0) radix = Digits.Length;
		if(radix < 2 || radix > Digits.Length)
			throw new ArgumentException("The radix must be >= 2 and <= " + Digits.Length.ToString());

		if(String.IsNullOrEmpty(number)) return 0;

		number = number.ToUpperInvariant();

		long result = 0;
		long multiplier = 1;
		for(int i = number.Length - 1; i >= 0; i--){
			char c = number[i];
			if(i == 0 && c == '-'){
				result = -result;
				break;
			}

			int digit = Digits.IndexOf(c);
			if(digit == -1)
				throw new ArgumentException("Invalid character in the arbitrary numeral system number", "number");

			result += digit * multiplier;
			multiplier *= radix;
		}

		return result;
	}

	TextBox inTB;

	private void ChangeCursor(object sender, EventArgs en){
		if(sender == null) return;
		this.inTB = (TextBox)sender;
		changeOnButtons((TextBox)sender);
	}

	public void changeOnButtons(TextBox textBox){
		if(textBox == this.out16TB || textBox == this.outMyTB || textBox == this.inMyTB){
			this.Button2.Enabled =
			this.Button3.Enabled =
			this.Button4.Enabled =
			this.Button5.Enabled =
			this.Button6.Enabled =
			this.Button7.Enabled =
			this.Button8.Enabled =
			this.Button9.Enabled =
			this.ButtonA.Enabled =
			this.ButtonB.Enabled =
			this.ButtonC.Enabled =
			this.ButtonD.Enabled =
			this.ButtonE.Enabled =
			this.ButtonF.Enabled =
			true;
		}else
		if(textBox == this.out10TB){
			this.Button2.Enabled =
			this.Button3.Enabled =
			this.Button4.Enabled =
			this.Button5.Enabled =
			this.Button6.Enabled =
			this.Button7.Enabled =
			this.Button8.Enabled =
			this.Button9.Enabled =
			true;
			this.ButtonA.Enabled =
			this.ButtonB.Enabled =
			this.ButtonC.Enabled =
			this.ButtonD.Enabled =
			this.ButtonE.Enabled =
			this.ButtonF.Enabled =
			false;
		}else
		if(textBox == this.out8TB){
			this.Button2.Enabled =
			this.Button3.Enabled =
			this.Button4.Enabled =
			this.Button5.Enabled =
			this.Button6.Enabled =
			this.Button7.Enabled =
			true;
			this.Button8.Enabled =
			this.Button9.Enabled =
			this.ButtonA.Enabled =
			this.ButtonB.Enabled =
			this.ButtonC.Enabled =
			this.ButtonD.Enabled =
			this.ButtonE.Enabled =
			this.ButtonF.Enabled =
			false;
		}else
		if(textBox == this.out2TB){
			this.Button2.Enabled =
			this.Button3.Enabled =
			this.Button4.Enabled =
			this.Button5.Enabled =
			this.Button6.Enabled =
			this.Button7.Enabled =
			this.Button8.Enabled =
			this.Button9.Enabled =
			this.ButtonA.Enabled =
			this.ButtonB.Enabled =
			this.ButtonC.Enabled =
			this.ButtonD.Enabled =
			this.ButtonE.Enabled =
			this.ButtonF.Enabled =
			false;
		}
	}




	private void Button1_Click(object sender, EventArgs en) => this.inTB.Text += "1";
	private void Button2_Click(object sender, EventArgs en) => this.inTB.Text += "2";
	private void Button3_Click(object sender, EventArgs en) => this.inTB.Text += "3";
	private void Button4_Click(object sender, EventArgs en) => this.inTB.Text += "4";
	private void Button5_Click(object sender, EventArgs en) => this.inTB.Text += "5";
	private void Button6_Click(object sender, EventArgs en) => this.inTB.Text += "6";
	private void Button7_Click(object sender, EventArgs en) => this.inTB.Text += "7";
	private void Button8_Click(object sender, EventArgs en) => this.inTB.Text += "8";
	private void Button9_Click(object sender, EventArgs en) => this.inTB.Text += "9";
	private void Button0_Click(object sender, EventArgs en) => this.inTB.Text += "0";
	private void ButtonA_Click(object sender, EventArgs en) => this.inTB.Text += "A";
	private void ButtonB_Click(object sender, EventArgs en) => this.inTB.Text += "B";
	private void ButtonC_Click(object sender, EventArgs en) => this.inTB.Text += "C";
	private void ButtonD_Click(object sender, EventArgs en) => this.inTB.Text += "D";
	private void ButtonE_Click(object sender, EventArgs en) => this.inTB.Text += "E";
	private void ButtonF_Click(object sender, EventArgs en) => this.inTB.Text += "F";
	private void Button_16_Click(object sender, EventArgs en) => convert(16, this.out16TB.Text);
	private void Button_10_Click(object sender, EventArgs en) => convert(10, this.out10TB.Text);
	private void Button_8_Click(object sender, EventArgs en) => convert(8, this.out8TB.Text);
	private void Button_2_Click(object sender, EventArgs en) => convert(2, this.out2TB.Text);
	private void Button_my_Click(object sender, EventArgs en){
        try{
            convert(int.Parse(this.inMyTB.Text.Trim()), this.outMyTB.Text);
        }catch{}
    }
}
