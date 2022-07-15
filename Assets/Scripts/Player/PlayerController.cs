using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool turnLeft, turnRight;
    private const float Turn_Speed = 0.05f;
    private const float LANE_DISTANCE = 5.0f;
    private CharacterController controller;
    private float jumpForce = 15.0f;
    private float verticalVelocity;
    private float gravity = 18.0f;
    private float speed = 15.0f;


    private Animator animator;

    private float desirdLine = 1;//0 is left and 2 is right


    // Use this for initialization
    private void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

    }
    private void Update()
    {
        if (!GameManager.Instance.isGameStarted)
            return;
        // where we be in this time 
        if (SwipeInput.Instance.SwipeLeft && LevelManager.Instance.movingLane == true)
            MoveLane(false);
        if (SwipeInput.Instance.SwipeRight && LevelManager.Instance.movingLane == true)
            MoveLane(true);

        Vector3 targetposition;
        Vector3 movevector = Vector3.zero;
        //ground check 
        if (IsGrounded())
        {
            verticalVelocity = -0.1f;
            if (SwipeInput.Instance.SwipeUp)
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
            if (SwipeInput.Instance.SwipeUp)
            {
                verticalVelocity = -jumpForce;

            }

        }
        movevector.y = verticalVelocity;

        if (LevelManager.Instance.currentDirection == Vector3.forward)
        {
            targetposition = transform.position.z * Vector3.forward;
            targetposition.x = TileManager.Instance.lastTile.transform.position.x;
            if (desirdLine == 0)
                targetposition += Vector3.left * LANE_DISTANCE;
            else if (desirdLine == 2)
                targetposition += Vector3.right * LANE_DISTANCE;
                //lests calculate our delta 
            movevector.x = (targetposition - transform.position).normalized.x * speed;
            movevector.z = speed;
        }
        else if (LevelManager.Instance.currentDirection == Vector3.right)
        {
            targetposition = transform.position.x * Vector3.right;
            targetposition.z = TileManager.Instance.lastTile.transform.position.z;
            if (desirdLine == 0)
                targetposition += Vector3.forward * LANE_DISTANCE;
            else if (desirdLine == 2)
                targetposition += Vector3.back * LANE_DISTANCE;
            //lests calculate our delta 
            movevector.x = speed;
            movevector.z = (targetposition - transform.position).normalized.z * speed;
        }
        else if (LevelManager.Instance.currentDirection == Vector3.left)
        {
            targetposition = transform.position.x * Vector3.left;
            targetposition.z = TileManager.Instance.lastTile.transform.position.z;
            if (desirdLine == 0)
                targetposition += Vector3.back * LANE_DISTANCE;
            else if (desirdLine == 2)
                targetposition += Vector3.forward * LANE_DISTANCE;
            //lests calculate our delta 
            movevector.x = -speed;
            movevector.z = (targetposition - transform.position).z * speed / 3;
        }
        else if (LevelManager.Instance.currentDirection == Vector3.back)
        {
            targetposition = transform.position.z * Vector3.back;
            targetposition.x = TileManager.Instance.lastTile.transform.position.x;
            if (desirdLine == 0)
                targetposition += Vector3.right * LANE_DISTANCE;
            else if (desirdLine == 2)
                targetposition += Vector3.left * LANE_DISTANCE;
        //lests calculate our delta 
        movevector.x = (targetposition - transform.position).x * speed / 3;
        movevector.z = -speed;
        }
        //move the pengu
        controller.Move(movevector * Time.deltaTime);
        //rotate over pangu
        Vector3 dir = controller.velocity;
        if (dir != Vector3.zero)
        {
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, Turn_Speed);
        }

}
    private void MoveLane(bool goingRight)
    {
        desirdLine += (goingRight) ? 1 : -1;
        desirdLine = Mathf.Clamp(desirdLine, 0, 2);
    }
    private bool IsGrounded()
    {
        Ray groundray = new Ray(new Vector3(controller.bounds.center.x, (controller.bounds.center.y - controller.bounds.extents.y) + 0.2f, controller.bounds.center.z), Vector3.down);
        Debug.DrawRay(groundray.origin, groundray.direction, Color.cyan, 1.0f);
        return Physics.Raycast(groundray, 0.2f + 0.1f);
    }
    
}