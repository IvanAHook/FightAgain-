using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour {


	/* Main Menu Buttons //Not sure if needed
	public Button fightButton;
	public Button shopButton;
	public Button optionsButton;
	public Button muteButton;
	public Button creditsButton;
	public Button quitButton;*/

	public Canvas pauseMenuCanvas;
	public Canvas deathScreenCanvas;
	bool paused = false;

	public GameObject CreditsScreenHolder; 
	public GameObject BackButton; 
	public GameObject FightButton;
	public GameObject ShopButton;
	public GameObject OptionsButton;
	public GameObject MuteButton;
	public GameObject CreditsButton;

	void Awake()
	{
		if (pauseMenuCanvas != null)
		{
			pauseMenuCanvas.enabled = false;
		}

		if (deathScreenCanvas != null)
		{
			deathScreenCanvas.enabled = false;
		} 
		
	}

	void Update() // Probably temp solution
	{
		// Wait for Pause input.
		if (Input.GetKeyDown(KeyCode.P))
		{
			PausePress();
		}
	}

	public void FightPress () 
	{
		Application.LoadLevel("FirstTest"); //Remember to change the string here later.
	}

	public void ShopPress()
	{
		Debug.Log("Go to shop");
		Application.LoadLevel("StoreMenu");
	}

	public void OptionsPress()
	{
		Debug.Log("Go to options");
	}

	public void MutePress()
	{
		Mute();
	}

	void Mute()
	{
		Debug.Log("Mute/Unmute");
	}

	public void CreditsPress()
	{
		Debug.Log("Go to Credits"); 
		CreditsScreenHolder.SetActive(true); 
		BackButton.SetActive (true); 
		FightButton.SetActive (false);
		ShopButton.SetActive (false);
		OptionsButton.SetActive (false);
		MuteButton.SetActive (false);
		CreditsButton.SetActive (false);

	}

	public void BackButtonPress()
	{
		Debug.Log ("pressedBackButton");
		CreditsScreenHolder.SetActive(false);  
		BackButton.SetActive (false); 
		FightButton.SetActive (true);
		ShopButton.SetActive (true);
		OptionsButton.SetActive (true);
		MuteButton.SetActive (true);
		CreditsButton.SetActive (true);
	}

	public void PausePress()
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

	public void ExitPress()
	{
		Time.timeScale = 1;
		Application.LoadLevel("MainMenu");
	}

	public void RetryPress()
	{
		Time.timeScale = 1;
		Application.LoadLevel("FirstTest"); // Temp
	}

	public void DeathScreen ()
	{
		deathScreenCanvas.enabled = true;
		if (Time.timeScale == 1)
		{
			Time.timeScale = 0;
		}
	}
	


	
	
}
