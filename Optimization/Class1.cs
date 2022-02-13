using Optimization;

class Matrix
{
  private short RowCount { get; }
  private short ColCount { get; }

  private readonly StructType _type;
  private readonly int[,] _intMatrix;
  private readonly Vector[,] _vectorMatrix;
  private readonly Complex[,] _complexMatrix;

  public Matrix(short rows, short cols, StructType type)
  {
    RowCount = rows;
    ColCount = cols;
    _type = type;

    switch (type)
    {
      case StructType.Integer:
        var intList = new int[100, 100];
        for (int i = 0; i < 100; i++)
        {
          for (int j = 0; j < 100; j++)
          {
            intList[i, j] = 0;
          }
        }
        _intMatrix = intList;
        break;
      
      case StructType.Vector:
        var pointList = new Vector[100, 100];
        for (int i = 0; i < 100; i++)
        {
          for (int j = 0; j < 100; j++)
          {
            pointList[i, j] = new Vector(0, 0);
          }
        }
        _vectorMatrix = pointList;
        break;
      
      case StructType.Complex:
        var complexList = new Complex[100, 100];
        for (int i = 0; i < 100; i++)
        {
          for (int j = 0; j < 100; j++)
          {
            complexList[i, j] = new Complex(0, 0);
          }
        }
        _complexMatrix = complexList;
        break;
    }
  }
  
  public Matrix Multiply(Matrix matrix)
  {
    var result = new Matrix(RowCount, ColCount, _type);

    for (int i = 0; i < RowCount; i++)
    for (int j = 0; j < matrix.ColCount; j++)
    for (int k = 0; k < matrix.RowCount; k++)
    {
      if (_type == StructType.Integer)
        result._intMatrix[i, j] += _intMatrix[i, k] * matrix._intMatrix[k, j];
      else if (_type == StructType.Vector)
      {
        int x1 = _vectorMatrix[i, k].X;
        int x2 = matrix._vectorMatrix[k, j].X;
        int y1 = _vectorMatrix[i, k].Y;
        int y2 = matrix._vectorMatrix[k, j].Y;

        result._vectorMatrix[i, j].X += x1 * x2;
        result._vectorMatrix[i, j].Y += y1 * y2;
      }
      else
      {
        double r1 = _complexMatrix[i, k].Re;
        double r2 = matrix._complexMatrix[k, j].Re;
        double i1 = _complexMatrix[i, k].Im;
        double i2 = matrix._complexMatrix[k, j].Im;

        result._complexMatrix[i, j].Re = r1 * r2;
        result._complexMatrix[i, j].Im = i1 * i2;
      }
    }
    return result; 
  }
}

class Vector
{
  public int X { get; set; }
  public int Y { get; set; }

  public Vector(int x, int y)
  {
    X = x;
    Y = y;
  }
}

class Complex
{
  public double Re { get; set; }
  public double Im { get; set; }

  public Complex(double r, double i)
  {
    Re = r;
    Im = i;
  }
}
