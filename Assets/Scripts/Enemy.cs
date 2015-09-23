using UnityEngine;
using System.Collections;

public class Enemy : Unit {

	public enum State { Targeting, Engaging, Attacking, Death };
	State state;

    public Transform target;

	// Attack stuff.
	float attackRange = 1f;
	float rAttackRange = 3f;
	bool attacked = false;

	public bool isRanged = true;


	public override void Start() {
		state = State.Engaging;
        base.Start();
	}

	public State UppdateAttention( Vector2 playerPos ) {
		if (state == State.Targeting) {
			Search(playerPos);
		} else if (state == State.Engaging) {
			Engage(playerPos);
		} else if (state == State.Attacking) {
			Attack();
		} else if (state == State.Death) {
			Die();
		}
		base.FlipCheck();
		return state;
	}

    void Search( Vector2 target ) {

		
		base.Move();
    }

    void Engage( Vector2 target ) {

		//If engaged to player

		
		
		// Variables
		Vector2 myPos = new Vector2(transform.position.x, transform.position.y);

		Vector2 targetDirection = target - myPos;
		float distance = targetDirection.magnitude;

		Vector2 rTarget = Vector2.zero;
		
		// Change target vector depending on if the player is to the right of left of me.
		// Left of me.
		if (target.x <= transform.position.x) 
		{
			rTarget = target + new Vector2(rAttackRange, 0f);
			
		}
		// Right of me.
		else if (target.x >= transform.position.x) 
		{
			rTarget = target - new Vector2(rAttackRange, 0f);
		}
		
		Vector2 rTargetDirection = rTarget - myPos;
		float rDistance = rTargetDirection.magnitude;

		if (isRanged) 
		{

			// Ranged
			if (Mathf.Abs(target.y - myPos.y) > 0.15f)
			{
				
				transform.Translate(rTargetDirection.normalized * 1f * Time.deltaTime);
				Debug.Log("moving");


			}
			else if (rDistance <= 0.5 && state != State.Attacking)
			{
				state = State.Attacking;
				Debug.Log("attacking");
			}

		}


		


		

	
		
		
	//	Vector2 myTarget = target + new Vector2(rAttackRange, 0f);
	//	Vector2 myTargetDir = myTarget - myPos;
	//	float myDistance = myTargetDir.magnitude;

		


		/*// Ranged enemy test
		
		// Target is to the left of me
		if (target.x < transform.position.x && Mathf.Abs( (target.y - transform.position.y) ) > 0.5f) {


			transform.Translate(myTargetDir.normalized * 1f * Time.deltaTime);
			Debug.Log("yo");
			

		}

		else if ( myDistance >= rAttackRange ) {
			Debug.Log("hej");
		}
		
			
		// Target is to the right of me
		else if (target.x > transform.position.x && Mathf.Abs( (target.y - transform.position.y) ) > 0.5f) {


			

			transform.Translate(myTargetDir.normalized * 1f * Time.deltaTime);

			Debug.Log("yoyo");

		}
		else if ( myDistance >= rAttackRange ) {

			Debug.Log("hej2");
		}*/



		
	//	if( target != null ) {
    //        base.Move(target);
    //    }
    }


	void Attack() {

		//Play attack animation

		//Prototype attack loop
		if (!attacked) {
			attacked = true;
			StartCoroutine("AttackLoop");
		}
		


	}

    void Die() {
        state = State.Death;
    }

    void OnTriggerEnter2D( Collider2D other ) {
        if( other.tag == "Wall" ) {
            base.direction = new Vector2( direction.x * -1, direction.y * -1 );
        } else if( other.tag == "Player" ) {
            target = other.transform;
            state = State.Death;
        }
    }


	IEnumerator AttackLoop() {

		
		yield return new WaitForSeconds(1.2f);
		Debug.Log( "I Attacked!" );
		state = State.Engaging;
		attacked = false;
		

	}

}
