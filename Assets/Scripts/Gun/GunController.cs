using UnityEngine;

public class GunController : MonoBehaviour
{
	[SerializeField]
	Transform shootPoint;

	[SerializeField]
	GameObject bulletPrefab;

	[SerializeField]
	float bulletSpeed = 10;

	[SerializeField]
	float fireRate = 0.5f;

	float nextTimeToFire = 0;

	void Start() { }

	// Update is called once per frame
	public void Shoot()
	{
		if (!CanShoot())
		{
			return;
		}
		GameObject bullet = Instantiate(
			bulletPrefab,
			shootPoint.position,
			transform.rotation
		);
		bullet.GetComponent<Rigidbody>().velocity =
			transform.forward * bulletSpeed;
		Destroy(bullet, 1);
	}

	public bool CanShoot()
	{
		if (Time.time < nextTimeToFire)

			return false;

		nextTimeToFire = Time.time + 1f / fireRate;
		return true;
	}
}
