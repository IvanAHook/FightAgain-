using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(FileReaderComponent))]
public class EnemyManager : MonoBehaviour {

	FileReaderComponent _freader;

	List<string> enemyWaves;
	int wave;
	bool spawning;
	List<Enemy> enemies = new List<Enemy>();
	public SpawnPoint[] spawnPoints;

	float waveTime;
	float waveDuration = 10f;

	public Enemy enemy;

	void Awake() {
		_freader = GetComponent<FileReaderComponent>();
		enemyWaves = _freader.ReadWavesFile( Application.dataPath + "/waves.txt" );
		wave = 0;
	}
    
    void Update() {
		waveTime += Time.deltaTime;

		if( enemies.Count > 0 ) {

			for( int i = 0; i < enemies.Count; i++ ) {
				if( enemies[ i ].UppdateAttention( GameManager.instance.GetPlayerTransform(), enemies ) == Enemy.State.Death ) {
					Destroy( enemies[ i ].gameObject );
					enemies.RemoveAt( i );
				}
			}

		} else if( wave < enemyWaves.Count - 1 && spawning == false ) {
			StartCoroutine( SpawnWave() );
		}

    }

	IEnumerator SpawnWave() {
		waveTime = 0f;
		spawning = true; //TODO: Temp solution?
		string wave = enemyWaves[ this.wave += 1 ];

		for( int i = 0; i < wave.Length / spawnPoints.Length; i++ ) {
			yield return new WaitForSeconds( 1f );
			SpawnEnemy( i );
		}
		spawning = false;
	}

	void SpawnEnemy( int point ) { // Do pooling for this
		for (int i = 0; i < spawnPoints.Length; i++) {
			//spawnPoints[ i ].increaseEnemiesSpawned( 1 );
			enemies.Add( Instantiate( enemy, spawnPoints[ i ].getPos(), Quaternion.identity ) as Enemy );
		}

	}

}
