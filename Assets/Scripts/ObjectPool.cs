using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	[SerializeField] private Enemy _prefab;
	[SerializeField] private Neco _neco;

	private Queue<Enemy> _pool;

	public IEnumerable<Enemy> PooledObjects => _pool;

	private void OnEnable()
	{
		_neco.GameReset += OnReset;
	}

	private void OnDisable()
	{
		_neco.GameReset -= OnReset;
	}

	private void Awake()
	{
		_pool = new Queue<Enemy>();
	}

	private void OnReset()
	{
		_pool.Clear();
	}

	public Enemy GetObject()
	{
		if (_pool.Count == 0)
			return Instantiate(_prefab);

		return _pool.Dequeue();
	}

	public void PutObject(Enemy enemy)
	{
		_pool.Enqueue(enemy);
		enemy.gameObject.SetActive(false);
	}
}