using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Focus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void focusOn( Image image)
    {
        if (image == null)
        {
            this.gameObject.SetActive(false);
            return;
        }
        this.gameObject.SetActive(true);
        Vector2 pos = image.GetComponent<UnityEngine.UI.Image>().rectTransform.anchoredPosition;
        this.GetComponent<UnityEngine.UI.Image>().rectTransform.anchoredPosition = new Vector2(pos.x, pos.y);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
