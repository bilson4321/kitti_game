using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    public Sprite[] cardFaces = new Sprite[52];
    public GameObject cardPrefab;

    public static string[] suits = new string[] { "Club", "Diamond", "Heart", "Spade" };
    public static string[] values = new string[] { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

    public List<string> deck;


    public GameObject[] hand; //(bottom position)

    public List<string>[] playerHand; // (bottoms)

    private List<string> cardSlot0 = new List<string>();
    private List<string> cardSlot1 = new List<string>();
    private List<string> cardSlot2 = new List<string>();
    private List<string> cardSlot3 = new List<string>();
    private List<string> cardSlot4 = new List<string>();
    private List<string> cardSlot5 = new List<string>();
    private List<string> cardSlot6 = new List<string>();
    private List<string> cardSlot7 = new List<string>();
    private List<string> cardSlot8 = new List<string>();


    //player no
    public int noOfPlayer = 2;
    public GameObject playerPrefab;

    public List<GameObject> participants;

    //Dropzone
    public GameObject dropzone;

    public Canvas canvas;


    void Start()
    {
        playerHand = new List<string>[] { cardSlot0, cardSlot1, cardSlot2, cardSlot3, cardSlot4, cardSlot5, cardSlot6, cardSlot7, cardSlot8 };
        participants = new List<GameObject>();
        SetParticipants();
        PlayCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetParticipants()
    {
        float yOffset = -3.30f;
        for(int i = 0; i < noOfPlayer; i++)
        {
            GameObject participant = Instantiate(playerPrefab, new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z), Quaternion.identity,canvas.transform);
            participant.name = "Player " + (i+1);
            yOffset += 6.6f;
            participants.Add(participant);
        }
    }

    public void PlayCards()
    {
        deck = GenerateDeck();
        Shuffle(deck);
        
        Deal();
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

    //Distribute card and also prefab of the card
    void Deal()
    {
        for (int j = 0; j < 9; j++)
        {
            for (int i = 0; i < noOfPlayer; i++)
            {
                PlayerController playerController = participants[i].GetComponent<PlayerController>();
                string card = deck.Last<string>();
                deck.RemoveAt(deck.Count - 1);

                GameObject newCard = Instantiate(cardPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, transform);
                newCard.name = card;
                newCard.GetComponent<Selectable>().faceUp = true;

                playerController.TakeInHand(card,newCard);
            }
        }
    }

    public void ShowCard()
    {
        for (int i = 0; i < noOfPlayer; i++)
        {
            PlayerController playerController = participants[i].GetComponent<PlayerController>();
            List<string> playerHand = playerController.ShowHand();
            foreach(var cards in playerHand)
            {
                GameObject cardObject = GameObject.Find(cards);
                cardObject.transform.SetParent(dropzone.transform);
            }
        }
    }
}
