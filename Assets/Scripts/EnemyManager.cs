using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class EnemyManager : MonoBehaviour {

	//List<Enemy> enemies = new List<Enemy>();

	List<string> enemyWaves;
    public Enemy[] enemies;

	void Start() {
		enemyWaves = ReadWavesFile( Application.dataPath + "/waves.txt" );
		foreach (var item in enemyWaves) {
			Debug.Log(item);
		}
	}
    
    void Update () {
        foreach( var e in enemies ) {
            e.UppdateAttention();
        }
    }

    void SpawnWave( int waveNo ) {
		string wave = enemyWaves[ waveNo ];
		string[] thisWave;
		for( int i = 0; i < wave.Length; i++ ) {
			thisWave[i] = wave[i].ToString();
		}
		Debug.Log( thisWave );
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
