using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	enum GameState { Menu, Fight };
	static GameState state;

	public static GameManager instance = null;

	public GameObject playerPrefab;

	bool playerDied = false;
	Transform playerTransform;
	int money;

	public GameObject menuPrefab;
	public static GameObject menuInstance = null;

	public WeaponData weapon;
	public EquipmentData head;
	public EquipmentData body;
	public EquipmentData feet;
	public static EquipmentList equipmentList;
	
	void Awake()
	{
		if( instance == null )
			instance = this;
		else if( instance != this )
			Destroy( gameObject );
		DontDestroyOnLoad( gameObject );
	}

	void Start()
	{
		SpawnPlayer(); // Temp / Arre Barre

		equipmentList = new EquipmentList();
		weapon = equipmentList.GetWeapon( 0 );
		head = new EquipmentData( EquipmentData.Type.Head, "Naked", 1, 1f );
		body = new EquipmentData( EquipmentData.Type.Body, "Naked", 1, 1f );
		feet = new EquipmentData( EquipmentData.Type.Feet, "Naked", 1, 1f );
		money = 0;
	}

	void InitGame()
	{
		state = GameState.Menu;
		// load menu
	}

	void Update()
	{
		if( playerTransform && playerTransform.gameObject.activeSelf == true ) {
			playerTransform = GameObject.FindGameObjectWithTag( "Player" ).transform;
		}

		// Player dies.
		if (playerDied) 
		{
			playerDied = false; //"Do Once"

			// Spawn menu if it doesn't exist.
			if (menuInstance == null)
				menuInstance = Instantiate(menuPrefab, Vector3.zero, Quaternion.identity) as GameObject;
				
			menuInstance.GetComponent<Menu>().DeathScreen();
		}
	}

	void LoadMenu()
	{
		state = GameState.Menu;
		Application.LoadLevel("MainMenu");
	}

	public void LoadArena()
	{
		state = GameState.Fight;
		Application.LoadLevel("ArenaLevel");
	}

	void OnLevelWasLoaded( int level )
	{
		if( level == 2 )
		{
			SpawnPlayer();
		}
	}

	void SpawnPlayer()
	{
		GameObject player = Instantiate( playerPrefab, Vector3.zero, Quaternion.identity ) as GameObject;
		playerTransform = player.transform;
		EquipmentComponent playerEquipment = playerTransform.GetComponent<EquipmentComponent>();
		playerEquipment.weapon = weapon;
		playerEquipment.head = head;
		playerEquipment.body = body;
		playerEquipment.feet = feet;
	}

	public Vector2 GetPlayerPosition()
	{
		return ( playerTransform != null ) ? new Vector2( playerTransform.position.x, playerTransform.position.y ) : Vector2.zero;
	}

	public Transform GetPlayerTransform()
	{
		return playerTransform;
	}

	public int GetMoney()
	{
		return money;
	}

	bool CanAfford( int price )
	{
		return ( money > price ) ? true : false;
	}

	public int IncreaseMoney( int ammount )
	{
		return money += ammount;
	}

	public int DecreaseMoney( int ammount )
	{
		return money -= ammount;
	}

	public void PlayerDied()
	{
		playerDied = true;
	}

}
