using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(CapsuleCollider),typeof(IngredientMaster))]
public class PlayerControl : MonoBehaviour {

	public static PlayerControl instance;

	private Rigidbody rb;

	public float movementSpeed;
	public float rotationSpeed;

	private Transform cam;

	[HideInInspector]
	public bool isHoldingAnObject;
	private GameObject heldObject;
	private Ingredients heldIngredient;

	void Awake () {
		rb = GetComponent<Rigidbody>();
		cam = Camera.main.transform;

		if(!instance){
			instance = this;
		}
	}
	
	void Update () {
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		Vector3 direction = cam.TransformDirection(new Vector3(
			(Input.GetKey(KeyCode.Q)? -1:0) + (Input.GetKey(KeyCode.E)?1:0), 
			0, 
			v)
		);

		direction = new Vector3(direction.x, 0, direction.z);

		transform.Rotate(new Vector3 (0, h, 0) * rotationSpeed * Time.deltaTime);

		rb.velocity = new Vector3(0, rb.velocity.y, 0) + (direction.normalized * movementSpeed);
	}

	public void InstantiateIngredient(GameObject go, Ingredients ingredient){
		if(!isHoldingAnObject){
			heldObject = Instantiate(go, transform);
			heldObject.transform.position = transform.GetChild(0).position;
			isHoldingAnObject = true;
			heldIngredient = ingredient;
		}
	}

	public Ingredients DropIngredient(){
		if(isHoldingAnObject){
			heldObject.AddComponent<Rigidbody>();
			heldObject.AddComponent<LerpScale>();
			heldObject.transform.SetParent(null);
			isHoldingAnObject = false;
			IngredientMaster.AddIngredient(heldIngredient);
		}
		return heldIngredient;
	}
}