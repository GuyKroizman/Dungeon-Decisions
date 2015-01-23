using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze1 : MonoBehaviour {
	public int mazeWidth = 5;
	public int mazeHeight = 5;
	public GameObject wall;
	public GameObject floor;

	private List<int> moves = new List<int>();

	private int[][] maze;
	//private moves;
	private int _width;
	private int _height;

	void Start(){
		_width = (2*mazeWidth)+1;
		_height = (2*mazeHeight)+1;
		Generate();
		DrawMaze();
		Debug.Log(maze);
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
			//if((maze[x_pos+2][y_pos] == 1) && (x_pos+2!=0) && (x_pos+2!=_height-1)){
			if((x_pos+2>=0) && (x_pos+2<=_height-1)){
				if(maze[x_pos+2][y_pos] == 1){
					possibleDirections += 'S';
				}
			}
			//if((x_pos-2!=_height-1)){

			//	if((maze[x_pos-2][y_pos] == 1) && (x_pos-2!=0)){

			//if((x_pos-2!=_height-1) && (maze[x_pos-2][y_pos] == 1) && (x_pos-2!=0)){
			if((x_pos-2<=_height-1) && (x_pos-2>=0)){
				if((maze[x_pos-2][y_pos] == 1)){
					possibleDirections += 'N';
				}
			}

			//if((maze[x_pos][y_pos-2]==1) && (y_pos-2!=0) && (y_pos-2!=_width-1)){
			if((y_pos-2>=0) && (y_pos-2<=_width-1)){
				if(maze[x_pos][y_pos-2]==1){
					possibleDirections += 'W';
				}
			}
			//if((maze[x_pos][y_pos+2]==1) && (y_pos+2!=0) && (y_pos+2!=_width-1)){
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
				Debug.Log(maze[i][j]);
				if(maze[i][j]==1){
					GameObject go = Instantiate(wall) as GameObject;
					go.transform.position = new Vector3(i, j, 0);
					go.transform.parent = transform;
				}else if(maze[i][j]==0){
					GameObject go = Instantiate(floor) as GameObject;
					go.transform.position = new Vector3(i, j, 0);
					go.transform.parent = transform;
				}

			}
		}
	}
}