using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{

    [SerializeField]
    private int baseValue = 0;

    private List<int> modifiers = new List<int>();
    
    //nao faço ideia
    public int GetValue()
    {

        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;

    }

    // nao faço ideia
    public void AddModifier (int modifier)
    {

        if(modifier != 0)
        {

            modifiers.Add(modifier);

        }

    }

    //nao faço ideia
    public void RemoveModifier(int modifier)
    {

        if (modifier != 0)
        {

            modifiers.Remove(modifier);

        }

    }

}
