using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Maze1 : MonoBehaviour {
	public int mazeWidth = 5;
	public int mazeHeight = 5;
	public GameObject wall;
	public GameObject floor;
	public GameObject startPlace;
	public GameObject endPlace;
	public GameObject caveWormGameObject;
	public GameObject spiderGameObject;
	public GameObject alienGameObject;

	private int startX;
	private int startY;
	private int endX;
	private int endY;

	private List<int> moves = new List<int>();

	private List<Vector2> endPoints = new List<Vector2>();

	private int[][] maze;
	//private moves;
	private int _width;
	private int _height;


	void Start(){
		_width = (2*mazeWidth)+1;
		_height = (2*mazeHeight)+1;
		Generate();
		DrawMaze();
		FindEndPoints();
		PlaceCamera();
		PositionEnemies ();
	}

	void Generate(){
		maze = new int[_width][];
		for(int i=0; i<_width; i++){
			maze[i] = new int[_height];
			for(int j=0; j<_height; j++){
				maze[i][j] = 1;
			}
		}

		int x_pos = 1;
		int y_pos = 1;
		maze[x_pos][y_pos] = 0;
		moves.Add(y_pos+(x_pos*_width));
		while(moves.Count>0){
			string possibleDirections = "";
			if((x_pos+2>=0) && (x_pos+2<=_height-1)){
				if(maze[x_pos+2][y_pos] == 1){
					possibleDirections += 'S';
				}
			}
			if((x_pos-2<=_height-1) && (x_pos-2>=0)){
				if((maze[x_pos-2][y_pos] == 1)){
					possibleDirections += 'N';
				}
			}

			if((y_pos-2>=0) && (y_pos-2<=_width-1)){
				if(maze[x_pos][y_pos-2]==1){
					possibleDirections += 'W';
				}
			}

			if((y_pos+2>=0) && (y_pos+2<=_width-1)){
				if(maze[x_pos][y_pos+2]==1){
					possibleDirections += 'E';
				}

			}
			
			if(possibleDirections.Length > 0){
				int move = Random.Range(0, possibleDirections.Length);
				switch(possibleDirections[move]){
					case 'N':
						maze[x_pos-2][y_pos]=0;
						maze[x_pos-1][y_pos]=0;
						x_pos -= 2;
						break;
					case 'S':
						maze[x_pos+2][y_pos]=0;
						maze[x_pos+1][y_pos]=0;
						x_pos += 2;
						break;
					case 'W':
						maze[x_pos][y_pos-2]=0;
						maze[x_pos][y_pos-1]=0;
						y_pos -= 2;
						break;
					case 'E':
						maze[x_pos][y_pos+2]=0;
						maze[x_pos][y_pos+1]=0;
						y_pos += 2;
						break;
				}
				moves.Add(y_pos+(x_pos*_width));
			}else{
				int back = moves[moves.Count-1];
				moves.RemoveAt(moves.Count-1);
				x_pos = Mathf.FloorToInt(back/_width);
				y_pos = back%_width;
			}
		}


	}

	void DrawMaze(){
		for(int i=0; i< _width; i++){
			for(int j=0; j<_height;j++){
				if(maze[i][j]==1){
					GameObject go = Instantiate(wall) as GameObject;
					go.transform.parent = transform;
					go.transform.localPosition = new Vector3(i, j, 0);

				}else if(maze[i][j]==0){
					GameObject go = Instantiate(floor) as GameObject;
					go.transform.parent = transform;
					go.transform.localPosition = new Vector3(i, j, 0);

				}

			}
		}
	}

	public bool IsEmpty(int x, int y){
		if(maze[x][y]==0){
			return true;
		}else{
			return false;
		}
	}

	public void PlaceCamera(){
		Camera.main.transform.position = startPlace.transform.position;
		if(maze[startX-1][startY]==0){
			Camera.main.transform.rotation = Quaternion.Euler(0,270,90);
		}else if(maze[startX+1][startY]==0){
			Camera.main.transform.rotation = Quaternion.Euler(0,90,270);
		}else if(maze[startX][startY-1]==0){
			Camera.main.transform.rotation = Quaternion.Euler(90, 180, 0);
		}else if(maze[startX][startY+1]==0){
			Camera.main.transform.rotation = Quaternion.Euler(270,0,0);
		}
	}

	private void FindEndPoints(){
		for(int x=1;x<_width-1;x++){
			for(int y=1;y<_height-1;y++){
				if(maze[x][y]==0){
					int walls = 0;
					if(maze[x-1][y]==1){
						walls++;
					}
					if(maze[x+1][y]==1){
						walls++;
					}
					if(maze[x][y+1]==1){
						walls++;
					}
					if(maze[x][y-1]==1){
						walls++;
					}
					
					if(walls>=3){
						endPoints.Add(new Vector2(x,y));
					}
				}
			}
		}

		Vector2 start = endPoints[Random.Range(0, endPoints.Count)];
		startX = (int)start.x;
		startY = (int)start.y;
		startPlace.transform.localPosition = new Vector3(start.x, start.y, 0);
		Vector2 end;
		do{
			end = endPoints[Random.Range(0, endPoints.Count)];
			endPlace.transform.localPosition = new Vector3(end.x, end.y, 0);
		}while(end==start);
	}

	public Vector2 getRandomUnoccupiedPosition() 
	{
		bool positionOccuppied = false;
		var randomXPosition = 0;
		var randomYPosition = 0;
		var unoccupiedPosition = new Vector2();

		while (positionOccuppied) 
		{
			randomXPosition = Random.Range (0, _width - 2);	
			randomYPosition = Random.Range (0, _height - 2);
			bool positionOccupied = IsEmpty (randomXPosition, randomYPosition);
		}

		unoccupiedPosition.x = randomXPosition;
		unoccupiedPosition.y = randomYPosition;

		return unoccupiedPosition;
	}

	public void PositionEnemies()
	{
		var unoccupiedPosition = getRandomUnoccupiedPosition ();

		var cw = caveWormGameObject.GetComponent<CaveWorm> ();
		cw.x = (int)unoccupiedPosition.x;
		cw.y = (int)unoccupiedPosition.y;

		unoccupiedPosition = getRandomUnoccupiedPosition ();

		var alien = alienGameObject.GetComponent<Alien> ();
		alien.x = unoccupiedPosition.x;
		alien.y = unoccupiedPosition.y;

		unoccupiedPosition = getRandomUnoccupiedPosition ();

		var spider = spiderGameObject.GetComponent<Spider> ();
		spider.x = unoccupiedPosition.x;
		spider.y = unoccupiedPosition.y;


	}

}

