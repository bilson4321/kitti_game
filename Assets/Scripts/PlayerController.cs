using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    public List<CardModel> cardsInHand = new List<CardModel>();
    public List<GameObject> cardsPrefabList = new List<GameObject>();

    public List<GameObject> cardSlots = new List<GameObject>();

    public GameObject cardSlotPrefab;

    //for moving in deck
    private CardModel tempCard;
    private GameObject tempCardPrefab;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum CardCombination
    {
        Trial,
        DoubleRun,
        Run,
        Color,
        Jute,
        HighCard
    }

    //TODO change all string to card model
    public void TakeInHand(CardModel card,GameObject cardPrefab)
    {
        GameObject cardSlot = Instantiate(cardSlotPrefab, new Vector3(0, 0, 0), Quaternion.identity,this.transform);
        cardSlot.name = this.name+"slot"+cardSlots.Count;
        cardSlot.GetComponent<CardSlot>().index = cardSlots.Count;
        cardSlots.Add(cardSlot);
        cardsInHand.Add(card);
        cardPrefab.transform.SetParent(cardSlot.transform);
        cardPrefab.transform.position = new Vector3(0, 0, 0);
        cardsPrefabList.Add(cardPrefab);

        string cardName=cardPrefab.GetComponent<UpdateSprite>().cardData.GetSuits();
    }

    public List<CardModel> ShowHand()
    {
        List<CardModel> newHand = new List<CardModel>();
        for(int i = 0; i < 3; i++)
        {
            newHand.Add(cardsInHand[0]);
            cardsInHand.RemoveAt(0);
        }
        return newHand;
    }

    public void ShiftCardsAtLeft(int index)
    {
        Debug.Log("Shifting" + index);
        tempCard = cardsInHand[index];
        tempCardPrefab = cardsPrefabList[index];
        cardsInHand.RemoveAt(index);
        cardsPrefabList.RemoveAt(index);
        for(int i = index; i<cardSlots.Count-1; i++)
        {
           
            cardSlots[i + 1].transform.GetChild(0).SetParent(cardSlots[i].transform);
            cardsPrefabList[i].transform.position = cardSlots[i].transform.position;
        }
    }

    public void ShiftCardsAtRight(int index)
    {
        cardsInHand.Insert(index, tempCard);
        cardsPrefabList.Insert(index, tempCardPrefab);
        for (int i = cardSlots.Count-1; i > index; i--)
        {
            cardSlots[i - 1].transform.GetChild(0).SetParent(cardSlots[i].transform);
            Debug.Log("Card slot" + cardSlots[i].name);
            cardsPrefabList[i].transform.position = cardSlots[i].transform.position;
        }
    }

    public void ArrangeCards()
    {
        List<CardModel> newhands = new List<CardModel>();
        ///for trial
        /*for (int i = 2; i < 15; i++)
        {
            int count = 0;
            int[] indexes = new int[2];
            foreach (GameObject card in cardsPrefabList)
            {
                CardModel data = card.GetComponent<UpdateSprite>().cardData;
                if (i == data.GetPrecedence())
                {
                    indexes[count] = cardsPrefabList.IndexOf(card);
                    count++;
                }
            }
        }*/
        var trialsGroup = cardsInHand.GroupBy(s => s.precedence)
             .Where(g => g.Count() == 3);
        foreach (var trials in trialsGroup)
        {
            Debug.Log("Trial of " + trials.Key);
            foreach (CardModel cards in trials)//Each group has a inner collection  
            {
                cardsInHand.Remove(cards);
                newhands.Add(cards);
            }
        }

        ///A,2,3 remains
        //For Double run
        var potetialDoubleRun=cardsInHand.GroupBy(s => s.suit).Where(g=>g.Count()>2);
        foreach (var suitsGroup in potetialDoubleRun)
        {
            Debug.Log("chances of double run in " + suitsGroup.Key);
            //Sort in descending order based on value of the card.
            var sortedGroup = suitsGroup.OrderByDescending(p=>p.precedence);
            //Create a subset of consecutive sequential cards based on value, with a minimum of 3 cards.
            var sequentialCardSet = sortedGroup.FindConsecutiveSequence(p => p.precedence, 3);
            foreach (var runSequence in sequentialCardSet)
            {
                Debug.Log("Double Run sequence" + runSequence.name);
                cardsInHand.Remove(runSequence);
                newhands.Add(runSequence);
            }
        }

        //For run
        //Sort in descending order based on value of the card.
        cardsInHand.Sort((x, y) => y.precedence.CompareTo(x.precedence));

        //Create a subset of distinct card values.
        var distinctCardSet = cardsInHand.Distinct(new CardValueComparer());

        //Create a subset of consecutive sequential cards based on value, with a minimum of 5 cards.
        var sequentialCard = distinctCardSet.FindConsecutiveSequence(p => p.precedence, 3).ToList();
        foreach (var runSequence in sequentialCard)
        {
            Debug.Log("Run sequence" + runSequence.name);
            cardsInHand.Remove(runSequence);
            newhands.Add(runSequence);
        }

    }
}
