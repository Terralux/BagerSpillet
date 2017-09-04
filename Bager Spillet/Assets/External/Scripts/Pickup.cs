using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	public Ingredients ingredient;
	private GameObject prefab;
	private bool playerIsNear = false;

	void Awake(){
		prefab = Resources.Load(ingredient.ToString()) as GameObject;
	}

	void OnTriggerEnter(Collider col){
		if(col.GetComponent<PlayerControl>() != null){
			playerIsNear = true;
		}
	}

	void Update(){
		if(playerIsNear){
			if(Input.GetKeyDown(KeyCode.Space)){
				PlayerControl.instance.InstantiateIngredient(prefab, ingredient);
			}
		}
	}

	void OnTriggerExit(Collider col){
		if(col.GetComponent<PlayerControl>() != null){
			playerIsNear = false;
		}
	}
}