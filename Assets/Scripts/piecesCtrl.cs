using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piecesCtrl : MonoBehaviour
{
    private int length = 5;
    public GameObject cell;
    private float cell_width, cell_height;
    private GameObject[] pieces = new GameObject[11];
    private MyMatrix[] myMatrixs = new MyMatrix[11];
    // Start is called before the first frame update
    void Start()
    {
        cell_width = cell.GetComponent<Transform>().lossyScale.x;
        cell_height = cell.GetComponent<Transform>().lossyScale.y;
        for (int i = 0; i < 11; i++)
        {
            myMatrixs[i] = new MyMatrix(length, ConstData.piece_init[i]);
            pieces[i] = GameObject.Find("Pieces" + i);
            createPiece(i);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void createPiece(int id)
    {
        int[,] matrix = myMatrixs[id].getMatrix();
        int size = myMatrixs[id].getSize();
        for (int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                if(matrix[i,j] == 1)
                {
                    GameObject gameObject = Instantiate(cell, Vector3.zero, Quaternion.identity, pieces[id].GetComponent<Transform>());
                    gameObject.GetComponent<Transform>().localPosition = new Vector3(cell_width * i, cell_height * j, 0);
                    pieces[id].GetComponent<PieceCtrl>().addCell(gameObject);
                }
            }
        }
    }



    //·­×ª×ÔÉí
    public void filp(int id)
    {
        myMatrixs[id].flip();
    }

}
