using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

public class BotMovement : MonoBehaviour
{
	[SerializeField]
	Transform[] patrolPoints;
	int targetPointIndex;

	NavMeshAgent agent;

	IDisposable movementDisposable;

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	void Start()
	{
		movementDisposable = Observable
			.EveryUpdate()
			.Where(
				_ =>
					agent.remainingDistance < agent.stoppingDistance
					&& !agent.pathPending
			)
			.Subscribe(_ =>
			{
				GotoNextPoint();
			});
	}

	private void OnDisable()
	{
		movementDisposable.Dispose();
	}

	void GotoNextPoint()
	{
		// Returns if no points have been set up
		if (patrolPoints.Length == 0)
			return;

		// Set the agent to go to the currently selected destination.
		agent.destination = patrolPoints[targetPointIndex].position;

		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		targetPointIndex = (targetPointIndex + 1) % patrolPoints.Length;
	}
}
