using UnityEngine;
using System.Collections;

public class BoundaryController : MonoBehaviour {
	public GameController controller;
	void OnTriggerExit(Collider other) {
		print ("out:" + other.tag);
		if (other.tag == "Player") {
			controller.onHitBoundary(other.gameObject);
		}
	}
}
