using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public struct AttackMovement
{
	public float speed;
	public float duration;
}

public class ComboNode : IComparable
{
	public float _weightValue; //for tree storage
	public string _id;
	public int _damage;
	public float _attackDuration;
	public AttackMovement[] _attackMovements;

	public ComboNode()
	{

	}

	public ComboNode(string id, int damage, float duration, AttackMovement[] movementArray)
	{
		_id = id;
		_damage = damage;
		_attackDuration = duration;
		_attackMovements = movementArray;
	}

	public int CompareTo(object obj)
	{
		if(obj == null) return 1;

		ComboNode otherNode = obj as ComboNode;
		if(otherNode != null)
		{
			return this._weightValue.CompareTo(otherNode._weightValue);
		}
		else 
		{
			throw new ArgumentException("Object is not a combonode");
		}
	}
}
