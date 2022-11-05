using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockMgr : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public int[,] shapedata = new int[3, 3]{
        {0, 0, 0 },
        {0, 1, 1},
        {1, 0, 0}
    };
    public GameObject childBlock;
    public float longPressTime = 0.3f;

    List<GameObject> childBlocks;
    Vector2 startPos;
    Vector2 startMousePos;
    float startClickTime;
    bool clickEnabled = true;
    bool flipY = true;
    RectTransform trans;
    bool bePut = false;
    public void OnBeginDrag(PointerEventData eventData)
    {
        CheckBefore();

        clickEnabled = false;
        startPos = trans.position;
        startMousePos = eventData.position;
    }

    
    private void CheckBefore()
    {
        if (bePut)
        {
            bePut = false;
            foreach (GameObject block in childBlocks)
                BoardMgr.instance.UpdateStates(block.transform.position, 0);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        trans.position = startPos + (eventData.position - startMousePos);
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        clickEnabled = true;
        CheckAfter();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!clickEnabled) return;
        CheckBefore();
        startClickTime = Time.time;
        startMousePos = eventData.position;
        startPos = trans.position;
    }



    public void OnPointerUp(PointerEventData eventData)
    {
        if (!clickEnabled) return;
        if (Time.time - startClickTime > longPressTime)
        {
            Vector2 offset = Vector2.Scale(2 * (startMousePos - startPos), flipY ? Vector2.up : Vector2.right);
            
            //Vector2 offset = Vector2.zero;
            trans.localScale = new Vector3(1, -trans.localScale.y, 1);
            trans.position = startPos + offset;
        }
        else
        {
            flipY = !flipY;
            trans.RotateAround(startMousePos, Vector3.forward, 90);

        }
        CheckAfter();
    }

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<RectTransform>();
        childBlocks = new List<GameObject>();
        Vector3 factor = Vector3.Scale(new Vector3(1 / 18f, 1 / 18f, 0), new Vector3(Screen.height, Screen.height, 0));
        childBlock.GetComponent<RectTransform>().sizeDelta = factor;
        for (int i = 0; i < shapedata.GetLength(0); i++)
        {
            for (int j = 0; j < shapedata.GetLength(1); j++)
            {
                if(shapedata[i, j] == 1)
                {
                    Vector3 pos = new Vector3(j, i, 0);
                    childBlocks.Add(Instantiate(childBlock,  transform.position + Vector3.Scale(pos, factor), Quaternion.identity, transform));
                }
            }
        }
    }

    void CheckAfter()
    {
        if (RayCast())
        {
            trans.position = trans.position + BoardMgr.instance.CalculateAlignOffset(childBlocks[0].transform.position);
            bePut = true;
            foreach (GameObject block in childBlocks)
                BoardMgr.instance.UpdateStates(block.transform.position, 1);
        }
    }

    bool RayCast()
    {
        foreach (GameObject block in childBlocks)
        {
            bool isAvailable = BoardMgr.instance.IsAvailable(block.transform.position);
            if (!isAvailable)
                return false;
        }
        return true;
    }
}
