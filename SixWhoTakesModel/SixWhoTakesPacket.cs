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
        // Mélange le paquet!
        Shuffle();
    }

    public void Shuffle()
    {
        cards = new List<Cards>(CARDS_NB);

        for (uint i = 0; i < CARDS_NB; ++i)
        {
            Cards tmp = new Cards(i + 1);
            cards.Add(tmp);
        }

        cardsLeft = CARDS_NB;
    }

    // Renvoie 10 cartes.
    public List<Cards> GetDeck() {
        // Si il ne reste que 10 cartes dans le paquet.
        if (cards.Count == 10) return cards;

        List<Cards> tmp = new List<Cards>(10);
        
        for (int i = 0; i < 10; ++i) {   
            Cards cardSelected = GetCard();
            tmp.Add(cardSelected);
        }

        return tmp;
    }

    // Renvoie une carte du paquet.
    public Cards GetCard() {
        int r = random.Next(0, cardsLeft);
        --cardsLeft;
        Cards tmp = cards[r];
        cards.Remove(tmp);
        return tmp;
    }

    
}

