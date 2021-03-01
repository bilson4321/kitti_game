
public class CardModel
{
    public string name;
    public int precedence;
    public Suits suit;

    public enum Suits
    {
        Club,
        Diamond,
        Heart,
        Spade
    }

    public CardModel(string precedence, string suit, string name)
    {
        this.name = suit + precedence;
        if (suit == "Club")
        {
            this.suit = Suits.Club;
        }
        if (suit == "Diamond")
        {
            this.suit = Suits.Diamond;
        }
        if(suit == "Heart")
        {
            this.suit = Suits.Heart;
        }
        if(suit == "Spade")
        {
            this.suit = Suits.Spade;
        }
        if(precedence == "Ace")
        {
            this.precedence = 14;
        }else if (precedence == "Jack")
        {
            this.precedence = 11;
        }else if (precedence == "Queen")
        {
            this.precedence = 12;
        }else if(precedence == "King")
        {
            this.precedence = 13;
        }
        else
        {
            this.precedence = int.Parse(precedence);
        }
    }

    public int GetPrecedence()
    {
        return this.precedence;
    }

    public string GetSuits()
    {
        return this.suit.ToString();
    }

    public string GetName()
    {
        return this.name;
    }
}
