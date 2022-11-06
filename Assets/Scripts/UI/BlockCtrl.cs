using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCtrl : MonoBehaviour
{
    public static BlockCtrl instance;
    public int index = 0;
    public GameObject block;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    private void Start()
    {
        GenerateBlock();
    }

    public void GenerateBlock()
    {
        if(index < ConstData.piece_init.Length)
        {
            Instantiate(block, transform);
            index++;
        }
    }
}
