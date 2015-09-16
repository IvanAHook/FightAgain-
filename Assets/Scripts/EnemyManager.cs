using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class EnemyManager : MonoBehaviour {

	//List<Enemy> enemies = new List<Enemy>();

	List<string> enemyWaves;
	List<Enemy> enemies = new List<Enemy>();
	public SpawnPoint[] spawnPoints;

	public Enemy enemy;

	void Start() {
		enemyWaves = ReadWavesFile( Application.dataPath + "/waves.txt" );
		foreach (var item in enemyWaves) {
			Debug.Log(item);
		}
		SpawnWave( 0 );
	}
    
    void Update () {
		if( enemies.Count > 0 ) {
			foreach( var e in enemies ) {
				e.UppdateAttention();
			}
		}
    }

    void SpawnWave( int waveNo ) {
		string wave = enemyWaves[ waveNo ];

		foreach (SpawnPoint sP in spawnPoints) {
			sP.resetEnemiesSpawned();
		}

		//string[] thisWave;
		for( int i = 0; i < wave.Length; i++ ) {
			enemies.Add( SpawnEnemy( i ) );
			//thisWave[i] = wave[i].ToString();
		}
		//Debug.Log( thisWave );
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
