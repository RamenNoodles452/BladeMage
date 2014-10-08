using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct CombatPath
{
	public string _pathName;
	public BinaryTree<ComboNode> _combatTree;
}

public enum CharacterState
{
	recovery = 0,
	idle = 1,
	running = 2,

}

public class CharacterInfo 
{
	public string _title;
	public string _name;

	public int _moveSpeed;
	public float _moveSpeedModifier; //(1.00x multiplier)

	public float _jumpSpeed;

	int _currentHP;
	int _maxHP;

	public List<CombatPath> _pathsList = new List<CombatPath>();

	public CharacterState _state = CharacterState.idle;

	//when initializing the combat trees, the XML nodes go Right-> Left In order fill
	public CharacterInfo()
	{

	}
}
