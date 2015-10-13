﻿using UnityEngine;
using System.Collections;

public class ShopMenu : MonoBehaviour {

	public GameObject PopUpMoneyMenu; 
	private bool isShowing; 

	public void BuyPress ()
	{
		Debug.Log ("pressedBuy");
	}

	public void FightPress ()
	{
		Application.LoadLevel("FirstTest"); //Remember to change the string here later.
	}

	public void PlusIconPress ()
	{
		Debug.Log ("pressedPlusIcon");
		isShowing = !isShowing;
		PopUpMoneyMenu.SetActive (isShowing); 

	}

	public void WeaponsPress ()
	{
		Debug.Log ("pressedWeapons");
	}

	public void ArmorPress ()
	{
		Debug.Log ("pressedArmor");
	}

	public void HelmetsPress ()
	{
		Debug.Log ("pressedHelmets");
	}

	public void BootsPress ()
	{
		Debug.Log ("pressedBoots");
	}
}

