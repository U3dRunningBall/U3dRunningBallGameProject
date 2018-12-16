using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCreate : MonoBehaviour {
    //预制体 坡
    public GameObject Slope;
    //预制体 坑
    public GameObject Hollow;
    //预制体 障碍物1
    public GameObject Obstacle1;
    //预制体 障碍物2
    public GameObject Obstacle2;
    //生成基准点
	public GameObject goal;
    public Vector3 originPoint;
    //存放预制体的容器
    private List<float> gameObjects = new List<float>();
    //需要生成预制体的数量
    public int count = 30;
	void Start () {
        //for(int i=0;i<10;i++)
        //      {
        //          if(i%2==0)
        //          {
        //              Instantiate(gameObject1, originPoint + new Vector3(0, 0, i * 20), Quaternion.identity);
        //          }
        //          else
        //          {
        //              Instantiate(gameObject2, originPoint + new Vector3(0, 0, i * 20), Quaternion.AngleAxis(180.0f,Vector3.up));
        //          }

        //      }
        while (gameObjects.Count<=50)
        {
            AddObjects();
        }
        Create();
	}
    void AddObjects()
    {
        GameObject[] objects = new GameObject[4] { Slope, Hollow,Obstacle1,Obstacle2 };
        int ran = Random.Range(0, objects.Length);
        GameObject expert = objects[ran];
        if (expert == Slope)
        {
            gameObjects.Add(1.0f);
            gameObjects.Add(1.5f);
        }
        else if(expert==Hollow)
        {
            gameObjects.Add(2.0f);
        }
        else if(expert==Obstacle1)
        {
            gameObjects.Add(3.0f);
        }
        else if(expert==Obstacle2)
        {
            gameObjects.Add(4.0f);
        }
    }
    void Create()
    {
        for(int i=0; i<count;i++)
        {
            float x = gameObjects[i];
            if (x == 1.0f)
            {
                Instantiate(Slope, originPoint + new Vector3(0, 0, 20 + 20 * i), Quaternion.identity);
            }
            if (x == 1.5f)
            {
                Instantiate(Slope, originPoint + new Vector3(0, 0, 20 + 20 * i), Quaternion.AngleAxis(180.0f, Vector3.up));
            }
            if (x == 2.0f)
            {
                Instantiate(Hollow, originPoint + new Vector3(0, 0, 20 + 20 * i), Quaternion.identity);
            }
            if(x==3.0f)
            {
                Instantiate(Obstacle1, originPoint + new Vector3(0, 0, 20 + 20 * i), Quaternion.identity);
            }
            if(x==4.0f)
            {
                Instantiate(Obstacle2, originPoint + new Vector3(0, 0, 20 + 20 * i), Quaternion.identity);
            }
			if (i == count - 1) 
			{
				Instantiate(goal, originPoint + new Vector3(0, 0, 20 + 20 * i), Quaternion.identity);
			}
        }
    }


}
