using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour 

{
	#region Singleton
	private static InputManager _instance;

	public static InputManager Instance
	{
		get
		{
			if (_instance == null) 
			{
				// ako netko zeli pristupiti prije nego se ovo postavi
				_instance = (InputManager)FindObjectOfType (typeof(InputManager));
				if (_instance == null)
					Debug.Log ("An input of InputManager doesn't exist!");
			}
			return _instance;
		}
	}
	#endregion

	[Header("Input Key Codes")]
	public List<KeyCode> UpKeyCodes;
	public List<KeyCode> DownKeyCodes;
	public List<KeyCode> LeftKeyCodes;
	public List<KeyCode> RightKeyCodes;

	public CustomUnityEvent<Direction> OnInputRecived = new CustomUnityEvent<Direction>();

	private void Awake()
	{
		if (Instance != this)
			Destroy (gameObject);
		
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

