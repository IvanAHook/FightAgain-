using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MovementComponent : MonoBehaviour {

	public struct MCCollision
	{
		public Transform t;
		public float distance;
		public Vector2 tangentialTranslation;
	}

	public float speed = 3.8f;
	float RADIUS;
	float DIAMETER_SQR;

	void Awake()
	{
		RADIUS = 1;
		DIAMETER_SQR = 4;
	}

	public void Move( float horizontal, float vertical ) {

		if( gameObject.tag == "Enemy" && GameManager.instance.collisionHandling )
		{
			Vector2 movement = new Vector3( Mathf.Clamp( transform.position.x + horizontal * speed * Time.deltaTime, -8f, 8f ),
		                               		Mathf.Clamp( transform.position.y + vertical * speed * Time.deltaTime, -4f, 4f ),
		                               		0.0f );

			Vector2 constrainedMove = ConstrainTranslation( this.transform, movement );
			transform.position = new Vector3( constrainedMove.x, constrainedMove.y, 0.0f );
		}
		else
		{
			Vector2 movement = new Vector3( Mathf.Clamp( transform.position.x + horizontal * speed * Time.deltaTime, -8f, 8f ),
			                               Mathf.Clamp( transform.position.y + vertical * speed * Time.deltaTime, -4f, 4f ),
			                               0.0f );
			transform.position = movement;
		}

	}

	public void ChangeSpeed( int speed )
	{
		if( gameObject.tag == "Player" )
			this.speed = speed;
	}

	/*void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere( transform.position, RADIUS );
	}*/

	Vector2 ConstrainTranslation( Transform t, Vector2 wantedTranslation )
	{
		float translationDistance = wantedTranslation.magnitude;
		if( translationDistance < float.Epsilon )
			return new Vector2( transform.position.x, transform.position.y );

		Vector2 position = t.position;

		float queryRadius = RADIUS + translationDistance * 0.5f;
		List<Transform> neighbors = new List<Transform>();
		Collider2D[] neighborColliders = Physics2D.OverlapCircleAll( position + 0.5f * wantedTranslation, queryRadius );
		foreach( Collider2D c2d in neighborColliders )
		{
			if( c2d.transform != this.transform && c2d.name.Contains( "Box" ) )
			{
				neighbors.Add(c2d.transform);
			}
		}

		MCCollision collision = new MCCollision();
		bool collided = NearestCollision( neighbors, position, wantedTranslation, collision );
		if( collided )
		{
			neighbors.Remove( collision.t );
			Vector2 translation = collision.tangentialTranslation;
			//Debug.Log( collision.tangentialTranslation );

			if( NearestCollision( neighbors, position, translation, collision ) )
				return translation * collision.distance;
			Debug.Log( translation );
			return translation;
		}

		return wantedTranslation;
	}

	bool NearestCollision( List<Transform> neighbors, Vector2 position, Vector2 translation, MCCollision collision )
	{
		if( neighbors.Count == 0 )
			return false;

		float translationLength = translation.magnitude;
		if( translationLength < float.Epsilon )
			return false;

		Vector2 wantedPosition = position + translation;
		Vector2 normalizedTranslation = translation.normalized;

		collision.distance = translationLength;
		foreach( Transform neighbor in neighbors )
		{
			Vector2 posB = neighbor.position;
			Vector2 toB = posB - position;


			float d = Vector2.Dot( normalizedTranslation, toB.normalized );
			if( d <= 0 )
				continue;

			Vector2 projToB = normalizedTranslation * d;
			Vector2 projPosB = position + projToB;

			float oppositeCathetusSqr = Vector2.SqrMagnitude( posB - projPosB );
			float hypotenuseSqr = DIAMETER_SQR;
			if( oppositeCathetusSqr > hypotenuseSqr )
				continue;

			float adjacentCathetus = Mathf.Sqrt( hypotenuseSqr - oppositeCathetusSqr );
			float distAlongTranslation = projToB.magnitude - adjacentCathetus;
			distAlongTranslation = Mathf.Clamp( distAlongTranslation, 0.0f, distAlongTranslation );

			if( distAlongTranslation < collision.distance )
			{
				Vector2 normToB = toB.normalized;
				Vector2 projWanted = Vector2.Dot( translation, normToB ) * normToB;
				Vector2 projWantedPosition = wantedPosition - projWanted;
				collision.tangentialTranslation = projWantedPosition - position;

				collision.t = neighbor;
				collision.distance = distAlongTranslation;
			}
		}

		return collision.distance < translationLength;
	}

}
