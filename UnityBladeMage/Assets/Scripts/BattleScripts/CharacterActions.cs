using UnityEngine;
using System.Collections;

public enum CharacterClass{ BLADEMAGE, VITAMANCER, PARAGON };

public class CharacterActions : MonoBehaviour 
{
	public CharacterClass _characterClass;

	//private BinaryTree<ComboNode> _combatTree1 = new BinaryTree<ComboNode>(); //grand duelist
	
	// Use this for initialization
	void Start () 
	{
		if(_characterClass == CharacterClass.BLADEMAGE)
		{

		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
