using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static string[] suits = new string[] { "Club", "Diamond", "Heart", "Spade" };
    public static string[] values = new string[] { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

    public List<string> deck;

    public Sprite[] cardFaces = new Sprite[52];
    public GameObject cardPrefab;
    void Start()
    {
        PlayCards();
        Deal();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCards()
    {
        deck = GenerateDeck();

        Shuffle(deck);
        foreach(string card in deck)
        {
            Debug.Log("Card" + card);
        }
    }

    public static List<string> GenerateDeck()
    {
        List<string> newDeck = new List<string>();
        foreach(string s in suits)
        {
            foreach(string v in values)
            {
                newDeck.Add(s + v);
            }
        }

        return newDeck;
    }

    //officiate shuffle
    void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while(n>1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    void Deal()
    {
        float yOffset = 0;
        float zOffest = 0.03f;
        foreach(string card in deck)
        {
            GameObject newCard = Instantiate(cardPrefab, new Vector3(transform.position.x,transform.position.y - yOffset,transform.position.z - zOffest), Quaternion.identity);
            newCard.name = card;

            yOffset = yOffset + 0.1f;
            zOffest = zOffest + 0.03f;
        }
    }
}
