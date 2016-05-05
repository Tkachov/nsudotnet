using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Tkachev.Nsudotnet.Enigma {
	class Program {
		private static bool InvalidUsage(string[] args, Dictionary<String, SymmetricAlgorithm> simpleFactory) {
			if(args.Length < 3) {
				Console.WriteLine("Not enough arguments.");
				return true;
			}

			if(args[0] != "encrypt" && args[0] != "decrypt") {
				Console.WriteLine("Unknown mode passed.");
				return true;
			}

			if(args[0] == "encrypt" && args.Length != 4) {
				Console.WriteLine("Wrong arguments number for 'encrypt' mode.");
				return true;
			}

			if(args[0] == "decrypt" && args.Length != 5) {
				Console.WriteLine("Wrong arguments number for 'decrypt' mode.");
				return true;
			}

			if(!simpleFactory.ContainsKey(args[2])) {
				Console.WriteLine("Unknown algorithm passed.");
				return true;
			}

			return false;
		}

		static void Main(string[] args) {			
			Dictionary<String, SymmetricAlgorithm> simpleFactory = new Dictionary<String, SymmetricAlgorithm>();
			simpleFactory.Add("aes", new AesManaged());
			simpleFactory.Add("des", new DESCryptoServiceProvider());
			simpleFactory.Add("rc2", new RC2CryptoServiceProvider());
			simpleFactory.Add("rijndael", new RijndaelManaged());

			if(InvalidUsage(args, simpleFactory)) { //prints why usage is invalid
				Console.WriteLine();
				Console.WriteLine("Usage:");
				Console.WriteLine("\tenigma encrypt <input file> <algorithm> <output file>");
				Console.WriteLine("\tenigma decrypt <input file> <algorithm> <key file> <output file>");
				return;
			}

			if(args[0] == "encrypt") Crypto.Encrypt(simpleFactory[args[2]], args[1], args[3]);
			else Crypto.Decrypt(simpleFactory[args[2]], args[1], args[3], args[4]);
		}
	}
}
