using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Main : MonoBehaviour
{
    private const string path = "C:/Users/Ujeen/Desktop/testAB";
    public string testUri;
    public ABmanager ABmanager {get; private set;}
    public ContentAdder ContentAdder {get; private set;}
    private Dictionary<int, ABinstance> RunningABs;
    void Start()
    {
        //I understand it's bad practices, i just don't want to build proper outline for AB module. 
        ContentAdder = GameObject.Find("Content").GetComponent<ContentAdder>();
        InitABsystem();
    }
    public void ShowcaseABs(List<ABinstance> ABs)
    {
        ContentAdder.ShowABsInfos(ABs);
    }
    private void InitABsystem()
    {
        ABmanager = new ABmanager(this);

        StartCoroutine(GetRequest(testUri));
    }
    private void CheckForABs() //Here for simplicity
    {
        ABmanager.GetFinishedABs(); //Return Dictionary of ABs that started on this device and finished due to time limit. We can react accordingly, removes finished ABs from active ABs list.
        RunningABs = ABmanager.GetActiveABs();   //Return Dictionary of active ABs that started on this device and are still running. We can dp smth with it as needed.
    }
    IEnumerator GetRequest(string uri)
    {
        Debug.Log("started coroutine");
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    //Handle non-responsive server here, as of now just make the app use storred AB settings.
                    CheckForABs();
                    ShowcaseABs(RunningABs.Values.ToList());
                    break;
                case UnityWebRequest.Result.Success:
                    ABmanager.UpdateAbs(webRequest.downloadHandler.text);
                    CheckForABs();
                    break;
            }
        }
    }
}
