using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOverrider : MonoBehaviour
{
    //public Transform lookAt;
    /*public Vector3 offset = new Vector3(0, 10f, -20f);

    public bool isMoving { set; get; }
    public GameObject player;*/
    /* private Vector3 offset;

     float distance;
     Vector3 playerPrevPos, playerMoveDir;

     // Use this for initialization
     void Start()
     {
         offset = transform.position - player.transform.position + new Vector3(0, 0, 15f);

         distance = offset.magnitude;
         playerPrevPos = player.transform.position + new Vector3(0, 0, 15f);
     }

     void LateUpdate()
     {

         playerMoveDir = player.transform.position - playerPrevPos;
         if (playerMoveDir != Vector3.zero)
         {
             playerMoveDir.Normalize();
             Vector3 newPos = player.transform.position - playerMoveDir * distance;
            if (LevelManager.Instance.currentDirection == Vector3.forward || LevelManager.Instance.currentDirection == Vector3.back)
                newPos.x = TileManager.Instance.lastTile.transform.position.x;
            else
                newPos.z = TileManager.Instance.lastTile.transform.position.z;

            newPos.y = 10f;
             transform.position = newPos;

             transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(15, TileManager.Instance.currentRotation * -1 + 180, 0), 0.1f);

             playerPrevPos = player.transform.position;
         }
     }*/
    public GameObject player;
    public Vector3 offset = new Vector3(0, 10f, -10f);
    public Vector3 offset2 = new Vector3(10f, 10f, 0);

    public bool IsMoving { set; get; }


    private void LateUpdate()
    {

        if (!IsMoving)
            return;

        Vector3 desiredPosition = Vector3.zero;
        if (LevelManager.Instance.currentDirection == Vector3.forward || LevelManager.Instance.currentDirection == Vector3.back)
        {
            if (LevelManager.Instance.currentDirection == Vector3.forward)
                desiredPosition = player.transform.position + offset;
            else
                desiredPosition = player.transform.position - offset;

            desiredPosition.x = TileManager.Instance.lastTile.transform.position.x;
        }
        else
        {
            if (LevelManager.Instance.currentDirection == Vector3.left)
                desiredPosition = player.transform.position - offset2;
            else
                desiredPosition = player.transform.position + offset2;
            desiredPosition.z = TileManager.Instance.lastTile.transform.position.z;
        }

        desiredPosition.y = 10f;
        transform.position = desiredPosition;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(15, TileManager.Instance.currentRotation * -1 + 180, 0), 0.1f);
    }
}
