using System;
using System.IO;
using System.Security.Cryptography;

namespace Tkachev.Nsudotnet.Enigma {
	class Crypto {
		public static void Encrypt(SymmetricAlgorithm crypto, string inputFilename, string outputFilename) { //key is in "output_filename.key.txt"
			try {
				byte[] original = File.ReadAllBytes(inputFilename);				
				crypto.GenerateKey();
				crypto.GenerateIV();
				
				ICryptoTransform encryptor = crypto.CreateEncryptor();
				using(FileStream fs = new FileStream(outputFilename, FileMode.Create, FileAccess.Write)) {
					using(CryptoStream csEncrypt = new CryptoStream(fs, encryptor, CryptoStreamMode.Write)) {
						csEncrypt.Write(original, 0, original.Length);
						csEncrypt.FlushFinalBlock();
					}
				}

				if(outputFilename.Contains(".txt"))
					outputFilename = outputFilename.Replace(".txt", ".key.txt");
				else
					outputFilename = outputFilename + ".key.txt";

				using(StreamWriter writetext = new StreamWriter(outputFilename)) {
					writetext.WriteLine(Convert.ToBase64String(crypto.IV));
					writetext.WriteLine(Convert.ToBase64String(crypto.Key));
				}
			} catch(Exception e) {
				Console.WriteLine("Error: {0}", e.Message);
			}
		}

		public static void Decrypt(SymmetricAlgorithm crypto, string inputFilename, string keyFilename, string outputFilename) {
			try {
				byte[] input = File.ReadAllBytes(inputFilename);
				String ivBase64, keyBase64;
				using(var streamReader = new StreamReader(keyFilename)) {
					ivBase64 = streamReader.ReadLine();
					keyBase64 = streamReader.ReadLine();
				}
				
				crypto.Key = Convert.FromBase64String(keyBase64);
				crypto.IV = Convert.FromBase64String(ivBase64);

				ICryptoTransform decryptor = crypto.CreateDecryptor();
				using(MemoryStream msDecrypt = new MemoryStream(input)) {
					using(CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) {												
						using(FileStream fs = new FileStream(outputFilename, FileMode.Create, FileAccess.Write)) {
							csDecrypt.CopyTo(fs);
						}
					}
				}
			} catch(Exception e) {
				Console.WriteLine("Error: {0}", e.Message);
			}
		}
	}
}
