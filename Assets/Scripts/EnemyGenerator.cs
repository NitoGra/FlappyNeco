using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
	[SerializeField] private ObjectPool _pool;
	[SerializeField] private float _delay;
	[SerializeField] private float _upperBound;
	[SerializeField] private float _lowerBound;

	private Coroutine _coroutine;

	private void OnEnable()
	{
		_coroutine = StartCoroutine(GeneratePipes());
	}

	private void OnDisable()
	{
		StopCoroutine(_coroutine);
		_coroutine = null;
	}

	private IEnumerator GeneratePipes()
	{
		WaitForSeconds wait = new(_delay);

		while (enabled)
		{
			Spawn();
			yield return wait;
		}
	}

	private void Spawn()
	{
		float spawnPositionY = Random.Range(_upperBound, _lowerBound);
		Vector3 spawnPoint = new(transform.position.x, spawnPositionY, transform.position.z);

		Enemy enemy = _pool.GetObject();

		enemy.gameObject.SetActive(true);
		enemy.transform.position = spawnPoint;
	}
}