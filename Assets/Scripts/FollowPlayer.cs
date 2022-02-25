using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;

    private float smoothness = 100f;
    private Vector3 initialOffset = new Vector3(-1, 7, -10);
    private Vector3 cameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        cameraPosition = new Vector3(Player.transform.position.x + initialOffset.x, Player.transform.position.y + initialOffset.y, initialOffset.z);
        transform.position = Vector3.Lerp(transform.position, cameraPosition, smoothness * Time.fixedDeltaTime);
    }
}
