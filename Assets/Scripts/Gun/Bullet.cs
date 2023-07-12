using UnityEngine;

public class Bullet : MonoBehaviour
{
	private int defaultBulletDamage = 1;

	private void OnTriggerEnter(Collider other)
	{
		ICanTakeDamage canTakeDamage = other.gameObject.GetComponentInParent<ICanTakeDamage>();
		if (canTakeDamage != null)
		{
			canTakeDamage.TakeDamage(defaultBulletDamage);
		}
		Destroy(gameObject);
	}

	public void SetBulletDamage(int _bulletDamage)
	{
		defaultBulletDamage = _bulletDamage;
	}
}
