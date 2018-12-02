using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballmove : MonoBehaviour {
	public bool flag1=false;
	public bool flag2=false;
	public GameObject thirdpersonplayer;
	public GameObject MainCamera;
	private Rigidbody ball_Rigidbody;
	public  float MoveForce;
	public float wait_time;
	public float wait_time1;
	public float jump_rate;
	private float nextjump;

	void Start () {
		ball_Rigidbody = thirdpersonplayer.GetComponent<Rigidbody> ();

	}
	

	void FixedUpdate ()
    {
        if (Input.GetKey(KeyCode.Space) && nextjump < Time.time)
        {
            nextjump = Time.time + jump_rate;
            ball_Rigidbody.AddForce(Vector3.up * 300, ForceMode.Force);
        }
        float h = Input.GetAxis ("Horizontal"); //左右
		float v=Input.GetAxis("Vertical");//前后

		Vector3 GlobalDirectionForward =MainCamera.transform.TransformDirection (Vector3.forward);
		Vector3 ForwardDirection = v * GlobalDirectionForward; //物体前后移动的方向
		Vector3 GlobalDirectionRight=MainCamera.transform.TransformDirection(Vector3.right);
		Vector3 RightDirection = h * GlobalDirectionRight;//物体左右移动的方向 
		Vector3 MainDirection = ForwardDirection + RightDirection;

		ball_Rigidbody.AddForce (MainDirection * MoveForce, ForceMode.Force); //施加力使物体移动
	
	}
    void OnGUI()
    {
        GUIStyle fontStyle = new GUIStyle();
        fontStyle.normal.background = null;    //设置背景填充
        fontStyle.normal.textColor = new Color(1, 0, 0);   //设置字体颜色
        fontStyle.fontSize = 40;       //字体大小
        if (flag1 == true)
        {
            GUI.Label(new Rect(Screen.width * 0.3f, Screen.height * 0.3f, 1000.0f, 300.0f), "you have arrived!", fontStyle);
        }
        if (flag2 == true)
        {
            GUI.Label(new Rect(Screen.width * 0.4f, Screen.height * 0.4f, 1000.0f, 300.0f), "gameover!\nPress x to restart", fontStyle);
        }
    }
    IEnumerator OnTriggerEnter(Collider other){    //踩到加速块时会使得速度变为原来的1.5倍 持续3秒
		if (other.tag == "speedup") {
			MoveForce = 12;
			yield return new WaitForSeconds (wait_time);
			MoveForce = 6;
		}
		if (other.tag == "slowdown") {
			MoveForce = 0;
			yield return new WaitForSeconds (wait_time1);
			MoveForce = 6;
		}
		if (other.tag == "destination") {
			flag1 = true;
			yield return new WaitForSeconds (0.1f);
		}
		if (other.tag == "fallen") {
			flag2 = true;
			yield return new WaitForSeconds (2.0f);
		}
	}
}

