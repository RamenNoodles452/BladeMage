using UnityEngine;
using System.Collections;

/// <summary>
/// Battle enviornment.
/// 
/// This class holds all the information for each battle enviornment in the game
/// </summary>
public class BattleEnviornment
{
	public string _name;
	public Sprite _groundTexture;

	public BattleEnviornment(string areaName)
	{
		_name = areaName;
		_groundTexture = Resources.Load<Sprite>("Art/BattleEnviornment/GroundTextures/" + areaName);
	}

}