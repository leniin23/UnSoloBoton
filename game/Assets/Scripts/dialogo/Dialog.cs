using System;
using UnityEngine.UI;

public class Dialog
{
    private string[] _texts;
    private bool[] _top;
    private Image[] _focus;
    private Action[] _actions;

    public int GetLenth()
    {
        return _texts.Length;
    }
        
    public string GetText(int i)
    {
        return i < _texts.Length ? _texts[i] : "";
    }
    public bool GetPosition(int i)
    {
        return i < _top.Length && _top[i];
    }
    public Image GetFocus(int i)
    {
        return i < _focus.Length ? _focus[i] : null;
    }
    public Action GetAction(int i)
    {
        return i < _actions.Length ? _actions[i] : null;
    }
    public Dialog(string[] t, bool[] p, Image[] f, Action[] a)
    {
        _texts = t;
        _top = p;
        _focus = f;
        _actions = a;
    }
    
}