using UnityEngine;

public class Bullet : MonoBehaviour
{
	private int defaultBulletDamage = 1;

	private void OnCollisionEnter(Collision other)
	{
		ICanTakeDamage canTakeDamage = other.gameObject.GetComponent<ICanTakeDamage>();
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
