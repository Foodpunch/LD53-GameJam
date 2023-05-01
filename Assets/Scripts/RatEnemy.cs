using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatEnemy : BaseEnemy
{
    NavMeshAgent agent;
    Vector2 cachedSpawnPos;
    float nextPositionTime;
    float detectionRange = 4.5f;

    [SerializeField]
    SpriteRenderer _sr;
    // Start is called before the first frame update
    protected override void Start()
    {
        MaxHP = 0.3f;
        base.Start();
        cachedSpawnPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Debug.DrawRay(transform.position,DirToPlayer.normalized * detectionRange,Color.red,Time.deltaTime);
        
        if(Vector2.Distance(PlayerTransform.position,transform.position) <= detectionRange){
            agent.SetDestination(PlayerTransform.position);
            _sr.flipX =  Vector2.Dot((PlayerTransform.position-transform.position).normalized, Vector2.right)>0;
        }
        else {
            if(Time.time >= nextPositionTime){
                nextPositionTime = Time.time+Random.Range(3f,5f);
                
                agent.SetDestination(cachedSpawnPos + (Random.insideUnitCircle*detectionRange/2f));
                _sr.flipX =  Vector2.Dot((cachedSpawnPos-(Vector2)transform.position).normalized, Vector2.right)>0;
            }
        }
    }
    void OnDrawGizmos(){
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(cachedSpawnPos,detectionRange/2f);
    }
}
