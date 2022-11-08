using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Variabels")]
    [SerializeField]
    int health = 5;
    // Start is called before the first frame update
    void Start()
    {
        this.health = 5;   
    }

    public void TakeDamage(int damage)
    {
        if (this.health <= 0)
        {
            Destroy(this.gameObject);
        }
        this.health -= damage;
    }
}
