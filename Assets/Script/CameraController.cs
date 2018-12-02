using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Transform obj;//跟随的物体
	public Vector3 objoffset = Vector3.zero;//偏移度
	public Transform target;

	public  float distance = 10.0f;  //与目标物体的距离
	public  float mindistance=2f;  //最小距离
	public  float maxDistance = 15f;  //最大距离
	public  float zoomSpeed=1f; 

	public  float xSpeed=250.0f;  //视角移动速度
	public  float ySpeed = 120.0f;

	public  bool allowYTilt=true; //Y轴视角允许与移动范围
	public  float yMinLimit=15.0f;
	public  float yMaxLimit=80.0f;

	private float x=0.0f;
	private float y=0.0f;

	private float targetX=0f;
	private float targetY=0f;
	private float targetDistance=0f;
	private float xVelocity=1f;
	private float yVelocity=1f;
	private float zoomVelocity=1f;

	void Start(){
		var angles = transform.eulerAngles;
		targetX = x = angles.x;
		targetY = y = ClampAngle (angles.y, yMinLimit, yMaxLimit);
		targetDistance = distance;
	}

	void LateUpdate(){
		if (obj) {
			float scroll = Input.GetAxis ("Mouse ScrollWheel");
			if (scroll > 0.0f)
				targetDistance -= zoomSpeed;
			else if (scroll < 0.0f)
				targetDistance += zoomSpeed;
			targetDistance = Mathf.Clamp (targetDistance, mindistance, maxDistance);

			//按住鼠标右键来控制视角变换或左ctrl和鼠标左键
			if (Input.GetMouseButton (1) || (Input.GetMouseButton (0) && (Input.GetKey (KeyCode.LeftControl) || Input.GetKey (KeyCode.RightControl)))) {
				targetX+=Input.GetAxis("Mouse X")*xSpeed*0.02f;
				if(allowYTilt){
					targetY -= Input.GetAxis ("Mouse Y") * ySpeed * 0.02f;
					targetY = ClampAngle (targetY, yMinLimit, yMaxLimit);
				}
			}
			x = Mathf.SmoothDampAngle (x, targetX, ref xVelocity, 0.3f);
			if (allowYTilt)
				y = Mathf.SmoothDampAngle (y, targetY, ref yVelocity, 0.3f);
			else
				y = targetY;
			Quaternion rotation = Quaternion.Euler (y, x, 0);
			distance = Mathf.SmoothDamp (distance, targetDistance, ref zoomVelocity, 0.5f);


			//视角面对跟随物体
			Vector3 position = rotation * new Vector3 (0.0f, 0.0f, -distance) + obj.position + objoffset;
			transform.rotation = rotation;
			transform.position = position;
		}
	}
	private float ClampAngle(float angle,float min,float max){
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}
	}










