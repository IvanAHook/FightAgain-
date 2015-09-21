using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	Transform playerTransform;
	int money;
	
	void Awake() {
		if( instance == null )
			instance = this;
		else if( instance != this )
			Destroy( gameObject );
		DontDestroyOnLoad( gameObject );
	}

	void Start() {
		playerTransform = GameObject.FindGameObjectWithTag( "Player" ).transform;
	}

	void InitGame() {

	}

	void Update() {
		playerTransform = GameObject.FindGameObjectWithTag( "Player" ).transform;
	}

	public Vector2 GetPlayerPosition() {
		return ( playerTransform != null ) ? new Vector2( playerTransform.position.x, playerTransform.position.y ) : Vector2.zero;
	}

	bool canAfford( int price ) {
		return ( money > price ) ? true : false;
	}

	int increaseMoney( int ammount ) {
		return money += ammount;
	}

	int decreaseMoney( int ammount ) {
		return money -= ammount;
	}

}
