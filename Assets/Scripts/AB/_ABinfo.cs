using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _ABinfo
{
    public int ABTestId;
    public DateTime ABstartDate;
    public DateTime ABendDate;
    public List<_ABgroup> Groups;
    public static _ABinfo GenerateTestABinfo()
    {
        var info = new _ABinfo();
        info.Groups = new List<_ABgroup>();
        info.Groups.Add(_ABgroup.GenerateTestABgroup());
        info.Groups.Add(_ABgroup.GenerateTestABgroup());
        return info;
    }
}
