using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource smgSoundEffect;
    public AudioSource explosionSoundEffect;
    public AudioSource grenadeSoundEffect;
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySMGSoundEffect()
    {
        smgSoundEffect.Play();
    }
    public void PlayGrenadeSoundEffect()
    {
        grenadeSoundEffect.Play();
    }
    public void PlayExplosionSoundEffect()
    {
        explosionSoundEffect.Play();
    }
}
