using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTrigger : MonoBehaviour
{

    private int direction = 1;
    private Vector3 midPos;
    private Transform player;

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
            LevelManager.Instance.movingLane = false;
            midPos = other.transform.position + LevelManager.Instance.currentDirection * 15;
            player = other.transform;
        }

    }

    private void Update()
    {
        if(!LevelManager.Instance.movingLane)
        {
            if (SwipeInput.Instance.SwipeLeft)
                direction = 0;
            if (SwipeInput.Instance.SwipeRight)
                direction = 2;

            if (direction != 1)
            {
                if(direction == 0)
                {
                    player.Rotate(new Vector3(0f, -90f, 0f));
                    TileManager.Instance.changeTileDirection(true);
                    direction = 1;
                }
                else
                {
                    player.Rotate(new Vector3(0f, 90f, 0f));
                    TileManager.Instance.changeTileDirection(false);
                    direction = 1;
                }

                LevelManager.Instance.movingLane = true;
            }
        }
    }
}
