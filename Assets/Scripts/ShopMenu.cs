using UnityEngine;
using System.Collections;

public class ShopMenu : MonoBehaviour {

	public GameObject PopUpMoneyMenu; 
	public GameObject WeaponsItems;
	public GameObject ArmorItems;
	public GameObject HelmetsItems;
	public GameObject BootsItems;
	
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
		PopUpMoneyMenu.SetActive (isShowing = true); 

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

	public void ClosePopUpPress ()
	{
		Debug.Log ("pressedCloseIcon");
		isShowing = !isShowing;
		PopUpMoneyMenu.SetActive (isShowing = false); 
		 

	}

	public void SmallMoneyPress ()
	{
		Debug.Log ("pressedSmallMoney");
	}

	public void MediumMoneyPress ()
	{
		Debug.Log ("pressedMediumMoney");
	}

	public void BigMoneyPress ()
	{
		Debug.Log ("pressedBigMoney");
	}
}


