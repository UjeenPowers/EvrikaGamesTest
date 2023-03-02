using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentAdder : MonoBehaviour
{
    public GameObject ABinfoPrefab;
    public void ShowABsInfos(List<ABinstance> item)
    {
        ClearContents();
        foreach(var info in item)
        {
            var newEntry = GameObject.Instantiate(ABinfoPrefab,this.transform);
            var transfomrForLookup = newEntry.transform;
            transfomrForLookup.Find("ABid").GetComponent<Text>().text = info.ABTestId.ToString();
            transfomrForLookup.Find("EndDate").GetComponent<Text>().text = info.ABendDate.ToString();
            transfomrForLookup.Find("GroupNumber").GetComponent<Text>().text = info.GroupSelected.GroupId.ToString();
        }
    }
    public void ClearContents()
    {
        foreach(Transform item in transform)
        {
            GameObject.Destroy(item.gameObject);
        }
    }
}
