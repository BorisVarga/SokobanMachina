using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElement : Element

{
	private Transform _transform;

	private void Awake()
	{
		_transform = transform;

		InputManager.Instance.OnInputRecived.AddListener (OnInputRecivedListener);
	}

	public override bool Move(Direction direction)
	{
		Vector2 directionVector = Vector2.zero;




		try 
		{
			Vector2 newPosition = (Vector2)_transform.position + (Vector2)directionVector;
			Element destinationElement = _Level [(int)newPosition.x, (int)newPosition.y];
		
			if(destinationElement)
			{
				if(destinationElement.Move(direction))
				{
					_Level[(int) _transform.position.x, (int)_transform.position.y] = null;

					//samo vizualni dio!
					_transform.position += (Vector3)directionVector;

					_Level[(int) _transform.position.x, (int)_transform.position.y] = this;

					return true;

				}
				Debug.Log("Unable to move,player is being blocked.");
				
				}
			else
			{
				_Level[(int) _transform.position.x, (int)_transform.position.y] = null;

				//samo vizualni dio!
				_transform.position += (Vector3)directionVector;

				_Level[(int) _transform.position.x, (int)_transform.position.y] = this;

				if(destinationElement is ExitElement)
					Debug.Log("vistory!");

				return true;
				
			}
		} 

		catch (System.Exception ex) 
		{
			Debug.LogWarning ("Unable to move player out of bounds.");
		}
		return true;
	}

	private void PrintLevelState()
	{
		string levelState = "";

		for (int y = _Level.GetLength(1) -1; y >= 0; y--) 
		{
			for (int x = 0; x < _Level.GetLength(0); x++) 
			{
				Element element = _Level [x, y];

				if (element is PlayerElement)
					levelState += "P";
				else if (element is ExitElement)
					levelState += "E";
				else if (element is BlockElement)
					levelState += "B";
				else if (element is CrateElement)
					levelState += "C";
				else
					levelState += "-";
			}

			levelState += "\n";
		}

		Debug.Log (levelState);
	}

	private void OnInputRecivedListener(Direction direction)
	{
		

		Move (direction);
		PrintLevelState ();
	}

}
