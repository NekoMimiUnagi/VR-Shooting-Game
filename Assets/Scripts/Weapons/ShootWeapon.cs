using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWeapon : MonoBehaviour
{   
    public Rigidbody projectile;
    public float speed=500f;
//    public Transform target;
    public int magazineSize = 6;
    public float reloadTime = 0.5f; // for each bullet
    public float shootDelay = 1f; // for each bullet
    private int currentMagazineSize;
    private bool isReloading = false;
    private bool canShoot = true;
    private int damage = 0;
    private int weaponScore = 0;
    private int ammoScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentMagazineSize = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {   
        if(isReloading){
            return;
        }
        // rifle shooting
        if(Input.GetButton("js0") && currentMagazineSize> 0 && GameObject.FindWithTag("Rifle")==true && canShoot)
        {
            canShoot = false;
            shot();
            
            currentMagazineSize--;
            Debug.Log(currentMagazineSize);
            StartCoroutine(ShootDelay());
        }
        // other weapon shooting, shotgun, bow and crossbow
        else if(Input.GetButtonDown("js0") && currentMagazineSize > 0 && GameObject.FindWithTag("Rifle")==false){
            
            shot();
            currentMagazineSize--;
            Debug.Log("Shot");
            Debug.Log(currentMagazineSize);
        }
        // empty ammo reload
        else if (Input.GetButtonDown("js0") && currentMagazineSize == 0){
            Debug.Log("Out of Ammo");
            StartCoroutine(Reload());
        }
        // reload
        else if (Input.GetButtonDown("js2")){
            StartCoroutine(Reload());
        }
    }

    void shot()
    {
        Rigidbody instantiatedProjectile = Instantiate(projectile,transform.position,transform.rotation) as Rigidbody;
//        Vector3 direction = (target.position - transform.position).normalized;
        instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(-0.5f,1,10))*speed;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log(gameObject.name + " Reloading...");
       
        while(currentMagazineSize < magazineSize)
        {
            Debug.Log(gameObject.name + " Reloading..." + currentMagazineSize);
            yield return new WaitForSeconds(reloadTime);
        
            currentMagazineSize++;
        }
        
        Debug.Log(gameObject.name + " Reloaded.");
        isReloading = false;
    }
 
    //  shot delay for rifle
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }

    // collider for rifle
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Target"){
            Debug.Log("Hit");
            Destroy(other.gameObject);
            
        }
        Debug.Log("Hit");
        Destroy(gameObject);
    }

    public string GetRemainingAmmo()
    {
        return currentMagazineSize.ToString() + "/" + magazineSize.ToString();
    }
    
    // ammo type score
    public int GetAmmoScore()
    {
        if(GameObject.FindWithTag("Rifle")){
            ammoScore = 10;
        } 
        else if(GameObject.FindWithTag("Shotgun")){
            ammoScore = 30;
        }
        else if(GameObject.FindWithTag("Crossbow")){
            ammoScore = 100;
        }
        else if(GameObject.FindWithTag("Bow")){
            ammoScore = 100;
        }
        return ammoScore;
    }

    // damage for different weapons
    public int GetDamage()
    {
        if(GameObject.FindWithTag("Rifle")){
            damage = 10;
           
        } 
        else if(GameObject.FindWithTag("Shotgun")){
            damage = 30;
        }
        else if(GameObject.FindWithTag("Crossbow")){
            damage = 100;
        }
        else if(GameObject.FindWithTag("Bow")){
            damage = 100;
        }
   
       
        return damage;
    }

// every weapon has different score
    public int GetWeaponScore()
    {
        if(GameObject.FindWithTag("Rifle")){
            weaponScore = 10;
        } 
        else if(GameObject.FindWithTag("Shotgun")){
            weaponScore = 30;
        }
        else if(GameObject.FindWithTag("Crossbow")){
            weaponScore = 50;
        }
        else if(GameObject.FindWithTag("Bow")){
            weaponScore = 50;
        }
        return weaponScore;
    }
}
