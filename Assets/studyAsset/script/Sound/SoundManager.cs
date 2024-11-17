using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {  get; private set; }
    private AudioSource audioSource;
    // Start is called before the first frame update
    [Header("Weapon Sounds")]
    public AudioClip circleWeaponSound;
    public AudioClip bombWeaponSound;
    public AudioClip BasicWeaponSound;
    
    public void PlaySound(AudioClip sound)
    {
        if (sound != null)
        {
            audioSource.PlayOneShot(sound);
        }
    }

    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
