using UnityEngine;
using System.Collections;

public class ShopMenu : MonoBehaviour {

	public GameObject PopUpMoneyMenu; 

	public GameObject WeaponsItems;
	public GameObject ArmorItems;
	public GameObject HelmetsItems;
	public GameObject BootsItems;

	UiItem selectedItem;

	public void SelectPress( GameObject item )
	{
		selectedItem = item.GetComponent<UiItem>();
		Debug.Log( selectedItem.name );
	}

	public void BuyPress ()
	{
		int itemCost = selectedItem.GetItemCost();
		if( GameManager.instance.GetMoney() >= itemCost && !selectedItem.IsAquired() )
		{
			Debug.Log( "bought: " + selectedItem.name );
			GameManager.instance.DecreaseMoney( itemCost );
			selectedItem.Aquire( true );
		}
		Debug.Log ("pressedBuy");
	}

	public void FightPress ()
	{
		//GameManager.instance.LoadArena();
		Application.LoadLevel("FirstTest"); //Remember to change the string here later.
	}

	public void BackButtonPress ()
	{
		//GameManager.instance.LoadMenu();
		Application.LoadLevel("MainMenu");
	}
	
	//Items Inventory Navigation Buttons Functionality
	public void WeaponsPress ()
	{
		WeaponsItems.SetActive (true);
		ArmorItems.SetActive (false); 
		HelmetsItems.SetActive (false);
		BootsItems.SetActive (false);
	}

	public void ArmorPress ()
	{
		WeaponsItems.SetActive (false);
		ArmorItems.SetActive (true); 
		HelmetsItems.SetActive (false);
		BootsItems.SetActive (false);
	}

	public void HelmetsPress ()
	{
		WeaponsItems.SetActive (false);
		ArmorItems.SetActive (false); 
		HelmetsItems.SetActive (true);
		BootsItems.SetActive (false);

	}

	public void BootsPress ()
	{
		WeaponsItems.SetActive (false);
		ArmorItems.SetActive (false); 
		HelmetsItems.SetActive (false);
		BootsItems.SetActive (true);

	}

	//Purchase Money Pop Up Menu Functionality
	public void PlusIconPress ()
	{
		PopUpMoneyMenu.SetActive (true);
	}

	public void ClosePopUpPress ()
	{
		PopUpMoneyMenu.SetActive (false); 
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

	public void MoveInHierarchy(GameObject myObject, int delta) 
	{
		int index = myObject.transform.GetSiblingIndex();
		myObject.transform.SetSiblingIndex (index + delta);
	}
}


