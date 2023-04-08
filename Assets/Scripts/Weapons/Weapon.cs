using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Bow,
    Crossbow,
    Shotgun,
    Rifle,
    RayGun,
    Grapple,
    FrogGenerator,
    Grenade
}
public class Weapon : MonoBehaviour
{
    public WeaponType type;
    public int magazineSize;
    public int maxMagazines;
    public float reloadTime;
    public float fireRate;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed;
    public float projectileGravity;
    public bool chambered;

    private int currentMagazine;
    private int magazinesRemaining;
    private bool reloading;
    private float timeSinceLastShot;
    // Start is called before the first frame update
    void Start()
    {
        currentMagazine = magazineSize;
        magazinesRemaining = maxMagazines - 1;
        reloading = false;
        timeSinceLastShot = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (reloading)
        {
            // Handle reloading logic
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot >= reloadTime)
            {
                // Done reloading
                reloading = false;
                timeSinceLastShot = 0f;
                currentMagazine = magazineSize;
                magazinesRemaining--;
            }
        }
        else
        {
            // Handle firing logic
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot >= 1f / fireRate)
            {
                // Can fire again
                if (type == WeaponType.Bow || type == WeaponType.Crossbow || type == WeaponType.RayGun)
                {
                    // Shoot one projectile
                    ShootProjectile();
                    timeSinceLastShot = 0f;
                }
                else if (type == WeaponType.Shotgun)
                {
                    // Shoot up to 6 projectiles in a spread pattern
                    for (int i = 0; i < 6; i++)
                    {
                        ShootProjectile();
                    }
                    currentMagazine -= 6;
                    if (currentMagazine < 0)
                    {
                        // Out of ammo, start reloading
                        reloading = true;
                        timeSinceLastShot = 0f;
                    }
                    else
                    {
                        timeSinceLastShot = 0f;
                    }
                }
                else if (type == WeaponType.Rifle)
                {
                    // Shoot one projectile
                    ShootProjectile();
                    currentMagazine--;
                    if (currentMagazine < 0)
                    {
                        // Out of ammo, start reloading
                        reloading = true;
                        timeSinceLastShot = 0f;
                    }
                    else
                    {
                        timeSinceLastShot = 0f;
                    }
                }
                else if (type == WeaponType.Grenade)
                {
                    // Shoot one grenade
                    ShootProjectile();
                    currentMagazine--;
                    if (currentMagazine < 0)
                    {
                        // Out of ammo, start reloading
                        reloading = true;
                        timeSinceLastShot = 0f;
                    }
                    else
                    {
                        timeSinceLastShot = 0f;
                    }
                }
            }
        }
    }

    private void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.velocity = projectileSpawnPoint.forward * projectileSpeed;
        projectileRigidbody.useGravity = projectileGravity > 0f;
    }
}
