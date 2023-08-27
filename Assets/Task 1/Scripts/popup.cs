using DG.Tweening;
using System;
using System.Collections;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class popup : MonoBehaviour
{
    // Start is called before the first frame update
    processdata pd;
    [SerializeField]GameObject popupwindow;
    CanvasGroup cg;
    TMP_Text cname;
    TMP_Text cpoints;
    TMP_Text caddress;
    public float fadetime = 0.25f;
    WaitForSeconds fadewait;
    void Start()
    {
        pd = GetComponent<processdata>();
        cname = popupwindow.transform.Find("name").GetComponent<TMP_Text>();
        cpoints = popupwindow.transform.Find("points").GetComponent<TMP_Text>();
        caddress = popupwindow.transform.Find("address").GetComponent<TMP_Text>();
        cg = popupwindow.GetComponent<CanvasGroup>();
        fadewait = new WaitForSeconds(fadetime);
    }

    public void popupcall()
    {
        string id = EventSystem.current.currentSelectedGameObject.name;
        popupwindow.SetActive(true);
        cg.alpha = 0.0f;
        cg.DOFade(1.0f, fadetime).SetEase(Ease.InSine);
        cname.text = pd.datas[Int16.Parse(id)].name;
        cpoints.text = pd.datas[Int16.Parse(id)].points.ToString();
        caddress.text = pd.datas[Int16.Parse(id)].address.ToString();
    }

    public void popupclose()
    {
        StartCoroutine(ppclosecoroutine());
    }

    IEnumerator ppclosecoroutine()
    {
        cg.DOFade(0.0f, fadetime).SetEase(Ease.InSine);
        yield return fadewait;
        popupwindow.SetActive(false);
    }

}
