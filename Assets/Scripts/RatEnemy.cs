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
        }
        else {
            if(Time.time >= nextPositionTime){
                nextPositionTime = Time.time+Random.Range(3f,5f);
                agent.SetDestination(cachedSpawnPos + (Random.insideUnitCircle*detectionRange/2f));
            }
        }

    }
    void OnDrawGizmos(){
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(cachedSpawnPos,detectionRange/2f);
    }
}
