using System;
using UnityEngine;


public class processdata : MonoBehaviour
{
    FetchData fd;
    public bool processed = false;
    public clientdata[] datas;
    int[] ids;
    public string defaultfilter;
    public int count;
    void Start()
    {
        fd = GetComponent<FetchData>();
        processed = false;
    }

    void Update()
    {
        if (fd.fetched && processed == false)
        {

            SimpleJSON.JSONNode clients = fd.stats["clients"];
            count = clients.Count;
            Array.Resize(ref datas, count);
            Array.Resize(ref ids, count);
            SimpleJSON.JSONNode temp;

            for (int i = 0; i < count; i++)
            {
                temp = clients[i];
                datas[i] = new clientdata();
                datas[i].ismanager = temp["isManager"].AsBool;
                datas[i].id = temp["id"].AsInt;
                datas[i].label = temp["label"];
                ids[i] = datas[i].id;
            }

            SimpleJSON.JSONNode data = fd.stats["data"];

            for (int i = 0; i < count; i++)
            {
                temp = data[ids[i].ToString()];
                if(temp !=null)
                {
                    datas[i].address = temp["address"];
                    datas[i].name = temp["name"];
                    datas[i].points = temp["points"].AsInt;
                }
            }

            defaultfilter = fd.stats["label"];
            processed = true;
        }
    }
}
