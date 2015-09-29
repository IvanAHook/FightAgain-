using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	public Canvas pauseMenuCanvas;


	bool paused = false;



	void Awake ()
	{
		pauseMenuCanvas.enabled = false;
	}
	
	void Update () 
	{
		// Wait for Pause input.
		if ( Input.GetKeyDown( KeyCode.P ) )
		{
			PausePress();
		}
	}

	public void PausePress ()
	{
		paused = !paused;

		if (paused)
		{
			Time.timeScale = 0;
			pauseMenuCanvas.enabled = true;
		}
		else
		{
			Time.timeScale = 1;
			pauseMenuCanvas.enabled = false;
		}
	}

	public void ExitPress ()
	{
		Time.timeScale = 1;
		Application.LoadLevel("MainMenu");
	}

	public void MutePress ()
	{
		Mute();
	}

	void Mute ()
	{
		Debug.Log("Mute/Unmute");
	}

	public void OptionsPress ()
	{
		Debug.Log("Go to options");
	}
}
