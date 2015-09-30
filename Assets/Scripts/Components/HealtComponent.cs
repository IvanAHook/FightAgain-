using UnityEngine;
using System.Collections;

public class HealtComponent : MonoBehaviour {

	public int health;
	
	SpriteRenderer mySprite;
	Color defaultColor;
	float timer;

	void Awake() 
	{
		mySprite = GetComponent<SpriteRenderer>();
		defaultColor = mySprite.color;
		if (mySprite == null)
			Debug.LogError("Unable to find Sprite component");
	}

	void Update() // for test only plz
	{
		timer += Time.deltaTime;
		if (timer > 0.5f)
			mySprite.color = defaultColor;
	}

	public void TakeDamage( int damage )
	{
		health -= damage;
		if ( health <= 0 )
			gameObject.SetActive( false );

		mySprite.color = Color.blue;
		timer = 0f;
		
	}

	public int GetHealth() 
	{
		return health;
	}

}
