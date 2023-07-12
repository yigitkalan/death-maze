using System;
using DG.Tweening;
using UniRx;
using UnityEngine;

public class BotSight : MonoBehaviour
{
	Transform _player;
	IDisposable sightDisposable;

	[Header("SightSettings")]
	[SerializeField]
	Light sightLight;

	[SerializeField]
	float viewDistance = 14;
	float viewAngle;
	public Color patrolColor { get; private set; } = new Color(1, 0.98f, 0.47f);
	public Color chaseColor { get; private set; } = Color.red;

	[SerializeField]
	LayerMask wallLayers;

	[Header("Chasing")]
	[SerializeField]
	public float initialTimeLeftToFocusPlayer = 1f;
	float timeLeftToFocusPlayer;
	bool canSeePlayer = false;

	private void Start()
	{
		timeLeftToFocusPlayer = initialTimeLeftToFocusPlayer;
		_player = FindAnyObjectByType<PlayerMovementInput>().transform;
		viewAngle = sightLight.spotAngle;

		sightDisposable = Observable
			.EveryUpdate()
			.Where(_ => timeLeftToFocusPlayer > 0)
			.Subscribe(_ =>
			{
				if (CanSeePlayer())
				{
					timeLeftToFocusPlayer -= Time.deltaTime;
					if (!canSeePlayer)
					{
						canSeePlayer = true;
						OnPlayerSeen();
					}
					if (timeLeftToFocusPlayer < 0.001f)
					{
						GetComponent<BotMovement>().onPatrol = false;
					}
				}
				else
				{
					if (canSeePlayer)
					{
						timeLeftToFocusPlayer = initialTimeLeftToFocusPlayer;
						canSeePlayer = false;
						OnPlayerLost();
					}
				}
			});
	}

	private void OnDisable()
	{
		sightDisposable.Dispose();
	}

	public bool CanSeePlayer()
	{
		if (Vector3.Distance(transform.position, _player.position) < viewDistance)
		{
			Vector3 dirToPlayer = (_player.position - transform.position).normalized;
			float angleBetweenBotAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
			//if player is in the angles half then it is in the view are
			if (angleBetweenBotAndPlayer < viewAngle / 2)
			{
				if (!Physics.Linecast(transform.position, _player.position, wallLayers))
				{
					return true;
				}
			}
		}
		return false;
	}

	private void OnPlayerLost()
	{
		ChangeLightColor(new Color(1, 0.98f, 0.47f));
	}

	void OnPlayerSeen()
	{
		ChangeLightColor(Color.red);
	}

	public void ChangeLightColor(Color color)
	{
		sightLight.DOColor(color, initialTimeLeftToFocusPlayer).SetEase(Ease.InOutSine);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
	}
}
