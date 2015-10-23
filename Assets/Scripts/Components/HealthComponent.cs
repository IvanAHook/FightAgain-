using UnityEngine;
using System.Collections;

public class HealthComponent : MonoBehaviour {
	
	public int health;
	[HideInInspector]public float armorValue = 0;
	
	SpriteRenderer mySprite;
	Color defaultColor;
	float timer;
	bool invulnerable = false;
	float invTime = 0.15f;
	
	void Update() // for test only plz
	{
		timer += Time.deltaTime;
		if (timer > invTime)
			invulnerable = false;
			
			//For testing
			if (gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.O))
		{
			health = 50000;
		}
		
	}
	
	public void TakeDamage( int damage )
	{
		if (!invulnerable)
		{

			health -= damage;
			
			if (gameObject.tag == "Enemy")
			{
				GetComponent<AnimationComponent>().PlayGotHitAnim(); // UUGHLY
			}

			if (health <= 0)
			{
				gameObject.SetActive(false);

				if (gameObject.tag == "Player")
				{
					GameManager.instance.PlayerDied();
				}
			}
			invulnerable = true;
			timer = 0f;
		}
		
	}
	
	public void TakeDamageFromPlayer( int damage )
	{
		health -= damage;
		if ( health <= 0 )
		{
			GameManager.instance.IncreaseMoney( 10 ); // store value in enemy
			
			gameObject.SetActive(false);
		}
		timer = 0f;
	}
	
	public int GetHealth() 
	{
		return health;
	}
	
}
