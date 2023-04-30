using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : BaseEnemy
{
    enum MinotaurState{
        PATROL,
        PREP,
        CHARGE,
        STUCK,
        DEAD
    }

    [SerializeField]
    MinotaurState _state = MinotaurState.PATROL;

    float stateTimer;

    List<Vector2> DirectionsList = new List<Vector2>{
        Vector2.right,
        Vector2.up,
        Vector2.left,
        Vector2.down
    };
    Vector2 randomDir;
    Vector2 cachedDir;
    float detectionRange =3f;
    [SerializeField]
    LayerMask rayLayer;
    [SerializeField]
    LayerMask chargeLayer;
    // Start is called before the first frame update
    protected override void Start()
    {
        MaxHP = 20f;
        base.Start();
        randomDir = DirectionsList[Random.Range(0,DirectionsList.Count)];
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        stateTimer+= Time.deltaTime;
        switch(_state){
            case MinotaurState.PATROL:
            //First we pick a random direction, then we make the minotaur walk till it hits something
            _rb.MovePosition((Vector2)transform.position+(randomDir*moveSpeed*0.5f)*Time.fixedDeltaTime);
            RaycastHit2D minotaurRay = Physics2D.Raycast(transform.position,randomDir,3f,rayLayer);
            if(minotaurRay.collider != null){
                randomDir = DirectionsList[Random.Range(0,DirectionsList.Count)];
            }
            if(Vector2.Distance(transform.position,PlayerTransform.transform.position) < detectionRange){
                isTrackingPlayer = true;
                _state = MinotaurState.PREP;
                stateTimer = 0;
            }
            Debug.DrawRay(transform.position,randomDir*detectionRange,Color.red,Time.deltaTime);
            break;
            case MinotaurState.PREP:
            //play prep animation here
            if(stateTimer >= 2f){
                cachedDir = DirToPlayer;
                _state = MinotaurState.CHARGE;
                stateTimer = 0;
            }
            break;
            case MinotaurState.CHARGE:
            _rb.MovePosition((Vector2)transform.position+(cachedDir*moveSpeed*1.5f)*Time.fixedDeltaTime);
            // RaycastHit2D chargeRayCheck = Physics2D.Raycast(transform.position,cachedDir,1f,chargeLayer);
            // if(chargeRayCheck.collider !=null){
            //     _state = MinotaurState.STUCK;
            //     stateTimer = 0;
            // }
            // Debug.DrawRay(transform.position,cachedDir,Color.red,Time.deltaTime);
            break;
            case MinotaurState.STUCK:
            //stuck anim and logic
            if(stateTimer >= 1f){
                _state = MinotaurState.PREP;
                stateTimer =0;
            }
            break;
            case MinotaurState.DEAD:
            break;
        }
    }
    protected override void OnCollisionEnter2D(Collision2D collision){
        base.OnCollisionEnter2D(collision);      
        if(collision.collider!=null){
            if(_state== MinotaurState.STUCK) return;
            _state = MinotaurState.STUCK;
            stateTimer = 0;
        }
    }
    void OnDrawGizmos(){
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position,detectionRange);
    }
    
}
