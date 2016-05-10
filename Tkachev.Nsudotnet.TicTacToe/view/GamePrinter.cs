using System;
using System.Text;
using Tkachev.Nsudotnet.TicTacToe.model;

namespace Tkachev.Nsudotnet.TicTacToe.view {
	class GamePrinter {
		public void Print(Game game) {
			/*
			+---+---+---+     Total result:
			|   |   |   |     +---+
			|   |   |   |     |   |
			|   |   |   |     |   |
			+---+---+---+     |   |
			|   |   |   |     +---+
			|   |   |   |
			|   |   |   |     Current field:
			+---+---+---+     +---+
			|   |   |   |     |   |
			|   |   |   |     |   |
			|   |   |   |     |   |
			+---+---+---+     +---+
			*/

			//you'd probably want me dead for this
			//but that's all for the art and UI

			string separator = "+";
			string cellSeparator = new string('-', Game.COLS) + '+';
			for(int i = 0; i<Game.COLS; ++i)
				separator += cellSeparator;

			string smallSeparator = '+' + new string('-', Game.COLS) + '+';			

			int l1 = Game.ROWS*(1+Game.ROWS)+1;
			int l2 = 2*(Game.ROWS+3)+1;

			int fieldIndex = game.CurrentField;
			int fieldRow = fieldIndex/Game.COLS;
			int fieldCol = fieldIndex%Game.COLS;
			if (fieldIndex == Game.CAN_MAKE_MOVE_AT_ANY_CELL) l2 = 3 + Game.ROWS;

			int l = (l1<l2 ? l2 : l1);
			for(int i = 0; i<l; ++i) {
				int fieldLineI = i/(Game.ROWS + 1), fieldLineJ = i%(Game.ROWS + 1);

				string fieldLine;
				fieldLine = fieldLineJ == 0 ? separator : GetLine(game, fieldLineI, fieldLineJ-1);

				string infoLine;
				if (i == 0)
					infoLine = "Total result:";
				else if(i == 1 || i == Game.ROWS+2)
					infoLine = smallSeparator;
				else if (i < Game.ROWS + 2)
					infoLine = GetSmallLine(game, i-2);
				else if (i < l2) {
					if (i == Game.ROWS + 3)
						infoLine = "";
					else if (i == Game.ROWS + 4)
						infoLine = "Current field:";
					else if (i == Game.ROWS + 5 || i == 2*Game.ROWS + 6)
						infoLine = smallSeparator;
					else 
						infoLine = GetSmallLineOfField(game, fieldRow, fieldCol, i - Game.ROWS - 6);					
				} else infoLine = "";

				if(i<l1 && i<l2)
					Console.WriteLine(fieldLine+"     "+infoLine);
				else if(i<l1)
					Console.WriteLine(fieldLine);
				else
					Console.WriteLine(new string(' ', separator.Length)+"     "+infoLine);
			}
		}

		private char PrintingChar(CellType type) {
			switch(type) {
				case CellType.EMPTY:
					return ' ';
				case CellType.O_MOVE:
					return 'O';
				case CellType.X_MOVE:
					return 'X';
			}
			return '-';
		}

		private String GetLine(Game game, int row, int innerRow) {
			StringBuilder line = new StringBuilder("|");
			for(int col = 0; col<Game.COLS; ++col) {
				for(int innerCol = 0; innerCol<Game.COLS; ++innerCol)
					line.Append(PrintingChar(game[row, col][innerRow, innerCol]));
				line.Append('|');
			}
			return line.ToString();
		}

		private String GetSmallLine(Game game, int row) {
			StringBuilder line = new StringBuilder("|");			
			for(int innerCol = 0; innerCol<Game.COLS; ++innerCol)
				line.Append(PrintingChar(game[row, innerCol].Winner));
			line.Append('|');			
			return line.ToString();
		}

		private String GetSmallLineOfField(Game game, int row, int col, int innerRow) {
			StringBuilder line = new StringBuilder("|");
			for(int innerCol = 0; innerCol<Game.COLS; ++innerCol)
				line.Append(PrintingChar(game[row, col][innerRow, innerCol]));
			line.Append('|');
			return line.ToString();
		}
	}
}
