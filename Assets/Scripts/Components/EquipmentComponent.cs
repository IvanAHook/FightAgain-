using UnityEngine;
using System.Collections;

public class EquipmentComponent : MonoBehaviour {

	enum Type { Weapon, Armor, Helmet, Boots };
	Type type;

	int damage;
	int armor;
	int speed;
	// Ability ability

	public int GetDamage() {
		return damage;
	}

	public int GetArmor() {
		return armor;
	}

	public int GetSpeed() {
		return speed;
	}

}
