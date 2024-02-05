using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enchufe : MonoBehaviour
{
    [SerializeField] private Color color;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        var enchufe = other.transform.parent.GetComponent<Cable>();
        if (enchufe.GetColor().Compare(color))
        {
            enchufe.Connect(transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
