using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class ABmanager
{
    private static string CurrentDeviceAbsPath = "/DeviceAB.json";
    private Main Main;
    private List<_ABinfo> DeviseABsinfo;
    private Dictionary<int,ABinstance> CurrentABs;
    public ABmanager(Main main)
    {
        //This is makeshift initialization, in a propper application it should be handled differently. Used for the sake of simplicity of test project. 
        Main = main;
        LoadCurrentABs();
    }
    public void UpdateAbs(string jsonString)
    {
        DeviseABsinfo = JsonConvert.DeserializeObject<List<_ABinfo>>(jsonString);
        LoadABs();
    }
    public void RerollABs()
    {
        CurrentABs.Clear();
        LoadABs();
    }
    public Dictionary<int,ABinstance> GetFinishedABs()
    {
        var returnDict = new Dictionary<int,ABinstance>();
        foreach(var item in CurrentABs)
        {
            if (item.Value.ABendDate > System.DateTime.Now)
            {
                returnDict.Add(item.Key,item.Value);
            }
        }
        foreach(var item in returnDict.Keys)
        {
            CurrentABs.Remove(item);
        }
        return returnDict;
    }
    public Dictionary<int,ABinstance> GetActiveABs() //Only after GetFinishedABs
    {
        return CurrentABs;
    }
    private void LoadABs()
    {
        foreach(var _ABinfo in DeviseABsinfo)
        {
            if (_ABinfo.ABstartDate < System.DateTime.Now && !CurrentABs.ContainsKey(_ABinfo.ABTestId))
            {
                var contentEntry = new ABinstance();
                contentEntry.ABTestId = _ABinfo.ABTestId;
                contentEntry.ABendDate = _ABinfo.ABendDate;
                contentEntry.GroupSelected = GetRandomWeightedGroup(_ABinfo.Groups);
                if (contentEntry.GroupSelected != null) CurrentABs.Add(contentEntry.ABTestId,contentEntry);
            }
        }

        SaveCurrentABs();

        Main.ShowcaseABs(CurrentABs.Values.ToList()); //Just for representation of test project
    }
    private void SaveCurrentABs()
    {
        string jsonString = JsonConvert.SerializeObject(CurrentABs);
        string path = Application.persistentDataPath + CurrentDeviceAbsPath;
        File.WriteAllText(path,jsonString);
    }
    private void LoadCurrentABs() //Init ABs currently running on App
    {
        string path = Application.persistentDataPath + CurrentDeviceAbsPath;
        if (File.Exists(path))
        {
            var jsonString = File.ReadAllText(path);
            CurrentABs = JsonConvert.DeserializeObject<Dictionary<int,ABinstance>>(jsonString);
            Main.ShowcaseABs(CurrentABs.Values.ToList());
        }
        else CurrentABs = new Dictionary<int,ABinstance>();
    }
    private _ABgroup GetRandomWeightedGroup(List<_ABgroup> list)
    {
        double ratioSum = 0;
        foreach(var item in list)
        {
            ratioSum += item.Chance;
        }
        double fulSum = Random.Range(0f,1f) * ratioSum;
        foreach(var item in list)
        {
            fulSum -= item.Chance;
            if (fulSum <=0) return item;
        }
        Debug.LogError("ABinfo without groups spotted, contact game designers");
        return null;
    }
}
