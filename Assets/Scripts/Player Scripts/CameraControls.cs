using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    Mouse mouse;
    [Header("Follow Targets Variables")]
    public Transform followTargetTransform;
    
    public Transform playerTransform;
    //float angle;
    // Start is called before the first frame update
    void Start()
    {
        followTargetTransform = this.gameObject.transform;
        mouse = Mouse.current;
    }

    // Update is called once per frame
    void Update()
    {



       //Debug.Log(mouse.delta.y.ReadValue()); // new input system way of gettting mouse movement changes
        //Debug.Log(Input.GetAxis("Mouse X"));
        followTargetTransform.rotation *= Quaternion.AngleAxis((mouse.delta.x.ReadValue() * 0.5f), Vector3.up);

       followTargetTransform.rotation *= Quaternion.AngleAxis(-mouse.delta.y.ReadValue() * 0.2f, Vector3.right);

        Vector3 angles = followTargetTransform.localEulerAngles;

        if (followTargetTransform.localEulerAngles.x < -40 && followTargetTransform.localEulerAngles.x > -180)
        {
            angles.x = -80;
        }
        else if (followTargetTransform.localEulerAngles.x > 40 && followTargetTransform.localEulerAngles.x < 180)
        {
            angles.x = 40;
        }
        angles.z = 0;
        followTargetTransform.localEulerAngles = angles;
        playerTransform.rotation = Quaternion.Euler(0, followTargetTransform.rotation.eulerAngles.y, 0);
        // reset the localangles so the new direction the player is facing is it's starting point for rotatating again.
        followTargetTransform.localEulerAngles = new Vector3(angles.x, 0, 0);


    }
}
