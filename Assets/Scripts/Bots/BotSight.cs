using System;
using DG.Tweening;
using UniRx;
using UnityEngine;

public class BotSight : MonoBehaviour
{
	[SerializeField]
	Light sightLight;

	[SerializeField]
	float viewDistance = 14;
	float viewAngle;
	Transform player;

	[SerializeField]
	LayerMask wallLayers;

	IDisposable sightDisposable;

	private void Start()
	{
		player = FindAnyObjectByType<PlayerMovementInput>().transform;
		viewAngle = sightLight.spotAngle;

		sightDisposable = Observable.EveryUpdate().Subscribe(_ => print(CanSeePlayer()));
	}

	private void OnDisable()
	{
		sightDisposable.Dispose();
	}

	bool CanSeePlayer()
	{
		if (Vector3.Distance(transform.position, player.position) < viewDistance)
		{
			Vector3 dirToPlayer = (player.position - transform.position).normalized;
			float angleBetweenBotAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
			//if player is in the angles half it is in the view are
			if (angleBetweenBotAndPlayer < viewAngle / 2)
			{
				if (!Physics.Linecast(transform.position, player.position, wallLayers))
				{
					return true;
				}
			}
		}
		return false;
	}

	void OnPlayerSeen() { }

	void ChangeLightColor(Color color)
	{
		sightLight.DOColor(color, 0.5f);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
	}
}
