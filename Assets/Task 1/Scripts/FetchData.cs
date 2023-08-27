using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class clientdata
{
    public string label;
    public int id;
    public bool ismanager;
    public string name;
    public int points;
    public string address;

    public clientdata()
    {
        label = "null";
        id = -1;
        ismanager = false;
        name = "null";
        points = -1;
        address = "null";
    }
}

public class FetchData : MonoBehaviour
{
    public string URL = "https://qa2.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";
    public bool fetched;
    public SimpleJSON.JSONNode stats;

    void Start()
    {
        fetched = false;
        StartCoroutine(getdata());
    }

    IEnumerator getdata()
    {
        
        using (UnityWebRequest request = UnityWebRequest.Get(URL))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
                Debug.LogError(request.error);
            
            else
            {
                string json = request.downloadHandler.text;
                stats = SimpleJSON.JSON.Parse(json);
                fetched = true;   
            }
        }

    }
}
