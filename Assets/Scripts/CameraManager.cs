using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Camera cam;
    Transform player;
    [SerializeField]
    float threshold;
    [SerializeField]
    float lerpSpeed=2.5f;   //higher is tighter cam control

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerMovement.instance.transform;
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if (PlayerMovement.instance.isDead) return;
       // if (RoomManager.instance.isBossDead) return;
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -10;
        Vector3 targetPos = (mousePos+ player.position) / 2f;
        targetPos.x = Mathf.Clamp(targetPos.x, -threshold + player.position.x, threshold + player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -threshold + player.position.y, threshold + player.position.y);
        transform.localPosition = Vector3.Lerp(transform.localPosition,targetPos,Time.deltaTime*lerpSpeed);
    }
}
