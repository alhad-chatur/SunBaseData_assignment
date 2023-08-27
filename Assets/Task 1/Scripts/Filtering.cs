using DG.Tweening;
using TMPro;
using UnityEngine;

public class Filtering : MonoBehaviour
{
    processdata pd;
    DisplayData dd;
    bool filterchange = true;
    string currfilter;
    [SerializeField] TMP_Dropdown dropdown;
    void Start()
    {
        pd = GetComponent<processdata>();
        dd = GetComponent<DisplayData>();
        filterchange = true;
        currfilter = "null";
    }

    void Update()
    {
        if (dd.displayed && filterchange)
        {
            if (currfilter == "null")
                currfilter = pd.defaultfilter;

            if (currfilter == "Managers Only")
            {
                for (int i = 0; i < pd.count; i++)
                {
                    if (pd.datas[i].ismanager == false)
                        dd.rows[i].SetActive(false);
                    else if (pd.datas[i].ismanager == true)
                        dd.rows[i].SetActive(true);
                }
                dropdown.value = 1;
            }
            else if (currfilter == "All Clients")
            {
                for (int i = 0; i < pd.count; i++)
                    dd.rows[i].SetActive(true);

                dropdown.value = 0;
            }
            else if (currfilter == "Non managers")
            {
                for (int i = 0; i < pd.count; i++)
                {
                    if (pd.datas[i].ismanager == false)
                        dd.rows[i].SetActive(true);
                    else if (pd.datas[i].ismanager == true)
                        dd.rows[i].SetActive(false);
                }
                dropdown.value = 2;
            }
            filterchange = false;

            for (int i = 0; i < pd.count; i++)
            {
                RectTransform rowTransform = dd.rows[i].GetComponent<RectTransform>();

                if (dd.rows[i].activeSelf)
                {
                    rowTransform.localScale = Vector3.zero;
                    rowTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
                }
                else
                {
                    int currentIndex = i;

                    rowTransform.DOScale(Vector3.zero, 0.5f)
                        .SetEase(Ease.InBack)
                        .OnComplete(() => DeactivateRow(currentIndex)); 
                }
            }

        }
    }

    void DeactivateRow(int index)
    {
        if (index >= 0 && index < dd.rows.Length)
        {
            dd.rows[index].SetActive(false);
            dd.rows[index].GetComponent<RectTransform>().localScale = Vector3.one; 
        }
    }

    public void tochangefilter()
    {
        if (dropdown.value == 0)
            currfilter = "All Clients";
        else if (dropdown.value == 1)
            currfilter = "Managers Only";
        else
            currfilter = "Non managers";
        filterchange = true;
    }
}