using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {
	
	Text money;

	void Start () {
		money = GetComponent<Text>();

	}

	void Update () {

		if (GameManager.instance.GetPlayerTransform() != null)
		{
			money.text = GameManager.instance.GetMoney().ToString() + "$ "
			+ GameManager.instance.GetPlayerTransform().GetComponent<EquipmentComponent>().GetWeaponName();
		}
		
	}
}
