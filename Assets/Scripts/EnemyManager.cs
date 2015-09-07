using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

	//List<Enemy> enemies = new List<Enemy>();
	
    public Enemy[] enemies;
    
    void Update () {
        foreach( var e in enemies ) {
            e.UppdateAttention();
        }
    }

    void SpawnWave(int waveNo) {

    }

}
