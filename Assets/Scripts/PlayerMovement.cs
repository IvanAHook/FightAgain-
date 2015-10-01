using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(EquipmentComponent))]
public class PlayerMovement : MonoBehaviour {

	private Animator anim;
	private bool facingRight = true;

	EquipmentComponent _equipment;

	float speed;

	void Awake() 
	{
		_equipment = GetComponent<EquipmentComponent>();
		anim = GetComponentInChildren<Animator>();
		speed = 2;
	}

	void Update() 
	{
		Vector3 move = new Vector3( CrossPlatformInputManager.GetAxis( "Horizontal" ),
		                           CrossPlatformInputManager.GetAxis( "Vertical" ), 0 );

		transform.position = new Vector3( Mathf.Clamp( transform.position.x + move.x * speed * Time.deltaTime, -8f, 8f ),
		                                 Mathf.Clamp( transform.position.y + move.y * speed * Time.deltaTime, -4f, 4f ),
		                                 0f );

		if( CrossPlatformInputManager.GetAxis( "Horizontal" ) < 0 && facingRight ) 
			Flip();
		else if( CrossPlatformInputManager.GetAxis( "Horizontal" ) > 0 && !facingRight ) 
			Flip();

		if( CrossPlatformInputManager.GetAxis( "Vertical" ) != 0 || CrossPlatformInputManager.GetAxis( "Horizontal" ) != 0 ) 
			anim.SetFloat( "Speed", 1f );
		else 
            anim.SetFloat( "Speed", 0f );

        if( CrossPlatformInputManager.GetButtonDown( "Jump" ) )
			Attack( "Attack" );
        if( CrossPlatformInputManager.GetButtonDown( "Jump2" ) ) 
			ThrowWeapon();
	}

	void ThrowWeapon()
	{
		if( _equipment.weaponPrefab != null )
		{
			GameObject thrownWeapon = Instantiate( _equipment.weaponPrefab,
			                                      transform.TransformPoint( ((facingRight)? Vector3.right: Vector3.right*-1) * 1.5f ),
			                                      Quaternion.identity ) as GameObject;
			thrownWeapon.SetActive( true );

			if (facingRight)
				thrownWeapon.gameObject.GetComponent<WeaponComponent>().MoveRight(true);
			else
				thrownWeapon.gameObject.GetComponent<WeaponComponent>().MoveRight(false);
			thrownWeapon.GetComponent<WeaponComponent>().thrown = true;
			_equipment.DropWeapon();
		}
	}

    void Flip() 
	{
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Attack( string attack ) 
	{
        anim.SetTrigger( attack );
    }

	public void ChangeSpeed( int speed )
	{
		this.speed = speed;
	}

    private void OnTriggerEnter2D(Collider2D other) 
	{
		if ( other.tag == "Weapon" )
		{

		}
    }

}
