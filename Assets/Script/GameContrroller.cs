using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContrroller : MonoBehaviour {

    //public GameObject MainCamera;
    //public GameObject PlayerCamera;
    //public GameObject Player;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        //if(Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    MainCamera.active = true;
        //    PlayerCamera.active = false;
        //}
        //if(Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    MainCamera.active = false;
        //    PlayerCamera.active = true;
        //}
        
    }
    //void OnGUI()
    //{
    //    GUIStyle fontStyle = new GUIStyle();
    //    fontStyle.normal.background = null;    //设置背景填充
    //    fontStyle.normal.textColor = new Color(1, 0, 0);   //设置字体颜色
    //    fontStyle.fontSize = 40;       //字体大小
    //    if (flag1 == true)
    //    {
    //        GUI.Label(new Rect(Screen.width * 0.5f, Screen.height * 0.5f, 1000.0f, 300.0f), "you have arrived!", fontStyle);
    //    }
    //    if (flag2 == true)
    //    {
    //        GUI.Label(new Rect(Screen.width * 0.5f, Screen.height * 0.5f, 1000.0f, 300.0f), "gameover!\nPress X to restart", fontStyle);
    //    }
    //}
}
