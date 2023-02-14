using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    Keyboard keyboard;
    
    Gun[] guns = new Gun[2];

    Gun currentGun;

    public Gun primary;
    public Gun secondary;
    // Start is called before the first frame update
    void Start()
    {
        guns[0] = primary;
        guns[1] = secondary;
        currentGun = guns[0];
        keyboard = Keyboard.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (keyboard.digit1Key.isPressed)
        {
            currentGun = guns[0];
        }
        else if (keyboard.digit2Key.isPressed)
        {
            currentGun = guns[1];
        }
        
    }
}
