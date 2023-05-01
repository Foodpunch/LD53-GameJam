using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatNest : BaseEnemy
{
    [SerializeField]
    GameObject RatPrefab;

    [SerializeField]
    GameObject specialRat;
    // Start is called before the first frame update
    protected override void Start()
    {
        MaxHP = 1f;
        base.Start();
    }

    // // Update is called once per frame
    // protected override void Update()
    // {
        
    // }
    public override void OnTakeDamage(float damage)
    {
        //base.OnTakeDamage(damage);
        currHP -= damage;
        SpawnRat();
        if(currHP <=0 && gameObject.activeInHierarchy){
            Die();
        }
    }

    void SpawnRat(){
        int randomValue = Random.Range(0,101);
        AudioManager.instance.PlaySoundAtLocation(
            AudioManager.instance.MiscSounds[4],0.3f,transform.position
        );
        GameObject ratClone; 
        if(randomValue <= 5f){
            ratClone= Instantiate(specialRat, transform.position, Quaternion.identity);
        }
        else  ratClone = Instantiate(RatPrefab, transform.position, Quaternion.identity);
        ratClone.transform.SetParent(ObjectManager.instance.transform);
    }
}
