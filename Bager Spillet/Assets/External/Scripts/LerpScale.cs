using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpScale : MonoBehaviour {

	private Vector3 originalSize;
	private float lerpSpeed = 0.3f;
	private float fraction;

	void Awake () {
		originalSize = transform.localScale;
		StartCoroutine(WaitForStart());
	}

	IEnumerator WaitForStart() {
		yield return new WaitForSeconds(3f);
	}

	void Update () {
		fraction += Time.deltaTime * lerpSpeed;

		if(fraction > 1){
			Destroy(gameObject);
		}

		transform.localScale = Vector3.Lerp(originalSize, Vector3.zero, fraction);
	}
}
