using UnityEngine;
using System.Collections;

public class EquipmentComponent : MonoBehaviour { // Does not need monobehavior

	// enum Type { Weapon, Armor, Helmet, Boots }; // this should be in equipment data class
	
	// Ability ability

	Weapon weapon;

	public string GetWeaponName()
	{
		return ( weapon != null ) ? weapon.name : "None";
	}

	private void OnTriggerEnter2D(Collider2D other) 
	{
		if( other.tag == "Weapon" )
		{
			weapon = new Weapon( other.name );
			Destroy( other.gameObject );
		}
	}
	
}
