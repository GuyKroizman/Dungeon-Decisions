using UnityEngine;
using System.Collections;

public class Action : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetAction(Sprite bg){
		GetComponent<UnityEngine.UI.Image>().sprite = bg;
		gameObject.SetActive(true);
	}

	private void RemoveActionBg(){
		GetComponent<UnityEngine.UI.Image>().sprite = null;
	}

	public void EndSelectedAnimation(){
		//TODO should do the action now
		Destroy(gameObject);
	}

	public void EndNotSelectedAnimation(){
		Destroy(gameObject);
	}
}
