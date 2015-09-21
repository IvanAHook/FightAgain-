using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

    public int health = 100;
    public float speed;
    public Vector2 direction;

	

    public List<Enemy> engagedWithEnemies;


    public virtual void Start() {

    }

    public virtual void Move() {


    }

    public virtual void Move(Vector2 targetDirection) {
    }

    public int GetNumberEngagedWith() {
        return engagedWithEnemies.Count;
    }

}