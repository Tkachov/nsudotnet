using System;
using System.IO;
using System.Security.Cryptography;

namespace Tkachev.Nsudotnet.Enigma {
	class Crypto {
		public static void encrypt(SymmetricAlgorithm crypto, string input_filename, string output_filename) { //key is in "output_filename.key.txt"
			try {
				byte[] original = File.ReadAllBytes(input_filename);				
				crypto.GenerateKey();
				crypto.GenerateIV();

				byte[] encrypted;
				ICryptoTransform encryptor = crypto.CreateEncryptor();
				using(MemoryStream msEncrypt = new MemoryStream()) {
					using(CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) {
						csEncrypt.Write(original, 0, original.Length);
						csEncrypt.FlushFinalBlock();
						encrypted = msEncrypt.ToArray();
					}
				}

				using(FileStream fs = new FileStream(output_filename, FileMode.Create, FileAccess.Write)) {
					fs.Write(encrypted, 0, encrypted.Length);
				}

				if(output_filename.Contains(".txt"))
					output_filename = output_filename.Replace(".txt", ".key.txt");
				else
					output_filename = output_filename + ".key.txt";

				using(StreamWriter writetext = new StreamWriter(output_filename)) {
					writetext.WriteLine(Convert.ToBase64String(crypto.IV));
					writetext.WriteLine(Convert.ToBase64String(crypto.Key));
				}
			} catch(Exception e) {
				Console.WriteLine("Error: {0}", e.Message);
			}
		}

		public static void decrypt(SymmetricAlgorithm crypto, string input_filename, string key_filename, string output_filename) {
			try {
				byte[] input = File.ReadAllBytes(input_filename);
				String IV_base64 = null, key_base64 = null;
				using(var streamReader = new StreamReader(key_filename)) {
					IV_base64 = streamReader.ReadLine();
					key_base64 = streamReader.ReadLine();
				}

				byte[] output;
				crypto.Key = Convert.FromBase64String(key_base64);
				crypto.IV = Convert.FromBase64String(IV_base64);

				ICryptoTransform decryptor = crypto.CreateDecryptor();
				using(MemoryStream msDecrypt = new MemoryStream(input)) {
					using(CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) {

						byte[] buffer = new byte[16*1024];
						using(MemoryStream ms = new MemoryStream()) {
							int read;
							while((read = csDecrypt.Read(buffer, 0, buffer.Length)) > 0) {
								ms.Write(buffer, 0, read);
							}
							output = ms.ToArray();
						}
					}
				}

				using(FileStream fs = new FileStream(output_filename, FileMode.Create, FileAccess.Write)) {
					fs.Write(output, 0, output.Length);
				}
			} catch(Exception e) {
				Console.WriteLine("Error: {0}", e.Message);
			}
		}
	}
}
