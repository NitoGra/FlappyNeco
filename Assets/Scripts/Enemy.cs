using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(ShootBullet))]
public class Enemy : MonoBehaviour, IInteractable
{
	[SerializeField] private float _shootDelay;
	[SerializeField] private ScorePoint _prefab;

	private ObjectPool<Enemy> _pool;
	private ShootBullet _shootBullet;

	private void Awake()
	{
		_shootBullet = GetComponent<ShootBullet>();
		StartCoroutine(Attack());
	}

	private IEnumerator Attack()
	{
		WaitForSecondsRealtime whait = new(_shootDelay);

		while (enabled)
		{
			Shoot();
			yield return whait;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent(out Bullet bullet))
		{
			ScorePoint scorePoint = Instantiate(_prefab);
			scorePoint.transform.position = transform.position;
			Release();
		}
	}

	public void BackToDefault()
	{
		gameObject.SetActive(true);
		StartCoroutine(Attack());
	}

	public void SetSpawnSettings(ObjectPool<Enemy> pool, Neco neco)
	{
		_pool = pool;
		Shoot();
	}

	public void Release()
	{
		_pool.Release(this);
	}

	public void Shoot()
	{
		print("выстрел"+ _shootBullet);
		_shootBullet.Action();
	}
}