using UnityEngine;
using System.Collections;

public class CharController : MonoBehaviour {

	public enum DIRECTIONS{N,S,W,E};
	public enum ACTIONS{Forward, TurnLeft, TurnRight, Attack, Flee};
	public GameObject forwardUI;
	public GameObject turnLeftUI;
	public GameObject turnRightUI;
	public GameObject AttackUI;
	public GameObject FleeUI;

	private int _x;

	public int x{
		get{
			return _x;
		}
	}

	private int _y;
	public int y{
		get{
			return _y;
		}
	}


	private DIRECTIONS lookingDir;

	public DIRECTIONS dir{
		get{
			return lookingDir;
		}
	}

	private GameObject action1;
	private GameObject action2;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			MoveForward();
		}else if(Input.GetKeyDown(KeyCode.LeftArrow)){
			TurnLeft();
		}else if(Input.GetKeyDown(KeyCode.RightArrow)){
			TurnRight();
		}
	}

	private void MoveForward(){
		if(lookingDir == DIRECTIONS.N){
			transform.position+=Vector3.up;
			y--;
		}else if(lookingDir == DIRECTIONS.S){
			transform.position+=Vector3.down;
			y++;
		}else if(lookingDir == DIRECTIONS.W){
			transform.position+=Vector3.left;
			x--;
		}else if(lookingDir == DIRECTIONS.E){
			transform.position+=Vector3.right;
			x++;
		}
	}

	private void TurnLeft(){
		if(lookingDir == DIRECTIONS.N){
			Camera.main.transform.rotation = Quaternion.Euler(0,270,90);
			SetDir(CharController.DIRECTIONS.W);
		}else if(lookingDir == DIRECTIONS.S){
			Camera.main.transform.rotation = Quaternion.Euler(0,90,270);
			SetDir(CharController.DIRECTIONS.E);
		}else if(lookingDir == DIRECTIONS.W){
			Camera.main.transform.rotation = Quaternion.Euler(90, 180, 0);
			SetDir(CharController.DIRECTIONS.S);
		}else if(lookingDir == DIRECTIONS.E){
			Camera.main.transform.rotation = Quaternion.Euler(270,0,0);
			SetDir(CharController.DIRECTIONS.N);
		}
	}

	private void TurnRight(){
		if(lookingDir == DIRECTIONS.N){
			Camera.main.transform.rotation = Quaternion.Euler(0,90,270);
			SetDir(CharController.DIRECTIONS.E);
		}else if(lookingDir == DIRECTIONS.S){
			Camera.main.transform.rotation = Quaternion.Euler(0,270,90);
			SetDir(CharController.DIRECTIONS.W);
		}else if(lookingDir == DIRECTIONS.W){
			Camera.main.transform.rotation = Quaternion.Euler(270,0,0);
			SetDir(CharController.DIRECTIONS.N);
		}else if(lookingDir == DIRECTIONS.E){
			Camera.main.transform.rotation = Quaternion.Euler(90, 180, 0);
			SetDir(CharController.DIRECTIONS.S);
		}
	}

	private void Attack(){

	}

	private void Flee(){

	}

	public void SetDir(DIRECTIONS dir){
		lookingDir = dir;
	}

	public void SetActions(ACTIONS action1, ACTIONS action2){
		// TODO should set and display the timer
	}

	/*public void SetAction(ACTIONS action, Vector3 pos){
		if(action1==null){

		}
	}*/
}
