using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableManager : MonoBehaviour
{
    public static CableManager instance;

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    public void ResetCables()
    {
        var cables = transform.GetComponentsInChildren<Cable>();
        foreach (var cable in cables)
        {
            cable.ResetCable();
        }
    }
}
