using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip ArrowSound;
    [SerializeField] private AudioClip GuardSound;
    [SerializeField] private AudioClip IceSound;

    private AudioSource audioSource;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayWeaponSound(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Arrow:
                PlaySound(ArrowSound);
                break;
            case WeaponType.IceStaff:
                PlaySound(IceSound);
                break;
            case WeaponType.Guard:
                PlaySound(GuardSound);
                break;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
    public enum WeaponType
    {
        Arrow,
        IceStaff,
        Guard
    }

}
