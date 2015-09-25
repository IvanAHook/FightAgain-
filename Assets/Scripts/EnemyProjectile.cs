using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {

	bool sentMessage = false;
	bool moveRight = true;

	void Update () 
	{
		if (moveRight)
		{
			transform.position += transform.right * 3f * Time.deltaTime;
		}
		else
		{
			transform.position -= transform.right * 3f * Time.deltaTime;
		}
		
	}

	public void MoveRight (bool right)
	{
		if (right)
		{
			moveRight = true;
		}
		else
		{
			moveRight = false;
		}
	}


	void OnTriggerEnter2D (Collider2D other)
	{
		if ( other.gameObject.tag == "Player" && !sentMessage )
		{
			sentMessage = true;
			string hitBy = "EnemyProjectile";
			other.gameObject.SendMessage( "GotHit" , (string)hitBy , SendMessageOptions.DontRequireReceiver );
			Destroy(gameObject);

		}
	}
}
