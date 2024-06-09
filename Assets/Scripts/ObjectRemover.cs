using UnityEngine;

public class ObjectRemover : MonoBehaviour
{
	[SerializeField] private ScoreCounter _scoreCounter;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent(out Enemy enemy))
		{
			_scoreCounter.Add();
			enemy.Release();
		}
	}
}