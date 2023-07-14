using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class InGameUIController : MonoBehaviour
{
	public Button resumeButton;
	public Button mainButton;
	public Button pauseButton;

	public ProgressBar healthBar;

	VisualElement root;
	UIDocument uiDocument;

	private void Start()
	{
		SetPlayerUI();
	}

	void SetPlayerUI()
	{
		uiDocument = GetComponent<UIDocument>();
		root = uiDocument.rootVisualElement;
		pauseButton = root.Q<Button>("pauseb");
		pauseButton.clicked += OpenPauseMenu;
		healthBar = root.Q<ProgressBar>("hbar");
	}

	void SetPauseMenuUI()
	{
		mainButton = root.Q<Button>("mainb");
		resumeButton = root.Q<Button>("resumeb");

		mainButton.clicked += OnMainButtonClicked;
		resumeButton.clicked += OnResumeButtonClicked;
	}

	void OnMainButtonClicked()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(0);
	}

	void OpenPauseMenu()
	{
		Time.timeScale = 0;
		uiDocument.visualTreeAsset = Resources.Load<VisualTreeAsset>("UI/PauseMenu");
		root = uiDocument.rootVisualElement;
		SetPauseMenuUI();
	}

	void OnResumeButtonClicked()
	{
		Time.timeScale = 1;
		uiDocument.visualTreeAsset = Resources.Load<VisualTreeAsset>("UI/PlayerUI");
		root = uiDocument.rootVisualElement;
		SetPlayerUI();
	}

	public void SetHealthBar(float currentHealth)
	{
		// print(currentHealth);
		healthBar.value = currentHealth;
		//change health bar color
		if (currentHealth >= 80)
		{
			healthBar.style.unityBackgroundImageTintColor = Color.green;
		}
		else if (currentHealth >= 40)
		{
			healthBar.style.unityBackgroundImageTintColor = Color.yellow;
		}
		else
		{
			healthBar.style.unityBackgroundImageTintColor = Color.red;
		}
	}
}
