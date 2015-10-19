using UnityEngine;
using System.Collections;

public class WeaponComponent : MonoBehaviour {

	public WeaponData weaponData;

	bool moveRight = true;
	public bool thrown = false;
	float damage = 0.5f;

	void Update() 
	{
		if( thrown )
		{
			if( moveRight )
				transform.position += transform.right * 12f * Time.deltaTime;
			else
				transform.position -= transform.right * 12f * Time.deltaTime;
		}
	}
	
	public void MoveRight(bool right)
	{
		if (right)
			moveRight = true;
		else
			moveRight = false;
	}

	public void SetWeapon( WeaponData weaponData )
	{
		this.weaponData = weaponData;
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
