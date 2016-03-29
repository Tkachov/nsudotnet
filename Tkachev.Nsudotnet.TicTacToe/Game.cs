using System;

namespace Tkachev.Nsudotnet.TicTacToe {
	class Game {
		private const int ROWS = 3;
		private const int COLS = 3;
		private const int CAN_MAKE_MOVE_AT_ANY_CELL = -1;

		private Field[] fields = new Field[ROWS*COLS];
		private Field bigCellsResults = new Field();

		private bool X_move = true;
		private int lastMoveIndex = CAN_MAKE_MOVE_AT_ANY_CELL;
		private CellType winner = CellType.EMPTY;

		public Game() {
			for(int i = 0; i<ROWS*COLS; ++i)
				fields[i] = new Field();
		}

		public CellType getWinner() { return winner; }

		public char printingChar(CellType type) {
			switch(type) {
				case CellType.EMPTY: return ' ';
				case CellType.O_MOVE: return 'O';
				case CellType.X_MOVE: return 'X';
			}
			return '-';
		}

		public void print3x3() {
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

			Console.WriteLine("+---+---+---+     Total result:");

			Console.WriteLine(String.Format("|{0}{1}{2}|{3}{4}{5}|{6}{7}{8}|     +---+",

							printingChar(fields[0].getCellByPosition(0, 0)),
							printingChar(fields[0].getCellByPosition(0, 1)),
							printingChar(fields[0].getCellByPosition(0, 2)),

							printingChar(fields[0+1].getCellByPosition(0, 0)),
							printingChar(fields[0+1].getCellByPosition(0, 1)),
							printingChar(fields[0+1].getCellByPosition(0, 2)),

							printingChar(fields[0+2].getCellByPosition(0, 0)),
							printingChar(fields[0+2].getCellByPosition(0, 1)),
							printingChar(fields[0+2].getCellByPosition(0, 2))
						));

			Console.WriteLine(String.Format("|{0}{1}{2}|{3}{4}{5}|{6}{7}{8}|     |{9}{10}{11}|",

							printingChar(fields[0].getCellByPosition(1, 0)),
							printingChar(fields[0].getCellByPosition(1, 1)),
							printingChar(fields[0].getCellByPosition(1, 2)),

							printingChar(fields[0+1].getCellByPosition(1, 0)),
							printingChar(fields[0+1].getCellByPosition(1, 1)),
							printingChar(fields[0+1].getCellByPosition(1, 2)),

							printingChar(fields[0+2].getCellByPosition(1, 0)),
							printingChar(fields[0+2].getCellByPosition(1, 1)),
							printingChar(fields[0+2].getCellByPosition(1, 2)),

							printingChar(bigCellsResults.getCellByPosition(0, 0)),
							printingChar(bigCellsResults.getCellByPosition(0, 1)),
							printingChar(bigCellsResults.getCellByPosition(0, 2))
						));

			Console.WriteLine(String.Format("|{0}{1}{2}|{3}{4}{5}|{6}{7}{8}|     |{9}{10}{11}|",

							printingChar(fields[0].getCellByPosition(2, 0)),
							printingChar(fields[0].getCellByPosition(2, 1)),
							printingChar(fields[0].getCellByPosition(2, 2)),

							printingChar(fields[0+1].getCellByPosition(2, 0)),
							printingChar(fields[0+1].getCellByPosition(2, 1)),
							printingChar(fields[0+1].getCellByPosition(2, 2)),

							printingChar(fields[0+2].getCellByPosition(2, 0)),
							printingChar(fields[0+2].getCellByPosition(2, 1)),
							printingChar(fields[0+2].getCellByPosition(2, 2)),

							printingChar(bigCellsResults.getCellByPosition(1, 0)),
							printingChar(bigCellsResults.getCellByPosition(1, 1)),
							printingChar(bigCellsResults.getCellByPosition(1, 2))
						));

			Console.WriteLine(String.Format("+---+---+---+     |{0}{1}{2}|",
							printingChar(bigCellsResults.getCellByPosition(2, 0)),
							printingChar(bigCellsResults.getCellByPosition(2, 1)),
							printingChar(bigCellsResults.getCellByPosition(2, 2))
						));

			Console.WriteLine(String.Format("|{0}{1}{2}|{3}{4}{5}|{6}{7}{8}|     +---+",

							printingChar(fields[3].getCellByPosition(0, 0)),
							printingChar(fields[3].getCellByPosition(0, 1)),
							printingChar(fields[3].getCellByPosition(0, 2)),

							printingChar(fields[3+1].getCellByPosition(0, 0)),
							printingChar(fields[3+1].getCellByPosition(0, 1)),
							printingChar(fields[3+1].getCellByPosition(0, 2)),

							printingChar(fields[3+2].getCellByPosition(0, 0)),
							printingChar(fields[3+2].getCellByPosition(0, 1)),
							printingChar(fields[3+2].getCellByPosition(0, 2))
						));

			Console.WriteLine(String.Format("|{0}{1}{2}|{3}{4}{5}|{6}{7}{8}|",

							printingChar(fields[3].getCellByPosition(1, 0)),
							printingChar(fields[3].getCellByPosition(1, 1)),
							printingChar(fields[3].getCellByPosition(1, 2)),

							printingChar(fields[3+1].getCellByPosition(1, 0)),
							printingChar(fields[3+1].getCellByPosition(1, 1)),
							printingChar(fields[3+1].getCellByPosition(1, 2)),

							printingChar(fields[3+2].getCellByPosition(1, 0)),
							printingChar(fields[3+2].getCellByPosition(1, 1)),
							printingChar(fields[3+2].getCellByPosition(1, 2))
						));

			if(lastMoveIndex != CAN_MAKE_MOVE_AT_ANY_CELL) {
				Console.WriteLine(String.Format("|{0}{1}{2}|{3}{4}{5}|{6}{7}{8}|     Current field:",

								printingChar(fields[3].getCellByPosition(2, 0)),
								printingChar(fields[3].getCellByPosition(2, 1)),
								printingChar(fields[3].getCellByPosition(2, 2)),

								printingChar(fields[3+1].getCellByPosition(2, 0)),
								printingChar(fields[3+1].getCellByPosition(2, 1)),
								printingChar(fields[3+1].getCellByPosition(2, 2)),

								printingChar(fields[3+2].getCellByPosition(2, 0)),
								printingChar(fields[3+2].getCellByPosition(2, 1)),
								printingChar(fields[3+2].getCellByPosition(2, 2))
							));
				Console.WriteLine("+---+---+---+     +---+");
				for(int r = 0; r<ROWS; ++r)
					Console.WriteLine(String.Format("|{0}{1}{2}|{3}{4}{5}|{6}{7}{8}|     |{9}{10}{11}|",

								printingChar(fields[6].getCellByPosition(r, 0)),
								printingChar(fields[6].getCellByPosition(r, 1)),
								printingChar(fields[6].getCellByPosition(r, 2)),

								printingChar(fields[6+1].getCellByPosition(r, 0)),
								printingChar(fields[6+1].getCellByPosition(r, 1)),
								printingChar(fields[6+1].getCellByPosition(r, 2)),

								printingChar(fields[6+2].getCellByPosition(r, 0)),
								printingChar(fields[6+2].getCellByPosition(r, 1)),
								printingChar(fields[6+2].getCellByPosition(r, 2)),

								printingChar(fields[lastMoveIndex].getCellByPosition(r, 0)),
								printingChar(fields[lastMoveIndex].getCellByPosition(r, 1)),
								printingChar(fields[lastMoveIndex].getCellByPosition(r, 2))
							));
				Console.WriteLine("+---+---+---+     +---+");
			} else {
				Console.WriteLine(String.Format("|{0}{1}{2}|{3}{4}{5}|{6}{7}{8}|",

								printingChar(fields[3].getCellByPosition(2, 0)),
								printingChar(fields[3].getCellByPosition(2, 1)),
								printingChar(fields[3].getCellByPosition(2, 2)),

								printingChar(fields[3+1].getCellByPosition(2, 0)),
								printingChar(fields[3+1].getCellByPosition(2, 1)),
								printingChar(fields[3+1].getCellByPosition(2, 2)),

								printingChar(fields[3+2].getCellByPosition(2, 0)),
								printingChar(fields[3+2].getCellByPosition(2, 1)),
								printingChar(fields[3+2].getCellByPosition(2, 2))
							));
				Console.WriteLine("+---+---+---+");
				for(int r = 0; r<ROWS; ++r)
					Console.WriteLine(String.Format("|{0}{1}{2}|{3}{4}{5}|{6}{7}{8}|",

								printingChar(fields[6].getCellByPosition(r, 0)),
								printingChar(fields[6].getCellByPosition(r, 1)),
								printingChar(fields[6].getCellByPosition(r, 2)),

								printingChar(fields[6+1].getCellByPosition(r, 0)),
								printingChar(fields[6+1].getCellByPosition(r, 1)),
								printingChar(fields[6+1].getCellByPosition(r, 2)),

								printingChar(fields[6+2].getCellByPosition(r, 0)),
								printingChar(fields[6+2].getCellByPosition(r, 1)),
								printingChar(fields[6+2].getCellByPosition(r, 2))
							));
				Console.WriteLine("+---+---+---+");
			}
		}

		public void printFieldAndAskToMove() {
			//TODO: print any field
			print3x3();
			
			if(lastMoveIndex != CAN_MAKE_MOVE_AT_ANY_CELL) {
				if(fields[lastMoveIndex].isFull())
					lastMoveIndex = CAN_MAKE_MOVE_AT_ANY_CELL;
			}

			int fieldIndex = lastMoveIndex;

			if(lastMoveIndex == CAN_MAKE_MOVE_AT_ANY_CELL) {
				Console.WriteLine();
				
				while(true) {
					Console.WriteLine("Select one of these fields (no parentesis required):");
					for(int i = 0; i<ROWS*COLS; ++i) {
						if(!fields[i].isFull())
							Console.Write(" (" + (i/COLS)+" "+(i%COLS)+")");
						else
							Console.Write("      ");
						if(i%COLS == COLS-1)
							Console.WriteLine();
					}
					Console.WriteLine();

					String[] parts = Console.ReadLine().Split(' ');
					if(parts.Length < 2)
						continue;
					int fieldRow = int.Parse(parts[0]) % ROWS;
					int fieldCol = int.Parse(parts[1]) % COLS;
					fieldIndex = fieldCol + fieldRow*COLS;
					if(!fields[fieldIndex].isFull())
						break;
                }
			}

			int cellRow, cellCol;

			Console.WriteLine();
			Console.WriteLine("You're going to move in field (" + (fieldIndex/COLS)+" "+(fieldIndex%COLS) + ") with '" + (X_move?'X':'O') +  "'.");

			while(true) {
				for(int i = 0; i<ROWS*COLS; ++i) {
					if(fields[fieldIndex].getCellByPosition(i/COLS, i%COLS) == CellType.EMPTY)
						Console.Write(" (" + (i/COLS)+" "+(i%COLS)+")");
					else
						Console.Write("      ");
					if(i%COLS == COLS-1)
						Console.WriteLine();
				}
				Console.WriteLine();

				String[] parts = Console.ReadLine().Split(' ');
				if(parts.Length < 2)
					continue;
				cellRow = int.Parse(parts[0]) % ROWS;
				cellCol = int.Parse(parts[1]) % COLS;
				if(fields[fieldIndex].getCellByPosition(cellRow, cellCol) == CellType.EMPTY)
					break;
			}

			makeAMove(fieldIndex, cellCol + cellRow*COLS);
			Console.WriteLine();
		}

		public void makeAMove(int fieldIndex, int cellIndex) {
			fields[fieldIndex].setCell(cellIndex, (X_move ? CellType.X_MOVE : CellType.O_MOVE));
			bigCellsResults.setCell(fieldIndex, fields[fieldIndex].getWinner());
			lastMoveIndex = cellIndex;
			checkWins();
			X_move = !X_move;
		}

		private void checkWins() {
			winner = bigCellsResults.getWinner();			
		}
	}
}
