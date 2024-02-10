using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour
{
    public static StarScript instance;
    // Start is called before the first frame update
    private int life = 10;
    [SerializeField] private float halfStarSize;
    [SerializeField] private Transform starColors;
    void Start()
    {
        halfStarSize = starColors.localScale.x*1.019f;
        if (instance == null) instance = this;
        else Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnNext(int value)
    {
        Debug.Log("Value: " + value);
        if (value > 2) value = 1;
        life -= value;
        var size = starColors.localScale;
        var fullStarOffset = life % 2 != 0 ? 0.005 : 0f;
        starColors.position -= starColors.right * (halfStarSize * value);
        /*if (life >= 0)
        {
            starColors.position += Vector3.back;
        }
        else
        {
            size.x = (fullSize / life) * 10;
            starColors.localScale = size;
            //starColors.bounds.size = size;
        }*/
    }
}
