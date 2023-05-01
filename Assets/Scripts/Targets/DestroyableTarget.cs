using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableTarget : MonoBehaviour
{
// calculate score and damange when hit by bullet
// in canvas script update score and damage
    public int TotalScore = 0;
    public BulletInfo bulletInfo;
    private GameObject HitBullet;
    private int weaponScore = 0;
    private int ammoScore = 0;
    private int weaponDamage = 0;
    private int TargetHealth = 0;
    private int totalDamage = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {   HitBullet = collision.gameObject;
            Destroy(collision.gameObject);
            if(GetTargetHealth(TargetHealth) <= 0)
            {
                Destroy(this.gameObject);
            }
            // Destroy(this.gameObject);
          
            
        }
    }

    public int GetTotalDamage()
    {   // get damage from bullet
        weaponDamage = HitBullet.GetComponent<BulletInfo>().GetWeaponDamage();
        // calculate total damage
        totalDamage += weaponDamage;
        return totalDamage;
    }
    public BulletInfo GetBulletInfo()
    {
        return bulletInfo;
    }
    // pass in different target health from different target, specify from each target script
    public int GetTargetHealth(int TargetHealth)
    {   // get damage from bullet
        weaponDamage = HitBullet.GetComponent<BulletInfo>().GetWeaponDamage();
        // calculate target health
        TargetHealth -= weaponDamage;
        return TargetHealth;
    }
    public int GetTotalScore(int TargetScore)
    {   // get score from bullet
        weaponScore = HitBullet.GetComponent<BulletInfo>().GetWeaponScore();
        ammoScore = HitBullet.GetComponent<BulletInfo>().GetAmmoScore();
        // calculate total score
        TotalScore += (TargetScore + weaponScore + ammoScore);



        return TotalScore;
    }
}
