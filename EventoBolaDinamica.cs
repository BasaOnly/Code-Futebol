using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventoBolaDinamica : MonoBehaviour {

	void BolaDinamica()
	{
		this.gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
	}
}
