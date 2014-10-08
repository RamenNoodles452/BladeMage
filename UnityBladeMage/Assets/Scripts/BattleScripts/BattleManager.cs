using UnityEngine;
using System.Collections;

/// <summary>
/// This class keeps track of important variables during the fight that characters might need to reference
/// </summary>
public class BattleManager : MonoBehaviour 
{
	public Transform _upperBound;
	public Transform _lowerBound;
	public Transform _leftBound;
	public Transform _rightBound;

	public float _timeScale;

	public static BattleManager Instance { get; private set; }

	void Awake()
	{
		// Save a reference to the AudioHandler component as our singleton instance
		Instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		_timeScale = 1.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
