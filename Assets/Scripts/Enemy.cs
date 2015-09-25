using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Unit {

	public enum State { Targeting, Engaging, Attacking, Death };
	State state;

    public Transform target = null;
	public GameObject enemyProjectile;

	float attackRange = 0.5f;
	float rAttackRange = 3f;
	float range;
	bool isRanged = true;
	bool attacked = false;

	float xVel;
	float yVel;

	public override void Start() {
		state = State.Targeting;

		// Temp.
		if (isRanged)
		{
			range = rAttackRange;
		}
		else
		{
			range = attackRange;
		}


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
		
		if (xVel != 1 && state != State.Attacking)
		{
			base.FlipCheck(xVel);
		}
		//base.SpeedCheck(xVel,yVel);
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

    void Engage() 
	{

		//If engaged to player

		if( target == null ) { // Dont continue if target is missing
			state = State.Targeting;
			return;
		}
		
		// Variables
		Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
		Vector2 targetPos = new Vector2(target.position.x, target.position.y);

		Vector2 myTarget = Vector2.zero;
		
		// Change target vector depending on if the player is to the right of left of me.
		// Left
		if (target.position.x < transform.position.x)
		{
			myTarget = targetPos + new Vector2(range, 0f);
		}
		// Right
		else if (target.position.x > transform.position.x)
		{
			myTarget = targetPos - new Vector2(range, 0f);
		}

		// Get target direction.
		Vector2 targetDirection = myTarget - myPos;
		float distance = targetDirection.magnitude;
		Vector2 dir = targetDirection.normalized;
		xVel = dir.x;
		yVel = dir.y;
		
		
		// Enemy movement.
		// If difference in Y is less than var, and distance to target is geater than var, attack.
		if ( Mathf.Abs( target.position.y - myPos.y ) < 0.15f && distance <= 0.5f && state != State.Attacking )
		{
			state = State.Attacking;

			// Set speed for animation. 
			base.SetSpeed(0);

			//Flip if not facing player.
			if (target.position.x < transform.position.x && base.facingRight)
			{
				base.Flip();
			}
			else if (target.position.x > transform.position.x && !base.facingRight)
			{
				base.Flip();
			}
			//Debug.Log("attacking");

		}
		// Move
		else 
		{
			transform.Translate(dir * 1f * Time.deltaTime);
			base.SetSpeed(1);
			//Debug.Log("moving");
		}
	
		
		
	//	if( target != null ) {
    //        base.Move(target);
    //    }


    }


	void Attack() 
	{
		//Play attack animation
		base.AttackAnim();

		// Prototype recover thing
		if (!attacked) 
		{
			if (isRanged)
			{
				GameObject spawnedProjectile = (GameObject)Instantiate(enemyProjectile, transform.position, Quaternion.identity);
				
				if (base.facingRight)
				{
					spawnedProjectile.gameObject.GetComponent<EnemyProjectile>().MoveRight(true);
				}
				else
				{
					spawnedProjectile.gameObject.GetComponent<EnemyProjectile>().MoveRight(false);
				}
				
			}

			attacked = true;
			StartCoroutine("Recover");
		}
		


	}

    void Die() 
	{
        state = State.Death;
		GameManager.instance.IncreaseMoney( 10 );
    }

    void OnTriggerEnter2D( Collider2D other )
	{
        if( other.tag == "Wall" ) 
		{
            base.direction = new Vector2( direction.x * -1, direction.y * -1 );
        } 
		else if( other.tag == "Player" )
		{
            target = other.transform;
            state = State.Death;
        }
    }


	IEnumerator Recover()
	{
		yield return new WaitForSeconds(1.2f);
		state = State.Engaging;
		attacked = false;
	}


}
