using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoad_Map : MonoBehaviour {
    //地图需要的行数
    public int row = 5;
    //地图需要的列数
    public int col = 5;
    //地图需要的高
    public int height = 1;
    //格子之间的偏移
    public float offset = 10.0f;
    //地图直道预制体
    public GameObject StraightRoadPrefab;
    //地图弯道预制体
    public GameObject BendRoadPrefab;
    //地图上坡预制体
    public GameObject UphillPrefab;
    //生成地图的基准点
    public Vector3 originPoint;
    //地图的逻辑迷宫
    private string[,,] map;
    //地图的实际构造
    private GameObject[,,] _MAP;
    //储存目标点的容器 
    //private Stack<string[,,]> _moves = new Stack<string[,,]>();
    private Stack<string> _moves = new Stack<string>();

	void Start () {
        InitTerrain();

    }

    void InitTerrain()
    {
        //初始化逻辑地图
        map = new string[row, height, col];
        //初始化实际地图
        _MAP = new GameObject[row, height, col];
        //以xyz为基准点开始生成迷宫
        _moves.Push("");
        QueryRoad(0, 0, 0);


    }
    void QueryRoad(int x, int y, int z)
    { 
        string dirs = "";
        //检查左边的格子是否被访问
        if ((x - 1 >= 0-y) && map[x - 1, y, z]==null)
        {
            dirs += "L";
        }
        //检查右边的格子是否被访问
        if ((x + 1 < col+y) && map[x + 1, y, z] == null)
        {
            dirs += "R";
        }
        //检查下边的格子是否被访问
        if ((z - 1 >= 0-y) && map[x, y, z - 1] == null)
        {
            dirs += "D";
        }
        //检查上边的格子是否被访问
        if ((z + 1 < row+y) && map[x, y, z + 1] == null)
        {
            dirs += "U";
        }
        if(dirs=="")
        {
            //如果在该平面内四个方向都被堵住了
            if (y == height)
            {
                //如果高度达到设定的最高高度，则开始绘图
                DrawTerrain();
            }
            else
            {
                //如果没有达到最高高度，则从栈内调取最近的一个方向，上坡格的方向与最近格的方向相同
                string dir = _moves.Peek();
                switch (dir)
                {
                    case "L":
                        map[x, y, z] = "XL";
                        x = x - 1;
                        y = y + 1;
                        break;
                    case "R":
                        map[x, y, z] = "XR";
                        x = x + 1;
                        y = y + 1;
                        break;
                    case "U":
                        map[x, y, z] = "XU";
                        z = z + 1;
                        y = y + 1;
                        break;
                    case "D":
                        map[x, y, z] = "XD";
                        z = z - 1;
                        y = y + 1;
                           
                        break;
                }
                //QueryRoad(x, y, z);
                DrawTerrain();
            }

        }
        else
        {
            //这是一个在该平面可访问的点
            //  从dir里面随机一个方向
            int random = Random.Range(0, dirs.Length);
            char dir = dirs[random];
            string previous;
            if (_moves.Peek() =="")
            {
                switch (dir)
                {
                    //当方向是上下左右时，将该点标记为上下左右并存入栈中，递归下一个点
                    case 'L':
                        map[x, y, z] = "L";
                        _moves.Push("L");
                        x = x - 1;
                        break;
                    case 'R':
                        map[x, y, z] = "R";
                        _moves.Push("R");
                        x = x + 1;
                        break;
                    case 'U':
                        map[x, y, z] = "U";
                        _moves.Push("U");
                        z = z + 1;
                        break;
                    case 'D':
                        map[x, y, z] = "D";
                        _moves.Push("D");
                        z = z - 1;
                        break;
                    default:
                        Debug.Log(dir);
                        break;

                }
            }
            else
            {
                previous = _moves.Peek();


                //char previous = after[after.Length];
                if (previous != dir.ToString())
                {
                    switch (previous)
                    {
                        case "L":
                            switch (dir)
                            {
                                case 'U':
                                    map[x, y, z] = "LU";
                                    z = z + 1;
                                    _moves.Push("U");
                                    break;
                                case 'D':
                                    map[x, y, z] = "LD";
                                    z = z - 1;
                                    _moves.Push("D");
                                    break;
                            }
                            break;
                        case "R":
                            switch (dir)
                            {
                                case 'U':
                                    map[x, y, z] = "RU";
                                    z = z + 1;
                                    _moves.Push("U");
                                    break;
                                case 'D':
                                    map[x, y, z] = "RD";
                                    z = z - 1;
                                    _moves.Push("D");
                                    break;
                            }
                            break;
                        case "U":
                            switch (dir)
                            {
                                case 'L':
                                    map[x, y, z] = "UL";
                                    x = x - 1;
                                    _moves.Push("L");
                                    break;
                                case 'R':
                                    map[x, y, z] = "UR";
                                    x = x + 1;
                                    _moves.Push("R");
                                    break;
                            }
                            break;
                        case "D":
                            switch (dir)
                            {
                                case 'L':
                                    map[x, y, z] = "DL";
                                    x = x - 1;
                                    _moves.Push("L");
                                    break;
                                case 'R':
                                    map[x, y, z] = "DR";
                                    x = x + 1;
                                    _moves.Push("R");
                                    break;
                            }
                            break;
                    }

                }
                else
                {
                    switch (dir)
                    {
                        //当方向是上下左右时，将该点标记为上下左右并存入栈中，递归下一个点
                        case 'L':
                            map[x, y, z] = "L";
                            _moves.Push("L");
                            x = x - 1;
                            break;
                        case 'R':
                            map[x, y, z] = "R";
                            _moves.Push("R");
                            x = x + 1;
                            break;
                        case 'U':
                            map[x, y, z] = "U";
                            _moves.Push("U");
                            z = z + 1;
                            break;
                        case 'D':
                            map[x, y, z] = "D";
                            _moves.Push("D");
                            z = z - 1;
                            break;
                        default:
                            Debug.Log(dir);
                            break;

                    }
                }
            }
           

            //基于新的点寻找下一个点
            QueryRoad(x, y, z);
        }
    }
    void DrawTerrain()
    {
        for (int k = 0; k < height; k++)
        {
            for (int i = 0; i < col; i++)
            for (int j = 0; j < row; j++)
            {
                    string dir = map[i, k, j];
                    switch (dir)
                    {
                        case "R":
                            Instantiate(StraightRoadPrefab, new Vector3(j * offset, k * offset, i * offset), Quaternion.AngleAxis(0.0f, Vector3.up));
                            break;
                        case "L":
                            Instantiate(StraightRoadPrefab, new Vector3(j * offset, k * offset, i * offset), Quaternion.AngleAxis(180.0f, Vector3.up));
                            break;
                        case "U":
                            Instantiate(StraightRoadPrefab, new Vector3(j * offset, k * offset, i * offset), Quaternion.AngleAxis(270.0f, Vector3.up));
                            break;
                        case "D":
                            Instantiate(StraightRoadPrefab, new Vector3(j * offset, k * offset, i * offset), Quaternion.AngleAxis(90.0f, Vector3.up));
                            break;
                        case "XR":
                            Instantiate(UphillPrefab, new Vector3(j * offset, k * offset, i * offset), Quaternion.AngleAxis(0.0f, Vector3.up));
                            break;
                        case "XL":
                            Instantiate(UphillPrefab, new Vector3(j * offset, k * offset, i * offset), Quaternion.AngleAxis(180.0f, Vector3.up));
                            break;
                        case "XU":
                            Instantiate(UphillPrefab, new Vector3(j * offset, k * offset, i * offset), Quaternion.AngleAxis(270.0f, Vector3.up));
                            break;
                        case "XD":
                            Instantiate(UphillPrefab, new Vector3(j * offset, k * offset, i * offset), Quaternion.AngleAxis(90.0f, Vector3.up));
                            break;
                        case "RU":
                            Instantiate(BendRoadPrefab, new Vector3(j * offset, k * offset, i * offset), Quaternion.AngleAxis(180.0f,Vector3.up));
                            break;
                        case "RD":
                            Instantiate(BendRoadPrefab, new Vector3(j * offset, k * offset, i * offset), Quaternion.AngleAxis(270.0f,Vector3.up));
                            break;
                        case "LU":
                            Instantiate(BendRoadPrefab, new Vector3(j * offset, k * offset, i * offset), Quaternion.AngleAxis(90.0f,Vector3.up));
                            break;
                        case "LD":
                            Instantiate(BendRoadPrefab, new Vector3(j * offset, k * offset, i * offset), Quaternion.AngleAxis(0.0f,Vector3.up));
                            break;
                        case "UR":
                            Instantiate(BendRoadPrefab, new Vector3(j * offset, k * offset, i * offset), Quaternion.AngleAxis(180.0f,Vector3.up));
                            break;
                        case "UL":
                            Instantiate(BendRoadPrefab, new Vector3(j * offset, k * offset, i * offset), Quaternion.AngleAxis(90.0f,Vector3.up));
                            break;
                        case "DR":
                            Instantiate(BendRoadPrefab, new Vector3(j * offset, k * offset, i * offset), Quaternion.AngleAxis(270.0f,Vector3.up));
                            break;
                        case "DL":
                            Instantiate(BendRoadPrefab, new Vector3(j * offset, k * offset, i * offset), Quaternion.AngleAxis(0.0f,Vector3.up));
                            break;
                        default:
                            break;

                    }
            }
        }
    }

}
