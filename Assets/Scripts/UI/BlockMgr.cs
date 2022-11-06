using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockMgr : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject childBlock;
    public float longPressTime = 0.3f;
    static int index = 0;
    static int putCnt = 0;
    List<GameObject> childBlocks;
    Vector2 startPos;
    Vector2 startMousePos;
    float startClickTime;
    bool clickEnabled = true;
    bool flipY = true;
    RectTransform trans;
    bool bePut = false;
    bool firstUsed = true;


    public AudioSource music;
    public AudioClip rotateSound;
    public AudioClip pickSound;

    private void GenerateNextBlock()
    {
        firstUsed = false;
        BlockCtrl.instance.GenerateBlock();
    }

    private void CheckBefore()
    {
        if (bePut)
        {
            bePut = false;
            putCnt--;
            foreach (GameObject block in childBlocks)
                BoardMgr.instance.UpdateStates(block.transform.position, 0);
            BoardMgr.instance.UpdateDisabled();
        }
    }

    private void Check()
    {
        BoardMgr.instance.ClearStateGreen();
        if (RayCast())
        {
            foreach (GameObject block in childBlocks)
                BoardMgr.instance.UpdateStates(block.transform.position, 2);
        }
    }



    void CheckAfter()
    {
        if (RayCast())
        {
            trans.position = trans.position + BoardMgr.instance.CalculateAlignOffset(childBlocks[0].transform.position);
            bePut = true;
            putCnt++;
            if(putCnt == ConstData.piece_init.Length)
            {
                Debug.Log("Win!");
            }
            foreach (GameObject block in childBlocks)
                BoardMgr.instance.UpdateStates(block.transform.position, 1);
            BoardMgr.instance.UpdateDisabled();

            music.clip = pickSound;
            music.Play();
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (firstUsed)
        {
            GenerateNextBlock();
        }

        CheckBefore();

        clickEnabled = false;
        startPos = trans.position;
        startMousePos = eventData.position;
        Debug.Log("Drag begin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        trans.position = startPos + (eventData.position - startMousePos);
        Check();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        clickEnabled = true;
        CheckAfter();
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!clickEnabled) return;
        Debug.Log("Pointer down");
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
        music.clip = rotateSound;
        music.Play();
        CheckAfter();
    }

    // Start is called before the first frame update
    void Start()
    {
        int[,] shapedata = ConstData.piece_init[index++];
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
        music = gameObject.AddComponent<AudioSource>();
        music.playOnAwake = false;
        rotateSound = Resources.Load("sound/xiu") as AudioClip;
        pickSound = Resources.Load("sound/ze") as AudioClip;
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
