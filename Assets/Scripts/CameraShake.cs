using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
	CinemachineVirtualCamera _virtualCamera;

	[SerializeField]
	float shakeIntensity = 1f;

	[SerializeField]
	float shakeTime = 0.2f;

	float timer = 0f;

	CinemachineBasicMultiChannelPerlin _noise;

	private void Awake()
	{
		_virtualCamera = GetComponent<CinemachineVirtualCamera>();
	}

	private void Start()
	{
		SetStopShakeSettings();
	}

	public void ShakeCamera()
	{
		_noise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		_noise.m_AmplitudeGain = shakeIntensity;
		timer = shakeTime;
		StartCoroutine(StopShake());
	}

	IEnumerator StopShake()
	{
		while (timer > 0)
		{
			timer -= Time.deltaTime;
			yield return null;
		}
		SetStopShakeSettings();
	}

	void SetStopShakeSettings()
	{
		_noise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		_noise.m_AmplitudeGain = 0f;
		timer = 0f;
	}
}
