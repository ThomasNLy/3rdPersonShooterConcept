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

    [Header("Gun/Ammo References")]
    public TextMeshProUGUI ammoCount;
    public ShootGun gun;
    public GameObject reloadProgressBarUI;
    private Slider reloadProgressBar;
    
    // Start is called before the first frame update
    void Start()
    {
        reloadProgressBarUI.SetActive(false);
        reloadProgressBar = reloadProgressBarUI.GetComponent<Slider>();
        reloadProgressBar.maxValue = gun.ReloadTime;
        reloadProgressBar.value = 0;
        

    }

    // Update is called once per frame
    void Update()
    {
        UIHealth.text = playerHealth.HealthPoints.ToString();
        SetHealth(playerHealth.HealthPoints);

        ammoCount.text = gun.MagazineCount.ToString() + "/" + gun.TotalAmmo.ToString();
      

        if (gun.reloading)
        {

          
            reloadProgressBarUI.SetActive(true);
            ReloadProgressBar();
           

        }
        else
        {

            reloadProgressBarUI.SetActive(false);
            reloadProgressBar.value = 0;
          
          

        }
        
    }


    public void SetHealth(int health)
    {
        healthBarSlider.value = health;
    }

    public void ReloadProgressBar()
    {
       reloadProgressBar.value +=  Time.deltaTime;
    }


}
