using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniWeapon : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] float bulletVelocity = 50f;
    [SerializeField] float bulletPrefabLifetime = 2f;

    [SerializeField] enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }
    [SerializeField] ShootingMode currentShootingMode;

    [SerializeField] Camera cameraMiniPlayer;
    bool isShooting, readyToShot;
    bool allowReset = true;
    float shootingDelay = .5f;

    int bulletsPerBurst = 7;
    int burstBulletsLeft;

    [SerializeField] float spreadIntensity;


    [SerializeField] GameObject muzzleEffect;

    void Awake()
    {
        readyToShot = true;
        burstBulletsLeft = bulletsPerBurst;    
    }

    void Update()
    {
        if (currentShootingMode == ShootingMode.Auto)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (readyToShot && isShooting)
        {
            burstBulletsLeft = bulletsPerBurst;
            FireWeapon();
                
        }
    }

    void FireWeapon()
    {
        muzzleEffect.GetComponent<ParticleSystem>().Play();

        readyToShot = false;
        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        bullet.transform.forward = shootingDirection;

        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifetime));

        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
        }

        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }
    }

    void ResetShot()
    {
        readyToShot = true;
        allowReset = true;
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = cameraMiniPlayer.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;
        float x = Random.Range(-spreadIntensity, spreadIntensity);
        float y = Random.Range(-spreadIntensity, spreadIntensity);

        return direction + new Vector3(x, y, 0);
    }

    IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
