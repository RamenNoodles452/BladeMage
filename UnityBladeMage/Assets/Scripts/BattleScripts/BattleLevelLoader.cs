using UnityEngine;
using System.Collections;

public class BattleLevelLoader : MonoBehaviour
{
	public GameObject _battleGround;

	private BattleEnviornment _battleEnv;

	// Use this for initialization
	void Start () 
	{
		_battleEnv = EnviornmentLibrary._battleEnviornments[GameState._currentBattleEnviornment];
		_battleGround.GetComponent<SpriteRenderer>().sprite = _battleEnv._groundTexture;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
