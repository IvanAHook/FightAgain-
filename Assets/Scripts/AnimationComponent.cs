using UnityEngine;
using System.Collections;

public class AnimationComponent : MonoBehaviour {

	SkeletonAnimation skelAnim;
	SkeletonRenderer skelRenderer;
	bool likesHats = false;

	 // ATTACHMENTS
	public Sprite[] hats;
	


	//public Sprite mySprite;
	[SpineSkin]
	public string[] skins;
	[SpineSlot]
	public string slot;


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

		int randomSkin = Random.Range(0, skins.Length);
		int randomHat = Random.Range(0, hats.Length);
		
		skelRenderer = GetComponent<SkeletonRenderer>();
		
		if (skins.Length > 0)
		{
			skelRenderer.skeleton.SetSkin(skins [randomSkin] );
		}
			
		if (hats.Length > 0 && likesHats)
		{
			skelRenderer.skeleton.Data.AddUnitySprite(slot, hats[randomHat], skins [randomSkin ], "Sprites/Default");
			skelRenderer.skeleton.SetAttachment(slot, hats[randomHat].name);
		}
		

		// ANIMATIONZ
		_skelAnim = GetComponent<SkeletonAnimation>();

		
	}

	void Update ()
	{
		if (animCheck && _skelAnim.state.GetCurrent(0) == null )
		{
			animCheck = false;
			AnimCheck();
		}
	}

	
	// not sure I want this.
	/*public void PlayAnim( int track, string animation, bool loop )
	{
		_skelAnim.state.SetAnimation( track, animation, loop );
	}*/
	
	public void PlayIdleAnim()
	{
		if (!animCheck)
		//_skelAnim.state.AddAnimation(0, idleAnimation, true, 0f);
		_skelAnim.state.SetAnimation(0, idleAnimation, true);
	}

	public void PlayAttackAnim()
	{
		if (!animCheck)
		{
			//_skelAnim.state.AddAnimation(0, attackAnimation, false, 0f);
			_skelAnim.state.SetAnimation(0, attackAnimation, false);
			animCheck = true;
		}
	
	}
	
	public void PlayRunAnim ()
	{
		if (!animCheck)
			//_skelAnim.state.AddAnimation(0, moveAnimation, true, 0f);
			_skelAnim.state.SetAnimation(0, moveAnimation, true);
	}

	public void PlayGotHitAnim ()
	{
		if (!animCheck)
		{
			//_skelAnim.state.AddAnimation(0, gotHitAnimation, false, 0f);
			_skelAnim.state.SetAnimation(0, gotHitAnimation, false);
			animCheck = true;
		}
		
	}

	void AnimCheck()
	{
		if (GetComponent<Enemy>() != null)
		{
			if (GetComponent<Enemy>().GetState().ToString() == "Engaging")
			{
				PlayRunAnim();
			}
			else
				PlayIdleAnim();
			
		}
	}
}
