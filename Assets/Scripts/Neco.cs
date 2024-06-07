using System;
using UnityEngine;

[RequireComponent(typeof(NecoMover))]
[RequireComponent(typeof(ScoreCounter))]
[RequireComponent(typeof(NecoCollisionHandler))]
public class Neco : MonoBehaviour
{
	private NecoMover _necoMover;
	private ScoreCounter _scoreCounter;
	private NecoCollisionHandler _handler;

	public event Action GameOver;
	public event Action GameReset;

	private void Awake()
	{
		_scoreCounter = GetComponent<ScoreCounter>();
		_handler = GetComponent<NecoCollisionHandler>();
		_necoMover = GetComponent<NecoMover>();
	}

	private void OnEnable()
	{
		_handler.CollisionDetected += ProcessCollision;
	}

	private void OnDisable()
	{
		_handler.CollisionDetected -= ProcessCollision;
	}

	private void ProcessCollision(IInteractable interactable)
	{
		if (interactable is ScorePoint)
		{
			_scoreCounter.Add();
			return;
		}

		GameOver?.Invoke();
	}

	public void Reset()
	{
		GameReset?.Invoke();

		_scoreCounter.Reset();
		_necoMover.Reset();
	}
}