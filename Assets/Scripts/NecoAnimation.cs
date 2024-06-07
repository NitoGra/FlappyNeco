using UnityEngine;

[RequireComponent(typeof(Animator), typeof(NecoShoot), typeof(Neco))]
public class NecoAnimation : MonoBehaviour
{
	private readonly int NecoFire = Animator.StringToHash(nameof(NecoFire));
	private readonly int NecoDead = Animator.StringToHash(nameof(NecoDead));
	private readonly int NecoIdle = Animator.StringToHash(nameof(NecoIdle));

	private Neco _neco;
	private NecoShoot _necoShoot;
	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_necoShoot = GetComponent<NecoShoot>();
		_neco = GetComponent<Neco>();
	}

	private void OnEnable()
	{
		_necoShoot.Fire += OnFire;
		_neco.GameOver += OnDied;
		_neco.GameReset += OnReset;
	}

	private void OnDisable()
	{
		_necoShoot.Fire -= OnFire;
		_neco.GameOver -= OnDied;
		_neco.GameReset -= OnReset;
	}

	private void OnDied()
	{
		_animator.Play(NecoDead);
	}

	private void OnFire()
	{
		_animator.Play(NecoFire);
	}

	private void OnReset()
	{
		_animator.Play(NecoIdle);
	}
}