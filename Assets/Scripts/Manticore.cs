using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manticore : BaseEnemy
{
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    Transform shootPoint;
    [SerializeField]
    Transform gunHolder;
    enum ManticoreState{
        IDLE,
        MOVE,
        SHOOT,
        DEAD
    }
    [SerializeField]
    ManticoreState _state = ManticoreState.IDLE;
    float minRange = 3f;         //min range the manticore has to be from player
    float detectionRange = 3f;         //aggro range
    float stateTimer = 0;
    float distanceToPlayer;
    Vector2 cacheSpawnPos;
    Vector2 randomDir = Vector2.zero;
    [SerializeField]
    LayerMask rayLayer;
    // Start is called before the first frame update
    protected override void Start()
    {
        MaxHP = 4f;
        base.Start();
        cacheSpawnPos = transform.position;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        stateTimer += Time.deltaTime;
        distanceToPlayer = Vector2.Distance(transform.position,PlayerTransform.transform.position);
        if(isTrackingPlayer){
            gunHolder.transform.right = DirToPlayer;
            float angle = gunHolder.transform.rotation.eulerAngles.z;
            gunHolder.GetComponent<SpriteRenderer>().flipY = (angle > 90f && angle < 270f);
        }
        switch(_state){
            case ManticoreState.IDLE:
            if(stateTimer >= 2f){
                if(Vector2.Distance(transform.position,cacheSpawnPos) >=detectionRange){
                    randomDir = cacheSpawnPos-(Vector2)transform.position;
                    randomDir.Normalize();
                }
                else {
                    randomDir = Random.insideUnitCircle;
                    randomDir.Normalize();
                }
                _state = ManticoreState.MOVE;
                stateTimer = 0;
            }
            break;
            case ManticoreState.MOVE:
            if(isTrackingPlayer){
                //If tracking player, sprite should always be facing the player direction
                //If the player is too close
             
                if(distanceToPlayer <= minRange){
                    _rb.MovePosition((Vector2)transform.position - DirToPlayer*moveSpeed*Time.fixedDeltaTime);
                } else {
                    _state = ManticoreState.SHOOT;
                    stateTimer = 0;
                }
            }
            else {
                _rb.MovePosition((Vector2)transform.position + randomDir*moveSpeed*Time.fixedDeltaTime);
                RaycastHit2D rayCheck = Physics2D.Raycast(transform.position,randomDir,2f,rayLayer);
                Debug.DrawRay(transform.position,randomDir,Color.red,Time.deltaTime);
                if(rayCheck.collider!= null){
                    randomDir = cacheSpawnPos-(Vector2)transform.position;
                    randomDir.Normalize();
                }
                if(Vector2.Distance(cacheSpawnPos,transform.position) >= 4f){
                    randomDir = cacheSpawnPos-(Vector2)transform.position;
                    randomDir.Normalize();
                    
                }
                if(stateTimer >= 1f){
                    _state = ManticoreState.IDLE;
                    stateTimer = 0;
                }
            }

            break;
            case ManticoreState.SHOOT:
            //play shoot anim
            Shoot();
            _state = ManticoreState.IDLE;
            stateTimer = 0;
            break;
            case ManticoreState.DEAD:
            break;
        }
        if( distanceToPlayer <= detectionRange){
            isTrackingPlayer = true;
        }
    }
    void Shoot(){
        GameObject bulletClone = Instantiate(bulletPrefab,shootPoint.position,Quaternion.identity);
        bulletClone.transform.right = DirToPlayer;
        bulletClone.transform.SetParent(ObjectManager.instance.transform);
    }
    protected override void Die()
    {
        base.Die();

    }
    void OnDrawGizmos(){
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(cacheSpawnPos,4f);
    }

}
