using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstData : MonoBehaviour
{
    public enum CellState { unused, used };
    public static Color Color_Unused = Color.white;
    public static Color Color_Used = Color.grey;
    public static int[] BG_width = { 5,5,12}, BG_height = { 5,5,12};

    //11Ƭƴͼ�ĳ�ʼ��̬
    public static int[][, ] piece_init = new int[][,]{
        new int[5,5]{//0
            {0,0,1,0,0},
            {0,0,1,0,0},
            {0,0,1,1,0},
            {0,0,0,1,0},
            {0,0,0,1,0}
        },
        new int[5,5]{//1
            {0,0,1,1,0},
            {0,0,1,0,0},
            {0,1,1,0,0},
            {0,0,1,0,0},
            {0,0,0,0,0}
        },
        new int[5,5]{//2
            {0,1,0,0,0},
            {0,1,1,1,0},
            {0,0,1,0,0},
            {0,0,1,0,0},
            {0,0,0,0,0}
        },
        new int[5,5]{//3
            {0,1,1,1,0},
            {0,0,1,0,0},
            {0,0,1,0,0},
            {0,0,1,0,0},
            {0,0,0,0,0}
        },
        new int[5,5]{//4
            {0,0,1,0,0},
            {0,0,1,1,0},
            {0,1,1,0,0},
            {0,1,0,0,0},
            {0,0,0,0,0}
        },
        new int[5,5]{//5
            {0,0,0,1,0},
            {0,0,1,1,0},
            {0,1,1,0,0},
            {0,1,0,0,0},
            {0,0,0,0,0}
        },
        new int[5,5]{//6
            {0,0,1,0,0},
            {0,0,1,1,0},
            {0,1,1,0,0},
            {0,0,1,0,0},
            {0,0,0,0,0}
        },
        new int[5,5]{//7
            {0,1,1,0,0},
            {0,0,1,0,0},
            {0,0,1,1,0},
            {0,0,0,1,0},
            {0,0,0,0,0}
        },
        new int[5,5]{//8
            {0,1,1,0,0},
            {0,0,1,1,0},
            {0,0,1,0,0},
            {0,0,1,0,0},
            {0,0,0,0,0}
        },
        new int[5,5]{//9
            {0,0,1,0,0},
            {0,1,1,1,0},
            {0,0,1,0,0},
            {0,0,1,0,0},
            {0,0,0,0,0}
        },
        new int[5,5]{//10
            {0,1,1,0,0},
            {0,0,1,0,0},
            {0,0,1,0,0},
            {0,0,1,1,0},
            {0,0,0,0,0}
        }
    };
}
