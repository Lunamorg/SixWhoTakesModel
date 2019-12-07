using System;
using System.Collections.Generic;

public class Player : IEquatable<Player>, IComparable<Player>
{
    private string name;
    private List<Cards> deck;
    private List<Cards> discarding;
    private Cards selectedCard;

    public Player(string name) {
        // Nom du joueur.
        this.name = name;        
    }

    public void InitDecks(List<Cards> deck)
    {
        deck.Sort();
        this.deck = deck;
        discarding = new List<Cards>();
    }

    public void SeeMyHand()
    {
        Console.Write("{0} ; Main : ", this.GetName());
        foreach (Cards c in deck)
        {
            Console.Write(c.getValue() + " ; ");
        }

        Console.WriteLine();

        Console.Write("{0} ; Défausse : ", this.GetName());
        foreach (Cards c in discarding)
        {
            Console.Write(c.getValue() + " ; ");
        }

        Console.WriteLine();

    }

    public bool HasNoCard()
    {
        return deck.Count == 0;
    }

    public void Remove(uint value)
    {
        deck.Remove(new Cards(value));
    }

    public int CardsNumber()
    {
        return deck.Count;
    }

    public bool OwnThisCard(uint value)
    {        
        bool result = deck.Contains(new Cards(value));
        return result;
    }

    public void SetSelectedCards(uint value)
    {
        selectedCard = new Cards(value);
    }

    public Cards GetSelectedCards()
    {
        return this.selectedCard;
    }

    public void RemoveSelectedCard()
    {
        this.selectedCard = null;
    }

    public string GetName()
    {
        return name;
    }

    public bool Equals(Player other)
    {
        

        if (other.GetSelectedCards().getValue() == this.GetSelectedCards().getValue()) return true;

        return false;
    }

    /// <summary>
    /// Compare la valeur des cartes choisit par chacun des joueurs.
    /// </summary>
    /// <param name="other">Le joueurs avec lequel nous comparons les valeur de nos cartes.</param>
    /// <returns>Si les valeurs des cartes des joueurs sont égales renvoie 0, 1 si elle est supérieur, -1 sinon.</returns>
    public int CompareTo(Player other)
    {
        if (other.GetSelectedCards() != null && this.GetSelectedCards() != null)
        {
            return CompareToSelectedCards(other);
        }

        if (this.GetHeadBeefNumber() == other.GetHeadBeefNumber()) return 0;

        if (this.GetHeadBeefNumber() > other.GetHeadBeefNumber()) return 1;

        return -1;
    }

    /// <summary>
    /// Compare les valeurs des 2 cartes choisir par les joueurs.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareToSelectedCards(Player other)
    {
        return this.GetSelectedCards().CompareTo(other.GetSelectedCards());
    }

    public int GetCardsNumber()
    {
        return deck.Count;
    }

    /// <summary>
    /// Ajoute une liste de carte dans la pile de défausse du joueur.
    /// </summary>
    /// <param name="list">Liste de cartes</param>
    public void AddDiscardingList(List<Cards> list) {
        //discarding.CopyTo(list.ToArray());
        discarding.AddRange(list);
        Console.WriteLine("Taille list " + discarding.Count);
    }

    /// <summary>
    /// Renvoie la liste des cartes de la pile de défausse
    /// </summary>
    /// <returns></returns>
    public List<Cards> GetDiscardingList()
    {
        return discarding;
    }

    public int GetHeadBeefNumber()
    {
        int result = 0;

        foreach (Cards c in this.GetDiscardingList())
        {
            result += GetHeadBeef(c);
        }

        return result;
    }

    // Renvoie le nombre de tete de boeuf d'une carte
    public int GetHeadBeef(Cards c)
    {
        uint value = c.getValue();

        if (value % 10 == 0) return 3;

        if (value % 11 == 0 && value % 5 == 0) return 7;

        if (value % 11 == 0) return 5;

        if (value % 5 == 0) return 2;

        return 1;
    }
}