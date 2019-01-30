using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour 

{
	[Header("Input Key Codes")]
	public List<KeyCode> UpKeyCodes;
	public List<KeyCode> DownKeyCodes;
	public List<KeyCode> LeftKeyCodes;
	public List<KeyCode> RightKeyCodes;

	public CustomUnityEvent<Direction> OnInputRecived = new CustomUnityEvent<Direction>();

	private void Awake()
	{
		OnInputRecived.AddListener (OnInputRecivedListener);
	}

	private void Update()
	{
		CheckKeyboardInput ();
	}

	private bool IsOneKeyDown(List<KeyCode> keyCodes)
	{
		foreach (KeyCode keyCode in keyCodes)
			if (Input.GetKeyDown (keyCode))
				return true;

		return false;
			
		
	}

	private void CheckKeyboardInput()
	{
		if (IsOneKeyDown (UpKeyCodes))
			OnInputRecived.Invoke (Direction.Up);
		else if(IsOneKeyDown (DownKeyCodes))
			OnInputRecived.Invoke (Direction.Down);
		else if(IsOneKeyDown (LeftKeyCodes))
			OnInputRecived.Invoke (Direction.Left);
		else if(IsOneKeyDown (RightKeyCodes))
			OnInputRecived.Invoke (Direction.Right);

	}
	private void OnInputRecivedListener (Direction direction)
	{
		Debug.Log (direction);
	}
}

