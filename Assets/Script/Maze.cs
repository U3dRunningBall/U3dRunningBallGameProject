using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {
    //生成地图需要的行数
    public int row = 5;
    //生成地图需要的列数
    public int column = 10;
    //生成地图的基准点
    private Vector3 originPoint;
    //格子之间的偏移
    public float offset;
    //地面格子预设
    public GameObject floorPrefab;
    //墙壁格子预设
    public GameObject wallPrefab;
    //陷阱格子预设
    public GameObject trapPrefab;
    //陷阱数量
    public int traps = 3;
    //迷宫的逻辑地图
    private int[,] _maze;
    //根据逻辑地图生成的实际地图
    private GameObject[,] _map;
    //储存目标点的容器
    private List<Vector2> _moves = new List<Vector2>();
    //储存陷阱点的容器
    private List<int> trap = new List<int>();
    //随机生成陷阱参数
    private int Randomtrapset;
    //迷宫生成点
    public GameObject MazePoint;
	// Use this for initialization
	void Start () {
        originPoint += MazePoint.transform.position;
        Randomtrapset = (row * column) / 2 + 1;
        InitTerrain();
	}
   

    void InitTerrain()
    {
        //初始化逻辑地图
        _maze = new int[row, column];
        //初始化实际地图
        _map = new GameObject[row, column];
        //以（x，y）为基准点开始查找目标点生成迷宫
        QueryRoad(0, 0);
    }
    void QueryRoad(int r,int c)
    {
        string dirs = "";
        //检查北面的格子是否被访问
        if ((r - 2 >= 0) && (_maze[r - 2, c] == 0))
            dirs += "N";
        //检查西面的格子是否被访问
        if ((c - 2 >= 0) && (_maze[r, c-2] == 0))
            dirs += "W";
        //检查南面的格子是否被访问
        if ((r + 2 <row) && (_maze[r + 2, c] == 0))
            dirs += "S";
        //检查东面的格子是否被访问
        if ((c + 2 <column) && (_maze[r , c + 2] == 0))
            dirs += "E";
        if (dirs == "")
        {
            //删除栈顶的格子
            _moves.RemoveAt(_moves.Count - 1);
            if (_moves.Count == 0)
            {
                //如果容器空了。说明迷宫生成完毕
                DrawTerrain();
            }
            else
            {
                //如果容器没空，则基于新的点，继续查找下一个目标点
                QueryRoad((int)_moves[_moves.Count - 1].x, (int)_moves[_moves.Count - 1].y);
            }
        }
        else
        {
            //这是一个可以被访问的点
            int ran = Random.Range(0, dirs.Length);
            char dir = dirs[ran];
            //连通目标点和当前点之间的点
            switch(dir)
            {
                case 'E':
                    //将两点中间的点设置为已访问
                    _maze[r, c + 1] = 1;
                    c = c + 2;
                    break;
                case 'W':
                    //将两点中间的点设置为已访问
                    _maze[r, c - 1] = 1;
                    c = c - 2;
                    break;
                case 'N':
                    //将两点中间的点设置为已访问
                    _maze[r-1, c ] = 1;
                    r = r - 2;
                    break;
                case 'S':
                    //将两点中间的点设置为已访问
                    _maze[r+1, c ] = 1;
                    r = r + 2;
                    break;
            }
            //然后将这个新点设为已访问的
            _maze[r, c] = 1;
            //将这个新的目标点加入容器
            _moves.Add(new Vector2(r, c));
            //基于新的点，递归寻找下一个目标点
            QueryRoad(r, c);
        }
    }
    void DrawTerrain()
    {
        int count = 0;
       
        for(int x=0;x<traps;x++)
        {
            trap.Add(Random.Range(2, Randomtrapset));
        }
        //int[] trap=new int[] {3,8,11 };
        //trap = new int[traps];
        //for (int x = 0; x < traps; x++)
        //{
        //    trap[x] = Random.Range(1, Randomtrapset);
        //}
        for (int i=0;i<row;i++)
        {
            for (int j = 0; j < column; j++)
            {
                switch(_maze[i,j])
                {
                    case 1:
                        count = NewMethod(count);
                        if (_map[i,j]!=null)
                        {
                            if (_map[i, j].tag == "Floor")
                            {
                                
                                continue;
                            }
                            else if (_map[i, j].tag == "Wall")
                            {
                                Destroy(_map[i, j]);
                                _map[i, j] = null;
                            }
                            
                        }
                        if(trap.Contains(count)==true)
                        {
                            _map[i, j] = Instantiate(trapPrefab, originPoint + new Vector3(j * offset, 0, i * offset), Quaternion.identity);
                        }
                        else
                        {
                            _map[i, j] = Instantiate(floorPrefab, originPoint + new Vector3(j * offset, 0, i * offset), Quaternion.identity);
                        }
                        //for (int y = 0; y < traps; y++)
                        //{
                        //    if (count == trap[y])
                        //    {
                        //        _map[i, j] = Instantiate(trapPrefab, originPoint + new Vector3(j * offset, 0, i * offset), Quaternion.identity);
                        //        break;
                        //    }
                        //    else
                        //    {
                        //        _map[i, j] = Instantiate(floorPrefab, originPoint + new Vector3(j * offset, 0, i * offset), Quaternion.identity);
                        //        break;
                        //    }
                        //}
                        //生成地板，Quaternion.identity即为Quaternion（0,0,0,0）
                        
                        break;
                    case 0:
                        if (_map[i, j] != null)
                        {
                            if (_map[i, j].tag == "Wall")
                            {
                                continue;
                            }
                            if(_map[i,j].tag=="Floor")
                            {
                                Destroy(_map[i, j]);
                                _map[i, j] = null;
                            }
                        }
                        //生成墙壁，Quaternion.identity即为Quaternion（0,0,0,0）
                        _map[i, j] = Instantiate(wallPrefab, originPoint + new Vector3(j * offset,0, i * offset), Quaternion.identity);
                        break;
                }
            }
        }
    }

    private static int NewMethod(int count)
    {
        count = count + 1;
        return count;
    }
}
