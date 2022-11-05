using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCtrl : MonoBehaviour
{
    public GameObject cell;
    private const int unused = 0, used = 1;
    public int[,] data;//´æ·ÅÍø¸ñ×´Ì¬
    private GameObject[,] cells;
    private GameObject[] pieces;
    private int row = ConstData.BG_height[2], vol = ConstData.BG_width[2];
    private float cell_width, cell_height;

    // Start is called before the first frame update
    void Start()
    {
        dateInit();
        createBgGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dateInit()
    {
        cell_width = cell.GetComponent<Transform>().lossyScale.x;
        cell_height = cell.GetComponent<Transform>().lossyScale.y;
        data = new int[row, vol];
        cells = new GameObject[row, vol];
        pieces = new GameObject[11];
        
    }

    public void createBgGrid()
    {
        GameObject bg = GameObject.Find("BG");
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < vol; j++)
            {
                cells[i, j] = Object.Instantiate(cell, new Vector3(cell_width * (i - row / 2.0f), cell_height * (j - vol / 2.0f), 0), Quaternion.identity, bg.GetComponent<Transform>());
                CellCtrl cellCtrl = cells[i, j].GetComponent<CellCtrl>();
                cellCtrl.setId(i, j);
                cellCtrl.setColor(ConstData.Color_Unused);
            }
        }
    }

    public bool isCorrectPosition()
    {

        return true;
    }

    public Vector3 getAlignOffset()
    {
        Vector3 offset = Vector3.zero;

        return offset;
    }

}
