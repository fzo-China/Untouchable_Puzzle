public class MyMatrix//∑Ω’Û
{
    public int[,] matrix;
    private int size;

    public MyMatrix(int n = 5)
    {
        matrix = new int[n, n];
        size = n;
    }

    public MyMatrix(int n, int[,] m)
    {
        matrix = new int[n, n];
        matrix = m;
        size = n;
    }

    public static MyMatrix operator *(MyMatrix m1, MyMatrix m2)
    {
        int n = m1.getSize();
        int[,] matrix1 = m1.matrix, matrix2 = m2.matrix, matrix3 = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                for (int k = 0; k < n; k++)
                {
                    matrix3[i, j] += matrix1[i, k] * matrix2[k, j];
                }
            }
        }
        return new MyMatrix(n,matrix3);
    }

    //æÿ’Û◊™÷√
    public MyMatrix transport()
    {
        for(int i = 0; i < size; i++)
        {
            for(int j = i+1; j < size; j++)
            {
                int t = matrix[i, j];
                matrix[i, j] = matrix[j, i];
                matrix[j, i] = t;
            }
        }
        return this;
    }

    //æÿ’Û∑≠◊™
    public MyMatrix flip()
    {
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size / 2; j++)
            {
                int t = matrix[i, j];
                matrix[i, j] = matrix[i, size - 1 - j];
                matrix[i, size - 1 - j] = t;
            }
        }
        return this;
    }

    //À≥ ±’Î–˝◊™
    public MyMatrix rotate()
    {
        transport();
        flip();
        return this;
    }

    public void setMatrix(int[,] m)
    {
        matrix = m;
    }

    public int getSize()
    {
        return size;
    }

    public int[,] getMatrix()
    {
        return matrix;
    }

    public string toString()
    {
        string s = "";
        for (int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                s = string.Concat(s, matrix[i,j]," ");
            }
            s += "\n";
        }
        return s;
    }

    
}
