﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

	public float Speed;

	private Animator anim;
	private bool facingRight = true;

	public GameObject explosion;

	void Awake() 
	{
		anim = GetComponentInChildren<Animator>();
	}
	

	void Update() 
	{

		transform.Translate( Vector3.up * CrossPlatformInputManager.GetAxis( "Vertical" ) * Speed * Time.deltaTime );
		transform.Translate( Vector3.right * CrossPlatformInputManager.GetAxis( "Horizontal" ) * Speed * Time.deltaTime );

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
            Attack( "Attack2" );
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
        explosion.transform.position = other.transform.position;
        explosion.GetComponent<Animator>().SetTrigger( "Play" );
    }

}
