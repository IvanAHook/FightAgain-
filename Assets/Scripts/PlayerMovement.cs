﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using CnControls;

[RequireComponent(typeof(EquipmentComponent))]
[RequireComponent(typeof(MovementComponent))]
public class PlayerMovement : MonoBehaviour {

	private Animator anim;
	private bool facingRight = true;

	EquipmentComponent _equipment;
	MovementComponent _movement;

	SkeletonAnimation skelAnim;
	bool isIdle;

	void Awake() 
	{
		_equipment = GetComponent<EquipmentComponent>();
		_movement = GetComponent<MovementComponent>();
		anim = GetComponentInChildren<Animator>();

		skelAnim = GetComponent<SkeletonAnimation>();

	}

	void Update() 
	{
		//CrossPlatformInputManager.GetAxis("Vertical") != 0 || CrossPlatformInputManager.GetAxis("Horizontal") != 0
		if ( CnInputManager.GetAxis( "Vertical" ) != 0 || CnInputManager.GetAxis( "Horizontal" ) != 0 )
		{
			UpdateInput();
		}
		else if (!isIdle)
		{
			isIdle = true;
			skelAnim.state.SetAnimation(0, "Idle", true);
		}
			

		// Old prototype animations.
		/*if( CrossPlatformInputManager.GetAxis( "Vertical" ) != 0 || CrossPlatformInputManager.GetAxis( "Horizontal" ) != 0 ) 
			anim.SetFloat( "Speed", 1f );
		else 
            anim.SetFloat( "Speed", 0f );*/


        if( CrossPlatformInputManager.GetButtonDown( "Jump" ) )
			Attack( "Attack" );
        if( CrossPlatformInputManager.GetButtonDown( "Jump2" ) ) 
			Attack( "Attack2" );
	}

	void UpdateInput ()
	{
		_movement.Move(CnInputManager.GetAxis( "Horizontal" ),
					   CnInputManager.GetAxis( "Vertical") );

		if (CnInputManager.GetAxis("Horizontal") < 0 && facingRight)
			Flip();
		else if (CnInputManager.GetAxis( "Horizontal") > 0 && !facingRight)
			Flip();

		if (isIdle)
		{
			isIdle = false;
			skelAnim.state.SetAnimation(0, "Run", true);
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
        anim.SetTrigger( attack );
    }

    private void OnTriggerEnter2D(Collider2D other) 
	{
		if ( other.tag == "Weapon" )
		{

		}
    }

}
