using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _ABgroup
{
    public int GroupId;
    public float Chance;
    public List<System.Object> variables;
    //public System.Object variable2;
    public static _ABgroup GenerateTestABgroup()
    {
        var group = new _ABgroup();
        int coinsAdded = 100;
        Single coinsAdded2 = 100.0f;
        group.variables = new List<System.Object>();
        group.variables.Add(coinsAdded);
        group.variables.Add(coinsAdded2);
        return group;
    }
}
