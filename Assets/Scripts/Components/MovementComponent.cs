using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MovementComponent : MonoBehaviour {

	public float speed = 3.8f;


	public void Move( float horizontal, float vertical ) {
		Vector2 movement = new Vector3( Mathf.Clamp( transform.position.x + horizontal * speed * Time.deltaTime, -8f, 8f ),
		                               Mathf.Clamp( transform.position.y + vertical * speed * Time.deltaTime, -4f, 4f ),
		                               0.0f );
		transform.position = movement;
	}

	public void ChangeSpeed( int speed )
	{
		if( gameObject.tag == "Player" )
			this.speed = speed;
	}

}
