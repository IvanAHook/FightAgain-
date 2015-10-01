using UnityEngine;
using System.Collections;

public class EquipmentComponent : MonoBehaviour { // Does not need monobehavior

	// enum Type { Weapon, Armor, Helmet, Boots }; // this should be in equipment data class
	
	// Ability ability

	WeaponData weapon;
	//Helmet helmet;
	//Armor armor;
	//Boots boots;

	void Start()
	{
		if( weapon.damage == 0 ) // get weapon equiped before game here
		{
			weapon.name = "Fist";
			weapon.damage = 2;
			weapon.attackSpeed = 10;
		}
	}

	public void DropWeapon()
	{
		weapon.name = "Fist";
		weapon.damage = 2;
		weapon.attackSpeed = 10;
	}

	void EquipWeapon( string weapon ) // struct? array? of weapons
	{
	}

	public int GetWeaponDamage()
	{
		return weapon.damage;
	}

	public string GetWeaponName()
	{
		//weapon.name = "Fist";
		return weapon.name;
	}

	private void OnTriggerEnter2D( Collider2D other ) 
	{
		if( other.tag == "Weapon" )
		{
			weapon = other.GetComponent<WeaponComponent>().weaponData;
			Destroy( other.gameObject );
		}
	}
	
}
