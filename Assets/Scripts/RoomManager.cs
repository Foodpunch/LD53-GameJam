using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class RoomManager : MonoBehaviour
{

    [SerializeField]
    List<Room> RoomList = new List<Room>();
    int gridWidth = 4;
    int gridHeight = 4;

    [SerializeField]
    List<Room> RoomPrefabs = new List<Room>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [Button]
    public void GenerateRooms(){
        int startX = Random.Range(0,gridWidth);
        int endX = Random.Range(0,gridWidth);
        for(int x =0; x < gridWidth; ++x){
            for(int y=0; y < gridHeight; ++y){
                //Spawn in the room
                Room newRoom = Instantiate(RoomPrefabs[0]);
                newRoom.transform.SetParent(transform);
                newRoom.transform.localPosition = new Vector3(x*newRoom.size.x,y*newRoom.size.y);
                //Removing impossible exits
                if(y==0) newRoom.RemoveExit(Vector2Int.up);
                if(x==0) newRoom.RemoveExit(Vector2Int.left);
                if(x==gridWidth-1) newRoom.RemoveExit(Vector2Int.right);
                if(y==gridHeight-1) newRoom.RemoveExit(Vector2Int.down);
                //Setting the room index
                newRoom.index.x = x;
                newRoom.index.y = y;
                //Setting start and end rooms
                if(y==0 && x == startX) newRoom.isStart = true;
                if(y==gridHeight-1 && x == endX) newRoom.isEnd = true;
                newRoom.GenerateWalls();
                RoomList.Add(newRoom);
                
                
            }
        }
    }
}
