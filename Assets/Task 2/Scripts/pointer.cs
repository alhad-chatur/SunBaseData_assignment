using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class pointer : MonoBehaviour
{
    List<GameObject> todestroy = new List<GameObject>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            transform.position = GetInputPosition();
        }

        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            doraycast();
        }
        if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            foreach (GameObject x in todestroy)
                x.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() => x.SetActive(false));
            todestroy.Clear();
        }
    }
    void doraycast()
    {
        Vector2 rayOrigin = GetInputPosition();
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("circle"))
        {
            todestroy.Add(hit.transform.gameObject);
        }
    }
    private Vector3 GetInputPosition()
    {
        if (Input.touchCount > 0)
        {
            return Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
        else
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
