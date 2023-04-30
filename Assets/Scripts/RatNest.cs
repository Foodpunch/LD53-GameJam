using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatNest : BaseEnemy
{
    [SerializeField]
    GameObject RatPrefab;
    // Start is called before the first frame update
    protected override void Start()
    {
        MaxHP = 3f;
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
        GameObject ratClone = Instantiate(RatPrefab, transform.position, Quaternion.identity);
        ratClone.transform.SetParent(ObjectManager.instance.transform);
    }
}
