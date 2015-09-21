using UnityEngine;
using System.Collections;

public class Enemy : Unit {

	enum State { Searching, Engaged, Dead };
	State state;

    public Transform target;

	public override void Start() {
		state = State.Searching;
        base.Start();
	}

	public void UppdateAttention() {
		if (state == State.Searching) {
			Search();
		} else if (state == State.Engaged) {
			Engage();
		} else if (state == State.Dead) {
			Die();
		}
	}

    void Search() {

		//If engaged to player
		Vector2 myPos = new Vector2(transform.position.x, transform.position.y);

		Vector2 targetDirection = GameManager.instance - myPos;

		transform.Translate(targetDirection.normalized * 1f * Time.deltaTime);
		
		base.Move();
    }

    void Engage() {
        if( target != null ) {
            base.Move(target.position);
        }
    }

    void Die() {
        state = State.Dead;
    }

    void OnTriggerEnter2D( Collider2D other ) {
        if( other.tag == "Wall" ) {
            base.direction = new Vector2( direction.x * -1, direction.y * -1 );
        } else if( other.tag == "Player" ) {
            target = other.transform;
            state = State.Engaged;
        }
    }

}
