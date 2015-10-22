using UnityEngine;
using System.Collections;

public class DamageComponent : MonoBehaviour {

	Transform myOwner;

	float GetWeaponDamage()
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
		if (other.gameObject.GetComponentInParent<Rigidbody2D>().gameObject.GetComponent<Transform>() == myOwner ) return; // RIP Unity, leave as is / Arre Barre

		// If I'm an enemy and I hit another enemy and we don't have the same target.
		if ( gameObject.GetComponentInParent<Enemy>() != null && other.gameObject.tag == "Enemy"
			&& gameObject.GetComponentInParent<Enemy>().target != other.gameObject.GetComponentInParent<Enemy>().target)
		{
			Debug.Log("111");
			SendDamage(other.gameObject.GetComponentInParent<HealthComponent>());
		}
		// If we are the player, send message
		else if (GetComponent<PlayerMovement>() != null || GetComponentInParent<PlayerMovement>() != null )
		{
			Debug.Log("222");
			other.gameObject.GetComponentInParent<HealthComponent>().SendMessage( "TakeDamageFromPlayer", GetWeaponDamage(), SendMessageOptions.DontRequireReceiver );
		}
		// If we are an enemy and we hit the player, send message
		else if ( gameObject.GetComponentInParent<Enemy>() != null && other.tag == "Player" )
		{
			Debug.Log("333");
			SendDamage( other.gameObject.GetComponentInParent<HealthComponent>() );
		}
		// If we are an enemy projectile and we hit the player or another enemy, send message.
		else if (gameObject.tag == "EnemyProjectile" && ( other.tag == "Player" || other.tag == "Enemy" ) )
		{
			Debug.Log("444");
			SendDamage( other.gameObject.GetComponentInParent<HealthComponent>() );
		}
	}

	void SendDamage(HealthComponent healtComp)
	{
		healtComp.SendMessage("TakeDamage", GetWeaponDamage(), SendMessageOptions.DontRequireReceiver);

		if (gameObject.tag == "EnemyProjectile")
		{
			Destroy(gameObject);
		}
	}

	public void Owner(Transform owner)
	{
		myOwner = owner;
	}

}
