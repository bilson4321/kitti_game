using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kitti
{
    public class HandController : MonoBehaviour
    {
        public GameObject cardPrefab;

        List<GameObject> cardPrefabList =new List<GameObject>();

        public Vector2 moveTowards;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            float step = 200.0f * Time.deltaTime;
            // Check if the target position are approximately equal.
            if (Vector2.Distance(transform.localPosition, moveTowards) > 0.001f)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, moveTowards, step);
            }
        }

        public void CreateHand(List<CardModel> cards)
        {
            float xOffset = 0.3f;
            float zOffset = 0.4f;
            foreach (CardModel card in cards)
            {
                GameObject PrefabCard = Instantiate(cardPrefab, new Vector3(transform.position.x+xOffset, transform.position.y, transform.position.z-zOffset), Quaternion.identity,this.transform);
                PrefabCard.name = card.name;
                xOffset += 0.6f;
                zOffset += 0.1f;
            }
            Destroy(gameObject,4.6f);
        }

        public void SetMoveTowards(Vector2 moveTowards)
        {
            this.moveTowards = moveTowards;
        }
    }
}

