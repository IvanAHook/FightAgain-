using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	bool playerDied = false;

	Transform playerTransform;
	int money;
	
	public Transform player;
	public GameObject menu;
	
	void Awake() {
		if( instance == null )
			instance = this;
		else if( instance != this )
			Destroy( gameObject );
		DontDestroyOnLoad( gameObject );
	}

	void Start() {
		// Singelton Player instance?
		Debug.Log("Start");
		playerTransform = GameObject.FindGameObjectWithTag( "Player" ).transform;
		Debug.Log( "Player transform is " + playerTransform );
		money = 0;
	}

	void InitGame() {

	}

	void Update() {
		if( playerTransform && playerTransform.gameObject.activeSelf ) {
			playerTransform = GameObject.FindGameObjectWithTag( "Player" ).transform;
		}

		if (playerDied) {
			playerDied = false;
			menu.GetComponent<Menu>().DeathScreen();


		}
	}

	void LoadArena() {

	}

	void LoadShop() {

	}

	Transform SpawnPlayer() {
		return Instantiate( player, Vector3.zero, Quaternion.identity ) as Transform;
	}

	public Vector2 GetPlayerPosition() {
		return ( playerTransform != null ) ? new Vector2( playerTransform.position.x, playerTransform.position.y ) : Vector2.zero;
	}

	public Transform GetPlayerTransform() {
		return playerTransform;
	}

	public int GetMoney() {
		return money;
	}

	bool CanAfford( int price ) {
		return ( money > price ) ? true : false;
	}

	public int IncreaseMoney( int ammount ) {
		return money += ammount;
	}

	int DecreaseMoney( int ammount ) {
		return money -= ammount;
	}

	public void PlayerDied()
	{
		playerDied = true;
	}

}
