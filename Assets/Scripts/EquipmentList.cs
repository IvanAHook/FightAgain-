using UnityEngine;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;

public class EquipmentList {

	FileReader _fReader;
	List<EquipmentData> equipment;
	List<WeaponData> weapons;

	public EquipmentList()
	{
		equipment = new List<EquipmentData>();
		weapons = new List<WeaponData>();

		TextAsset equipmentFile = Resources.Load( "equipment" ) as TextAsset;
		var E = JSON.Parse( equipmentFile.text );

		InsertEquipment( EquipmentData.Type.Head, "head", E);
		InsertEquipment( EquipmentData.Type.Body, "body", E);
		InsertEquipment( EquipmentData.Type.Feet, "feet", E);

		InsertWeapons( WeaponData.Type.Melee, "melee", E);
		InsertWeapons( WeaponData.Type.Ranged, "ranged", E);
	}

	void InsertEquipment( EquipmentData.Type type, string typeString, JSONNode jsonNode ) // parse weapons and equipment from JSON file
	{
		//Debug.Log( "Equipment list parsing..." + typeString );
		
		for( int i = 0; i < jsonNode["equipment"]["head"].Count; i++ ) // add helmets
		{
			equipment.Add( new EquipmentData( type, jsonNode["equipment"]["head"][i]["name"],
			                                 jsonNode["equipment"]["type"][i]["armor"].AsInt,
			                                 jsonNode["equipment"]["type"][i]["speed"].AsInt ) );
		}
	}

	void InsertWeapons( WeaponData.Type type, string typeString, JSONNode jsonNode )
	{
		//Debug.Log( "Weapons list parsing..." + typeString );
		
		for( int i = 0; i < jsonNode["weapons"]["melee"].Count; i++ ) // add melee weapons
		{
			weapons.Add( new WeaponData( type, jsonNode["weapon"][typeString][i]["name"],
			                            jsonNode["weapon"][typeString][i]["damage"].AsInt ) );
		}
	}

}
