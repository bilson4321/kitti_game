using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public List<string> cardsInHand = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeInHand(string card)
    { 
        cardsInHand.Add(card);
    }

    public List<string> ShowHand()
    {
        List<string> newHand = new List<string>();
        for(int i = 0; i < 3; i++)
        {
            newHand.Add(cardsInHand[i]);
            cardsInHand.RemoveAt(i);
        }

        return newHand;
    }
}
