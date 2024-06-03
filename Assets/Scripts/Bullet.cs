using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private float _destroyDelay = 2f;
	private int _ignoreColliderCount = 1;

	public event Action Died;

	private void Start()
	{
		Invoke(nameof(Destroy), _destroyDelay);
	}

	private void Destroy()
	{
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out ShootBullet shootBullet))
		{
			if (_ignoreColliderCount != 0)
			{
				_ignoreColliderCount--;
				return;
			}
			shootBullet.GetComponent<IMakeAnimation>();
			Died?.Invoke();
			Destroy();
		}
	}
}