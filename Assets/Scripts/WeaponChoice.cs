using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChoice : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas weaponCanvas; 
    public Button Button1;
    public Button Button2;
    public PlayerController playercontroller;

    void Start()
    {
       /* Button1.onClick.AddListener(() => SelectWeapon("Weapon"));
        Button2.onClick.AddListener(() => SelectWeapon("Missile"));*/

        
        weaponCanvas.gameObject.SetActive(false);

        InvokeRepeating("ShowWeaponCanvas", 10f, 10f);
    }

    void ShowWeaponCanvas()
    {
        if (weaponCanvas.gameObject.activeSelf) return;

        Time.timeScale = 0;
        weaponCanvas.gameObject.SetActive(true); 
        Debug.Log("Weapon Selection Time!");
    }

    void SelectWeapon()
    {

    }
}
