using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHollowSphere : MonoBehaviour
{
    private Vector3 centerPos;    //你围绕那个点 就用谁的角度
    private float radius = 3;     //物理离 centerPos的距离
    private float angle = 0;      //偏移角度  

    void Start()
    {
        CreateSphere();
    }


    public void CreateSphere()
    {
        centerPos = transform.position;
        //20度生成一个圆
        for (int i = 0; i < 1000; i++)
        {
            Vector3 p = Random.insideUnitSphere * radius;
            Vector3 pos = p.normalized * (2 + p.magnitude);

            GameObject obj1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //设置物体的位置Vector3三个参数分别代表x,y,z的坐标数  
            obj1.transform.position = pos;
        }
    }
}
/*------------------ 
作者：BuladeMian
来源：CSDN
原文：https://blog.csdn.net/BuladeMian/article/details/81112135 
版权声明：本文为博主原创文章，转载请附上博文链接！*/