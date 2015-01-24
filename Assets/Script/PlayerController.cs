using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public bool madeDecision;
	public KeyCode key1;
	public KeyCode key2;


	public GameObject voted_ui;


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
				SetDecision(-1);
			}
			if(Input.GetKeyDown(key2)){
				SetDecision(1);
			}
		}
	}

	public void ResetDecision(){
		madeDecision = false;
		voted_ui.SetActive(false);
		Debug.Log(name+" icon is active"+voted_ui.activeSelf);
	}

	private void SetDecision(int num){
		character.Vote(num);
		madeDecision=true;
		voted_ui.SetActive(true);

		if(character.votes>=3){
			character.DoAction();
		}
	}

}
