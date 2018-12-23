using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballmove : MonoBehaviour {
	private bool flag1=false;
	private bool flag2=false;
	public GameObject thirdpersonplayer;
	public GameObject MainCamera;
	private Rigidbody ball_Rigidbody;
	public  float MoveForce;
	public float speed_up_wait;
	public float slow_down_wait;
	public float jump_rate;
	public float jump_force;
	public float bash_rate;
	public float bash_force;
	private float nextjump;
	private float nextbash;
	private bool isShow;  //问题的显示
	private string str="50*48*49=?";  //问题
	public int tmp=10; //倒计时
	void Start () {
		ball_Rigidbody = thirdpersonplayer.GetComponent<Rigidbody> ();
	}
	void FixedUpdate ()
	{
		if (Input.GetKey(KeyCode.Space) && nextjump < Time.time)
		{
			nextjump = Time.time + jump_rate;
			ball_Rigidbody.AddForce(Vector3.up * jump_force, ForceMode.Force);
		}
		if (Input.GetKey(KeyCode.B) && nextbash < Time.time)
		{
			nextbash = Time.time + bash_rate;
			ball_Rigidbody.AddForce(Vector3.forward * bash_force, ForceMode.Force);
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
		GUIStyle fontStyle = new GUIStyle ();
		GUIStyle QuestionStyle = new GUIStyle ();
		fontStyle.normal.background = null;
		fontStyle.normal.textColor = new Color (1, 0, 0);
		QuestionStyle.normal.textColor = new Color (1, 1, 0); 
		QuestionStyle.fontSize = 40;
		fontStyle.fontSize = 50;   
		GUIStyle tips = new GUIStyle ();
		tips.normal.background = null;
		tips.normal.textColor = new Color (1, 0, 0);  
		tips.fontSize = 20;       //字体大小
		GUI.Label (new Rect (Screen.width * 0.05f, Screen.height * 0.25f, 10.0f, 3.0f), "方向键——移动\n空格——跳跃\nB——冲刺\n距终点距离：" + ((600 - ball_Rigidbody.position.z) > 0 ? (600 - ball_Rigidbody.position.z) : 0).ToString ("0.0") + "\n目前速度：" + ball_Rigidbody.velocity.z.ToString ("0.0"), tips);
		if (flag1 == true)
			GUI.Label (new Rect (Screen.width * 0.4f, Screen.height * 0.4f, 1000.0f, 300.0f), "到达终点!", fontStyle);
		if (flag2 == true)
			GUI.Label (new Rect (Screen.width * 0.4f, Screen.height * 0.4f, 1000.0f, 300.0f), "游戏失败!\n按 x键 重新开始！", fontStyle);
		if (isShow == true && tmp > 0) {
			GUI.Label (new Rect (Screen.width * 0.04f, Screen.height * 0.05f, 1000.0f, 1000.0f), "问题:" + str, QuestionStyle); //踩到问题块后产生问题
			GUI.Label (new Rect (Screen.width * 0.70f, Screen.height * 0.05f, 1000.0f, 1000.0f), "倒计时:" + tmp, QuestionStyle);
			GUI.Label (new Rect (Screen.width * 0.04f, Screen.height * 0.15f, 1000.0f, 1000.0f), "Q:117600  E:127600", tips); //踩到问题块后产生问题
			if (Input.GetKey (KeyCode.Q))
				GUI.Label (new Rect (Screen.width * 0.5f, Screen.height * 0.6f, 1000.0f, 1000.0f), "正确！", QuestionStyle);
			if (Input.GetKey (KeyCode.E)) {
				GUI.Label (new Rect (Screen.width * 0.5f, Screen.height * 0.6f, 1000.0f, 1000.0f), "错误！", QuestionStyle);
				ball_Rigidbody.AddForce (Vector3.left * (-200), ForceMode.Force); //施加力使物体移动
			}
		}
	}
	IEnumerator OnTriggerEnter(Collider other)//踩到加速块时会使得速度变为原来的1.5倍 持续3秒
	{
		if (other.tag == "speedup") 
		{
			MoveForce *= 1.5f;
			yield return new WaitForSeconds (speed_up_wait);
			MoveForce /= 1.5f;
		}
		if (other.tag == "slowdown") 
		{
			MoveForce /= 2;
			yield return new WaitForSeconds (slow_down_wait);
			MoveForce *= 2;
		}
		if (other.tag == "destination") 
		{
			flag1 = true;
			yield return new WaitForSeconds (0.1f);
		}
		if (other.tag == "fallen") 
		{
			flag2 = true;
			yield return new WaitForSeconds (2.0f);
		}
		if (other.tag == "question") {   //问题块
			isShow = true;
			while (isShow == true) 
			{
				yield return new WaitForSeconds (1);
				tmp--;
			}
		}
	}
}
