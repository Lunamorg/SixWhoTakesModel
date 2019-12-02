using System;

namespace SixWhoTakesModel
{
    class Program
    {
        static void Main(string[] args)
        {
            Player p1 = new Player("John");
            Player p2 = new Player("Doe");

            SixWhoTakesBoard board = new SixWhoTakesBoard(p1);
            Console.Clear();
            board.AddPlayer(p2);

            Console.Clear();

            p1.SeeMyHand();
            p2.SeeMyHand();

            board.ShowBoard();

        }
    }
}
