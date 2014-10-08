using UnityEngine;
using System.Collections;

public class ShadowScript : MonoBehaviour 
{
	public Transform _upperBound;
	public Transform _lowerBound;
	public Transform _leftBound;
	public Transform _rightBound;

	public float _width;
	public float _height;


	// Use this for initialization
	void Start () 
	{
		_upperBound = transform.Find("UpperBound");
		_lowerBound = transform.Find("LowerBound");
		_leftBound = transform.Find("LeftBound");
		_rightBound = transform.Find("RightBound");

		_width = _rightBound.position.x - _leftBound.position.x;
		_height = _upperBound.position.y - _lowerBound.position.y;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
