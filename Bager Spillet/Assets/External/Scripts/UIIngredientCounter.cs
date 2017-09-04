using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIngredientCounter : MonoBehaviour {

	private Text myTextField;
	public bool isCountingGatheredIngredients;

	void Awake () {
		if(isCountingGatheredIngredients){
			IngredientMaster.UpdateUICounts += OnAddedNewIngredient;
		}else{
			RecipeMaster.UpdateUICounts += OnChangedRecipe;
		}
		myTextField = GetComponentInChildren<Text>();
	}

	void OnAddedNewIngredient(){
		myTextField.text = IngredientMaster.totalIngredients[transform.GetSiblingIndex()].ToString();
	}

	void OnChangedRecipe(){
		Recipe target = RecipeMaster.instance.GetTargetRecipe();

		for(int i = 0; i < target.ingredients.Count; i++){
			if(target.ingredients[i].ingredient == IngredientMaster.allIngredients[transform.GetSiblingIndex()]){
				myTextField.text = target.ingredients[i].count.ToString();
			}
		}
	}
}