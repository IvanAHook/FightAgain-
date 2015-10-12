﻿using UnityEngine;
using System.Collections;

public class DamageComponent : MonoBehaviour {

	Transform myOwner;

	void Start() 
	{
	}

	int GetWeaponDamage()
	{
		if ( gameObject.tag == "Player" )
			return GetComponentInParent<EquipmentComponent>().GetWeaponDamage();
		else
			return 10;
	}

	void OnTriggerEnter2D( Collider2D other )
	{

		// If I'm an enemy and I hit another enemy.
		// And we don't have the same target.
		// Send message.
		if (other.transform == myOwner) return;
		if ( gameObject.GetComponentInParent<Enemy>() != null && other.gameObject.tag == "Enemy"
			&& gameObject.GetComponentInParent<Enemy>().target != other.gameObject.GetComponentInParent<Enemy>().target)
		{
			other.gameObject.SendMessage( "TakeDamage", GetWeaponDamage(), SendMessageOptions.DontRequireReceiver );
		}
		// If we are the player, send message
		else if (gameObject.GetComponentInParent<Enemy>() == null)
		{
			//Debug.Log(other.tag);
			other.gameObject.SendMessage( "TakeDamageFromPlayer", GetWeaponDamage(), SendMessageOptions.DontRequireReceiver );
		}
		// If we are an enemy and we hit the player, send message
		else if ( gameObject.GetComponentInParent<Enemy>() != null && other.tag == "Player" )
		{
			other.gameObject.SendMessage( "TakeDamage", GetWeaponDamage(), SendMessageOptions.DontRequireReceiver );
		}
	}

	public void Owner(Transform owner)
	{
		myOwner = owner;
	}

}
