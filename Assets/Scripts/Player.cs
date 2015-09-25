using UnityEngine;
using System.Collections;

public class Player : Unit {

	float health;

	void TakeDamage( float ammount ) {
		health -= ammount;
	}

}
