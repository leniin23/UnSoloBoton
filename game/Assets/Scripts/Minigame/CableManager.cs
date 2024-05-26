using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableManager : MonoBehaviour
{
    public static CableManager instance;
    private Cable[] cables;

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        cables = transform.GetComponentsInChildren<Cable>();
    }

    public void ResetCables()
    {
        foreach (var cable in cables)
        {
            cable.ResetCable();
        }
    }
}
