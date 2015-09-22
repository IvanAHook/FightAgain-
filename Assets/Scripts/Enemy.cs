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
		return state;
	}

    void Search( Vector2 target ) {

		
		base.Move();
    }

    void Engage( Vector2 target ) {

		//If engaged to player

		//Chase target until within range

		
		
	/*	// Melee enemy test
		Vector2 myPos = new Vector2(transform.position.x, transform.position.y);

		Vector2 targetDirection = target - myPos;
		float distance = targetDirection.magnitude;

		if (distance >= attackRange) {
		
			transform.Translate(targetDirection.normalized * 1f * Time.deltaTime);
		}
		else if (state != State.Attacking) {
			state = State.Attacking;
		}*/




		Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
		// Ranged enemy test
		
		// Target is to the left of me
		if (target.x < transform.position.x) {



			Vector2 myTarget = target + new Vector2(rAttackRange, 0f);
			Vector2 myTargetDir = myTarget - myPos;

			transform.Translate(myTargetDir.normalized * 1f * Time.deltaTime);

		}
		// Target is to the right of me
		else if (target.x > transform.position.x) {


			Vector2 myTarget = target - new Vector2(rAttackRange, 0f);
			Vector2 myTargetDir = myTarget - myPos;

			transform.Translate(myTargetDir.normalized * 1f * Time.deltaTime);
		}



		
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
