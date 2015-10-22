using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using CnControls;

[RequireComponent(typeof(EquipmentComponent))]
[RequireComponent(typeof(MovementComponent))]
[RequireComponent(typeof(SkeletonAnimation))]
public class PlayerMovement : MonoBehaviour {

	EquipmentComponent _equipment;
	MovementComponent _movement;
	
	// ANIMATIONS
	SkeletonAnimation _skelAnim;
	[SpineAnimation( "Idle" )]
	public string idleAnimation;
	[SpineAnimation]
	public string moveAnimation;
	[SpineAnimation]
	public string attackAnimation;

	bool isIdle;
	private bool facingRight = true;

	void Awake() 
	{
		_equipment = GetComponent<EquipmentComponent>();
		_movement = GetComponent<MovementComponent>();
		_skelAnim = GetComponent<SkeletonAnimation>();

	}

	void Update() 
	{

		if (CnInputManager.GetAxis("Vertical") != 0 || CnInputManager.GetAxis("Horizontal") != 0)
		{
			UpdateInput();
		}
		else if (!isIdle)
		{
			isIdle = true;
			_skelAnim.state.SetAnimation(0, idleAnimation, true);
		}

        if( CrossPlatformInputManager.GetButtonDown( "Jump" ) || Input.GetKeyDown(KeyCode.U) )
			_skelAnim.state.SetAnimation( 0, attackAnimation, false );
        if( CrossPlatformInputManager.GetButtonDown( "Jump2" ) )
			_skelAnim.state.SetAnimation(0, attackAnimation, false );

	}

	void UpdateInput ()
	{
		_movement.Move(CnInputManager.GetAxis("Horizontal"),
					   CnInputManager.GetAxis("Vertical"));

		if (CnInputManager.GetAxis("Horizontal") < 0 && facingRight)
			Flip();
		else if (CnInputManager.GetAxis("Horizontal") > 0 && !facingRight)
			Flip();

		if (isIdle)
		{
			isIdle = false;
			_skelAnim.state.SetAnimation(0,  moveAnimation, true);
		}
	
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

}
