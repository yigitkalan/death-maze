using System;
using UnityEngine;
using TMPro;
using UniRx;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LevelDoorManager : MonoBehaviour
{
	[SerializeField]
	TMP_Text remainingEnemiesText;

	[SerializeField]
	float doorOpenDelay = 5f;

	[SerializeField]
	float doorOpenYPosition = -8f;
	IDisposable doorDisposable;

	Tween doorOpenTween;

	[SerializeField]
	InGameUIController inGameUIController;

	private void Awake()
	{
		inGameUIController = FindAnyObjectByType<InGameUIController>();
	}

	private void Start()
	{
		UpdateDoorText();
		doorDisposable = GameManager.Instance
			.ObserveEveryValueChanged(x => x.remainingEnemiesCount)
			.Subscribe(_ =>
			{
				UpdateDoorText();
				if (GameManager.Instance.remainingEnemiesCount <= 0)
				{
					OpenDoor();
				}
			})
			.AddTo(this);
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
		doorDisposable.Dispose();
		doorOpenTween.Kill();
	}

	void UpdateDoorText()
	{
		if (GameManager.Instance.remainingEnemiesCount > 0)
			remainingEnemiesText.text = Convert.ToString(
				GameManager.Instance.remainingEnemiesCount
			);
	}

	void OpenDoor()
	{
		inGameUIController.SetLevelFinishUI();
		doorOpenTween = transform.DOMoveY(doorOpenYPosition, doorOpenDelay);
		Destroy(gameObject, doorOpenDelay);
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		GameManager.Instance.SetInitialEnemyCount();
	}
}
