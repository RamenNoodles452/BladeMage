using UnityEngine;
using System.Collections;

/// <summary>
/// Game state.
/// This function holds information when the game swaps between battle and adventure modes
/// </summary>
public static class GameState
{
	public static string _currentBattleEnviornment;

	public static CharacterInfo[] _party;

	public static float _velocityLimiter = 0.01f;
}
