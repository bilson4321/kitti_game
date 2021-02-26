using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public List<string> cardsInHand = new List<string>();
    public List<GameObject> cardsPrefabList = new List<GameObject>();

    public List<GameObject> cardSlots = new List<GameObject>();

    public GameObject cardSlotPrefab;

    //for moving in deck
    private string tempCard;
    private GameObject tempCardPrefab;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeInHand(string card,GameObject cardPrefab)
    {
        GameObject cardSlot = Instantiate(cardSlotPrefab, new Vector3(0, 0, 0), Quaternion.identity,this.transform);
        cardSlot.name = this.name+"slot"+cardSlots.Count;
        cardSlot.GetComponent<CardSlot>().index = cardSlots.Count;
        cardSlots.Add(cardSlot);
        cardsInHand.Add(card);
        cardPrefab.transform.SetParent(cardSlot.transform);
        cardPrefab.transform.position = new Vector3(0, 0, 0);
        cardsPrefabList.Add(cardPrefab);
    }

    public List<string> ShowHand()
    {
        List<string> newHand = new List<string>();
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
}
