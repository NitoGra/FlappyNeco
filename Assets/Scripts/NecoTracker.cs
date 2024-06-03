using UnityEngine;

public class NecoTracker : MonoBehaviour
{
	[SerializeField] private Neco _neco;
	[SerializeField] private float _xOffset;

	private void Update()
	{
		Vector3 position = transform.position;

		if (_neco != null)
			position.x = _neco.transform.position.x + _xOffset;

		transform.position = position;
	}
}