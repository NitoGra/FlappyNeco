using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : Collider2D
{
	[SerializeField] private T _zoneSpawn;
	[SerializeField] private Enemy _enemy;
	[SerializeField] private Neco _neco;
	[SerializeField] private Transform _enemyBox;

	[SerializeField] private int _poolCapacity;
	[SerializeField] private int _poolMaxSize;
	[SerializeField] private float _spawnDelay;

	[SerializeField] private bool _isSpawnInRandomArea;

	private WaitForSecondsRealtime _wait;
	private ObjectPool<Enemy> _pool;
	private Coroutine coroutineWork = null;
	private bool _isWorking = true;

	private void Start()
	{
		_pool = new ObjectPool<Enemy>(
			createFunc: () => CreateFunc(),
			actionOnGet: (obj) => ActionOnGet(obj),
			actionOnRelease: (obj) => ActionOnRealese(obj),
			actionOnDestroy: (obj) => _pool.Release(obj),
			collectionCheck: true,
			defaultCapacity: _poolCapacity,
			maxSize: _poolMaxSize);
	}

	private void OnEnable()
	{
		_neco.GameOver += DisablingSpawn;
		_neco.GameReset += Reset;
	}

	private void OnDisable()
	{
		_neco.GameOver -= DisablingSpawn;
		_neco.GameReset -= Reset;
	}

	private Enemy CreateFunc()
	{
		Enemy enemy = Instantiate(_enemy);
		enemy.SetSpawnSettings(_pool, _neco);
		return enemy;
	}

	private void ActionOnGet(Enemy obj)
	{
		Vector3 spawnPosistion = transform.position;

		if (_isSpawnInRandomArea)
			spawnPosistion = MakeRandomPositionInArea();

		obj.transform.SetParent(_enemyBox);
		obj.transform.position = spawnPosistion;
		obj.BackToDefault();
	}

	private Vector3 MakeRandomPositionInArea()
	{
		Bounds colliderBounds = _zoneSpawn.bounds;
		Vector3 colliderCenter = colliderBounds.center;

		float randomX = UnityEngine.Random.Range(colliderCenter.x - colliderBounds.extents.x, colliderCenter.x + colliderBounds.extents.x);
		float randomY = UnityEngine.Random.Range(colliderCenter.y - colliderBounds.extents.y, colliderCenter.y + colliderBounds.extents.y);
		float randomZ = UnityEngine.Random.Range(colliderCenter.z - colliderBounds.extents.z, colliderCenter.z + colliderBounds.extents.z);

		return new(randomX, randomY, randomZ);
	}

	private IEnumerator SpawnObject()
	{
		_wait = new(_spawnDelay);

		while (enabled)
		{
			if (_pool.CountActive < _poolMaxSize)
			{
				_pool.Get();
			}

			yield return _wait;
		}
	}

	protected void LaunchSpawn(bool isWork)
	{
		if (isWork)
		{
			coroutineWork = StartCoroutine(SpawnObject());
		}
		else
		{
			StopCoroutine(coroutineWork);
			coroutineWork = null;
		}
	}

	protected virtual void ActionOnRealese(Enemy obj)
	{
		obj.gameObject.SetActive(false);
	}

	private void DisablingSpawn()
	{
		_isWorking = false;
		ClearAllEnemy();
		LaunchSpawn(_isWorking);
	}

	private void Reset()
	{
		_isWorking = true;
		LaunchSpawn(_isWorking);
	}

	private void ClearAllEnemy()
	{
		Enemy[] enemys = _enemyBox.GetComponentsInChildren<Enemy>();

		foreach (Enemy enemy in enemys)
			enemy.Release();
	}
}