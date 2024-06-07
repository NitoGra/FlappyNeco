using UnityEngine;

public class ShootBullet : MonoBehaviour
{
	[SerializeField] private Rigidbody2D _bullet;
	[SerializeField] private float _bulletForse;
	[SerializeField] private Transform _gunBarrel;

	private readonly Quaternion _backwardRotation = Quaternion.Euler(0f, 180f, 0f);

	public void Action()
	{
		Rigidbody2D bullet = Instantiate(_bullet, _gunBarrel.position, transform.rotation);

		if (_bulletForse < 0)
			bullet.transform.rotation = _backwardRotation;
	}
}