using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissle : MonoBehaviour
{
    [SerializeField]
    private List<Transform>  targets;
    
    // Start is called before the first frame update
    void Start()
    {
        targets = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targets.Add(other.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        targets.Remove(other.transform);
    }
}
