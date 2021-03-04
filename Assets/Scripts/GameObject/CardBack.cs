using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardBack : MonoBehaviour
{
    public Transform moveTowards;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = 18.0f * Time.deltaTime;

        // Check if the target position are approximately equal.
        if (Vector3.Distance(transform.position, moveTowards.position) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTowards.position, step);
        } else
        {
            Destroy(gameObject);
        }
    }


    public void SetMoveTowards(Transform moveTowards)
    {
        this.moveTowards = moveTowards;
    }
}
