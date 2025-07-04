using System.Collections;
using UnityEngine;
using TMPro;

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

    int bulletsPerBurst = 1;
    int burstBulletsLeft;

    [SerializeField] float spreadIntensity;


    [SerializeField] GameObject muzzleEffect;

    Animator pistolAnimation;

    [SerializeField] float reloadTime;
    [SerializeField] int magazineSize, bulletsLeft;
    bool isReloading;

    [SerializeField] TMP_Text ammoDisplay;
    void Awake()
    {
        pistolAnimation = GetComponent<Animator>();
        readyToShot = true;
        burstBulletsLeft = bulletsPerBurst;    
    }

    void Update()
    {
        if(bulletsLeft == 0 && isShooting) { SoundsManager.instance.audioEmpty.Play(); }

        if (currentShootingMode == ShootingMode.Auto)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !isReloading) { Reloading(); }

        if (readyToShot && !isShooting && !isReloading && bulletsLeft <= 0) { Reloading(); }

        if (readyToShot && isShooting && bulletsLeft > 0)
        {
            burstBulletsLeft = bulletsPerBurst;
            FireWeapon();
        }
        
        if(ammoDisplay != null) { ammoDisplay.text = $"{bulletsLeft/bulletsPerBurst}/{magazineSize/bulletsPerBurst}"; }
    }

    void FireWeapon()
    {
        bulletsLeft--;
        pistolAnimation.SetTrigger("Recoil");

        muzzleEffect.GetComponent<ParticleSystem>().Play();

        SoundsManager.instance.audioBullet.PlayOneShot(SoundsManager.instance.audioBullet.clip);
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

    void Reloading()
    {
        SoundsManager.instance.audioReloading.Play();

        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
    }

    void ReloadCompleted()
    {
        bulletsLeft = magazineSize;
        isReloading = false;
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
