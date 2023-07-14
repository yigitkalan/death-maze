using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPad : MonoBehaviour
{
	InGameUIController _inGameUIController;

	private void Start()
	{
		_inGameUIController = FindObjectOfType<InGameUIController>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
		else
		{
			_inGameUIController.SetCompleteMenu();
		}
	}
}
