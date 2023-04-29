using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BaseEnemy : MonoBehaviour, IDamageable
{

    float MaxHP = 1f;
    public float currHP;

    public float moveSpeed;
    public Vector2 DirToPlayer;
    Transform PlayerTransform;
    bool isTrackingPlayer;
    public GameObject morselPrefab;
    Rigidbody2D _rb;
    public Animator _anim;
    public event System.Action deathEvent;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        currHP = MaxHP;
        PlayerTransform = PlayerMovement.instance.transform;    
    }

    // Update is called once per frame
    void Update()
    {
        DirToPlayer = PlayerTransform.position - transform.position;
        DirToPlayer.Normalize();
    }
    public void OnTakeDamage(float damage){
        _rb.AddForce(-DirToPlayer *5f);
        currHP -= damage;
        //play hurt anim here
        if(currHP <=0 && gameObject.activeInHierarchy){
            Die();
        }
    }
    [Button]
    void Die(){
        if(deathEvent!=null){
            deathEvent();
        }
        //Play audio 
        //Play anim
        Vector2 dir = (transform.position - PlayerTransform.position).normalized;
        VFXManager.instance.BloodSplat(dir,transform.position);
        VFXManager.instance.BloodSpray(transform.position);
        //spawn gibs
        Instantiate(morselPrefab,transform.position,Quaternion.identity);
        gameObject.SetActive(false);

    }
    protected virtual void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.CompareTag("Player")){
            //Send 1 damage to the player
            collision.collider.GetComponent<IDamageable>()?.OnTakeDamage(1);
            //maybe get knocked back?
        }
    }
}
