using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestTelefono : MonoBehaviour
{
    [SerializeField] float rotCore;
    float falseTimer = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    //--------------------------------------POSSIBLE UPGRADE-------------------------------//
    // private IEnumerable<WaitForSeconds> ChangeDirection()
    // {
    //     yield return new WaitForSeconds(0.1f);
    //     rotCore = -rotCore;
    // }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 0.0f, rotCore, Space.World);
        falseTimer += 0.2f;
        if (falseTimer > 5f)
        {
            rotCore = -rotCore;
            falseTimer = 0;
        }
    }
}
