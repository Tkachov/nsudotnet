using System;
using Tkachev.Nsudotnet.TicTacToe.model;
using Tkachev.Nsudotnet.TicTacToe.view;

namespace Tkachev.Nsudotnet.TicTacToe {
	class Program {
		static void Main() {
			PrintIntro();

			while(true) {
				Game game = new Game();
				while(game.Winner == CellType.EMPTY) {
					PrintField(game);
					MakeAMove(game);
				}
				ShowWinner(game);
				if(!PlayAgain()) break;
			}
		}

		private static void PrintIntro() {
			Console.WriteLine("Tic-Tac-Toe");
			Console.WriteLine("-----------");
			Console.WriteLine();
			Console.WriteLine("This is a modified Tic-Tac-Toe game. Before you make your first move,");
			Console.WriteLine("you should select a field. After that just make moves in shown field.");
			Console.WriteLine("To make a move, you must type in row and then column where you want to");
			Console.WriteLine("place your mark ('X' or 'O'). All available moves are shown under the field.");
			Console.WriteLine();
			Console.WriteLine("Enjoy the game!");
			Console.WriteLine();
		}

		private static readonly GamePrinter Printer = new GamePrinter();
		private static void PrintField(Game game) {
			Printer.Print(game);
		}

		private static int GetField(Game game) {
			Console.WriteLine();

			int fieldIndex = Game.CAN_MAKE_MOVE_AT_ANY_CELL;
			while(true) {
				Console.WriteLine("Select one of these fields (no parentesis required):");
				for(int i = 0; i<Game.ROWS*Game.COLS; ++i) {
					if(!game[i].IsFull())
						Console.Write(" (" + (i/Game.COLS)+" "+(i%Game.COLS)+")");
					else
						Console.Write("      ");
					if(i%Game.COLS == Game.COLS-1)
						Console.WriteLine();
				}
				Console.WriteLine();

				String[] parts = Console.ReadLine().Split(' ');
				if(parts.Length < 2)
					continue;
				int fieldRow = int.Parse(parts[0]) % Game.ROWS;
				int fieldCol = int.Parse(parts[1]) % Game.COLS;
				fieldIndex = fieldCol + fieldRow*Game.COLS;
				if(!game[fieldIndex].IsFull())
					break;
			}

			return fieldIndex;
		}

		private static void GetCellPosition(Game game, int fieldIndex, ref int cellRow, ref int cellCol) {
			Console.WriteLine();
			Console.WriteLine("You're going to move in field (" + (fieldIndex/Game.COLS)+" "+(fieldIndex%Game.COLS) + ") with '" + (game.XMove ? 'X' : 'O') +  "'.");

			while(true) {
				for(int i = 0; i<Game.ROWS*Game.COLS; ++i) {
					if(game[fieldIndex][i/Game.COLS, i%Game.COLS] == CellType.EMPTY)
						Console.Write(" (" + (i/Game.COLS)+" "+(i%Game.COLS)+")");
					else
						Console.Write("      ");
					if(i%Game.COLS == Game.COLS-1)
						Console.WriteLine();
				}
				Console.WriteLine();

				String[] parts = Console.ReadLine().Split(' ');
				if(parts.Length < 2)
					continue;
				cellRow = int.Parse(parts[0]) % Game.ROWS;
				cellCol = int.Parse(parts[1]) % Game.COLS;
				if(game[fieldIndex][cellRow, cellCol] == CellType.EMPTY)
					break;
			}		
		}

		private static void MakeAMove(Game game) {
			int fieldIndex = game.CurrentField;
			if(fieldIndex == Game.CAN_MAKE_MOVE_AT_ANY_CELL)
				fieldIndex = GetField(game);

			int cellRow = 0, cellCol = 0;
			GetCellPosition(game, fieldIndex, ref cellRow, ref cellCol);			
			game.MakeAMove(fieldIndex, cellCol + cellRow*Game.COLS);
			Console.WriteLine();
		}

		private static void ShowWinner(Game game) {
			PrintField(game);
			Console.WriteLine("");
			Console.WriteLine("The game is over and '" + (game.Winner==CellType.O_MOVE?'O':'X') + "' is the winner!");
		}

		private static bool PlayAgain() {
			Console.WriteLine("");
			Console.WriteLine("Do you want to play again? (anything but 'no' is treated as 'yes')");
			String[] parts = Console.ReadLine().Split(' ');
			if(parts.Length > 0 && parts[0] == "no")
				return false;
			return true;
		}
	}
}
