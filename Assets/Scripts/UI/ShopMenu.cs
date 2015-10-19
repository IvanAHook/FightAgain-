using UnityEngine;
using System.Collections;

public class ShopMenu : MonoBehaviour {

	public GameObject PopUpMoneyMenu; 

	public GameObject WeaponsItems;
	public GameObject ArmorItems;
	public GameObject HelmetsItems;
	public GameObject BootsItems;

	public Transform weapon1Slot;
	public Transform weapon2Slot;
	public Transform headSlot;
	public Transform bodySlot;
	public Transform feetSlot;

	UiItem weapon1E;
	UiItem weapon2E;
	UiItem headE;
	UiItem bodyE;
	UiItem feetE;

	UiItem selectedItem;

	public void SelectPress( GameObject item )
	{
		selectedItem = item.GetComponent<UiItem>();
		//Debug.Log( selectedItem.name );
	}

	public void BuyPress()
	{
		if( selectedItem == null ) return;

		int itemCost = selectedItem.GetItemCost();
		if( GameManager.instance.GetMoney() >= itemCost && !selectedItem.IsAquired() )
		{
			//Debug.Log( "bought: " + selectedItem.name );
			GameManager.instance.DecreaseMoney( itemCost );
			selectedItem.Aquire( true );
		}
		else if( selectedItem.IsAquired() )
		{
			EquipPress();
		}
		//Debug.Log ("pressedBuy");
	}

	public void EquipPress()
	{
		int id = selectedItem.GetItemId();
		if( selectedItem.GetType() == UiItem.Type.Equipment )
		{
			EquipmentData item = GameManager.equipmentList.GetEquipment( id );
			if( item.type == EquipmentData.Type.Head )
			{
				//GameManager.instance.head = item;
				GameManager.instance.head = GameManager.equipmentList.GetEquipment( 2 );
				headE = Instantiate( selectedItem, headSlot.position, Quaternion.identity ) as UiItem;
				headE.transform.parent = headSlot;
			}
			else if( item.type == EquipmentData.Type.Body )
			{
				//GameManager.instance.body = item;
				GameManager.instance.body = GameManager.equipmentList.GetEquipment( 11 );
				bodyE = Instantiate( selectedItem, bodySlot.position, Quaternion.identity ) as UiItem;
				bodyE.transform.parent = bodySlot;
			}
			else if( item.type == EquipmentData.Type.Feet )
			{
				//GameManager.instance.feet = item;
				GameManager.instance.feet = GameManager.equipmentList.GetEquipment( 19 );
				feetE = Instantiate( selectedItem, feetSlot.position, Quaternion.identity ) as UiItem;
				feetE.transform.parent = feetSlot;

			}
		}
		else if( selectedItem.GetType() == UiItem.Type.Weapon )
		{
			GameManager.instance.weapon = GameManager.equipmentList.GetWeapon( 4 );
			weapon1E = Instantiate( selectedItem, weapon1Slot.position, Quaternion.identity ) as UiItem;
			weapon1E.transform.parent = weapon1Slot;
		}
		//Debug.Log ("pressedEquip");
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


