using System;
using System.Collections.Generic;

namespace SixWhoTakesModel
{
    class Program
    {
        static void Main(string[] args)
        {                    
            SixWhoTakesBoard board = new SixWhoTakesBoard();

            board.AddPlayer(new Player("Player 1"));
            board.AddPlayer(new Player("Player 2"));
            //board.AddPlayer(new Player("Player 3"));
            //board.AddPlayer(new Player("Player 4"));
            //board.AddPlayer(new Player("Player 5"));
            //board.AddPlayer(new Player("Player 6"));
            //board.AddPlayer(new Player("Player 7"));
            //board.AddPlayer(new Player("Player 8"));
            //board.AddPlayer(new Player("Player 9"));
            //board.AddPlayer(new Player("Player 10"));

            List<Player> players = board.GetPlayers();

            // Demandez à rejouer, test du reset.

            char continuer = 'o';

            do
            {
                board.Init();
                // Une partie dure 10 tours.
                for (int i = 0; i < 10; ++i)
                {
                    board.ShowBoard();
                    foreach (Player p in players)
                    {
                        uint cards;

                        do
                        {
                            // On montre les cartes des joueurs    
                            p.SeeMyHand();
                            Console.WriteLine("{0} choisit une carte de ton deck.", p.GetName());
                            // Chaque joueur choisit une carte
                            string result = Console.ReadLine();
                            cards = uint.Parse(result);
                        } while (!p.OwnThisCard(cards));

                        p.SetSelectedCards(cards);
                    }
                    // La plus petite carte commence
                    players.Sort();

                    foreach (Player p in players)
                    {
                        board.ShowBoard();
                        uint value = p.GetSelectedCards().getValue();
                        int row;
                        // Si elle peut etre place
                        if (board.CanBePlaced(value, out row))
                        {
                            // Si c'est la 6 ieme colonne, toutes les cartes vont dans la pile du joueur
                            // Sinon on la place
                            if (board.NumberOfCardsOnTheLine(row) > 4)
                            {
                                Console.WriteLine("{0} prends toutes les cartes de la ligne {1}", p.GetName(), (row + 1));
                                Console.WriteLine("La carte {0} devient la premiere carte de la ligne.", p.GetSelectedCards().getValue());
                                board.ReplaceRow(p, row);
                            }
                            else
                            {
                                Console.WriteLine("{0} dépose la carte {1} à la ligne {2}.", p.GetName(), value, (row + 1));
                                board.PlaceCards(p, row);
                            }
                        }
                        else
                        {
                            // Sinon on demande a choisir une ligne
                            int r = 0;
                            do
                            {
                                Console.WriteLine("{0} votre carte {1} ne vas dans aucune ligne.", p.GetName(), p.GetSelectedCards().getValue());
                                Console.WriteLine("Quel ligne voulez-vous défaussez ? (1-4)");
                                string rowChoosed = Console.ReadLine();
                                r = int.Parse(rowChoosed);
                            } while (r < 1 && r > 4);
                            // on place toutes les cartes de la ligne dans la piles du joueur
                            board.ReplaceRow(p, r - 1);
                        }
                    }
                }

                board.ShowBoard();

                players.Sort();
                int index = 1;
                foreach (Player p in players)
                {
                    List<Cards> tmp = p.GetDiscardingList();

                    //Console.WriteLine("Nombre de carte dans la défausse de {0} : {1}", p.GetName(), tmp.Count);

                    foreach (Cards c in tmp)
                    {
                        Console.Write(c.getValue() + " ; ");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Position {0} : {1}", index, p.GetName());
                    Console.WriteLine("Nombre de tête de boeuf {0}", p.GetHeadBeefNumber());
                    ++index;
                }
                Console.WriteLine("Voulez-vous faire une nouvelle partie ? o / n");
                continuer = Console.ReadKey().KeyChar;
            } while (continuer == 'o');

            
        }
    }
}
