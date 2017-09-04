using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oven : MonoBehaviour {

	private bool playerIsNear = false;

	public Slider temperatureSlider;
	public Text temperatureField;

	public Text tidsFelt;
	private float timer;
	private bool timerIsRunning;

	private int currentTemperature;

	private Recipe currentRecipe;

	void Awake(){
		currentRecipe = new Recipe();
	}

	void OnTriggerEnter(Collider col){
		if(col.GetComponent<PlayerControl>() != null){
			playerIsNear = true;
		}
	}

	void Update(){
		if(playerIsNear){
			if(Input.GetKeyDown(KeyCode.Space) && !timerIsRunning){
				if(PlayerControl.instance.isHoldingAnObject){
					currentRecipe.AddIngredient(PlayerControl.instance.DropIngredient());
				}
			}
		}

		if(timerIsRunning){
			timer += Time.deltaTime;
			tidsFelt.text = ((int)timer).ToString();

			if(Input.GetKeyDown(KeyCode.Space)){
				currentRecipe.bakeTime = (int) timer;
				SpawnBread();
				timerIsRunning = false;
			}
		}
	}

	void OnTriggerExit(Collider col){
		if(col.GetComponent<PlayerControl>() != null){
			playerIsNear = false;
		}
	}

	public void OnSliderValueChanged(){
		currentTemperature = (int) temperatureSlider.value;
		temperatureField.text = currentTemperature.ToString();
	}

	public void OnStartOven(){
		timerIsRunning = true;
		IngredientMaster.ClearIngredients();
		currentRecipe.bakeDegrees = (int) temperatureSlider.value;
	}

	public void SpawnBread(){
		Debug.Log(currentRecipe.bakeDegrees + " : " + currentRecipe.bakeTime + " : " + currentRecipe.ingredients.Count);
		switch(RecipeMaster.ValidateProduct(currentRecipe)){
		case SuccessStates.PERFECT:
			Debug.Log("Perfect!");
			break;
		case SuccessStates.COMPLETED:
			Debug.Log("Completed!");
			break;
		case SuccessStates.FAILED:
			Debug.Log("Better luck next time!");
			break;
		}
		currentRecipe = new Recipe();

		tidsFelt.text = "0";
		temperatureSlider.value = 0;
	}
}