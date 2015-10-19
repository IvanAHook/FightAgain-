using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	enum GameState { Menu, Fight };
	GameState state;

	public static GameManager instance = null;

	public GameObject playerPrefab;

	bool playerDied = false;

	Transform playerTransform;
	int money;

	public GameObject menu;

	public WeaponData weapon;
	public EquipmentData head;
	public EquipmentData body;
	public EquipmentData feet;
	public static EquipmentList equipmentList;

	public bool collisionHandling;
	
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
		equipmentList = new EquipmentList();
		money = 0;
	}

	void InitGame()
	{
		state = GameState.Menu;
		// load menu
	}

	void Update()
	{
		if( playerTransform && playerTransform.gameObject.activeSelf == false ) { //TODO wtf?
			playerTransform = GameObject.FindGameObjectWithTag( "Player" ).transform;
		}

		if (playerDied) {
			playerDied = false;
			menu.GetComponent<Menu>().DeathScreen();
		}

	}

	public void LoadArena()
	{
		state = GameState.Fight;
		Application.LoadLevel("FirstTest");
	}

	void OnLevelWasLoaded( int level )
	{
		if( level == 1 )
		{
			SpawnPlayer();
		}
	}

	void LoadShop()
	{

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

/*		if( weapon != null )
			playerEquipment.EquipWeapon( weapon.type, weapon.name );
		if( head != null )
			playerEquipment.Equip( head.type, head.name );
		if( body != null )
			playerEquipment.Equip( body.type, body.name );
		if( feet != null )
			playerEquipment.Equip( feet.type, feet.name );*/
		//player.GetComponent<EquipmentComponent>().Equip( weapon, head, body, feet );
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
