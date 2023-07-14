using UniRx;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public bool isPlayerDead = false;
	public int remainingEnemiesCount { get; private set; }
	public static GameManager Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
		remainingEnemiesCount = FindObjectsOfType<BotMovement>().Length;
	}

	public void SetRemainingEnemies(int count)
	{
		remainingEnemiesCount = count;
	}
}
