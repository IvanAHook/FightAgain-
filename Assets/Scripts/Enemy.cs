using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(MovementComponent))]
[RequireComponent(typeof(AnimationComponent))]
public class Enemy : Unit {

	public enum State { Targeting, Engaging, Attacking, Death };
	public State state;

	// Required componentent
	HealthComponent _healthcomponent;
	MovementComponent _movement;
	AnimationComponent _animComponent;

    public Transform target = null;
	public GameObject enemyProjectile;

	float attackRange = 64f;
	float rAttackRange = 320f;
	float range;
	bool isRanged = false;
	
	bool attacked = false;
	bool strafing = false;

	float xVel;
	float yVel;
	Vector2 randomVector;

	public bool isIdle = true;

	void Awake() 
	{
		_healthcomponent = GetComponent<HealthComponent>();
		_movement = GetComponent<MovementComponent>();
		_animComponent = GetComponent<AnimationComponent>();

		float random = Random.Range ( -25f, 25f );
		_movement.BASESPEED += random;
	}

	public override void Start() 
	{
		state = State.Targeting;

		// Temporary /Ariel
		// Make enemy melee or ranged at random
		int meleeOrRanged = Random.Range(0, 2);

		isRanged = false;

		//if (meleeOrRanged > 0)
		//	isRanged = true;
		

		// Change range depening on if enemy is ranged or not.
		if (isRanged)
			range = rAttackRange;
		else
			range = attackRange;

		// Make a random vector.
		randomVector =  new Vector2( Random.Range(-0.35f, 0.35f), Random.Range(-0.1f, 0.1f ) );

        base.Start();
	}

	void Update() // Only for Colins feel test 
	{
		if ( GameObject.Find( "EnemyManager" ) == null )
		{
			if ( Vector3.Normalize( GameObject.Find( "Player" ).transform.position - transform.position ).x < 0 && facingRight )	
			{
				Flip();
			}
				
			else if ( Vector3.Normalize( GameObject.Find( "Player" ).transform.position - transform.position ).x >= 0 && !facingRight )
			{
				Flip();
			}
		}

	}

	public State UppdateAttention( Transform player, List<Enemy> enemies )
	{
		if ( _healthcomponent.GetHealth() <= 0 ) // Dont continue if dead
		{
			state = State.Death;
			return state;
		}

		if ( state == State.Targeting ) 
			Search( player, enemies );
		else if ( state == State.Engaging ) 
			Engage();
		else if ( state == State.Attacking ) 
			Attack();
		else if ( state == State.Death ) 
			Die();

		if (xVel != 1 && state != State.Attacking) 
			FlipCheck(xVel);

		return state;
	}

	public State GetState() {
		return state;
	}

	void Search( Transform player, List<Enemy> enemies ) 
	{
		strafing = false;
		if ( this.target == null ) 
			SelectTarget( player, enemies );

		base.Move();
    }

	void SelectTarget( Transform player, List<Enemy> enemies ) 
	{
		int engagedToPlayer = 0;
		foreach (Enemy e in enemies)
		{
			if (e.target == player)
				engagedToPlayer += 1;
		}
		
		foreach (Enemy e in enemies)
		{	
			if ( engagedToPlayer < 3 )
			{
				target = player;
				state = State.Engaging;
				engagedToPlayer += 1;
			}
			else if ( e.target == null && e != this )
			{
				// Find one enemy, let's fight!
				target = e.transform;
				e.target = transform;
				e.state = State.Engaging;
				state = State.Engaging;
			}
		}
	}

    void Engage() 
	{

		//If engaged to player
		if( target == null ) // Dont continue if target is missing
		{
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
			myTarget = targetPos + new Vector2(range - 9.6f, 0f);
		// Right
		else if (target.position.x > transform.position.x)
			myTarget = targetPos - new Vector2(range - 9.6f, 0f);

		// Clamperino
		myTarget = new Vector2 (Mathf.Clamp( myTarget.x, -512f, 512f ), Mathf.Clamp( myTarget.y, -256f, 256f ) );

		/*Mathf.Clamp( myTarget.x, -8f, 8f );
		Mathf.Clamp( myTarget.y, -4f, 4f );*/

		// Add a bit of randomness to the target.
		myTarget += randomVector;


		// Get target direction.
		Vector2 targetDirection = myTarget - myPos;
		float distance = targetDirection.magnitude;
		Vector2 dir = targetDirection.normalized;
		xVel = dir.x;
		yVel = dir.y;
		
		// Enemy movement.
		// If close enough, Attack
		if (distance < 19.2f && state != State.Attacking && !attacked)
		{
			state = State.Attacking;

			// not sure why this is here /Arre
			if (!isIdle)
			{
				isIdle = true;
		
			}
			
			//Flip if not facing player.
			if (target.position.x < transform.position.x && base.facingRight)
				Flip();
			else if (target.position.x > transform.position.x && !base.facingRight)
				Flip();
		}
		else if (distance > 19.2f) // Move if not too close
		{
			_movement.Move( dir.x, dir.y, 1 );
		
			if (isIdle)
			{
				isIdle = false;
				_animComponent.PlayRunAnim();
			}
			
		}

    }

	void Attack() 
	{
		strafing = true;
		if (!attacked && isRanged)
		{
			attacked = true;
			StartCoroutine("RangedAttack");
		}
		else if (!attacked)
		{
			attacked = true;
			StartCoroutine("MeleeAttack");
		}


	}

	IEnumerator RangedAttack()
	{
		// Wait while Locking On
		yield return new WaitForSeconds(0.2f);

		// Shoot
		_animComponent.PlayAttackAnim();

		Vector2 pos = new Vector3( transform.position.x, transform.position.y + 32f, 0f);
		GameObject spawnedProjectile = (GameObject)Instantiate(enemyProjectile, pos, Quaternion.identity);
		if (base.facingRight)
		{
			spawnedProjectile.gameObject.GetComponent<EnemyProjectile>().MoveRight(true);
			spawnedProjectile.gameObject.GetComponent<DamageComponent>().Owner(gameObject.transform);
		}	
		else
		{
			spawnedProjectile.gameObject.GetComponent<EnemyProjectile>().MoveRight(false);
			spawnedProjectile.gameObject.GetComponent<DamageComponent>().Owner(gameObject.transform);
		}
			

		// Recover
		yield return new WaitForSeconds(0.5f);
		state = State.Engaging;
		attacked = false;
	}

	IEnumerator MeleeAttack()
	{
		_animComponent.PlayAttackAnim();

		yield return new WaitForSeconds(0.5f); // Recover
		state = State.Engaging;

	
		yield return new WaitForSeconds(1f); // Attack CD
		attacked = false;
		
	}

	void FlipCheck(float vel)
	{
		
		if (strafing && state == State.Engaging ) //Flip if not facing target
		{
			if (target.position.x < transform.position.x && base.facingRight)
				Flip();
			else if (target.position.x > transform.position.x && !base.facingRight)
				Flip();
		}
		else // flip according to velocity
		{
			// If moving left
			if (vel < 0 && facingRight)
				Flip();
			// if moving right
			else if (vel > 0 && !facingRight)
				Flip();
		}
		

	}

	void Flip()
	{
		//Debug.Log("Flip!");
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

    void Die() 
	{
        state = State.Death;
		GameManager.instance.IncreaseMoney( 10 );
    }
	


}
