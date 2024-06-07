using System;
using UnityEngine;

[RequireComponent(typeof(ShootBullet))]
public class NecoShoot : MonoBehaviour
{
	private ShootBullet _shootBullet;
	private KeyCode _shootButton = KeyCode.F;

	public event Action Fire;

	private void Start()
	{
		_shootBullet = GetComponent<ShootBullet>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(_shootButton))
		{
			Fire?.Invoke();
			_shootBullet.Action();
		}
	}
}