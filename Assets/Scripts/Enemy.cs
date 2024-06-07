using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ShootBullet))]
public class Enemy : MonoBehaviour, IInteractable
{
	[SerializeField] private float _shootDelay;
	[SerializeField] private ScorePoint _prefab;

	private ShootBullet _shootBullet;

	private void Awake()
	{
		_shootBullet = GetComponent<ShootBullet>();
		StartCoroutine(Attack());
	}

	private void OnEnable()
	{
		_shootBullet.Action();
	}

	private IEnumerator Attack()
	{
		WaitForSecondsRealtime whait = new(_shootDelay);

		while (enabled)
		{
			_shootBullet.Action();
			yield return whait;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent(out Bullet bullet))
		{
			ScorePoint scorePoint = Instantiate(_prefab);
			scorePoint.transform.position = transform.position;
			Destroy(gameObject);
		}
	}
}