using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColinFeelUI : MonoBehaviour {

	public void ChangePlayerSpeed( InputField speed )
	{
		int number;
		if ( int.TryParse( speed.text, out number ) )
			GameObject.Find( "Player" ).GetComponent<MovementComponent>().ChangeSpeed( number );
	}

}
