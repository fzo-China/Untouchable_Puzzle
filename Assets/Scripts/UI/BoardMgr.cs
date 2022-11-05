using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardMgr : MonoBehaviour
{
    public int[] size = new int[2] { 12, 12 };
    public GameObject grid;
    GameObject[,] grids;
    int[,] states;
    // Start is called before the first frame update

    public static BoardMgr instance;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    void Start()
    {
        grids = new GameObject[size[0], size[1]];
        states = new int[size[0], size[1]];
        Vector3 factor = Vector3.Scale(new Vector3(1 / 18f, 1 / 18f, 0), new Vector3(Screen.height, Screen.height, 0));
        grid.GetComponent<RectTransform>().sizeDelta = factor;
        Vector3 startPos = new Vector3(-(size[0] - 1) / 2f, -(size[1] - 1) / 2f, 0);
        for (int i = 0; i < size[0]; i++)
        {
            for (int j = 0; j < size[1]; j++)
            {
                Vector3 pos = startPos + new Vector3(i, j, 0);
                grids[i, j] = Instantiate(grid, transform.position + Vector3.Scale(pos, factor), Quaternion.identity, transform);
            }
        }
    }

    public bool IsAvailable(Vector2 pos)
    {
        int[] index;
        if(PosToIndex(pos, out index))
        {
            Debug.Log((index[0], index[1]));
            return states[index[0], index[1]] == 0;
        }
        return false;
    }

    public Vector3 CalculateAlignOffset(Vector3 pos)
    {
        int[] index;
        PosToIndex(pos, out index);
        return grids[index[0], index[1]].transform.position - pos;
        
    }

    public void UpdateStates(Vector2 pos, int state)
    {
        int[] index;
        PosToIndex(pos, out index);
        states[index[0], index[1]] = state;
        if(state == 0)
        {
            grids[index[0], index[1]].GetComponent<Image>().color = Color.white;
        }
        else if(state == 1)
        {
            grids[index[0], index[1]].GetComponent<Image>().color = Color.red;
        }
    }

    bool PosToIndex(Vector2 pos, out int[] index)
    {
        Vector2 sizeDelta = grid.GetComponent<RectTransform>().sizeDelta;
        index = new int[2];
        index[0] = Mathf.FloorToInt((pos.x - grids[0, 0].transform.position.x) / sizeDelta.x + 0.5f);
        index[1] = Mathf.FloorToInt((pos.y - grids[0, 0].transform.position.y) / sizeDelta.y + 0.5f);
        if (index[0] < 0 || index[1] < 0 || index[0] >= size[0] || index[1] >= size[1])
        {
            return false;
        }

        return true;
    }


}
