using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cyclops : BaseEnemy
{
    [SerializeField]
    LineRenderer _line;

    [SerializeField]
    float detectionRange = 8f;

    float shootTimer;

    enum CyclopsState{
        IDLE,
        TRACK,
        PREP,
        SHOOT,
        DEAD,
    }
    [SerializeField]
    CyclopsState _state = CyclopsState.IDLE;
    Vector2 cachedPosition;
    float lineWidthMult = 1f;

    [SerializeField]
    LayerMask rayLayer;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _line.SetPosition(0,transform.position);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        shootTimer+=Time.deltaTime;
        switch(_state){
            case CyclopsState.IDLE:
                if(Vector2.Distance(transform.position,PlayerTransform.position)<= detectionRange){
                    if(shootTimer >= 1f){
                        _state = CyclopsState.TRACK;
                        shootTimer = 0;
                    }
                }
                else {
                    _line.SetPosition(1,transform.position);
                    shootTimer = 0;
                }
            break;
            case CyclopsState.TRACK:
                _line.SetPosition(1,PlayerTransform.position); 
                if(shootTimer >= 2f){
                    cachedPosition = PlayerTransform.position;
                    _state = CyclopsState.PREP;
                    _anim.SetTrigger("Prep");
                    shootTimer = 0;
                }
            break;
            case CyclopsState.PREP:
                _line.SetPosition(1,cachedPosition);
                if(shootTimer >= 0.8f){
                    _state = CyclopsState.SHOOT;
                    shootTimer = 0;
                    _anim.SetTrigger("Fire");
                }
            break;
            case CyclopsState.SHOOT:
                _line.SetPosition(1,cachedPosition);
                _line.widthMultiplier = 20f;
                Shoot();
                if(shootTimer >= 0.3f){
                    _line.widthMultiplier = 1f;
                    _line.SetPosition(1,transform.position);
                    _state = CyclopsState.IDLE;
                    shootTimer =0;
                }
            break;
            case CyclopsState.DEAD:
            break;
        }
    }

    void Shoot(){
        Vector2 rayDir = cachedPosition - (Vector2)transform.position;
        rayDir.Normalize();
        Debug.DrawRay(transform.position,rayDir*detectionRange,Color.yellow,3f);

        RaycastHit2D rayHit = Physics2D.Raycast(transform.position,rayDir,Mathf.Infinity,rayLayer);
        if(rayHit.collider!=null){
            if(rayHit.collider.CompareTag("Player")){
                rayHit.collider.GetComponent<IDamageable>()?.OnTakeDamage(1);
            }
        }
    }
    void OnDrawGizmos(){
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position,detectionRange);
    }
}