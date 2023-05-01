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

    [SerializeField]
    Room StartRoom;

    [SerializeField]
    Room EndRoom;
    // Start is called before the first frame update
    void Start()
    {
        GenerateRooms();
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
                if(y==0 && x == startX){
                    Room startRoomClone = Instantiate(StartRoom);
                    startRoomClone.transform.SetParent(transform);
                    startRoomClone.transform.localPosition = new Vector3(x*startRoomClone.size.x,y*startRoomClone.size.y);
                    PlayerMovement.instance.transform.position = startRoomClone.transform.position;
                    startRoomClone.RemoveExit(Vector2Int.up);
                    if(startX == 0) startRoomClone.RemoveExit(Vector2Int.left);
                    if(startX == gridWidth-1 ) startRoomClone.RemoveExit(Vector2Int.right);
                    startRoomClone.GenerateWalls();
                    continue;
                } 
                if(y==gridHeight-1 && x == endX){
                    Room endRoomClone = Instantiate(EndRoom);
                    endRoomClone.transform.SetParent(transform);
                    endRoomClone.transform.localPosition = new Vector3(x*endRoomClone.size.x,y*endRoomClone.size.y);
                    endRoomClone.RemoveExit(Vector2Int.down);
                    if(endX == 0) endRoomClone.RemoveExit(Vector2Int.left);
                    if(endX == gridWidth-1 ) endRoomClone.RemoveExit(Vector2Int.right);
                    endRoomClone.GenerateWalls();
                    continue;
                } 
                //Spawn in the room
                Room newRoom = Instantiate(RoomPrefabs[Random.Range(0,RoomPrefabs.Count)]);
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
                newRoom.GenerateWalls();
                RoomList.Add(newRoom);
                
            }
        }
    }
}
