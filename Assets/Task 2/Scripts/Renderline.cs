using UnityEngine;

public class Renderline : MonoBehaviour
{
    LineRenderer lr;
    Vector2 prevpos;
    
    void Start()
    {
        lr = GetComponent<LineRenderer>();    
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)||(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            createline();
        }
        
        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            Vector2 tempfingerpos = GetInputPosition();
            if (Vector2.Distance(tempfingerpos, prevpos) > 0.05f)
            {
                updateline(tempfingerpos);
            }

        }
        if(Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            lr.enabled = false;
        }
    }


    void createline()
    {
        lr.enabled = true;
        lr.positionCount = 2;
        Vector2 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        prevpos = temp;
        lr.SetPosition(0, temp);
        lr.SetPosition(1, temp);
    }
    void updateline(Vector2 newfingerpos)
    {
        prevpos = newfingerpos;
        lr.positionCount++;
        lr.SetPosition(lr.positionCount - 1, newfingerpos);
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
