using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    public GameObject cardPrefab;
    public Sprite[] cardFaces = new Sprite[52];

    public static string[] suits = new string[] { "Club", "Diamond", "Heart", "Spade" };
    public static string[] values = new string[] { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

    public List<CardModel> deck;

    //player no
    public int noOfPlayer = 2;
    public GameObject playerPrefab;

    public List<GameObject> participants;

    //Dropzone
    public GameObject dropzone;

    public Canvas canvas;

    public GameObject placeholderPrefab;
    public GameObject cardBackPrefab;

    public static Vector2[] playersPosition = new Vector2[]
    {
       new Vector2(39,-299),
       new Vector2(39,258),
       new Vector2(-354,0),
       new Vector2(400,0)
    };

    public static Vector2[] playersStackPosition = new Vector2[]
    {
       new Vector2(-118,-286),
       new Vector2(-99,236),
       new Vector2(-373,90),
       new Vector2(411,90)
    };


    void Start()
    {
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
        for(int i = 0; i < noOfPlayer; i++)
        {
            GameObject participant = Instantiate(placeholderPrefab, new Vector3(transform.position.x,transform.position.y, transform.position.z), Quaternion.identity, canvas.transform);
            participant.name = "Player " + (i + 1);
            RectTransform placeholderRect = participant.GetComponent<RectTransform>();
            placeholderRect.localPosition = playersPosition[i];

            participants.Add(participant);
        }
    }

    public void PlayCards()
    {
        deck = GenerateDeck();
        Shuffle(deck);
        StartCoroutine(Deal());
    }

    public static List<CardModel> GenerateDeck()
    {
        List<CardModel> newDeck = new List<CardModel>();
        foreach(string suit in suits)
        {
            foreach(string value in values)
            {
                CardModel newCard = new CardModel(value, suit,(suit+value));
                newDeck.Add(newCard);
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
    IEnumerator Deal()
    {
        for (int j = 0; j < 9; j++)
        {
            for (int i = 0; i < noOfPlayer; i++)
            {
                PlayerController playerController = participants[i].GetComponent<PlayerController>();
                CardModel card = deck.Last<CardModel>();
                deck.RemoveAt(deck.Count - 1);
                playerController.TakeInHand(card);

                GameObject newCard = Instantiate(cardBackPrefab, new Vector3(canvas.transform.position.x, canvas.transform.position.y, canvas.transform.position.z-4), Quaternion.identity, canvas.transform);
                newCard.name = card.GetName();
                newCard.GetComponent<CardBack>().SetMoveTowards(participants[i].transform);
                yield return new WaitForSeconds(0.3f);
            }
        }
        participants[0].GetComponent<PlayerController>().ShowDeck();
    }

    public void ShowCard()
    {
        participants[1].GetComponent<PlayerController>().ArrangeCards();
        /*for (int i = 0; i < noOfPlayer; i++)
        {
            PlayerController playerController = participants[i].GetComponent<PlayerController>();
            List<CardModel> playerHand = playerController.ShowHand();
            foreach(var cards in playerHand)
            {
                GameObject cardObject = GameObject.Find(cards.GetName());
                cardObject.transform.SetParent(dropzone.transform);
            }
        }*/
    }
}
