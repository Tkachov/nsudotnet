using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tkachev.Nsudotnet.TicTacToe {
	class Field {
		private const int ROWS = 3;
		private const int COLS = 3;
		private const int N_IN_ROW_TO_WIN = 3;

		private CellType[] cells = new CellType[ROWS*COLS];
		private CellType winner = CellType.EMPTY;

		public Field() {
			for(int i = 0; i<ROWS*COLS; ++i)
				cells[i] = CellType.EMPTY;
		}

		//getters		

		public CellType getCell(int index) { return cells[(index%(ROWS*COLS))];  }

		public CellType getCellByPosition(int row, int col) { return cells[(row%ROWS)*COLS + (col%COLS)];  }

		public bool isFull() {
			for(int i = 0; i<ROWS*COLS; ++i)
				if(cells[i] == CellType.EMPTY)
					return false;
			return true;
		}

		public CellType getWinner() {
			return winner;
		}

		//setters

		public void setCell(int index, CellType value) {
			cells[(index%(ROWS*COLS))] = value;
			checkWin();
		}

		public void setCellByPosition(int row, int col, CellType value) {
			cells[(row%ROWS)*COLS + (col%COLS)] = value;
			checkWin();
		}

		private void checkWin() {
			if(winner != CellType.EMPTY)
				return; //someone already won there

			winner = findWinner(this);
		}

		private static CellType findWinnerMoving(Field field, int iterations, initIteration init, move mv) {
			for(int iteration = 0; iteration<iterations; ++iteration) {
				int row = 0, col = 0;
				init(iteration, ref row, ref col);

				CellType potentinalWinner = CellType.EMPTY;
				int inRow = 0;
				while(row>=0 && row<ROWS && col>=0 && col<COLS) {
					CellType cell = field.getCellByPosition(row, col);
					if(potentinalWinner != cell) {
						potentinalWinner = cell;
						inRow = 1;
					} else
						++inRow;

					if(inRow >= N_IN_ROW_TO_WIN && potentinalWinner != CellType.EMPTY) {
						return potentinalWinner;
					}

					mv(ref row, ref col);
				}
			}

			return CellType.EMPTY;
		}

		private delegate void initIteration(int iteration, ref int row, ref int col);
		private delegate void move(ref int row, ref int col);

		public static CellType findWinner(Field field) {
			//check cols
			CellType result = findWinnerMoving(
				field, ROWS,
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
			result = findWinnerMoving(
				field, COLS,
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
			result = findWinnerMoving(
				field, ROWS+COLS-1,
				(int iteration, ref int row, ref int col) => {
					row=iteration;
					col=0;
					if(iteration >= ROWS) {
						row = ROWS-1;
						col = iteration-ROWS+1;
					}
				},
				(ref int row, ref int col) => {
					--row;
					++col;
				}
			);
			if(result != CellType.EMPTY)
				return result;

			result = findWinnerMoving(
				field, ROWS+COLS-1,
				(int iteration, ref int row, ref int col) => {
					col=iteration;
					row=0;
					if(iteration >= COLS) {
						col = 0;
						row = iteration-COLS+1;
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
