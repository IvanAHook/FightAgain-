using UnityEngine;
using System.Collections;

public class WeaponComponent : MonoBehaviour {

	int damage;

	void Awake() 
	{
		// get damage from weapon in equipment component
		damage = 10;
	}

	void OnTriggerEnter2D( Collider2D other )
	{
		// If I'm an enemy and I hit another enemy.
		// And we don't have the same target.
		// Send message.
		if (gameObject.GetComponent<Enemy>() != null && other.gameObject.tag == "Enemy"
			&& gameObject.GetComponent<Enemy>().target != other.gameObject.GetComponent<Enemy>().target )
		{
			other.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
		}
		
		//If we are the player, send message
		else if (gameObject.GetComponent<Enemy>() == null)
		{
			//Debug.Log(other.tag);
			other.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
		}

		
	}

}
