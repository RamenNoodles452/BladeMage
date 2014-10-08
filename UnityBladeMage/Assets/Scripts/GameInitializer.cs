using UnityEngine;
using System.Collections;

public class GameInitializer : MonoBehaviour 
{
	public LoadXMLData _xmlLoader;

	// Use this for initialization
	void Start () 
	{
		_xmlLoader = GetComponent<LoadXMLData>();

		LoadBattleEnviornments();
		LoadAdventureEnviornement();
		LoadCharacterInfo();

		GameState._currentBattleEnviornment = "testGrassyPlain";
	}
	
	// Update is called once per frame
	void Update () 
	{
		Application.LoadLevel("Battle 2");
	}

	void LoadBattleEnviornments()
	{
		BattleEnviornment temp = new BattleEnviornment("testDustyPlain");
		EnviornmentLibrary._battleEnviornments.Add(temp._name, temp);

		temp = new BattleEnviornment("testGrassyPlain");
		EnviornmentLibrary._battleEnviornments.Add(temp._name, temp);
	}

	/// <summary>
	/// This function only loads the current enviornment that the player is in. The rest of the enviornments are loaded on demand
	/// OR
	/// this function loads all the enviornments and throws them into a hashtable/BST with the name as it's key
	/// </summary>
	void LoadAdventureEnviornement()
	{

	}

	void LoadCharacterInfo()
	{
		GameState._party = new CharacterInfo[3];

		_xmlLoader.LoadPartyCombatData();
	}
}
