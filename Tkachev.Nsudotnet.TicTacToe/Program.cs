using System;
using Tkachev.Nsudotnet.TicTacToe.view;

namespace Tkachev.Nsudotnet.TicTacToe {
	class Program {
		static void Main(string[] args) {
			printIntro();

			while(true) {
				Game game = new Game();				
				while(game.getWinner() == CellType.EMPTY) {
					printField(ref game);
					makeAMove(ref game);
				}
				showWinner(ref game);
				if(!playAgain()) break;
			}
		}

		private static void printIntro() {
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

		private static GamePrinter printer = new GamePrinter();
		private static void printField(ref Game game) {
			printer.print(ref game);
		}

		private static int getField(ref Game game) {
			Console.WriteLine();

			int fieldIndex = Game.CAN_MAKE_MOVE_AT_ANY_CELL;
			while(true) {
				Console.WriteLine("Select one of these fields (no parentesis required):");
				for(int i = 0; i<Game.ROWS*Game.COLS; ++i) {
					if(!game.getField(i).isFull())
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
				if(!game.getField(fieldIndex).isFull())
					break;
			}

			return fieldIndex;
		}

		private static void getCellPosition(ref Game game, int fieldIndex, ref int cellRow, ref int cellCol) {
			Console.WriteLine();
			Console.WriteLine("You're going to move in field (" + (fieldIndex/Game.COLS)+" "+(fieldIndex%Game.COLS) + ") with '" + (game.xMove() ? 'X' : 'O') +  "'.");

			while(true) {
				for(int i = 0; i<Game.ROWS*Game.COLS; ++i) {
					if(game.getField(fieldIndex).getCellByPosition(i/Game.COLS, i%Game.COLS) == CellType.EMPTY)
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
				if(game.getField(fieldIndex).getCellByPosition(cellRow, cellCol) == CellType.EMPTY)
					break;
			}		
		}

		private static void makeAMove(ref Game game) {
			int fieldIndex = game.getCurrentField();
			if(fieldIndex == Game.CAN_MAKE_MOVE_AT_ANY_CELL)
				fieldIndex = getField(ref game);

			int cellRow = 0, cellCol = 0;
			getCellPosition(ref game, fieldIndex, ref cellRow, ref cellCol);			
			game.makeAMove(fieldIndex, cellCol + cellRow*Game.COLS);
			Console.WriteLine();
		}

		private static void showWinner(ref Game game) {
			printField(ref game);
			Console.WriteLine("");
			Console.WriteLine("The game is over and '" + (game.getWinner()==CellType.O_MOVE?'O':'X') + "' is the winner!");
		}

		private static bool playAgain() {
			Console.WriteLine("");
			Console.WriteLine("Do you want to play again? (anything but 'no' is treated as 'yes')");
			String[] parts = Console.ReadLine().Split(' ');
			if(parts.Length > 0 && parts[0] == "no")
				return false;
			return true;
		}
	}
}
