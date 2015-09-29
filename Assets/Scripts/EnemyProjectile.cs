using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {
	
	bool moveRight = true;
	int damage;

	void Awake() 
	{
		damage = 10;
	}

	void Update () 
	{
		if (moveRight)
			transform.position += transform.right * 3f * Time.deltaTime;
		else
			transform.position -= transform.right * 3f * Time.deltaTime;
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
		if ( other.gameObject.tag == "Player" )
		{
			other.gameObject.SendMessage( "TakeDamage" , damage , SendMessageOptions.DontRequireReceiver );
			Destroy(gameObject);
		}
	}
}
