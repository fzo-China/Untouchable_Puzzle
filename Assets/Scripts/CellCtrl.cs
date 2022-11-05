using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellCtrl : MonoBehaviour
{
    // Start is called before the first frame update

    private int id_x, id_y;
    private ConstData.CellState state = ConstData.CellState.unused;

    void Load()
    {

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setId(int i, int j)
    {
        id_x = i;
        id_y = j;
    }

    public void setColor(Color color)
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;
        
    }
}
