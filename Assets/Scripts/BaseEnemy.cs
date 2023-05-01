using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BaseEnemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected float MaxHP = 1f;
    public float currHP;

    public float moveSpeed;
    public Vector2 DirToPlayer;
    protected Transform PlayerTransform;
    protected bool isTrackingPlayer;
    public GameObject morselPrefab;
    protected Rigidbody2D _rb;
    public Animator _anim;
    public event System.Action deathEvent;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        currHP = MaxHP;
        PlayerTransform = PlayerMovement.instance.transform;    
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(ScoreManager.instance.isGameOver) gameObject.SetActive(false);
        DirToPlayer = PlayerTransform.position - transform.position;
        DirToPlayer.Normalize();
    }
    public virtual void OnTakeDamage(float damage){
        _rb.AddForce(-DirToPlayer *5f);
        // Debug.Log(damage);
        currHP -= damage;
        //play hurt anim here
        if(currHP <=0 && gameObject.activeInHierarchy){
            Die();
        }
    }
    [Button]
    protected virtual void Die(){
        if(deathEvent!=null){
            deathEvent();
        }
        //Play audio 
        //Play anim
        Vector2 dir = (transform.position - PlayerTransform.position).normalized;
        VFXManager.instance.BloodSplat(dir,transform.position);
        VFXManager.instance.BloodSpray(transform.position);
        //spawn gibs
        GameObject morselClone = Instantiate(morselPrefab,transform.position,Quaternion.identity);
        morselClone.GetComponent<Rigidbody2D>().AddForce(dir*Random.Range(1f,2f),ForceMode2D.Impulse);
        morselClone.transform.SetParent(ObjectManager.instance.transform);
        gameObject.SetActive(false);

    }
    protected virtual void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.CompareTag("Player")){
            //Send 1 damage to the player
            collision.collider.GetComponent<IDamageable>()?.OnTakeDamage(1);
            Debug.Log("HIT PLAYER");
            //maybe get knocked back?
        }
    }
}
