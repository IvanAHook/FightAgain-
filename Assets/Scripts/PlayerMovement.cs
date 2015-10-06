﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(EquipmentComponent))]
[RequireComponent(typeof(MovementComponent))]
[RequireComponent(typeof(SkeletonAnimation))]
public class PlayerMovement : MonoBehaviour {

	private Animator anim;
	private bool facingRight = true;

	EquipmentComponent _equipment;
	MovementComponent _movement;
	SkeletonAnimation _skelAnim;

	bool isIdle;

	[SpineAnimation( "Idle" )]
	public string idleAnimation;

	[SpineAnimation]
	public string moveAnimation;

	[SpineAnimation]
	public string attackAnimation;

	void Awake() 
	{
		_equipment = GetComponent<EquipmentComponent>();
		_movement = GetComponent<MovementComponent>();
		_skelAnim = GetComponent<SkeletonAnimation>();

	}

	void Update() 
	{
		// wtf?
		if (CrossPlatformInputManager.GetAxis("Vertical") != 0 || CrossPlatformInputManager.GetAxis("Horizontal") != 0)
		{
			UpdateInput();
		}
		else if (!isIdle)
		{
			isIdle = true;
			_skelAnim.AnimationName = idleAnimation;
			//_skelAnim.state.SetAnimation(0, "Idle", true);
			Debug.Log("Idle");
		}
			

		// Old prototype animations.
		/*if( CrossPlatformInputManager.GetAxis( "Vertical" ) != 0 || CrossPlatformInputManager.GetAxis( "Horizontal" ) != 0 ) 
			anim.SetFloat( "Speed", 1f );
		else 
            anim.SetFloat( "Speed", 0f );*/


        if( CrossPlatformInputManager.GetButtonDown( "Jump" ) )
			_skelAnim.AnimationName = attackAnimation;
        if( CrossPlatformInputManager.GetButtonDown( "Jump2" ) )
			_skelAnim.AnimationName = attackAnimation;

	}

	void UpdateInput ()
	{
		_movement.Move(CrossPlatformInputManager.GetAxis("Horizontal"),
					   CrossPlatformInputManager.GetAxis("Vertical"));

		if( CrossPlatformInputManager.GetAxis( "Horizontal" ) < 0 && facingRight )
			// _skelAnim.skeleton.FlipX = true;
			Flip();
		else if (CrossPlatformInputManager.GetAxis("Horizontal") > 0 && !facingRight)
			// _skelAnim.skeleton.FlipX = false;
			Flip();

		if (isIdle)
		{
			isIdle = false;
			_skelAnim.AnimationName = moveAnimation;
			// _skelAnim.state.SetAnimation(0, "Run", true);
			Debug.Log("Run");
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

    void Attack( string attack ) 
	{
    }

    private void OnTriggerEnter2D(Collider2D other) 
	{
		if ( other.tag == "Weapon" )
		{

		}
    }

}
