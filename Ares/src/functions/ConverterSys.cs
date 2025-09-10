namespace Ares;

public class ConverterSys{
	public static string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public static string DecimalToArbitrarySystem(long decimalNumber, int radix=0){
		const int BitsInLong = 64;

		if(radix == 0) radix = Digits.Length;
		if(radix < 2 || radix > Digits.Length)
			throw new ArgumentException("The radix must be >= 2 and <= " + Digits.Length.ToString());

		if(decimalNumber == 0) return "0";

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

	public static decimal ArbitraryToDecimalSystem(string number, int radix=0){
		if(radix == 0) radix = Digits.Length;
		if(radix < 2 || radix > Digits.Length)
			throw new ArgumentException("The radix must be >= 2 and <= " + Digits.Length.ToString());

		if(String.IsNullOrEmpty(number)) return 0;

		number = number.ToUpperInvariant();

		decimal result = 0;
		decimal multiplier = 1;
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
}