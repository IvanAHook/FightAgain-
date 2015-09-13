using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	int money;
	
	void Awake() {
		if( instance == null )
			instance = this;
		else if( instance != this )
			Destroy( gameObject );
		DontDestroyOnLoad( gameObject );
	}

	void InitGame() {

	}

	void Update() {
	
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
