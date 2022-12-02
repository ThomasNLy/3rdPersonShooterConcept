using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI UIHealth;
    public Health playerHealth;
    public Slider healthBarSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UIHealth.text = playerHealth.HealthPoints.ToString();
        SetHealth(playerHealth.HealthPoints);
    }


    public void SetHealth(int health)
    {
        healthBarSlider.value = health;
    }
}
