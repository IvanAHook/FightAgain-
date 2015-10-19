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

	float attackRange = 1f;
	float rAttackRange = 5f;
	float range;
	bool isRanged;
	
	bool attacked = false;

	float xVel;
	float yVel;
	Vector2 randomVector;

	bool isIdle;

	void Awake() 
	{
		_healthcomponent = GetComponent<HealthComponent>();
		_movement = GetComponent<MovementComponent>();
		_animComponent = GetComponent<AnimationComponent>();
	}


	public override void Start() 
	{
		state = State.Targeting;

		// Temporary
		// Make enemy melee or ranged at random
		int meleeOrRanged = Random.Range(0, 2);
		
		if (meleeOrRanged == 0)
			isRanged = false;
		else
			isRanged = false;

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
			if ( Vector3.Normalize( GameObject.Find( "Player" ).transform.position
			          - transform.position ).x < 0 && facingRight )
				Flip();
			else if ( Vector3.Normalize( GameObject.Find( "Player" ).transform.position
			                            - transform.position ).x >= 0 && !facingRight )
				Flip();
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
			base.FlipCheck(xVel);

		return state;
	}

	public State GetState() {
		return state;
	}

	void Search( Transform player, List<Enemy> enemies ) 
	{
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
			if ( engagedToPlayer <= 3 )
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
			myTarget = targetPos + new Vector2(range - 0.15f, 0f);
		// Right
		else if (target.position.x > transform.position.x)
			myTarget = targetPos - new Vector2(range - 0.15f, 0f);

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
		if (distance < 0.3f && state != State.Attacking && !attacked)
		{
			state = State.Attacking;

			// Set speed for animation. Not needed for spine
			// base.SetSpeed(0);
			if (!isIdle)
			{
				isIdle = true;
				_animComponent.PlayIdleAnim();
			}
			

			//Flip if not facing player.
			if (target.position.x < transform.position.x && base.facingRight)
				base.Flip();
			else if (target.position.x > transform.position.x && !base.facingRight)
				base.Flip();
		}
		else if (distance > 0.3f) // Move if not too close
		{
			//transform.Translate(dir * moveSpeed * Time.deltaTime);
			_movement.Move( dir.x, dir.y );
			//base.SetSpeed(1); // not needed for spine

			if (isIdle)
			{
				isIdle = false;
				_animComponent.PlayRunAnim();
			}
			
		}

    }

	void Attack() 
	{

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

		Vector2 pos = new Vector3( transform.position.x, transform.position.y + 0.5f, 0f);
		GameObject spawnedProjectile = (GameObject)Instantiate(enemyProjectile, pos, Quaternion.identity);
		if (base.facingRight)
		{
			spawnedProjectile.gameObject.GetComponent<EnemyProjectile>().MoveRight(true);
			spawnedProjectile.gameObject.GetComponent<DamageComponent>().Owner(gameObject.GetComponentInChildren<Collider2D>().gameObject.transform);
		}	
		else
		{
			spawnedProjectile.gameObject.GetComponent<EnemyProjectile>().MoveRight(false);
			spawnedProjectile.gameObject.GetComponent<DamageComponent>().Owner(gameObject.GetComponentInChildren<Collider2D>().gameObject.transform);
		}
			

		// Recover
		yield return new WaitForSeconds(0.5f);
		state = State.Engaging;
		attacked = false;
	}

	IEnumerator MeleeAttack()
	{
		_animComponent.PlayAttackAnim();

		yield return new WaitForSeconds(0.5f);
		state = State.Engaging;
		
		yield return new WaitForSeconds(1f); // Recover
		attacked = false;
		
	}

    void Die() 
	{
        state = State.Death;
		GameManager.instance.IncreaseMoney( 10 );
    }
	


}
