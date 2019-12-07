using System;
using System.Collections.Generic;

class SixWhoTakesBoard {

    private static readonly int PLAYER_MAX = 10;
    private static readonly int BOARD_HEIGHT = 4;
    private static readonly int BOARD_WIDTH = 5;
    private readonly List<Player> players;
    private SixWhoTakesPacket sixWhoTakesPacket;
    private List<List<Cards>> boards;

    public SixWhoTakesBoard()
    {
        players = new List<Player>(PLAYER_MAX); 
    }

    public void Init()
    {
        // Initialise le paquet de carte.
        sixWhoTakesPacket = new SixWhoTakesPacket();

        foreach (Player p in players)
        {
            p.InitDecks(sixWhoTakesPacket.GetDeck());
        }

        boards = new List<List<Cards>>();

        for (int i = 0; i < 4; ++i)
        {
            List<Cards> tmp = new List<Cards>();
            Cards c = sixWhoTakesPacket.GetCard();
            tmp.Add(c);
            boards.Add(tmp);
        }
    }

    public void AddPlayer(Player p)
    {
        players.Add(p);        
    }
    
    public void ShowBoard()
    {
        Console.WriteLine("-------------------------------------------");
        for (int i = 0; i < 4; ++i)
        {
            List<Cards> tmp = boards[i];

            foreach (Cards c in tmp)
            {
                string value = c.getValue().ToString();
                string FormatName = String.Format(" {0, 3}  |", value);
                Console.Write(FormatName);
            }
            Console.WriteLine();
        }
        Console.WriteLine("-------------------------------------------");
    }

    /// <summary>
    /// Renvoie la liste des joueurs.
    /// </summary>
    /// <returns>Liste de Player</returns>
    public List<Player> GetPlayers()
    {
        return players;
    }

    /// <summary>
    /// Indique si la carte de vleur 'value' peut etre placé sur le plateau.
    /// </summary>
    /// <param name="value">Valeur d'une carte</param>
    /// <returns>Renvoie vrai si elle peut etre placé sur le plateau.</returns>
    public bool CanBePlaced(uint value, out int row)
    {
        row = -1;
        uint difference = 105;
        for (int i = 0; i < 4; ++i)
        {
            List<Cards> tmp = boards[i];
            Cards c = tmp[tmp.Count - 1];
            if (c.getValue() < value)
            {
                uint dif = value - c.getValue();
                if (difference > dif)
                {
                    difference = dif;
                    row = i;
                }
            }
        }

        // On peut placé la carte si on à trouvé une ligne valide et que la ligne n'a pas déja 5 cartes.
        return row != -1;        
    }

    public void PlaceCards(Player p, int index)
    {
        // On place la carte sur le board
        List<Cards> tmp = boards[index];
        tmp.Add(new Cards(p.GetSelectedCards().getValue()));
        // On enleve le carte de sa main
        p.Remove(p.GetSelectedCards().getValue());
        // On enleve la carte selectionné
        p.RemoveSelectedCard();
    }

    /// <summary>
    /// Renvoie toutes les cartes qui se trouve sur la ligne 'row' du plateau de jeu.
    /// </summary>
    /// <param name="c">La carte qui deviendra la première de la nouvelle ligne</param>
    /// <param name="row">La ligne du swap</param>
    /// <returns>Renvoie la liste des cartes qui se trouvais sur la ligne.</returns>
    public void ReplaceRow(Player p, int row)
    {
        List<Cards> t = boards[row];
        p.AddDiscardingList(t);

        List<Cards> newRow = new List<Cards>();
        newRow.Add(new Cards(p.GetSelectedCards().getValue()));
        p.Remove(p.GetSelectedCards().getValue());
        p.RemoveSelectedCard();
        boards[row] = newRow;        
    } 

    public int NumberOfCardsOnTheLine(int row)
    {
        return boards[row].Count;
    }
}

