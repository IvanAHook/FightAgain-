using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public GameObject playerPrefab;

	bool playerDied = false;

	Transform playerTransform;
	int money;
	
	public Transform player;
	public GameObject menu;

	public WeaponData weapon;
	public EquipmentData head;
	public EquipmentData body;
	public EquipmentData feet;
	EquipmentList equipmentList;
	
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
		//SpawnPlayer();

	}

	void InitGame()
	{

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

		// Singelton Player instance?
		if(playerTransform == null)
		{
			playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		}
		
	}

	void LoadArena()
	{
		// load level
		// spawn player
	}

	void LoadShop()
	{

	}

	Transform SpawnPlayer()
	{
		player = Instantiate( playerPrefab, Vector3.zero, Quaternion.identity ) as Transform;
		player.GetComponent<EquipmentComponent>().Equip( weapon, head, body, feet );
		return player;
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

	int DecreaseMoney( int ammount )
	{
		return money -= ammount;
	}

	public void PlayerDied()
	{
		playerDied = true;
	}

}
