using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class SixWhoTakesBoard {

    private static int PLAYER_MAX = 10;
    private static int BOARD_HEIGHT = 4;
    private static int BOARD_WIDTH = 5;
    private int turn;
    private List<Player> players;
    private SixWhoTakesPacket sixWhoTakesPacket;
    private Cards[][] board;

    public SixWhoTakesBoard(Player creator)
    {
        sixWhoTakesPacket = new SixWhoTakesPacket();
        players = new List<Player>(PLAYER_MAX);

        board = new Cards[BOARD_HEIGHT][];

        for (int i = 0; i < BOARD_HEIGHT; ++i)
        {
            board[i] = new Cards[BOARD_WIDTH];
            Cards c = sixWhoTakesPacket.GetCard();

            board[i][0] = c;
        }

        creator.AddDeck(sixWhoTakesPacket.GetDeck());
        players.Add(creator);        
        
        turn = 10;
    }

    public void AddPlayer(Player p)
    {
        p.AddDeck(sixWhoTakesPacket.GetDeck());
        players.Add(p);        
    }

    public void ShowBoard() {
        Console.WriteLine("----------------------------------");
        for (int i = 0; i < BOARD_HEIGHT; ++i)
        {
            
            for (int j = 0; j < BOARD_WIDTH; ++j)
            {
                if (board[i][j] != null) Console.Write(board[i][j].getValue() + " | ");

            }
            Console.WriteLine();
        }
        Console.WriteLine("----------------------------------");
    }



}

