using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public AIPathFinding pathFinding;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            pathFinding.SetTarget(other.gameObject.transform);
            //Debug.Log("Found");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Out of range");
            pathFinding.SetTarget(null);
            
        }
    }
}
