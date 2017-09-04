using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeMaster : MonoBehaviour {

	public static RecipeMaster instance;

	public List<Recipe> recipes = new List<Recipe>();

	[HideInInspector]
	public int targetRecipe = 0;

	public delegate void UpdateUI();
	public static UpdateUI UpdateUICounts;

	void Awake(){
		if(instance){
			Destroy(this);
		}else{
			instance = this;
		}

		StartCoroutine(StartNewRecipe());
	}

	public Recipe GetTargetRecipe(){
		return recipes[targetRecipe];
	}

	private IEnumerator StartNewRecipe(){
		yield return new WaitForSeconds(3f);

		targetRecipe = Random.Range(0, recipes.Count);

		if(UpdateUICounts != null){
			UpdateUICounts();
		}

		yield return new WaitForSeconds(5f);
	}

	public static void CompleteRecipe(){
		
	}

	public static void FailedRecipe(){
		
	}

	public static SuccessStates ValidateProduct(Recipe other){
		return instance.recipes[instance.targetRecipe].Compare(other);
	}
}