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
            return states[index[0], index[1]] == 0 || states[index[0], index[1]] == 2;
        }
        return false;
    }

    public Vector3 CalculateAlignOffset(Vector3 pos)
    {
        int[] index;
        PosToIndex(pos, out index);
        return grids[index[0], index[1]].transform.position - pos;
        
    }

    public void ClearStateGreen()
    {
        for (int i = 0; i < size[0]; i++)
        {
            for (int j = 0; j < size[1]; j++)
            {
                if(states[i, j] == 2){
                    UpdateStates(i, j, 0);
                }
            }
        }
    }

    public void UpdateDisabled()
    {
        int[] ox = new int[8] { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] oy = new int[8] { -1, 0, 1, -1, 1, -1, 0, 1 };
        for (int i = 0; i < size[0]; i++)
        {
            for (int j = 0; j < size[1]; j++)
            {
                if(states[i, j] == 3)
                {
                    UpdateStates(i, j, 0);
                }
                if (states[i, j] != 1)
                {
                    for(int k = 0; k < 8; k++)
                    {
                        int x = i + ox[k];
                        int y = j + oy[k];
                        if (x >= 0 && x < size[0] && y >= 0 && y < size[1] && states[x, y] == 1)
                        {
                            UpdateStates(i, j, 3);
                            break;
                        }
                    }
                }
            }
        }
    }

    private void UpdateStates(int x, int y, int state)
    {
        states[x, y] = state;
        if (state == 0)
        {
            grids[x, y].GetComponent<Image>().color = Color.white;
        }
        else if (state == 1)
        {
            grids[x, y].GetComponent<Image>().color = Color.red;
        }
        else if (state == 2)
        {
            grids[x, y].GetComponent<Image>().color = Color.green * 0.5f;
        }
        else if (state == 3)
        {
            grids[x, y].GetComponent<Image>().color = Color.grey;
        }
    }

    public void UpdateStates(Vector2 pos, int state)
    {
        int[] index;
        PosToIndex(pos, out index);
        UpdateStates(index[0], index[1], state);
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
