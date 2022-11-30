using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    
    Keyboard keyboard;
    CharacterController controller;
    [Header("Grapple Hook script")]
    public GrappleHook hook;

    [Header("Movement Variables")]
    [SerializeField]
    float movementSpeed = 10;
    Vector3 ySpeed;
    public float gravityScale;
    

    //[Header("Camera Component")]
    //public Transform cameraFollowTargetTransform; // used to dertermine the direction player moves based of the camera's forward direction
    // Start is called before the first frame update
    void Start()
    {
        keyboard = Keyboard.current;
        controller = GetComponent<CharacterController>();
       
       
        gravityScale = -10f;
      
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded && ySpeed.y <= 0)
        {
            ySpeed.y = 0;
        }

        if (hook.usingGrapple)
        {
           // gravityScale = 0;
            ySpeed.y = 0;
        }
        else 
        {
            //gravityScale = -10f;
        }


        if (keyboard.wKey.isPressed)
        {
           // Vector3 forward = new Vector3(cameraFollowTargetTransform.forward.x, 0, cameraFollowTargetTransform.forward.z);
            //Debug.Log(cameraFollowTargetTransform.forward);
            //controller.Move(forward * movementSpeed * Time.deltaTime);
            controller.Move(transform.forward  * movementSpeed * Time.deltaTime);
        }
        if (keyboard.sKey.isPressed)
        {
            //Vector3 forward = new Vector3(cameraFollowTargetTransform.forward.x, 0, cameraFollowTargetTransform.forward.z);
            controller.Move(-transform.forward * movementSpeed* Time.deltaTime);
        }
        if (keyboard.aKey.isPressed)
        {
            //Vector3 right = new Vector3(cameraFollowTargetTransform.right.x, 0, cameraFollowTargetTransform.right.z);
            controller.Move(-transform.right * movementSpeed * Time.deltaTime);
        }
        if (keyboard.dKey.isPressed)
        {
            //Vector3 right = new Vector3(cameraFollowTargetTransform.right.x, 0, cameraFollowTargetTransform.right.z);
            controller.Move(transform.right * movementSpeed * Time.deltaTime);
        }

       

        if (keyboard.spaceKey.isPressed && ySpeed.y == 0)
        {

            ySpeed.y = 6;
        }
        Debug.Log(controller.isGrounded);

        ySpeed.y +=  gravityScale *  Time.deltaTime;
        if (ySpeed.y >= 30f)
        {
            ySpeed.y = 30f;
        }
        controller.Move(ySpeed * Time.deltaTime);
      

       
    }

   
}
