using UnityEngine;

public class ScorePoint : MonoBehaviour, IInteractable
{
	[SerializeField] private float _destroyDelay;

	private void Start()
	{
		Invoke(nameof(Destroy), _destroyDelay);
	}

	private void Destroy()
	{
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent(out Neco neco))
		{
			Destroy(gameObject);
		}
	}
}