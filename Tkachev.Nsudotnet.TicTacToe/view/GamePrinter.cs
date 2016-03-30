using System;
using System.Text;

namespace Tkachev.Nsudotnet.TicTacToe.view {
	class GamePrinter {
		public void print(ref Game game) {
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

			StringBuilder separator = new StringBuilder("+");
			StringBuilder cellSeparator = new StringBuilder("");
			cellSeparator.Append(new String('-', Game.COLS));
			cellSeparator.Append("+");
			for(int i = 0; i<Game.COLS; ++i)
				separator.Append(cellSeparator);

			String smallSeparator = '+' + new String('-', Game.COLS) + '+';

			String[] fieldLines = new String[Game.ROWS*(1+Game.ROWS)+1];
			String[] infoLines = new String[2*(Game.ROWS+3)+1]; //2 fields (header, top line, rows, bottom line) + one empty line in between

			int fieldLinesIndex = 0;
			for(int i = 0; i<Game.ROWS; ++i) {
				fieldLines[fieldLinesIndex++] = separator.ToString();
				for(int j = 0; j<Game.ROWS; ++j) {
					fieldLines[fieldLinesIndex++] = getLine(ref game, i, j);
				}
			}
			fieldLines[fieldLinesIndex++] = separator.ToString();

			int infoLinesIndex = 0;
			infoLines[infoLinesIndex++] = "Total result:";
			infoLines[infoLinesIndex++] = smallSeparator;
			for(int j = 0; j<Game.ROWS; ++j) {
				infoLines[infoLinesIndex++] = getSmallLine(ref game, j);
			}
			infoLines[infoLinesIndex++] = smallSeparator;
			infoLines[infoLinesIndex++] = "";

			int fieldIndex = game.getCurrentField();
			if(fieldIndex != Game.CAN_MAKE_MOVE_AT_ANY_CELL) {
				infoLines[infoLinesIndex++] = "Current field:";
				infoLines[infoLinesIndex++] = smallSeparator;
				int fieldRow = fieldIndex/Game.COLS;
				int fieldCol = fieldIndex%Game.COLS;
				for(int j = 0; j<Game.ROWS; ++j) {
					infoLines[infoLinesIndex++] = getSmallLineOfField(ref game, fieldRow, fieldCol, j);
				}
				infoLines[infoLinesIndex++] = smallSeparator;
			} else {
				for(int j = 0; j<Game.ROWS+3; ++j)
					infoLines[infoLinesIndex++] = "";
			}

			int l1 = fieldLines.Length;
			int l2 = infoLines.Length;
			int l = (l1<l2 ? l2 : l1);
			int defaultFieldLinesLength = 13;
			if(l1>0)
				defaultFieldLinesLength = fieldLines[0].Length;
			for(int i = 0; i<l; ++i) {
				if(i<l1 && i<l2)
					Console.WriteLine(fieldLines[i]+"     "+infoLines[i]);
				else if(i<l1)
					Console.WriteLine(fieldLines[i]);
				else
					Console.WriteLine(new String(' ', defaultFieldLinesLength)+"     "+infoLines[i]);
			}
		}

		private char printingChar(CellType type) {
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

		private String getLine(ref Game game, int row, int innerRow) {
			StringBuilder line = new StringBuilder("|");
			for(int col = 0; col<Game.COLS; ++col) {
				for(int innerCol = 0; innerCol<Game.COLS; ++innerCol)
					line.Append(printingChar(game.getFieldByPosition(row, col).getCellByPosition(innerRow, innerCol)));
				line.Append('|');
			}
			return line.ToString();
		}

		private String getSmallLine(ref Game game, int row) {
			StringBuilder line = new StringBuilder("|");			
			for(int innerCol = 0; innerCol<Game.COLS; ++innerCol)
				line.Append(printingChar(game.getFieldByPosition(row, innerCol).getWinner()));
			line.Append('|');			
			return line.ToString();
		}

		private String getSmallLineOfField(ref Game game, int row, int col, int innerRow) {
			StringBuilder line = new StringBuilder("|");
			for(int innerCol = 0; innerCol<Game.COLS; ++innerCol)
				line.Append(printingChar(game.getFieldByPosition(row, col).getCellByPosition(innerRow, innerCol)));
			line.Append('|');
			return line.ToString();
		}
	}
}
