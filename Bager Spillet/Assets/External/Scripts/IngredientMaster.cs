using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientMaster : MonoBehaviour {

	public static List<int> totalIngredients = new List<int>();
	public static List<Ingredients> allIngredients = new List<Ingredients>();

	public delegate void UpdateUI();
	public static UpdateUI UpdateUICounts;

	void Awake(){
		System.Array A = System.Enum.GetValues(typeof(Ingredients));

		for(int i = 0; i < A.Length; i++){
			totalIngredients.Add(0);
			allIngredients.Add((Ingredients) A.GetValue(i));
		}
	}

	public static void AddIngredient(Ingredients ingredient){
		for(int i = 0; i < allIngredients.Count; i++){
			if(ingredient == allIngredients[i]){
				totalIngredients[i]++;
			}
		}

		if(UpdateUICounts != null){
			UpdateUICounts();
		}
	}

	public static void ClearIngredients(){
		for(int i = 0; i < allIngredients.Count; i++){
			totalIngredients[i] = 0;
		}

		if(UpdateUICounts != null){
			UpdateUICounts();
		}
	}
}