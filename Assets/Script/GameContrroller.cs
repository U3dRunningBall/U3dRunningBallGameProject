using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContrroller : MonoBehaviour 
{
	private void Update()
    {
		if (Input.GetKeyDown(KeyCode.X))
			Application.LoadLevel(Application.loadedLevel);
    }
}
