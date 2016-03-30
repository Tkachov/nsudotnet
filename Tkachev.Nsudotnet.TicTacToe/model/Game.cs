namespace Tkachev.Nsudotnet.TicTacToe {
	class Game {
		public const int ROWS = 3;
		public const int COLS = 3;
		public const int N_IN_ROW_TO_WIN = 3;
		public const int CAN_MAKE_MOVE_AT_ANY_CELL = -1;

		private Field[] fields = new Field[ROWS*COLS];
		private Field bigCellsResults = new Field();

		private bool X_move = true;
		private int lastMoveIndex = CAN_MAKE_MOVE_AT_ANY_CELL;
		private CellType winner = CellType.EMPTY;

		public Game() {
			for(int i = 0; i<ROWS*COLS; ++i)
				fields[i] = new Field();
		}

		//getters

		public Field getField(int index) { return fields[index]; }

		public Field getFieldByPosition(int row, int col) { return fields[(row%ROWS)*COLS + (col%COLS)]; }

		public CellType getWinner() { return winner; }

		public bool xMove() { return X_move;  }

		public int getCurrentField() {
			if(lastMoveIndex != CAN_MAKE_MOVE_AT_ANY_CELL) {
				if(fields[lastMoveIndex].isFull())
					lastMoveIndex = CAN_MAKE_MOVE_AT_ANY_CELL;
			}

			return lastMoveIndex;
		}

		//state changers

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
