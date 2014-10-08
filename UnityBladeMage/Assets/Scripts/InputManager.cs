using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour 
{
	public Vector2 _joystick;
	public Vector2 _joystickPrev;

	public bool _rightPressed;
	public bool _leftPressed;
	public bool _downPressed;
	public bool _upPressed;

	public bool _rightPressedPrev;
	public bool _leftPressedPrev;
	public bool _downPressedPrev;
	public bool _upPressedPrev;

	public bool _jumpPressed;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		_joystick.x = Input.GetAxis("Horizontal");
		_joystick.y = Input.GetAxis("Vertical");

		if(Input.GetAxis("Horizontal") > 0.2f)
		{
			_rightPressed = true;
		}
		else
		{
			_rightPressed = false;
		}

		if(Input.GetAxis("Horizontal") < -0.2f)
		{
			_leftPressed = true;
		}
		else
		{
			_leftPressed = false;
		}

		if(Input.GetAxis("Vertical") < -0.2f)
		{
			_downPressed = true;
		}
		else
		{
			_downPressed = false;
		}

		if(Input.GetAxis("Vertical") > 0.2f)
		{
			_upPressed = true;
		}
		else
		{
			_upPressed = false;
		}

		if(Input.GetButtonDown("Jump/Select"))
		{
			_jumpPressed = true;
		}
		else
		{
			_jumpPressed = false;
		}

		_upPressedPrev = _upPressed;
		_downPressedPrev = _downPressed;
		_leftPressedPrev = _leftPressed;
		_rightPressedPrev = _rightPressed;
		_joystickPrev = _joystick;
	}
}
