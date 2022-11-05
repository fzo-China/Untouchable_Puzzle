using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCellCtrl : MonoBehaviour
{
    private GameObject parent;
    Vector3 beginPoint, beginPoint_center, beginPosition;
    private float width;
    private const int idle = -1, left = 0, right = 1, mid = 2;
    private int Mouse_state = idle;
    
    private int currentId;
    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponent<Transform>().parent.gameObject;
        width = GetComponent<Transform>().localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        currentId = int.Parse(parent.name.Substring(6));
        Mouse_state = left;
        beginPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        beginPoint_center = beginPoint + new Vector3(width*1.25f, width * 1.25f);
        beginPosition = parent.GetComponent<Transform>().localPosition;
    }

    private void OnMouseDrag()
    {
        if(Mouse_state == left)
        {
            Vector3 currentPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            parent.GetComponent<Transform>().localPosition = beginPosition + currentPoint - beginPoint;
        }
        else
        {
            beginPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            beginPoint_center = beginPoint + new Vector3(width * 1.25f, width * 1.25f);
            beginPosition = parent.GetComponent<Transform>().localPosition;
        }
    }

    private void OnMouseUp()
    {
        Mouse_state = idle;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(right))
        {
            parent.GetComponent<PieceCtrl>().flip();
        }
        float scrool = Input.GetAxis("Mouse ScrollWheel");
        if (scrool > 0.05)
        {
            for (int i = 0; i < 3; i++)
            {
                parent.GetComponent<PieceCtrl>().rotate();
            }
        }
        else if(scrool < -0.05)
        {
            parent.GetComponent<PieceCtrl>().rotate();
        }
    }

    private void OnMouseEnter()
    {
        
    }

    public void flip()
    {
        
        Vector3 oldPosition = GetComponent<Transform>().localPosition;
        Vector3 newPosition = Vector3.right * (2 - 2 * transform.localPosition.x) + transform.localPosition;
        beginPoint += newPosition - oldPosition;
        GetComponent<Transform>().localPosition = newPosition;
    }

    private void transport()
    {
        Transform transform = GetComponent<Transform>();
        Vector3 oldPosition = transform.localPosition;
        Vector3 newPosition = new Vector3(transform.localPosition.y, transform.localPosition.x, transform.localPosition.z);
        beginPoint += newPosition - oldPosition;
        GetComponent<Transform>().localPosition = newPosition;
    }

    public void rotate()
    {
        flip();
        transport();
    }

}
