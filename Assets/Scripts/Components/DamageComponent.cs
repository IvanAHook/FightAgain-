using UnityEngine;
using System.Collections;

public class DamageComponent : MonoBehaviour {

	Transform myOwner;

	int GetWeaponDamage()
	{
		if ( gameObject.tag == "Player" )
			return GetComponentInParent<EquipmentComponent>().GetWeaponDamage();
		else
			return 10;
	}

	void OnTriggerEnter2D( Collider2D other )
	{
		Debug.Log(gameObject.tag + " collided with " + other.tag);

	
		if (other.transform == myOwner) return;

		// If I'm an enemy and I hit another enemy and we don't have the same target.
		if ( gameObject.GetComponentInParent<Enemy>() != null && other.gameObject.tag == "Enemy"
			&& gameObject.GetComponentInParent<Enemy>().target != other.gameObject.GetComponentInParent<Enemy>().target)
		{
			other.gameObject.SendMessage( "TakeDamage", GetWeaponDamage(), SendMessageOptions.DontRequireReceiver );
			//Debug.Log("111");
		}
		// If we are the player, send message
		else if (GetComponent<PlayerMovement>() != null || GetComponentInParent<PlayerMovement>() != null )
		{
			other.gameObject.SendMessage( "TakeDamageFromPlayer", GetWeaponDamage(), SendMessageOptions.DontRequireReceiver );
			//Debug.Log("222");
		}
		// If we are an enemy and we hit the player, send message
		else if ( gameObject.GetComponentInParent<Enemy>() != null && other.tag == "Player" )
		{
			other.gameObject.GetComponentInParent<HealthComponent>().SendMessage( "TakeDamage", GetWeaponDamage(), SendMessageOptions.DontRequireReceiver );
			//Debug.Log("333");
		}
		// If we are an enemy projectile and we hit the player, send message.
		else if (gameObject.tag == "EnemyProjectile" && ( other.tag == "Player" || other.tag == "Enemy" ) )
		{
			other.gameObject.GetComponentInParent<HealthComponent>().SendMessage( "TakeDamage", GetWeaponDamage(), SendMessageOptions.DontRequireReceiver );
			//Debug.Log("444");
		}
	}

	public void Owner(Transform owner)
	{
		myOwner = owner;
	}

}
