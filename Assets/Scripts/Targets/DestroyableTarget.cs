using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableTarget : MonoBehaviour
{
    // calculate score and damange when hit by bullet
    // in canvas script update score and damage
    protected BulletInfo bulletInfo;
    protected GameObject hitBullet;
    //int weaponScore = 0;
    //int ammoScore = 0;
    //int weaponDamage = 0;
    int TargetHealth = 0;
    int totalDamage = 0;
    int TargetScore = 0;
    public bool isDestroyed = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hitBullet = collision.gameObject;
            bulletInfo = hitBullet.GetComponent<BulletInfo>().GetBulletInfo();
            Destroy(collision.gameObject);
            // if health 0 and destroyed

            isDestroyed = true;
            Destroy(this.gameObject);
        }
    }
    // public int GetTotalDamage()
    // {   // get damage from bullet
    //     weaponDamage = HitBullet.GetComponent<BulletInfo>().GetWeaponDamage();
    //     // calculate total damage
    //     totalDamage += weaponDamage;
    //     return totalDamage;
    // }
    public BulletInfo GetBulletInfo()
    {   
        return bulletInfo;
    }

    public void SetTargetScore(int score)
    {
        TargetScore = score;
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
        
        return TargetScore;
    }
    public int GetTargetHealth()
    {   
        
        return TargetHealth;
    }
    public int GetTargetDamage()
    {   
        
        return totalDamage;
    }
    // pass in different target health from different target, specify from each target script
    // public int GetTargetHealth(int TargetHealth)
    // {   // get damage from bullet
    //     weaponDamage = HitBullet.GetComponent<BulletInfo>().GetWeaponDamage();
    //     // calculate target health
    //     TargetHealth -= weaponDamage;
    //     return TargetHealth;
    // }
    // public int GetTotalScore(int TargetScore)
    // {   // get score from bullet
    //     weaponScore = HitBullet.GetComponent<BulletInfo>().GetWeaponScore();
    //     ammoScore = HitBullet.GetComponent<BulletInfo>().GetAmmoScore();
    //     // calculate total score
    //     TotalScore += (TargetScore + weaponScore + ammoScore);



    //     return TotalScore;
    // }
}
