using UnityEngine;
using System.Collections;

public class pileController : MonoBehaviour {
	public GameController gameController;
	// Use this for initialization
	void Start () {
		
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if(gameController == null)return;
		if (other.tag == "Player")
		{
			gameController.onFallCall(this.gameObject);
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
