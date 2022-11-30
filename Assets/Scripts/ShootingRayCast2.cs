using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingRayCast2 : MonoBehaviour
{
   
    [Header("Ray cast Variables")]
    [SerializeField]
    private LayerMask aimColliderMask = new LayerMask();
    [SerializeField]
    protected Transform hitTarget = null;
    [SerializeField]
    protected RaycastHit hit;
    [SerializeField]
    protected float maxHitDistance = Mathf.Infinity;

    // crosshair/ screen center variables
    protected Ray ray; // creating a ray from the center of the screen
    Vector2 screenCenterPoint;


    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    public virtual void Update()
    {
        hitTarget = null;

        screenCenterPoint = new Vector3(Screen.width / 2, Screen.height / 2);
        ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        ShootRay();



    }

    public virtual void ShootRay()
    {
        if (Physics.Raycast(ray, out hit, maxHitDistance, aimColliderMask))
        {
           
            hitTarget = hit.transform;
            //Debug.Log(hit.transform.gameObject.name);
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.blue);
            if (hitTarget != null)
            {
                DoSomethingWhenHit();
            }
        }
    }
    protected virtual void DoSomethingWhenHit()
    {

    }
}
