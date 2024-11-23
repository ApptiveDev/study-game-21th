using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss : MonoBehaviour
{
    private void Start()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        StartCoroutine(AuraRoutine());
    }

    IEnumerator AuraRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(3f);
            transform.GetChild(1).gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
