using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public bool madeDecision;
	public KeyCode key1;
	public KeyCode key2;

	private CharController character;

	public 

	// Use this for initialization
	void Start () {
		character = Camera.main.GetComponent<CharController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!madeDecision){
			if(Input.GetKeyDown(key1)){
				character.Vote(-1);
				madeDecision=true;
			}
			if(Input.GetKeyDown(key2)){
				character.Vote(1);
				madeDecision=true;
			}
		}
	}

	public void ResetDecision(){
		madeDecision = false;
	}
}
