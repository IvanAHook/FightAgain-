using UnityEngine;
using System.Collections;

public class EquipmentComponent : MonoBehaviour { // Does not need monobehavior

	// enum Type { Weapon, Armor, Helmet, Boots }; // this should be in equipment data class
	
	// Ability ability

	public WeaponData weapon;
	public EquipmentData head;
	public EquipmentData body;
	public EquipmentData feet;

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
			// make fist default
			weapon.name = "Fist";
			weapon.damage = 2;
			weapon.attackSpeed = 10;
			weaponPrefab = null;
		}
	}

	void EquipWeapon( string weapon ) // struct? array? of weapons
	{
	}

	public void Equip( WeaponData weapon, EquipmentData head, EquipmentData body, EquipmentData feet )
	{
		this.weapon = weapon;
		this.head = head;
		this.body = body;
		this.feet = feet;
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
