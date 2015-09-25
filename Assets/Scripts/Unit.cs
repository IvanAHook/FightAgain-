using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

    public int health = 100;
    public float speed;
    public Vector2 direction;

	bool facingRight;

    public List<Enemy> engagedWithEnemies;

    public virtual void Start() 
	{

    }

    public virtual void Move() 
	{

    }

    public virtual void Move(Vector2 targetDirection) 
	{

    }

	public void FlipCheck()
	{
		Rigidbody rb = gameObject.GetComponent<Rigidbody>();
		
		if (rb.velocity.x > 0 && !facingRight)
		{
			Flip();
		}
		else if (rb.velocity.x < 0 && facingRight)
		{
			Flip();
		}
	}

	public void Flip()
	{
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

    public int GetNumberEngagedWith() 
	{
        return engagedWithEnemies.Count;
    }

}