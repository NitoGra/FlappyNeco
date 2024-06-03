using System;
using UnityEngine;

[RequireComponent(typeof(ShootBullet), typeof(Rigidbody2D))]
public class NecoMover : MonoBehaviour
{
	[SerializeField] private float _tapForce;
	[SerializeField] private float _speed;
	[SerializeField] private float _rotationSpeed;
	[SerializeField] private float _maxRotationZ;
	[SerializeField] private float _minRotationZ;

	private Vector3 _startPosition;
	private Rigidbody2D _rigidbody2D;
	private ShootBullet _shootBullet;
	private Quaternion _maxRotation;
	private Quaternion _minRotation;

	public event Action Fire;

	private void Start()
	{
		_startPosition = transform.position;
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_shootBullet = GetComponent<ShootBullet>();

		_maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
		_minRotation = Quaternion.Euler(0, 0, _minRotationZ);

		Reset();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Fire?.Invoke();
			_rigidbody2D.velocity = new(_speed, _tapForce);
			transform.rotation = _maxRotation;
			_shootBullet.Action();
		}

		transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
	}

	public void Reset()
	{
		transform.position = _startPosition;
		transform.rotation = Quaternion.identity;
		_rigidbody2D.velocity = Vector2.zero;
	}
}