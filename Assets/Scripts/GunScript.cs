using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField]
    float fireRate;
    [SerializeField]
    float spreadAngle = 4f;

    int pelletCount = 3;
    float gunTime;

    [SerializeField]
    Transform shootPoint;

    [SerializeField]
    GameObject bulletPrefab;
    float nextTimeToFire;
    
    public static GunScript instance;


    void Awake(){
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)){
            FireGun();
        }
    }

    void FireGun(){
        gunTime += Time.deltaTime;
        if(Time.time >= nextTimeToFire){
            SpawnBullet();
            nextTimeToFire = Time.time + (1f/fireRate);
        }
    }
    void SpawnBullet(){
        for(int i =0; i<pelletCount; ++i){
           float spreadRange = Random.Range(-(spreadAngle * pelletCount), spreadAngle * pelletCount);
            Quaternion randomArc = Quaternion.Euler(0, 0, spreadRange);
            GameObject bulletClone = Instantiate(bulletPrefab, shootPoint.position, transform.rotation * randomArc);
            bulletClone.transform.SetParent(ObjectManager.instance.transform);
        }
    }
}
