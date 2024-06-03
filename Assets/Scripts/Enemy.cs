using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IInteractable
{
	[SerializeField] private float _shootDelay;

	private bool _isShoot;
	private ShootBullet _shootBullet;

	private void Start()
	{
		_isShoot = TryGetComponent(out ShootBullet shootBullet);

		if (_isShoot)
		{
			_shootBullet = shootBullet;
			StartCoroutine(Attack());
		}
	}
	private IEnumerator Attack()
	{
		WaitForSecondsRealtime whait = new (_shootDelay);
		yield return whait;

		while (enabled)
		{
			_shootBullet.Action();
			yield return whait;
		}
	}
}