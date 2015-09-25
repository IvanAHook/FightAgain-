using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Unit {

	public enum State { Targeting, Engaging, Attacking, Death };
	State state;

    public Transform target = null;

	// Attack stuff.
	float attackRange = 1f;
	float rAttackRange = 3f;
	bool attacked = false;

	public bool isRanged = true;


	public override void Start() {
		state = State.Targeting;
        base.Start();
	}

	public State UppdateAttention( Transform player, List<Enemy> enemies ) {
		if ( state == State.Targeting ) {
			Search( player, enemies );
		} else if ( state == State.Engaging ) {
			Engage();
		} else if ( state == State.Attacking ) {
			Attack();
		} else if ( state == State.Death ) {
			Die();
		}
		return state;
	}

	void Search( Transform player, List<Enemy> enemies ) {

		if ( this.target == null ) {
			SelectTarget( player, enemies );
		}

		base.Move();
    }

	void SelectTarget( Transform player, List<Enemy> enemies ) {

		foreach (Enemy e in enemies)
		{
			Debug.Log( 1 );
			if ( e.target == null && e != this )
			{
				Debug.Log( 1 );
				target = e.transform;
				e.target = transform;
				state = State.Engaging;
				return;
			}
		}

		target = player;
		state = State.Engaging;
	}

    void Engage() {

		//If engaged to player

		if( target == null ) { // Dont continue if target is missing
			state = State.Targeting;
			return;
		}
		
		// Variables
		Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
		Vector2 targetPos = new Vector2(target.position.x, target.position.y);

		Vector2 targetDirection = targetPos - myPos;
		float distance = targetDirection.magnitude;

		Vector2 rTarget = Vector2.zero;
		
		// Change target vector depending on if the player is to the right of left of me.
		if (target.position.x < transform.position.x) {
			rTarget = targetPos + new Vector2(rAttackRange, 0f);
		}
		else if (target.position.x > transform.position.x) {
			rTarget = targetPos - new Vector2(rAttackRange, 0f);
		}
		
		Vector2 rTargetDirection = rTarget - myPos;
		float rDistance = rTargetDirection.magnitude;


		if (!isRanged)
		{

			// Melee
			// Chase target until within range, then attack.
			if (distance >= attackRange)
			{
				transform.Translate(targetDirection.normalized * 1f * Time.deltaTime);
			}
			else if (state != State.Attacking)
			{
				state = State.Attacking;
			}

		}
		else
		{

			// Ranged
			if (Mathf.Abs(target.position.y - myPos.y) > 0.15f)
			{
				
				transform.Translate(rTargetDirection.normalized * 1f * Time.deltaTime);
				//Debug.Log("moving");

			}
			else if (rDistance <= 0.5 && state != State.Attacking)
			{
				state = State.Attacking;
				//Debug.Log("attacking");
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
		GameManager.instance.IncreaseMoney( 10 );
    }

	void OnTriggerEnter2D( Collider2D other ) { // TODO: temp solution with the tag shit
        if( other.tag == "Wall" ) {
            base.direction = new Vector2( direction.x * -1, direction.y * -1 );
        } else if( other.tag == "Damage" && other.transform == target ) {
            target = other.transform;
            state = State.Death;
		} else if ( other.tag == "Damage" && other.GetComponentInParent<Transform>().tag == "Player" ) {
			target = other.transform;
			state = State.Death;
		}
    }


	IEnumerator AttackLoop() {

		
		yield return new WaitForSeconds(1.2f);
		//Debug.Log( "I Attacked!" );
		state = State.Engaging;
		attacked = false;
		

	}

}
