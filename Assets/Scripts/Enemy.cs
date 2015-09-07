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
