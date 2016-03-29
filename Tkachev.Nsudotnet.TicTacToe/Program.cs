namespace Tkachev.Nsudotnet.TicTacToe {
	class Program {
		static void Main(string[] args) {
			Game game = new Game();
			//TODO: ascii intro
			while(game.getWinner() == CellType.EMPTY) {
				game.printFieldAndAskToMove();
			}
			//TODO: show winner and ask to play more
		}
	}
}
