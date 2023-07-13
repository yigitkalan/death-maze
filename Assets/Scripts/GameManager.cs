using UniRx;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public bool isPlayerDead = false;
	public int remainingEnemiesCount { get; private set; }
	public static GameManager Instance { get; private set; }

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
		}
		remainingEnemiesCount = FindObjectsOfType<BotMovement>().Length;
	}

	public void SetRemainingEnemies(int count)
	{
		remainingEnemiesCount = count;
	}
}
