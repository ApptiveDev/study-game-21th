using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectPool
{
    // Get Obj From ObjPool
    GameObject GetObject();
    // Return
    void ReturnObject(GameObject obj);
}
