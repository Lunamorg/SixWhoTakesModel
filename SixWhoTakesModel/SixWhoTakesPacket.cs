using System;
using System.Collections.Generic;

class SixWhoTakesPacket
{
    public static int CARDS_NB = 104;
    private List<Cards> cards;
    private int cardsLeft;
    private Random random;

    public SixWhoTakesPacket()
    {
        random= new Random();
        cards = new List<Cards>(CARDS_NB);

        for (int i = 0; i < CARDS_NB; ++i)
        {
            Cards tmp = new Cards(i + 1);
            cards.Add(tmp);
        }

        cardsLeft = CARDS_NB;
    }

    public List<Cards> GetDeck()
    {
        List<Cards> tmp = new List<Cards>(10);
        
        for (int i = 0; i < 10; ++i)
        {   
            Cards cardSelected = GetCard();

            tmp.Add(cardSelected);

            Console.WriteLine("Nombre de carte restante " + cards.Count);
            Console.WriteLine("Numéro de la carte " + cardSelected.getValue());
        }

        return tmp;
    }

    public Cards GetCard()
    {
        int r = random.Next(0, cardsLeft);
        --cardsLeft;
        Cards tmp = cards[r];
        cards.Remove(tmp);
        return tmp;
    }

}

