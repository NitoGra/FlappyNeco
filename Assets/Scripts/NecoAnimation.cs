using UnityEngine;

[RequireComponent(typeof(Animator), typeof(NecoMover))]
public class NecoAnimation : MonoBehaviour, IMakeAnimation
{
	private readonly int NecoFire = Animator.StringToHash(nameof(NecoFire));
	private readonly int NecoDead = Animator.StringToHash(nameof(NecoDead));
	private readonly int NecoIdle = Animator.StringToHash(nameof(NecoIdle));

	private Bullet _bullet; 
	private NecoMover _neco;
	private Animator _animator;

	private void Start()
	{
		_animator = GetComponent<Animator>();
		_neco = GetComponent<NecoMover>();
	}

	private void OnEnable()
	{
		_neco.Fire += OnFire;
		_bullet.Died += OnDied;
	}

	private void OnDisable()
	{
		_neco.Fire -= OnFire;
		_bullet.Died -= OnDied;
	}

	private void OnDied()
	{
		_animator.Play(NecoDead);
	}

	private void OnFire()
	{
		_animator.Play(NecoFire);
	}

	public void SetBullet(Bullet bullet)
	{
		_bullet = bullet;
	}
}
