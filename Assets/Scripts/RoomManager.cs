using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    [SerializeField]
    List<Room> RoomList = new List<Room>();
    int gridWidth = 4;
    int gridHeight = 4;

    [SerializeField]
    List<GameObject> RoomPrefabs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GenerateRooms(){
        int startX = Random.Range(0,gridWidth);
        int endX = Random.Range(0,gridWidth);
        for(int x =0; x < gridWidth; ++x){
            for(int y=0; y < gridHeight; ++y){
                Room newRoom = new Room();
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
                RoomList.Add(newRoom);
            }
        }
    }
}
