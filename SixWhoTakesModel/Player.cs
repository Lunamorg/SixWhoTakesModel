using System;
using System.Collections.Generic;

public class Player
{
    private string name;
    private List<Cards> deck;

    public Player(string name)
    {
        this.name = name; 
        
        
    }

    public void AddDeck(List<Cards> deck)
    {
        deck.Sort();
        this.deck = deck;
    }

    public void SeeMyHand()
    {
        Console.Write("Main : ");
        foreach (Cards c in deck)
        {
            Console.Write(c.getValue() + " ; ");
        }

        Console.WriteLine();
    }

    public int CardsNumber()
    {
        return deck.Count;
    }


}