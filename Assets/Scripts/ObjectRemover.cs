using UnityEngine;

public class ObjectRemover : MonoBehaviour
{
	[SerializeField] private ObjectPool _pool;
	[SerializeField] private ScoreCounter _scoreCounter;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent(out Enemy enemy))
		{
			_scoreCounter.Add();
			_pool.PutObject(enemy);
		}
	}
}