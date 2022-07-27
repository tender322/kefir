using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Counter
{
    public int count {  get;private set; }
    private Text _text;

    public Counter(Text _text)
    {
        this._text = _text;
        GlobalEventManager.EnemyKill.AddListener(addCount);
        UpdateText();
    }

    void addCount()
    {
        count++;
        UpdateText();
    }

    void UpdateText()=> _text.text = count.ToString();

   
}
