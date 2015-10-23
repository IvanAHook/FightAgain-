using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MovementComponent : MonoBehaviour {

	public float BASESPEED = 243.2f;
	float horizontalClamp = 8f;
	float verticalClamp = 4f;

	public void Move( float horizontal, float vertical, float speedModifier ) {
		float speed = BASESPEED * speedModifier;

		Vector2 movement = new Vector3(
			Mathf.Clamp( transform.position.x + horizontal * speed * Time.deltaTime, -horizontalClamp*64, horizontalClamp*64 ),
			Mathf.Clamp( transform.position.y + vertical * speed * Time.deltaTime, -verticalClamp*64, verticalClamp*64 ),
		    0.0f );
		transform.position = movement;
	}

	public void ChangeSpeed( int speed )
	{
		if( gameObject.tag == "Player" )
			this.BASESPEED = speed;
	}

	

}
