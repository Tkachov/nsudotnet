using System;
using System.IO;

namespace cloc {
	class Program {		
		private static int count_file_lines(string filename) {
			int loc = 0;
			bool block_comment = false;

			StreamReader reader = File.OpenText(filename);
			string line;
			while((line = reader.ReadLine()) != null) {
				line = line.Trim();
				char previous = '\0';
				int non_whitespace_chars_count = 0;
				for(int i=0; i<line.Length; ++i) {
					switch(line[i]) {
						case '/':
							if(previous == '/' && !block_comment) { //line comment
								i = line.Length-1;
								break;
							}

							if(block_comment && previous == '*') {
								block_comment = false;
								previous = '\0'; //for "*//*" case
								continue;
							}						
						break;

						case '*':
							if(!block_comment && previous == '/') {
								block_comment = true;
								previous = '\0'; //for "/*/" case
								continue;
							}
						break;

						default:
							if(!block_comment && !string.IsNullOrWhiteSpace(line[i]+"")) ++non_whitespace_chars_count;
						break;
					}
					previous = line[i];
				}

				if(non_whitespace_chars_count > 0) ++loc;
			}

			return loc;
		}

		static void Main(string[] args) {
			bool verbose = false;
			bool help = false;
			string mask = null;
			for(int i = 0; i<args.Length; ++i) {
				if(args[i] == "--verbose" || args[i] == "-v")
					verbose = true;
				else if(args[i] == "--help" || args[i] == "-h" || args[i] == "/?")
					help = true;
				else {
					//we believe this is our "mask" argument
					mask = args[i];
				}
			}

			help = (help || mask==null);

			if(help) {
				Console.WriteLine("Yet Another CLOC");
				Console.WriteLine("Counts lines of code. Supports // and /**/, skips empty lines.");
				Console.WriteLine("Sample:");
				Console.WriteLine("\tcloc path/start_*.cpp");
				Console.WriteLine();
				Console.WriteLine("Supported flags:");
				Console.WriteLine("\t--help (-h, /?) - shows this help message;");
				Console.WriteLine("\t--verbose (-v) - shows each file name and its CLOC.");
			}

			if(mask == null)
				return;

			string path, filemask;
			int index = mask.LastIndexOf('/');
			if(index == -1)
				index = mask.LastIndexOf('\\');
			if(index == -1) {
				path = "";
				filemask = mask;
			} else {
				path = mask.Substring(0, index);
				filemask = mask.Substring(index+1);
			}

			if(path == "") path = Directory.GetCurrentDirectory();

			string[] files = Directory.GetFiles(path, filemask, SearchOption.AllDirectories);
			int total = 0;
			foreach(string filename in files) {
				int loc = count_file_lines(filename);
				if(verbose)
					Console.WriteLine(loc + " lines in " + filename);
				total += loc;
			}

			if(verbose)
				Console.WriteLine("--------");
			Console.WriteLine(total + " lines in " + files.Length + " files");

			return;
		}
	}
}
