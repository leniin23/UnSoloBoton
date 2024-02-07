using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngranajeComponent : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 0.0f, speed, Space.World);
    }
}
