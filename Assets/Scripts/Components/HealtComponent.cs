using UnityEngine;
using System.Collections;

public class HealtComponent : MonoBehaviour {

	public int health;

	void Awake() 
	{
	}

	public void TakeDamage( int damage )
	{
		health -= damage;
		if ( health <= 0 )
			gameObject.SetActiveRecursively( false );
	}

	public int GetHealth() 
	{
		return health;
	}

}
