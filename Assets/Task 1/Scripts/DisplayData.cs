using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayData : MonoBehaviour
{
    processdata pd;
    [SerializeField]GameObject rowprefab;
    public GameObject[] rows;
    [SerializeField]GameObject grid;
    public bool displayed = false;
    popup pp;

    void Start()
    {
       pd = GetComponent<processdata>();
       pp = GetComponent<popup>();
       displayed = false;
    }

    void Update()
    {
       if(pd.processed && displayed == false)
       {
            Array.Resize(ref rows, pd.count);
            for(int i=0;i<pd.count;i++)
            {
                rows[i] = Instantiate(rowprefab, grid.transform);
                rows[i].name = i.ToString();
                rows[i].GetComponent<Button>().onClick.AddListener(pp.popupcall);
                rows[i].transform.Find("label").GetComponent<TMP_Text>().text = pd.datas[i].label;
                rows[i].transform.Find("points").GetComponent<TMP_Text>().text = pd.datas[i].points.ToString();
            }
            displayed = true;
        }
    }

    
}
