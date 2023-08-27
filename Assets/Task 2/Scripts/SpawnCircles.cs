using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCircles : MonoBehaviour
{
    [SerializeField] GameObject circle;
    bool tospawn;
    int circlecount;
    float ar;
    float orthosize;
    List<GameObject> circles = new List<GameObject>();
    Vector3 initialscale;
    void Start()
    {
        tospawn = true;
        orthosize = Camera.main.orthographicSize;
        circlecount = Random.RandomRange(5, 10);
        initialscale = circle.transform.localScale;
    }

    void Update()
    {
        if(tospawn == true)
        {
            ar = Screen.width/Screen.height;
            for(int i=0;i<circlecount;i++)
            {
                circles.Add(Instantiate(circle));
                circles[i].transform.position = new Vector2(Random.Range(-orthosize*ar/1.1f,orthosize*ar/1.1f),Random.Range(-orthosize/1.1f,orthosize/1.1f));
                circles[i].transform.localScale = Vector3.zero;
                circles[i].transform.DOScale(initialscale, 0.25f).SetEase(Ease.OutBack);
            }
            tospawn = false;
        }   
    }
    public void restart()
    {
        StartCoroutine(waitforrestart());
    }

    IEnumerator waitforrestart()
    {
        foreach (GameObject x in circles)
        {
           x.transform.DOScale(Vector3.zero,0.5f).SetEase(Ease.InBack).OnComplete(() => Destroy(x));
        }
        yield return new WaitForSeconds(0.5f);
        tospawn = true;
        circles = new List<GameObject>();
        Start();
    }
}
