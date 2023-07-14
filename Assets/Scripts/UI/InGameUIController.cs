using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class InGameUIController : MonoBehaviour
{
	[Header("Pause Menu")]
	[HideInInspector]
	public Button resumeButton;

	[HideInInspector]
	public Button mainButton;

	[Header("Player UI")]
	[HideInInspector]
	public Button pauseButton;

	[HideInInspector]
	public ProgressBar healthBar;

	[Header("Death Menu")]
	public Label score;

	VisualElement root;
	UIDocument uiDocument;

	private void Start()
	{
		uiDocument = GetComponent<UIDocument>();
		SetPlayerUI();
	}

	public void SetDeathUI()
	{
		uiDocument.visualTreeAsset = Resources.Load<VisualTreeAsset>("UI/DeathMenu");
		root = uiDocument.rootVisualElement;
		mainButton = root.Q<Button>("mainb");
		mainButton.clicked += OnMainButtonClicked;
		score = root.Q<Label>("scoreText");
		score.text = "Your Score : " + GameManager.Instance.playerPoints.ToString();
	}

	public void SetCompleteMenu()
	{
		uiDocument.visualTreeAsset = Resources.Load<VisualTreeAsset>("UI/CompleteMenu");
		root = uiDocument.rootVisualElement;
		mainButton = root.Q<Button>("mainb");
		mainButton.clicked += OnMainButtonClicked;
		score = root.Q<Label>("scoreText");
		score.text = "Your Score : " + GameManager.Instance.playerPoints.ToString();
	}

	void SetPlayerUI()
	{
		uiDocument.visualTreeAsset = Resources.Load<VisualTreeAsset>("UI/PlayerUI");
		root = uiDocument.rootVisualElement;
		pauseButton = root.Q<Button>("pauseb");
		pauseButton.clicked += OpenPauseMenu;
		healthBar = root.Q<ProgressBar>("hbar");
	}

	void SetPauseMenuUI()
	{
		uiDocument.visualTreeAsset = Resources.Load<VisualTreeAsset>("UI/PauseMenu");
		root = uiDocument.rootVisualElement;
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
		SetPauseMenuUI();
	}

	void OnResumeButtonClicked()
	{
		Time.timeScale = 1;
		SetPlayerUI();
	}

	public void SetHealthBar(float currentHealth)
	{
		// print(currentHealth);
		// healthBar.value = currentHealth;
		DOTween.To(() => healthBar.value, x => healthBar.value = x, currentHealth, 0.2f);

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
