using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCtrl : MonoBehaviour
{
    private GameObject[] cells = new GameObject[6];
    private int cnt = 0;
    private int state_idle = 0, state_flip = 1, state_rotate = 2, state;
    private const int error = 0, correct = 1;
    private int Cell_state = error;
    // Start is called before the first frame update
    void Start()
    {
        state = state_idle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void flip()
    {
        if(state == state_idle)
        {
            state = state_flip;
            foreach (GameObject gameObject in cells)
            {
                gameObject.GetComponent<PieceCellCtrl>().flip();
            }
        }
        state = state_idle;
    }

    public void rotate()
    {
        if (state == state_idle)
        {
            state = state_rotate;
            foreach (GameObject gameObject in cells)
            {
                gameObject.GetComponent<PieceCellCtrl>().rotate();
            }
        }
        state = state_idle;
        
    }

    public void addCell(GameObject c)
    {
        cells[cnt++] = c;
    }

    public void align()
    {

    }
}
