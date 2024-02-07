using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ventanaDialogo : MonoBehaviour
{
    [SerializeField] public Text txt;
    //[SerializeField] public Focus focus;

    //[SerializeField] public GameObject pause;

    //[SerializeField] private GameObject cantMoveBooks;

    //private static bool[] _dialogsFocus = { false, false, false, false, false, true, true, false };
    // Start is called before the first frame update
    private Dialog _d;

    public void LoadDialogue(Dialog d)
    {
        _d = d;
        _tip = 0;
        //pause.gameObject.SetActive(true);
        //cantMoveBooks.gameObject.SetActive(true);
        gameObject.SetActive(true);
        NextTip();
    }
    void Start()
    {
        _tip = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static int _tip = 0;
    public static bool Loop = false;
    public static int GetTip()
    {
        return _tip;
    }

    public bool isInDialog()
    {
        return _tip != -2;
    }
    public void NextTip()
    {
        if (_tip < 0)
        {
            _tip = -2;
            gameObject.SetActive(false);
            //pause.SetActive(false);
            //cantMoveBooks.SetActive(false);
            return;
        }

        gameObject.SetActive(_d.GetText(_tip) != "");
        /*
        if (_d.GetPosition(_tip))
        {
            Vector2 pos = this.GetComponent<UnityEngine.UI.Image>().rectTransform.anchoredPosition;
            this.GetComponent<UnityEngine.UI.Image>().rectTransform.anchoredPosition = new Vector2(pos.x, 350);
        }
        else
        {
            Vector2 pos = this.GetComponent<UnityEngine.UI.Image>().rectTransform.anchoredPosition;
            this.GetComponent<UnityEngine.UI.Image>().rectTransform.anchoredPosition = new Vector2(pos.x, -350);
        }*/

        if (_d.GetAction(_tip) != null) _d.GetAction(_tip)();
        
        //focus.focusOn(_d.GetFocus(_tip));
        txt.text = _d.GetText(_tip);

        if (_tip < _d.GetLenth() - 1) _tip++;
        else _tip = -1;
    }
    
}
