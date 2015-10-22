using UnityEngine;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;

public class EquipmentList {

	FileReader _fReader;
	List<EquipmentData> equipment;  // Move to arrays when JSON is implemented
	List<WeaponData> weapons;

	public EquipmentList() // Loading equipment from JSON coming soon
	{
		equipment = new List<EquipmentData>();
		weapons = new List<WeaponData>();

		// Helmets
		equipment.Add( new EquipmentData( EquipmentData.Type.Head, "Gas Mask", 1, 1 ) );							// 0
		equipment.Add( new EquipmentData( EquipmentData.Type.Head, "Pasta Strainer", 2, 1 ) );						// 1
		equipment.Add( new EquipmentData( EquipmentData.Type.Head, "Chicken Mask", 1, 1.2f ) );						// 2
		equipment.Add( new EquipmentData( EquipmentData.Type.Head, "Severed Head", 1, 0.9f ) );						// 3
		equipment.Add( new EquipmentData( EquipmentData.Type.Head, "Amercian Football", 2, 1.1f ) );				// 4
		equipment.Add( new EquipmentData( EquipmentData.Type.Head, "Samurai Helmet", 3, 0.9f ) );					// 5
		equipment.Add( new EquipmentData( EquipmentData.Type.Head, "Cannibal Mask", 1, 1.1f ) );					// 6
		equipment.Add( new EquipmentData( EquipmentData.Type.Head, "Medieval Helmet", 4, 0.8f ) );					// 7

		// Body armor
		equipment.Add( new EquipmentData( EquipmentData.Type.Body, "Motorbike Jacket", 3, 1.3f ) );					// 8
		equipment.Add( new EquipmentData( EquipmentData.Type.Body, "Full Metal Jaket", 5, 0.9f ) );					// 9
		equipment.Add( new EquipmentData( EquipmentData.Type.Body, "Kilt", 1, 1.4f ) );								// 10
		equipment.Add( new EquipmentData( EquipmentData.Type.Body, "Car Tires", 4, 0.7f ) );						// 11
		equipment.Add( new EquipmentData( EquipmentData.Type.Body, "Legion of Doom", 5, 1.2f ) );					// 12
		equipment.Add( new EquipmentData( EquipmentData.Type.Body, "Car Battery", 2, 0.7f ) );						// 13
		equipment.Add( new EquipmentData( EquipmentData.Type.Body, "Medieval Armor", 6, 0.6f ) );					// 14
		equipment.Add( new EquipmentData( EquipmentData.Type.Body, "Samurai", 5, 1.2f ) );							// 15

		// Footwear
		equipment.Add( new EquipmentData( EquipmentData.Type.Feet, "Snickers", 0, 1.2f ) );							// 16
		equipment.Add( new EquipmentData( EquipmentData.Type.Feet, "Medieval Boots", 0, 1.1f ) );					// 17
		equipment.Add( new EquipmentData( EquipmentData.Type.Feet, "Kungfu slippers", 0, 1.3f ) );					// 18
		equipment.Add( new EquipmentData( EquipmentData.Type.Feet, "Biker Boots", 0, 1.2f ) );						// 19
		equipment.Add( new EquipmentData( EquipmentData.Type.Feet, "American footbal trainers", 0, 1.3f ) );		// 20
		equipment.Add( new EquipmentData( EquipmentData.Type.Feet, "Rollerblades", 0, 1.4f ) );						// 21

		// Melee weapons
		weapons.Add( new WeaponData( WeaponData.Type.Melee, "Fists", 0.5f ) );										// 0
		weapons.Add( new WeaponData( WeaponData.Type.Melee, "Baseball Bat", 1 ) );									// 1
		weapons.Add( new WeaponData( WeaponData.Type.Melee, "Pipe Wrench", 2 ) );									// 2
		weapons.Add( new WeaponData( WeaponData.Type.Melee, "Sword", 3 ) );											// 3
		weapons.Add( new WeaponData( WeaponData.Type.Melee, "Mace", 4 ) );											// 4
		weapons.Add( new WeaponData( WeaponData.Type.Melee, "Axe", 5 ) );											// 5
		weapons.Add( new WeaponData( WeaponData.Type.Melee, "Chainsaw", 6 ) );										// 6
		weapons.Add( new WeaponData( WeaponData.Type.Melee, "Katana", 7 ) );										// 7
		weapons.Add( new WeaponData( WeaponData.Type.Melee, "Lightsaver", 8 ) );									// 8

		// Ranged Weapons
		weapons.Add( new WeaponData( WeaponData.Type.Ranged, "Bow & Arrows", 1 ) );									// 9
		weapons.Add( new WeaponData( WeaponData.Type.Ranged, "Pistol", 4 ) );										// 10
		weapons.Add( new WeaponData( WeaponData.Type.Ranged, "Shotgun", 6 ) );										// 11
		weapons.Add( new WeaponData( WeaponData.Type.Ranged, "Crossbow", 7 ) );										// 12
		weapons.Add( new WeaponData( WeaponData.Type.Ranged, "Bazooka", 8 ) );										// 13
		weapons.Add( new WeaponData( WeaponData.Type.Ranged, "Virus Gun", 8 ) );									// 14

	}
	
	public WeaponData GetWeapon( WeaponData.Type type, string name )
	{
		foreach( WeaponData weaponData in weapons )
		{
			if( weaponData.type == type && weaponData.name == name )
			{
				return weaponData;
			}
		}
		return weapons[0];
	}
	
	public EquipmentData GetEquipment( EquipmentData.Type type, string name )
	{
		foreach( EquipmentData equipmentData in equipment )
		{
			if( equipmentData.type == type && equipmentData.name == name )
			{
				return equipmentData;
			}
		}
		return new EquipmentData( type, "Naked", 0, 0 );
	}

	public WeaponData GetWeapon( int id )
	{
		if( weapons.Count > id )
			return weapons[ id ];
		else
			return weapons[0];
	}
	
	public EquipmentData GetEquipment( int id )
	{
		if( equipment.Count > id )
			return equipment[ id ];
		else
			return equipment[0];
	}

	void InsertEquipment( EquipmentData.Type type, string typeString, JSONNode jsonNode ) // parse weapons and equipment from JSON file
	{
		
		for( int i = 0; i < jsonNode["equipment"][typeString].Count; i++ ) // add helmets
		{
			equipment.Add( new EquipmentData( type, jsonNode["equipment"][typeString][i]["name"],
			                                 jsonNode["equipment"][typeString][i]["armor"].AsInt,
			                                 jsonNode["equipment"][typeString][i]["speed"].AsInt ) );
		}
	}

	void InsertWeapons( WeaponData.Type type, string typeString, JSONNode jsonNode )
	{
		
		for( int i = 0; i < jsonNode["weapons"][typeString].Count; i++ ) // add melee weapons
		{
			weapons.Add( new WeaponData( type, jsonNode["weapon"][typeString][i]["name"],
			                            jsonNode["weapon"][typeString][i]["damage"].AsInt ) );
		}
	}

}
