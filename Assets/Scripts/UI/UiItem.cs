using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UiItem : MonoBehaviour {

	public enum Type { Equipment, Weapon };

	Type type;
	public int itemId;
	public int cost;
	bool aquired;

	public int GetItemId()
	{
		return itemId;
	}

	public int GetItemCost()
	{
		return cost;
	}

	public UiItem.Type GetType()
	{
		return type;
	}

	public bool IsAquired()
	{
		return aquired;
	}

	public void Aquire( bool a )
	{
		aquired = a;
		if( aquired == true )
		{
			Button button = GetComponent<Button>();
			ColorBlock colorBlock = button.colors;
			colorBlock.normalColor = Color.blue;
			button.colors = colorBlock;
		}
	}

}
