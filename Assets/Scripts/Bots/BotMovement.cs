using System;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

public class BotMovement : MonoBehaviour
{
	NavMeshAgent _agent;
	Transform _player;

	[Header("Chasing")]
	[SerializeField]
	float chaseSpeed = 4;

	[SerializeField]
	float lookDelay = 0.1f;
	Tween lookTween;
	bool chaseStarted = false;

	[Header("Patrol")]
	[SerializeField]
	Transform[] patrolPoints;
	int targetPointIndex = 0;
	public bool onPatrol = true;

	IDisposable movementDisposable;

	private void Awake()
	{
		_agent = GetComponent<NavMeshAgent>();
		_player = FindAnyObjectByType<PlayerMovementInput>().transform;
	}

	void OnDisable()
	{
		movementDisposable.Dispose();
		lookTween.Kill();
	}

	void Start()
	{
		movementDisposable = Observable
			.EveryUpdate()
			.Subscribe(_ =>
			{
				if (onPatrol)
				{
					if (_agent.remainingDistance < _agent.stoppingDistance && !_agent.pathPending)
					{
						GotoNextPoint();
					}
				}
				else if (!GameManager.Instance.isPlayerDead)
				{
					if (!chaseStarted)
					{
						SetChaseSettings();
					}
					ChasePlayer();
				}
			});
	}

	private void ChasePlayer()
	{
		_agent.SetDestination(_player.position);
		RotateTowardsPlayer();
	}

	void SetChaseSettings()
	{
		_agent.stoppingDistance = 6;
		_agent.speed = chaseSpeed;
		StartAllBotsChasing();
		chaseStarted = true;
	}

	void RotateTowardsPlayer()
	{
		lookTween.Kill();
		lookTween = transform.DOLookAt(_player.position, lookDelay);
	}

	void GotoNextPoint()
	{
		// Returns if no points have been set up
		if (patrolPoints.Length == 0)
			return;

		// Set the agent to go to the currently selected destination.
		_agent.destination = patrolPoints[targetPointIndex].position;

		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		targetPointIndex = (targetPointIndex + 1) % patrolPoints.Length;
	}

	void StartAllBotsChasing()
	{
		var bots = FindObjectsOfType<BotMovement>();
		foreach (var bot in bots)
		{
			bot.onPatrol = false;
		}
		BotSight[] sights = FindObjectsOfType<BotSight>();
		foreach (var sight in sights)
		{
			sight.ChangeLightColor(sight.chaseColor);
		}
	}
}
