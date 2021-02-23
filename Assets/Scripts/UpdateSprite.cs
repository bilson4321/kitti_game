using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Match card sprite to name
public class UpdateSprite : MonoBehaviour
{
    //for swap
    public Sprite cardFace;
    public Sprite cardBack;

    private SpriteRenderer spriteRenderer;
    private Selectable selectable;
    private GameController gameController;
    void Start()
    {
        List<string> deck = GameController.GenerateDeck();
        gameController = FindObjectOfType<GameController>();

        int i = 0;
        foreach(string card in deck)
        {
            if(this.name == card)
            {
                cardFace = gameController.cardFaces[i];
                break;
            }
            i++;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        selectable = GetComponent<Selectable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(selectable.faceUp == true)
        {
            spriteRenderer.sprite = cardFace;
        }
        else
        {
            spriteRenderer.sprite = cardBack;
        }
    }
}
