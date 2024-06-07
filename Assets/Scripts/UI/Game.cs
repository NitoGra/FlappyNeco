using UnityEngine;

public class Game : MonoBehaviour
{
	[SerializeField] private Neco _neco;
	[SerializeField] private EnemyGenerator _enemyGenerator;
	[SerializeField] private StartScreen _startScreen;
	[SerializeField] private EndGameScreen _endGameScreen;
	[SerializeField] private Canvas _scoreScreen;

	private void OnEnable()
	{
		_startScreen.PlayButtonClicked += OnPlayButtonClick;
		_endGameScreen.RestartButtonClicked += OnRestartButtonClick;
		_neco.GameOver += OnGameOver;
	}

	private void OnDisable()
	{
		_startScreen.PlayButtonClicked -= OnPlayButtonClick;
		_endGameScreen.RestartButtonClicked -= OnRestartButtonClick;
		_neco.GameOver -= OnGameOver;
	}

	private void Start()
	{
		Time.timeScale = 0;
		_scoreScreen.enabled = false;
		_startScreen.Open();
	}

	private void OnGameOver()
	{
		Time.timeScale = 0;
		_endGameScreen.Open();
	}

	private void OnRestartButtonClick()
	{
		_endGameScreen.Close();
		StartGame();
	}
	private void OnPlayButtonClick()
	{
		_startScreen.Close();
		StartGame();
		_scoreScreen.enabled = true;
	}

	private void StartGame()
	{
		Time.timeScale = 1;
		_neco.Reset();
	}
}