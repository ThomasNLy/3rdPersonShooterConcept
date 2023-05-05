using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    Keyboard keyboard;
    Mouse mouse;
    
    public Gun[] guns = new Gun[2];

    public Gun currentWeapon;

   
    // Start is called before the first frame update
    private void Awake()
    {
        //guns[0] = primary;
        //guns[1] = secondary;
        currentWeapon = guns[0];
        guns[1].gameObject.SetActive(false);
        keyboard = Keyboard.current;
        mouse = Mouse.current;
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
        if (keyboard.digit1Key.isPressed)
        {
            currentWeapon = guns[0];
            guns[0].gameObject.SetActive(true);
            guns[1].gameObject.SetActive(false);
        }
        else if (keyboard.digit2Key.isPressed)
        {
            currentWeapon = guns[1];
            guns[0].gameObject.SetActive(false);
            guns[1].gameObject.SetActive(true);
        }
        //currentWeapon.Shoot();
        currentWeapon.FireRateCoolDown();


        if (mouse.leftButton.isPressed && currentWeapon.MagazineCount > 0)
        {
            currentWeapon.Shoot();
           
        }
        else if (currentWeapon.MagazineCount == 0 && mouse.leftButton.isPressed && currentWeapon.TotalAmmo != 0)
        {
            currentWeapon.reloading = true;
            currentWeapon.Invoke("Reload", currentWeapon.ReloadTime);
        }


      

        if (Keyboard.current.rKey.isPressed && currentWeapon.MagazineCount < currentWeapon.MagazineSize)
        {
            currentWeapon.reloading = true;
            currentWeapon.Invoke("Reload", currentWeapon.ReloadTime);

        }

    }
}
