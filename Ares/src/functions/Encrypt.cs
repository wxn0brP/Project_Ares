namespace Ares;

public class EncryptC{
    public static string XORCipher(string data, string key){
		int dataLen = data.Length;
		int keyLen = key.Length;
		char[] output = new char[dataLen];

		for(int i = 0; i < dataLen; ++i){
			output[i] = (char)(data[i] ^ key[i % keyLen]);
		}
		return new string(output);
	}

}