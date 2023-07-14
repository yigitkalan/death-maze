using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StartMenuUIController : MonoBehaviour
{
	public Button startButton;
	public Button quitButton;

	private void Start()
	{
		var root = GetComponent<UIDocument>().rootVisualElement;

		startButton = root.Q<Button>("startb");
		quitButton = root.Q<Button>("quitb");

		startButton.clicked += OnStartButtonClicked;
		quitButton.clicked += OnQuitButtonClicked;
	}

	void OnStartButtonClicked()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	void OnQuitButtonClicked()
	{
		Application.Quit();
	}
}
