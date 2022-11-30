using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrappleHook : ShootingRayCast2
{
    Keyboard keyboard;
    [SerializeField]
    CharacterController controller;
    [Header("Grapple Hook Variables")]
    [SerializeField]
    private bool _usingGrapple;
    [SerializeField]
    private int speed = 12;

    [SerializeField]
    private LineRenderer lineRender;
   
    public Transform startPoint;

    Vector3 direction;

    private void Awake()
    {
        lineRender = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        keyboard = Keyboard.current;
        controller = GetComponent<CharacterController>();
        _usingGrapple = false;
        maxHitDistance = 35f;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        
    }

    public override void ShootRay()
    {
       
        if (keyboard.fKey.IsPressed() && !usingGrapple)
        {
            base.ShootRay();

        }

        

        MoveTowardsTarget();

    }

    protected override void DoSomethingWhenHit()
    {
        _usingGrapple = true;
        
        direction = hit.point - transform.position; // get direction vector from player to target
        direction = direction.normalized;
       
       


    }

    public bool usingGrapple
    {
        get { return _usingGrapple;}
        private set { _usingGrapple = value; }
    }

    protected void MoveTowardsTarget()
    {
        if (usingGrapple)
        {
            controller.Move(direction * speed * Time.deltaTime);
            lineRender.SetPosition(0, startPoint.position);
            lineRender.SetPosition(1, hit.point);
        }
        else
        {
            lineRender.SetPositions(new Vector3[] { this.transform.position, this.transform.position});
        }
       
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        usingGrapple = false;
    }
}
