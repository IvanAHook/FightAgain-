﻿using UnityEngine;
using System.Collections;

public class AnimationComponent : MonoBehaviour {

	SkeletonAnimation skelAnim;
	SkeletonRenderer skelRenderer;
	bool likesHats = false;

	 // ATTACHMENTS
	public Sprite[] hats;

	//public Sprite mySprite;
	[SpineSlot]
	public string slot;
	[SpineSkin]
	public string skin;

	// ANIMATIONZ
	SkeletonAnimation _skelAnim;
	[SpineAnimation]
	public string idleAnimation;
	[SpineAnimation]
	public string moveAnimation;
	[SpineAnimation]
	public string attackAnimation;
	[SpineAnimation]
	public string rangedAttackAnimation;
	[SpineAnimation]
	public string gotHitAnimation;

	bool animCheck = false;

	void Start () 
	{
		int rng = Random.Range(0, 10);
		if (rng > 6)
		{
			likesHats = true;
		}

		
		skelRenderer = GetComponent<SkeletonRenderer>();
		if (hats.Length > 0 && likesHats)
		{
			int randomInt = Random.Range(0, hats.Length);
			skelRenderer.skeleton.Data.AddUnitySprite(slot, hats[randomInt], skin, "Sprites/Default");
			skelRenderer.skeleton.SetAttachment(slot, hats[randomInt].name);
		}
		

		// ANIMATIONZ
		_skelAnim = GetComponent<SkeletonAnimation>();

		
	}

	void Update ()
	{
		if (animCheck && _skelAnim.state.GetCurrent(0) == null )
		{
			animCheck = false;
			PlayIdleAnim();
		}

			
			
	}
	
	public void PlayIdleAnim()
	{
		_skelAnim.state.SetAnimation(0, idleAnimation, true);
		animCheck = false;
	}

	public void PlayAttackAnim()
	{
		_skelAnim.state.SetAnimation(0, attackAnimation, false);
		animCheck = true;
	}
	
	public void PlayRunAnim ()
	{
		_skelAnim.state.SetAnimation(0, moveAnimation, true);
		animCheck = false;
	}

	public void PlayGotHitAnim ()
	{
		_skelAnim.state.SetAnimation(0, gotHitAnimation, false);
		animCheck = true;
	}
}
