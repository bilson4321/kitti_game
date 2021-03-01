﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    public Sprite[] cardFaces = new Sprite[52];
    public GameObject cardPrefab;

    public static string[] suits = new string[] { "Club", "Diamond", "Heart", "Spade" };
    public static string[] values = new string[] { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

    public List<CardModel> deck;


    public GameObject[] hand; //(bottom position)

    //player no
    public int noOfPlayer = 2;
    public GameObject playerPrefab;

    public List<GameObject> participants;

    //Dropzone
    public GameObject dropzone;

    public Canvas canvas;


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
        float yOffset = -3.30f;
        for(int i = 0; i < noOfPlayer; i++)
        {
            GameObject participant = Instantiate(playerPrefab, new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z), Quaternion.identity,canvas.transform);
            participant.name = "Player" + (i+1);
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

    public static List<CardModel> GenerateDeck()
    {
        List<CardModel> newDeck = new List<CardModel>();
        foreach(string s in suits)
        {
            foreach(string v in values)
            {
                CardModel newCard = new CardModel(v, s,(s+v));
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
    void Deal()
    {
        for (int j = 0; j < 9; j++)
        {
            for (int i = 0; i < noOfPlayer; i++)
            {
                PlayerController playerController = participants[i].GetComponent<PlayerController>();
                CardModel card = deck.Last<CardModel>();
                deck.RemoveAt(deck.Count - 1);

                GameObject newCard = Instantiate(cardPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, transform);
                newCard.name = card.GetName();
                newCard.GetComponent<Selectable>().faceUp = true;
                newCard.GetComponent<UpdateSprite>().cardData = card;

                playerController.TakeInHand(card,newCard);
            }
        }
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
