using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableTarget : MonoBehaviour
{
    public BulletInfo bulletInfo;
    private GameObject HitBullet;
    private int weaponDamage = 0;
    private int TargetHealth = 100; // Set an initial value for TargetHealth
    private int totalDamage = 0;
    protected int targetScore = 0;

    public DestroyableTarget()
    {
    }

    protected DestroyableTarget(int score)
    {
        targetScore = score;
    }

    public bool isDestroyed = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            HitBullet = collision.gameObject;
            weaponDamage = HitBullet.GetComponent<BulletInfo>().GetWeaponDamage();
            totalDamage += weaponDamage;

            if (totalDamage >= TargetHealth) // Check if received damage is greater than or equal to object's health
            {
                isDestroyed = true;
                Destroy(this.gameObject);
            }

            Destroy(collision.gameObject);
        }
    }

    public BulletInfo GetBulletInfo()
    {
        BulletInfo bulletInfo = HitBullet.GetComponent<BulletInfo>().GetBulletInfo(HitBullet);
        return bulletInfo;
    }

    public void SetTargetScore(int score)
    {
        targetScore = score;
    }

    public void SetTargetDamage(int damage)
    {
        totalDamage = damage;
    }

    public void SetTargetHealth(int health)
    {
        TargetHealth = health;
    }

    public int GetTargetScore()
    {
        return targetScore;
    }

    public int GetTargetHealth()
    {
        return TargetHealth;
    }

    public int GetTargetDamage()
    {
        return totalDamage;
    }
}
