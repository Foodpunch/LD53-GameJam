using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siren : BaseEnemy
{
    [SerializeField]
    float detectionRange = 5f;

    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    Transform shootPoint;
    float shootTimer;
    // Start is called before the first frame update
    // protected override void Start()
    // {
    //     base.Start();
    // }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        shootTimer += Time.deltaTime;
        if(Vector2.Distance(transform.position, PlayerTransform.position) <= detectionRange){
            //Play spawn animation
            if(shootTimer >=.15f){
                Shoot();
                shootTimer = 0;
            }
        }    
    }
    void Shoot(){
        shootPoint.right = DirToPlayer;
        GameObject bulletClone = Instantiate(bulletPrefab,shootPoint.position,Quaternion.identity);
        bulletClone.transform.right = DirToPlayer;
        bulletClone.transform.SetParent(ObjectManager.instance.transform);
    }
    void OnDrawGizmos(){
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position,detectionRange);
    }
}
