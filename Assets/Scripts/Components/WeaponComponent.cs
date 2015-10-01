using UnityEngine;
using System.Collections;

public class WeaponComponent : MonoBehaviour {

	public WeaponData weaponData;

	bool moveRight = true;
	public bool thrown = false;
	int damage;

	void Start()
	{
		damage = weaponData.damage;
	}

	void Update () 
	{
		if( thrown )
		{
			if( moveRight )
				transform.position += transform.right * 12f * Time.deltaTime;
			else
				transform.position -= transform.right * 12f * Time.deltaTime;
		}
	}
	
	public void MoveRight (bool right)
	{
		if (right)
			moveRight = true;
		else
			moveRight = false;
	}
	
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if ( other.gameObject.tag == "Enemy" )
		{
			other.gameObject.SendMessage( "TakeDamage" , damage , SendMessageOptions.DontRequireReceiver );
			thrown = false;
		}
	}

}
