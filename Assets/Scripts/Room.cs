using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Room : MonoBehaviour
{
    public Vector2 size;
    public Vector2Int index;
    public List<Vector2Int> Exits = new List<Vector2Int> {
        Vector2Int.up,      //0,1
        Vector2Int.right,   //1,0
        Vector2Int.down,    //0,-1
        Vector2Int.left     //-1,0
    };

    [SerializeField]
    GameObject WallObj;

    public bool isStart = false;
    public bool isEnd = false;

    public Vector2Int GetRandomExit(){
        return Exits[Random.Range(0,Exits.Count)];
    }
    public Vector2Int GetRandomExit(Vector2Int existing){
        List<Vector2Int> remainingExits = new List<Vector2Int>();
        for(int i =0; i < Exits.Count; ++i){
            if(existing == Exits[i]) continue;
            if(existing == -Exits[i]) continue;
            remainingExits.Add(Exits[i]);
        }
        return remainingExits[Random.Range(0,remainingExits.Count)];
    }
    public void RemoveExit(Vector2Int exitToRemove){
        for(int i =0; i < Exits.Count; ++i){
            if(Exits[i]==exitToRemove) Exits.Remove(exitToRemove);
        }
    }
    void Start(){
        GenerateWalls();
    }
    [Button]
    public void GenerateWalls(){
        for(int x = 0; x < size.x; ++x){
            for(int y = 0; y < size.y; ++y){
                if(y==0){
                    if(Exits.Contains(Vector2Int.up)){
                        if(x == 10 || x == 9) continue;
                    }
                } 
                if(y==size.y-1){
                    if(Exits.Contains(Vector2Int.down)){
                        if(x == 10 || x == 9) continue;
                    }
                }
                if(x == 0){
                    if(Exits.Contains(Vector2Int.left)){
                        if(y == 10 || y == 9) continue;
                    } 
                }
                if(x==size.x-1){
                    if(Exits.Contains(Vector2Int.right)){
                        if(y == 10 || y == 9) continue;
                    }
                }
                if( (y > 0 && y < size.y-1) && (x > 0 && x < size.x-1)) continue;
                GameObject wallClone = Instantiate(WallObj);
                wallClone.transform.SetParent(gameObject.transform);
                wallClone.transform.localPosition = new Vector3(x-(size.x/2),y-(size.y/2),0);
            }
        }
    }
}
