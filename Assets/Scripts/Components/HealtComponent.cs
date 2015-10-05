using UnityEngine;
using System.Collections;

public class HealtComponent : MonoBehaviour {

	public int health;
	
	SpriteRenderer mySprite;
	Color defaultColor;
	float timer;

	//Game manager for death stuff
	public GameManager gameManager;

	void Awake() 
	{
		if ( GetComponent<SpriteRenderer>() != null )
		{
			mySprite = GetComponent<SpriteRenderer>();
			defaultColor = mySprite.color;
		}
		
	}

	void Update() // for test only plz
	{
		timer += Time.deltaTime;
		if (timer > 0.5f && mySprite != null)
			mySprite.color = defaultColor;

		//For testing
		if (gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.O))
		{
			health = 50000;
		}

	}

	public void TakeDamage( int damage )
	{
		health -= damage;
		if ( health <= 0 )
		{
			gameObject.SetActive(false);

			if (gameObject.tag == "Player")
			{
				gameManager.PlayerDied();
			}
		}

		timer = 0f;
		if (mySprite != null)
			mySprite.color = Color.blue;
		
		
		
	}

	public int GetHealth() 
	{
		return health;
	}

}
