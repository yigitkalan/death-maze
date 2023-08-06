using UniRx;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public bool isPlayerDead = false;
	public int remainingEnemiesCount { get; private set; }
	public int playerPoints { get; private set; }
	public static GameManager Instance { get; private set; }

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(this);
		}
	}
    private void Start() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

	public void SetRemainingEnemies(int count)
	{
		remainingEnemiesCount = count;
	}

	public void AddPoints(int count)
	{
		playerPoints += count;
	}

	public void SetInitialEnemyCount()
	{
		remainingEnemiesCount = FindObjectsOfType<BotMovement>().Length;
	}
}
