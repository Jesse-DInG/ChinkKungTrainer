using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Boundary
{
	public float xspeed;
	public float pressSpeed;
	public float sceneSpeed;

	public float startX;
	public float endX;
	public float minW;
	public float maxW;

	public GameObject pile;
}
public class GameController : MonoBehaviour {

	public GameObject player;
	public GameObject container;
	public GameObject MCamera;
	public GameObject speedTxt;
	public GameObject scoreTxt;
	public Boundary bound;
	private int _state;//0:ready 1:jump 2:fall

	private Vector3 mMoveMent;
//	private Vector3 pMovement;

	private GameObject[] pileList;
	private Text text;
	private Transform ptransform;
	private float curPosX;
	private float yspeed;
	// Use this for initialization
	void Start () {
		mMoveMent = new Vector3 (bound.xspeed, 0f, 0f);
//		pMovement = new Vector3 (bound.xspeed, bound.yspeed, 0f);

		pileList = new GameObject[3];
		reset ();
	}

	private void reset()
	{
		_state = 0;
		curPosX = 0f;
		player.collider.enabled = true;
		player.transform.localPosition = new Vector3 ();
		ptransform = bound.pile.transform;

		Destroy (pileList [0]);
		Destroy (pileList [1]);
		Destroy (pileList [2]);
		pileList [1] = createRandomPile ();
		pileList [1].transform.localPosition = ptransform.position;
		pileList [1].transform.localScale = ptransform.localScale;
		pileList [2] = createRandomPile ();
	}

	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate()
	{
		//print ("GameController>>state:" + _state.ToString());
		switch (_state) {
		case 0:
				if (Input.GetButton ("Fire1")) {
						player.rigidbody.WakeUp ();
						yspeed = 0f;
						_state = 1;
				}
				break;
		case 1:
				if (Input.GetButton ("Fire1")) {
						yspeed += bound.pressSpeed * Time.deltaTime;
						print ("yspeed:" + yspeed + ",time:" + Time.deltaTime);
				text = speedTxt.gameObject.GetComponent<Text>();
				text.text = "speed:" + yspeed.ToString("#0.00");
//				player.rigidbody.velocity = mMoveMent;
//				player.rigidbody.AddForce(new Vector3(0,Time.deltaTime * bound.yspeed,0f));
				} else {
						print ("yspeed:" + yspeed);
						Vector3 movement = new Vector3 (bound.xspeed, yspeed, 0f);
						player.rigidbody.velocity = movement;
						_state = 2;
				}
				break;
		case 2:

				break;
		case 3:
				container.rigidbody.velocity = mMoveMent;
				MCamera.rigidbody.velocity = mMoveMent;
				if (player.transform.localPosition.x <= 0) {

						container.rigidbody.velocity = new Vector3 ();
						MCamera.rigidbody.velocity = new Vector3 ();
						_state = 0;
			}
				break;
		}
}

	private GameObject createRandomPile()
	{
		float width = Random.Range (bound.minW, bound.maxW);
		Vector3 pos = new Vector3 (curPosX + Random.Range (bound.startX + width / 2, bound.endX - width / 2), ptransform.position.y, ptransform.position.z);

		GameObject gO = Instantiate (bound.pile, pos, ptransform.rotation) as GameObject;
		gO.transform.localScale = new Vector3 (width, ptransform.localScale.y, ptransform.localScale.z);
		return gO;
	}
	
	public void onFallCall(GameObject pile)
	{
		if(_state != 2)return;
		if (player.transform.localPosition.y >= 0) {
						curPosX = player.transform.localPosition.x;
						_state = 3;
						if (pileList [0]) {
								Destroy (pileList [0]);
						}
						pileList [0] = pileList [1];
						pileList [1] = pileList [2];
						curPosX = pileList [1].transform.localPosition.x + pileList [1].transform.localScale.x / 2;
						pileList [2] = createRandomPile ();
						player.rigidbody.velocity = new Vector3 ();
				} else {
						//player.collider.enabled = false;
				}
		//Destroy (pile);
	}

	public void onHitBoundary(GameObject gO)
	{
		print ("onHitBoundary>>state:" + _state);
		reset();
//		switch(_state)
//		{
//		case 0:
//			break;
//		case 1:
//			break;
//		case 2:
//			reset();
//			break;
//		case 3:
//			reset();
////			curPosX = player.transform.position.x;
////			_state = 0;
//			break;
//		}
	}
}