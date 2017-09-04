using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe {
	public List<RecipeIngredient> ingredients = new List<RecipeIngredient>();

	public int bakeTime;
	public int bakeDegrees;

	public SuccessStates Compare(Recipe other){
		ingredients.Sort();
		other.ingredients.Sort();

		foreach(RecipeIngredient ri in ingredients){
			Debug.Log(ri.ingredient + ": " + ri.count);
		}

		foreach(RecipeIngredient ri in other.ingredients){
			Debug.Log(ri.ingredient + ": " + ri.count);
		}

		Debug.Log(bakeTime + " : " + other.bakeTime);
		Debug.Log(bakeDegrees + " : " + other.bakeDegrees);

		if(ingredients.Count == other.ingredients.Count){
			bool isSame = true;

			for(int i = 0; i < ingredients.Count; i++){
				if(ingredients[i].ingredient != other.ingredients[i].ingredient || ingredients[i].count != other.ingredients[i].count){
					isSame = false;
				}
			}

			if(isSame){
				if(bakeTime == other.bakeTime){
					if(bakeDegrees == other.bakeDegrees){
						return SuccessStates.PERFECT;
					}
				}

				if(Mathf.Abs(bakeTime - other.bakeTime) < (float) bakeTime * 0.1f){
					if(Mathf.Abs(bakeDegrees - other.bakeDegrees) < (float) bakeDegrees * 0.05f){
						return SuccessStates.COMPLETED;
					}
				}
			}
		}

		return SuccessStates.FAILED;
	}

	public void AddIngredient(Ingredients ingredient){
		foreach(RecipeIngredient ri in ingredients){
			if(ri.ingredient == ingredient){
				ri.count++;
				return;
			}
		}

		ingredients.Add(new RecipeIngredient(1,ingredient));
	}
}

[System.Serializable]
public class RecipeIngredient : IComparable<RecipeIngredient> {
	public int count = 0;
	public Ingredients ingredient = Ingredients.EGG;

	public RecipeIngredient(int count, Ingredients ingredient){
		this.count = count;
		this.ingredient = ingredient;
	}

	public int CompareTo (RecipeIngredient other){
		if(other == null || ingredient > other.ingredient){
			return 1;
		}else if(ingredient == other.ingredient){
			return 0;
		}else{
			return -1;
		}
	}
}