namespace Tkachev.Nsudotnet.TicTacToe.model {
	class Field {
		private readonly CellType[] _cells = new CellType[Game.ROWS*Game.COLS];

		public Field() {
			for(int i = 0; i<Game.ROWS*Game.COLS; ++i)
				_cells[i] = CellType.EMPTY;
		}

		public CellType this[int i] {
			get { return _cells[(i%(Game.ROWS*Game.COLS))]; }
			set {
				_cells[(i%(Game.ROWS*Game.COLS))] = value;
				CheckWin();
			}
		}

		public CellType this[int row, int col] {
			get { return _cells[(row%Game.ROWS)*Game.COLS + (col%Game.COLS)]; }
			set {
				_cells[(row%Game.ROWS)*Game.COLS + (col%Game.COLS)] = value;
				CheckWin();
			}
		}

		public bool IsFull() {
			for(int i = 0; i<Game.ROWS*Game.COLS; ++i)
				if(_cells[i] == CellType.EMPTY)
					return false;
			return true;
		}

		public CellType Winner { get; private set; } = CellType.EMPTY;

		private void CheckWin() {
			if(Winner != CellType.EMPTY)
				return; //someone already won there

			Winner = FindWinner(this);
		}

		private static CellType FindWinnerMoving(Field field, int iterations, InitIteration init, Move mv) {
			for(int iteration = 0; iteration<iterations; ++iteration) {
				int row = 0, col = 0;
				init(iteration, ref row, ref col);

				CellType potentinalWinner = CellType.EMPTY;
				int inRow = 0;
				while(row>=0 && row<Game.ROWS && col>=0 && col<Game.COLS) {
					CellType cell = field[row, col];
					if(potentinalWinner != cell) {
						potentinalWinner = cell;
						inRow = 1;
					} else
						++inRow;

					if(inRow >= Game.N_IN_ROW_TO_WIN && potentinalWinner != CellType.EMPTY) {
						return potentinalWinner;
					}

					mv(ref row, ref col);
				}
			}

			return CellType.EMPTY;
		}

		private delegate void InitIteration(int iteration, ref int row, ref int col);
		private delegate void Move(ref int row, ref int col);

		public static CellType FindWinner(Field field) {
			//check cols
			CellType result = FindWinnerMoving(
				field, Game.ROWS,
				(int iteration, ref int row, ref int col) => {
					row=iteration;
					col=0;
				},
				(ref int row, ref int col) => {
					++col;
				}
			);
			if(result != CellType.EMPTY)
				return result;

			//check rows
			result = FindWinnerMoving(
				field, Game.COLS,
				(int iteration, ref int row, ref int col) => {
					col=iteration;
					row=0;
				},
				(ref int row, ref int col) => {
					++row;
				}
			);
			if(result != CellType.EMPTY)
				return result;

			//check diagonals
			result = FindWinnerMoving(
				field, Game.ROWS+Game.COLS-1,
				(int iteration, ref int row, ref int col) => {
					row=iteration;
					col=0;
					if(iteration >= Game.ROWS) {
						row = Game.ROWS-1;
						col = iteration-Game.ROWS+1;
					}
				},
				(ref int row, ref int col) => {
					--row;
					++col;
				}
			);
			if(result != CellType.EMPTY)
				return result;

			result = FindWinnerMoving(
				field, Game.ROWS+Game.COLS-1,
				(int iteration, ref int row, ref int col) => {
					col=iteration;
					row=0;
					if(iteration >= Game.COLS) {
						col = 0;
						row = iteration-Game.COLS+1;
					}
				},
				(ref int row, ref int col) => {
					++col;
					++row;
				}
			);
			return result;
		}
	}
}
