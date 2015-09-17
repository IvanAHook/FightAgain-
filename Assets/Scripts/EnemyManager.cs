using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class EnemyManager : MonoBehaviour {

	List<string> enemyWaves;
	int wave;
	List<Enemy> enemies = new List<Enemy>();
	public SpawnPoint[] spawnPoints;

	public Enemy enemy;

	void Awake() {
		enemyWaves = ReadWavesFile( Application.dataPath + "/waves.txt" );
		foreach (var item in enemyWaves) {
			Debug.Log(item);
		}
		wave = 0;
		SpawnWave( wave );
	}
    
    void Update () {
		if( enemies.Count > 0 ) {
			for( int i = 0; i < enemies.Count; i++ ) {
				if( enemies[ i ].UppdateAttention() == Enemy.State.Dead ) {
					Destroy( enemies[ i ].gameObject );
					Debug.Log(enemies[ i ].UppdateAttention());
					enemies.RemoveAt( i );
				}
			}
		} else if( wave < enemyWaves.Count - 1 ) {
			SpawnWave( wave += 1 );
		}
    }

    void SpawnWave( int waveNo ) {
		string wave = enemyWaves[ waveNo ];

		foreach (SpawnPoint sP in spawnPoints) {
			sP.resetEnemiesSpawned();
		}

		for( int i = 0; i < wave.Length; i++ ) {
			enemies.Add( SpawnEnemy( i ) );
		}
		foreach (var item in enemies) {
			Debug.Log(item.name);
		}
    }

	Enemy SpawnEnemy( int point ) { // Do pooling for this

		for (int tryPoint = point; tryPoint < spawnPoints.Length; tryPoint++) {
			if( spawnPoints[ tryPoint ].isFree() ) {
				spawnPoints[ tryPoint ].increaseEnemiesSpawned( 1 );
				return Instantiate( enemy, spawnPoints[ tryPoint ].getPos(), Quaternion.identity ) as Enemy;
			} else {
				tryPoint += 1;
			}
		}
		Debug.Log( "null enemy fore some reason" );
		return null;
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
