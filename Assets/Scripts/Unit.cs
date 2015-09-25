using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

	private Animator animator;
	
	public int health = 100;
    public float speed;
    public Vector2 direction;

	public bool facingRight;

    public List<Enemy> engagedWithEnemies;

    public virtual void Start() 
	{
		animator = GetComponentInChildren<Animator>();
		facingRight = true;
    }

    public virtual void Move() 
	{

    }

    public virtual void Move(Vector2 targetDirection) 
	{

    }

	public void FlipCheck(float vel)
	{
		//Debug.Log(vel);

		// If moving left
		if (vel < 0 && facingRight)
		{
			Flip();
		}
		// if moving right
		else if (vel > 0 && !facingRight)
		{
			Flip();
		}

	}

	public void Flip()
	{
		//Debug.Log("Flip!");
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	// Not working right now. Using SetSpeed() instead.
	public void SpeedCheck (float xVel, float yVel)
	{
		if (xVel > 0f || yVel > 0f)
		{
			animator.SetFloat("Speed", 1f);
		}
		else
		{
			animator.SetFloat("Speed", 0f);
		}
	}


	public void SetSpeed(int spd)
	{
		if (spd == 1)
		{
			animator.SetFloat("Speed", 1f);
		}
		else if (spd == 0)
		{
			animator.SetFloat("Speed", 0f);
		}
	}

	public void AttackAnim()
	{
		animator.SetTrigger("Attack");
	}

    public int GetNumberEngagedWith() 
	{
        return engagedWithEnemies.Count;
    }

}