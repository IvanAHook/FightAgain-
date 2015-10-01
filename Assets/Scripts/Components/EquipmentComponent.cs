using UnityEngine;
using System.Collections;

public class EquipmentComponent : MonoBehaviour { // Does not need monobehavior

	// enum Type { Weapon, Armor, Helmet, Boots }; // this should be in equipment data class
	
	// Ability ability

	public WeaponData weapon;
	//Helmet helmet;
	//Armor armor;
	//Boots boots;

	public GameObject weaponPrefab;
	
	void Start()
	{
		if( weaponPrefab == null ) // get weapon equiped before game here
		{
			weapon.name = "Fist";
			weapon.damage = 2;
			weapon.attackSpeed = 10;
		}
	}

	public void DropWeapon()
	{
		if( weaponPrefab != null )
		{
			weapon.name = "Fist";
			weapon.damage = 2;
			weapon.attackSpeed = 10;
			weaponPrefab = null;
		}
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
		return weapon.name;
	}

	private void OnTriggerEnter2D( Collider2D other ) 
	{
		if( other.tag == "Weapon" && weaponPrefab == null )
		{
			weaponPrefab = other.gameObject as GameObject;
			weapon = other.GetComponent<WeaponComponent>().weaponData;
			other.gameObject.SetActive( false );
		}
	}
	
}
