using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMotor : MonoBehaviour
{
    private Player player = null;
    private Vector3 startLocalPosition;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        startLocalPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
    }

    private void FixedUpdate()
    {
        float distance = player.score / 4;
        Vector3 newPosition = new Vector3(startLocalPosition.x, startLocalPosition.y + distance, startLocalPosition.z - distance);

        Debug.Log("Position : " + transform.localPosition);
        Debug.Log("New Position : " + newPosition);

        //transform.localPosition += (transform.localPosition - newPosition) * Time.fixedDeltaTime;
    }
}
