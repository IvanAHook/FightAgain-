using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class EnemyManager : MonoBehaviour {

	List<string> enemyWaves;
	int wave;
	bool spawning;
	List<Enemy> enemies = new List<Enemy>();
	public SpawnPoint[] spawnPoints;

	float waveTime;
	float waveDuration = 10f;

	public Enemy enemy;

	void Awake() {
		enemyWaves = ReadWavesFile( Application.dataPath + "/waves.txt" );
		//foreach (var item in enemyWaves) {
			//Debug.Log(item);
		//}
		wave = 0;
	}
    
    void Update () {
		waveTime += Time.deltaTime;
		if( enemies.Count > 0 ) {
			for( int i = 0; i < enemies.Count; i++ ) {
				if( enemies[ i ].UppdateAttention( GameManager.instance.GetPlayerPosition() ) == Enemy.State.Dead ) {
					Destroy( enemies[ i ].gameObject );
					enemies.RemoveAt( i );
				}
			}
		} else if( wave < enemyWaves.Count - 1 && spawning == false ) {
			StartCoroutine( SpawnWave() );
		}
		Debug.Log(enemies.Count);
    }

	IEnumerator SpawnWave() {
		waveTime = 0f;
		spawning = true;
		string wave = enemyWaves[ this.wave += 1 ];
		
		foreach (SpawnPoint sP in spawnPoints) {
			sP.resetEnemiesSpawned();
		}
		for( int i = 0; i < wave.Length; i++ ) {
			yield return new WaitForSeconds( 1f );
			SpawnEnemy( i );
		}
		spawning = false;
	}

	void SpawnEnemy( int point ) { // Do pooling for this
		for (int tryPoint = point; tryPoint < spawnPoints.Length; tryPoint++) {
			if( spawnPoints[ tryPoint ].isFree() ) {
				spawnPoints[ tryPoint ].increaseEnemiesSpawned( 1 );
				enemies.Add( Instantiate( enemy, spawnPoints[ tryPoint ].getPos(), Quaternion.identity ) as Enemy );
				return;
			} else {
				tryPoint += 1;
			}
		}
	}

	List<string> ReadWavesFile( string fileName ) {
		List<string> fileLines = new List<string>();
		try {
			string line;
			StreamReader reader = new StreamReader( fileName, Encoding.Default );
			using( reader ) {
				do {
					line = reader.ReadLine();
					if( line != null )
						fileLines.Add( line );
				} while ( line != null );

				reader.Close();
				return fileLines;
			}
		} catch( IOException e ) {
			Debug.Log( "error " + e.Message + " " + e.StackTrace );
		}
		return null;
	}

}
