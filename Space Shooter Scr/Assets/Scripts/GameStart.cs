using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStart : MonoBehaviour {

	void Update ()
	{
		if (Input.GetKeyDown ("return"))
		{
			SceneManager.LoadScene ("Main");
		}
	}
}
