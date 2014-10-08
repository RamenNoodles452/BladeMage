using UnityEngine;
using System.Collections;

public class ControllerScript : MonoBehaviour {

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
	public bool _jumpPressedPrev;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	public virtual void Update () 
	{
	
	}
}
