using UnityEngine;
using System.Collections;

public class PlayerController : ControllerScript 
{
	// Update is called once per frame
	public override void Update () 
	{
		_upPressedPrev = _upPressed;
		_downPressedPrev = _downPressed;
		_leftPressedPrev = _leftPressed;
		_rightPressedPrev = _rightPressed;
		_joystickPrev = _joystick;
		_jumpPressedPrev = _jumpPressed;

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
		


	}
}
