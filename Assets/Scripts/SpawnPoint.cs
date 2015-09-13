using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

	int id;
	int enemiesSpawned;

	public void setId( int id ) {
		this.id = id;
	}

	public bool isFree() {
		return ( enemiesSpawned == 0 ) ? true : false;
	}

	public Vector2 getPos() {
		return new Vector2( transform.position.x, transform.position.y );
	}
	public void increaseEnemiesSpawned( int i ) {
		enemiesSpawned += i;
	}

	public void resetEnemiesSpawned() {
		enemiesSpawned = 0;
	}

}
