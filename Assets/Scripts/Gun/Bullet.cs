using UnityEngine;

public class Bullet : MonoBehaviour
{
	private int defaultBulletDamage = 1;

	private void OnTriggerEnter(Collider other)
	{
		ICanTakeDamage damageTaker = other.gameObject.GetComponentInParent<ICanTakeDamage>();

		if (damageTaker != null)
		{
			damageTaker.TakeDamage(defaultBulletDamage);
		}
		Destroy(gameObject);
	}

	public void SetBulletDamage(int _bulletDamage)
	{
		defaultBulletDamage = _bulletDamage;
	}
}
