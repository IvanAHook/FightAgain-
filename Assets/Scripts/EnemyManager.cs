using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

	FileReader _fReader;

	List<string> enemyWaves;
	int wave;
	bool spawning;
	List<Enemy> enemies = new List<Enemy>();
	public SpawnPoint[] spawnPoints;

	float waveTime;
	float waveDuration = 10f;

	public Enemy enemy;

	public bool spawnOne;

	void Awake() 
	{
		_fReader = new FileReader();
		enemyWaves = _fReader.ReadWavesFile( Application.dataPath + "/waves.txt" );
		wave = 0;
		if ( spawnOne )
			SpawnEnemy( 1 );
	}
    
    void Update() 
	{
		waveTime += Time.deltaTime;

		if( enemies.Count > 0 ) 
		{
			for( int i = 0; i < enemies.Count; i++ ) 
			{
				if( enemies[ i ].UppdateAttention( GameManager.instance.GetPlayerTransform(), enemies ) == Enemy.State.Death )
				{
					Destroy( enemies[ i ].gameObject );
					enemies.RemoveAt( i );
				}
			}

		} 
		else if( wave < enemyWaves.Count  && spawning == false && !spawnOne ) 
		{
			StartCoroutine( SpawnWave() );
		}

    }

	IEnumerator SpawnWave() 
	{
		waveTime = 0f;
		spawning = true; //TODO: Temp solution?
		string wave = enemyWaves[ this.wave += 1 ];

		for( int i = 0; i < wave.Length / spawnPoints.Length; i++ ) 
		{
			yield return new WaitForSeconds( 1f ); // This breaks on level load for some reason
			for (int j = 0; j < spawnPoints.Length; j++) 
				enemies.Add( SpawnEnemy( j ) );
		}
		spawning = false;
	}

	Enemy SpawnEnemy( int point ) 
	{ // Do pooling for this
		return Instantiate( enemy, spawnPoints[ point ].getPos(), Quaternion.identity ) as Enemy;
	}

}
