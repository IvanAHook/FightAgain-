using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {
	
	bool moveRight = true;
	int damage;
	float lifeTime;

	void Awake() 
	{
		damage = 10;
	}

	void Update () 
	{

		lifeTime += Time.deltaTime;
		if (lifeTime > 10f)
			Destroy(gameObject);

		if (moveRight)
			transform.position += transform.right * 12f * Time.deltaTime;
		else
			transform.position -= transform.right * 12f * Time.deltaTime;
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
