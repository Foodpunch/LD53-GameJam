using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    float bulletDespawnTime = 0.3f;
    float bulletAirTime;
    [SerializeField]
    float bulletSpeed;

    public float bulletDamage = 0.5f;

    public LayerMask bulletLayer;
    Rigidbody2D _rb;
    [SerializeField]
    bool isDisableParticles = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        bulletAirTime += Time.deltaTime;
        _rb.velocity = transform.right * bulletSpeed;
        if(bulletAirTime >= bulletDespawnTime){
            Despawn();
        }
    }
    void Despawn(){
        _rb.Sleep();
        gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision !=null){
            SendDamage(collision.collider);
            if(collision.collider.GetComponent<IDamageable>() == null){
                if(!isDisableParticles) {
                    //spawn sparks animation because we prolly hit a wall
                    VFXManager.instance.Spark(transform.position,collision.contacts[0].normal);
                    //spawn poof smoke here
                    VFXManager.instance.Poof(transform.position);
                }
            }
            Despawn();
        }
    }
    void SendDamage(Collider2D col){
        if(col.GetComponent<IDamageable>()!=null){
            col.GetComponent<IDamageable>().OnTakeDamage(bulletDamage);
            //Play Audio here
        }
    }
}
