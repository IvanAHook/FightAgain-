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
	
	
	public void FightPress () 
	{
		Application.LoadLevel("FirstTest"); //Remember to change the string here later.
	}

	public void ShopPress()
	{
		Debug.Log("Go to shop");
	}

	public void OptionsPress()
	{
		Debug.Log("Go to options");
	}

	public void MutePress()
	{
		Debug.Log("Mute Game");
	}

	public void CreditsPress()
	{
		Debug.Log("Go to Credits");
	}
	
	
}
