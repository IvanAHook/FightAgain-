using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipmentComponent : MonoBehaviour { // Does not need monobehavior

	// enum Type { Weapon, Armor, Helmet, Boots }; // this should be in equipment data class
	
	// Ability ability

	public WeaponData weapon;
	public List<EquipmentData> equipment;
	public EquipmentData head;
	public EquipmentData body;
	public EquipmentData feet;

	public GameObject weaponPrefab;
	
	void Start()
	{
		if( weaponPrefab == null ) // get weapon equiped before game here
		{
			weapon.type = WeaponData.Type.Melee;
			weapon.name = "Fist";
			weapon.damage = 2;
		}
		GetComponent<WeaponComponent>().SetWeapon( weapon );
	}

	public void DropWeapon()
	{
		if( weaponPrefab != null )
		{
			// make fist default
			weapon.type = WeaponData.Type.Melee;
			weapon.name = "Fist";
			weapon.damage = 2;
			weaponPrefab = null;
		}
	}

	public void EquipWeapon( WeaponData.Type type, string name )
	{
		weapon = GameManager.equipmentList.GetWeapon( type, name );
		// Set weapon prefab
	}

	public void Equip( EquipmentData.Type type, string name )
	{
		for (int i = 0; i < equipment.Count; i++) {
			if( equipment[i].type == type )
			{
				equipment[i] = GameManager.equipmentList.GetEquipment( type, name );
				return;
			}
		}
		equipment.Add( GameManager.equipmentList.GetEquipment( type, name ) );
	}

	public float GetArmor()
	{
		return head.armor + body.armor + feet.armor;
	}

	public float GetSpeed()
	{
		return head.speed + body.speed + feet.speed;
	}

	public float GetWeaponDamage()
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
