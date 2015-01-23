using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {

	//constants
	public int MAZE_WIDTH;
	public int MAZE_HEIGHT;
	private const float TILE_SIZE = 1;
	public GameObject wall;
	//private const START_COLOR		: uint = 0xFF0000;
	//private const FINISH_COLOR		: uint = 0x00FF00;
	//private const WALL_COLOR		: uint = 0x000000;
	//private const WALKABLE_COLOR	: uint = 0xDDDDDD;
	
	//directions
	private const char NORTH = 'N';
	private const char SOUTH = 'S';
	private const char EAST = 'E';
	private const char WEST = 'W';
	
	//variables
	private int _width;
	private int _height;

	private bool[][] _maze;

	private List<int> _moves;
	private Vector2 _start;
	private Vector2 _finish;
	//private _container	: Sprite;
	
	public void TiledMazeGen ()
	{		
		_width	= MAZE_WIDTH * 2 + 1;
		_height	= MAZE_HEIGHT * 2 + 1;

		_start = new Vector2(1, 1);
		//_finish = new Point(_height - 2, _width - 2);
		
		//_container = new Sprite();
		
		_generate();
	}
	
	private void _generate ()
	{
		_initMaze();
		_createMaze();
		_drawMaze();
	}
	
	private void _initMaze ()
	{
		_maze = new bool[_width][];
		
		for ( int x = 0; x < _height; x++ )
		{
			_maze[x] = new bool[_height];
			
			for ( int y = 0; y < _width; y++ )
			{
				_maze[x][y] = true;
			}
		}
		
		_maze[(int)_start.x][(int)_start.y] = false;
	}
	
	private void _createMaze (){
		int back;
		int move;
		string possibleDirections;
		Vector2 pos = _start;

		_moves = new List<int>();
		//_moves.Ad
		_moves.Add((int)(pos.y + (pos.x * _width)));
		
		while ( _moves.Count > 0 )
		{
			possibleDirections = "";
			
			if (((int)pos.x + 2 < _height ) && (_maze[(int)pos.x + 2][(int)pos.y] == true) && ((int)pos.x + 2 != null) && ((int)pos.x + 2 != _height - 1) )
			{
				possibleDirections += SOUTH;
			}
			
			if (((int)pos.x - 2 >= 0 ) && (_maze[(int)pos.x - 2][(int)pos.y] == true) && ((int)pos.x - 2 != null) && ((int)pos.x - 2 != _height - 1) )
			{
				possibleDirections += NORTH;
			}
			
			if (((int)pos.y - 2 >= 0 ) && (_maze[(int)pos.x][(int)pos.y - 2] == true) && ((int)pos.y - 2 != null) && ((int)pos.y - 2 != _width - 1) )
			{
				possibleDirections += WEST;
			}
			
			if (((int)pos.y + 2 < _width ) && (_maze[(int)pos.x][(int)pos.y + 2] == true) && ((int)pos.y + 2 != null) && ((int)pos.y + 2 != _width - 1) )
			{
				possibleDirections += EAST;
			}
			
			if ( possibleDirections.Length > 0 )
			{
				move = Random.Range(0, possibleDirections.Length);

				//TODO
				/*if(possibleDirections[move] == NORTH){
					_maze[pos.x - 2][pos.y] = false;
					_maze[pos.x - 1][pos.y] = false;
					pos.x -=2;
				}*/
				switch (possibleDirections[move])
				{
				case NORTH: 
					_maze[(int)pos.x - 2][(int)pos.y] = false;
					_maze[(int)pos.x - 1][(int)pos.y] = false;
					pos.x -= 2;
					break;
					
				case SOUTH: 
					_maze[(int)pos.x + 2][(int)pos.y] = false;
					_maze[(int)pos.x + 1][(int)pos.y] = false;
					pos.x +=2;
					break;
					
				case WEST: 
					_maze[(int)pos.x][(int)pos.y - 2] = false;
					_maze[(int)pos.x][(int)pos.y - 1] = false;
					pos.y -=2;
					break;
					
				case EAST: 
					_maze[(int)pos.x][(int)pos.y + 2] = false;
					_maze[(int)pos.x][(int)pos.y + 1] = false;
					pos.y +=2;
					break;        
				}
				_moves.Add((int)pos.y + ((int)pos.x * _width));
			}
			else
			{
				back = _moves[_moves.Count-1];
				_moves.Remove(_moves.Count-1);
				pos.x = back / _width;
				pos.y = back % _width;
			}
		}
	}
	
	private void _drawMaze()
	{
		GameObject tile;
		//var tile : Sprite;
		
		/*if ( contains(_container) )
		{
			removeChild(_container)
		}*/
		
		//_container = new Sprite();
		//addChild(_container);
		
		for ( int x = 0; x < _height; x++ )
		{
			for ( int y = 0; y < _width; y++ )
			{
				tile = Instantiate(wall) as GameObject;
				tile.transform.position = new Vector3(x * TILE_SIZE, y * TILE_SIZE, 0);
				tile.transform.parent = transform;
				//tile = (_maze[x][y] == true) ? _drawTile(WALL_COLOR) : _drawTile(WALKABLE_COLOR);

			}
		}
		
		//start tile
		//tile.x = _start.x * TILE_SIZE;
		//tile.y = _start.y * TILE_SIZE;
		//_container.addChild(tile);
		
		//finish tile
		/*tile = _drawTile(FINISH_COLOR);
			tile.x = _finish.x * TILE_SIZE;
			tile.y = _finish.y * TILE_SIZE;
			_container.addChild(tile);*/
	}
	
	
	
	/*private int _randInt ( int min, int max)
	{
		return int((Math.random() * (max - min + 1)) + min);
	}*/

	// Use this for initialization
	void Start () {
		TiledMazeGen();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
