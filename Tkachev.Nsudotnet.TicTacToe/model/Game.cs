namespace Tkachev.Nsudotnet.TicTacToe.model {
	class Game {
		public const int ROWS = 3;
		public const int COLS = 3;
		public const int N_IN_ROW_TO_WIN = 3;
		public const int CAN_MAKE_MOVE_AT_ANY_CELL = -1;

		private readonly Field[] _fields = new Field[ROWS*COLS];
		private readonly Field _bigCellsResults = new Field();

		private int _lastMoveIndex = CAN_MAKE_MOVE_AT_ANY_CELL;

		public Game() {
			for(int i = 0; i<ROWS*COLS; ++i)
				_fields[i] = new Field();
		}

		//getters		

		public Field this[int i] => _fields[i];

		public Field this[int row, int col] => _fields[(row%ROWS)*COLS + (col%COLS)];

		public CellType Winner { get; private set; } = CellType.EMPTY;

		public bool XMove { get; private set; } = true;

		public int CurrentField {
			get {
				if(_lastMoveIndex != CAN_MAKE_MOVE_AT_ANY_CELL) {
					if(_fields[_lastMoveIndex].IsFull())
						_lastMoveIndex = CAN_MAKE_MOVE_AT_ANY_CELL;
				}

				return _lastMoveIndex;
			}
		}

		//state changers

		public void MakeAMove(int fieldIndex, int cellIndex) {
			_fields[fieldIndex][cellIndex] = (XMove ? CellType.X_MOVE : CellType.O_MOVE);
			_bigCellsResults[fieldIndex] = _fields[fieldIndex].Winner;
			_lastMoveIndex = cellIndex;
			CheckWins();
			XMove = !XMove;
		}

		private void CheckWins() {
			Winner = _bigCellsResults.Winner;			
		}
	}
}
