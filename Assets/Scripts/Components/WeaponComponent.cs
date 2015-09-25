using UnityEngine;
using System.Collections;

public class WeaponComponent : MonoBehaviour {

	int damage;

	void Awake() {
		damage = 10;
	}

	void OnTriggerEnter2D( Collider2D other ) {
		Debug.Log(other.tag);
		other.gameObject.SendMessage( "TakeDamage", damage, SendMessageOptions.DontRequireReceiver );
	}

}
