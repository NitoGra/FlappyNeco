using UnityEngine;

public class Bullet : MonoBehaviour, IInteractable
{
	[SerializeField] private float _destroyDelay;
	[SerializeField] private float _speed;

	private void Start()
	{
		Invoke(nameof(Destroy), _destroyDelay);
	}

	private void FixedUpdate()
	{
		transform.position += transform.right * _speed * Time.deltaTime;
	}

	private void Destroy()
	{
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out ShootBullet shootBullet))
		{
			Destroy();
		}
	}
}