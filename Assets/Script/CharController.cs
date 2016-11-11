using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharController : MonoBehaviour {

	public enum DIRECTIONS{N,S,W,E};
	public enum ACTIONS{Forward, TurnLeft, TurnRight, Attack, Flee};
	public GameObject forwardUI;
	public GameObject turnLeftUI;
	public GameObject turnRightUI;
	public GameObject attackUI;
	public GameObject fleeUI;
	public GameObject timer;

	public Color actionColor1;
	public Color actionColor2;

	public Maze1 maze;

	private ACTIONS action1;
	private ACTIONS action2;
	private ACTIONS selectedAction;

	private int vote;
	private int numOfVotes = 0;

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

	public int votes{
		get{
			return numOfVotes;
		}
	}


	private DIRECTIONS lookingDir;

	public DIRECTIONS dir{
		get{
			return lookingDir;
		}
	}

	//private GameObject action1;
	//private GameObject action2;

	private PlayerController player1;
	private PlayerController player2;
	private PlayerController player3;

	public void SetPos(int xPos, int yPos){
		_x = xPos;
		_y = yPos;
	}

	// Use this for initialization
	void Start () {
		maze = FindObjectOfType<Maze1>();

		player1 = transform.GetChild(0).GetComponent<PlayerController>();
		player2 = transform.GetChild(1).GetComponent<PlayerController>();
		player3 = transform.GetChild(2).GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void MoveForward(){
		if(lookingDir == DIRECTIONS.N){
			transform.position+=Vector3.up;
			_y--;
		}else if(lookingDir == DIRECTIONS.S){
			transform.position+=Vector3.down;
			_y++;
		}else if(lookingDir == DIRECTIONS.W){
			transform.position+=Vector3.left;
			_x--;
		}else if(lookingDir == DIRECTIONS.E){
			transform.position+=Vector3.right;
			_x++;
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

	public void SetDir(DIRECTIONS dir){
		lookingDir = dir;
	}

	public void SetActions(ACTIONS actionA, ACTIONS actionB){
		forwardUI.SetActive(false);
		turnRightUI.SetActive(false);
		turnLeftUI.SetActive(false);
		attackUI.SetActive(false);
		fleeUI.SetActive(false);

		SetAction(actionA,1);
		action1 = actionA;
		SetAction(actionB,2);
		action2 = actionB;
		timer.SetActive(true);
	}

	public void SetAction(ACTIONS action, int id){
		if(action==ACTIONS.Forward){
			if(id==1){
				forwardUI.transform.GetChild(0).GetComponent<Image>().color = actionColor1;
			}else{
				forwardUI.transform.GetChild(0).GetComponent<Image>().color = actionColor2;
			}
			forwardUI.SetActive(true);
		}else if(action==ACTIONS.TurnRight){
			if(id==1){
				turnRightUI.transform.GetChild(0).GetComponent<Image>().color = actionColor1;
			}else{
				turnRightUI.transform.GetChild(0).GetComponent<Image>().color = actionColor2;
			}
			turnRightUI.SetActive(true);
		}else if(action==ACTIONS.TurnLeft){
			if(id==1){
				turnLeftUI.transform.GetChild(0).GetComponent<Image>().color = actionColor1;
			}else{
				turnLeftUI.transform.GetChild(0).GetComponent<Image>().color = actionColor2;
			}
			turnLeftUI.SetActive(true);
		}
	}

	public void Vote(int num){
		vote+=num;
		numOfVotes++;
		Debug.Log("Voted. Num: "+numOfVotes);
		/*if(numOfVotes>=3){
			DoAction();
		}*/
	}
	
	public void DoAction(){
		if(vote<0){
			selectedAction = action1;
		}else if(vote>0){
			selectedAction = action2;
		}else{
			if(Random.Range(0,2)==0){
				selectedAction = action1;
			}else{
				selectedAction = action2;
			}
		}



		if(selectedAction == ACTIONS.Forward){
			forwardUI.GetComponent<Animation>().Play("SelectedAction");
			MoveForward();
		}else if(selectedAction == ACTIONS.TurnLeft){
			turnLeftUI.GetComponent<Animation>().Play("SelectedAction");
			TurnLeft();
		}else if(selectedAction == ACTIONS.TurnRight){
			turnRightUI.GetComponent<Animation>().Play("SelectedAction");
			TurnRight();
		}/*else if(selectedAction == ACTIONS.Attack){
			attackUI.animation.Play("SelectedAction");
			Attack();
		}else if(selectedAction == ACTIONS.Flee){
			fleeUI.animation.Play("SelectedAction");
			Flee();
		}*/
		AfterAction();
		maze.SetTileActions();
	}

	private void AfterAction(){
		vote=0;
		numOfVotes=0;
		
		timer.SetActive(false);
		Debug.Log("Reseting player 1");
		player1.ResetDecision();
		Debug.Log("Reseting player 2");
		player2.ResetDecision();
		Debug.Log("Reseting player 3");
		player3.ResetDecision();


	}
}
