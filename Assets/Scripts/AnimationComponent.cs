using UnityEngine;
using System.Collections;

public class AnimationComponent : MonoBehaviour {

	SkeletonAnimation skelAnim;
	SkeletonRenderer skelRenderer;

	public Sprite mySprite;
	
	[SpineSlot]
	public string slot;

	[SpineSkin]
	public string skin;

	void Start () 
	{
		skelRenderer = GetComponent<SkeletonRenderer>();

		skelRenderer.skeleton.Data.AddUnitySprite(slot, mySprite, skin);

		skelRenderer.skeleton.SetAttachment(slot, mySprite.name);


	}
	
	
	void Update ()
	{
	
	}
}
