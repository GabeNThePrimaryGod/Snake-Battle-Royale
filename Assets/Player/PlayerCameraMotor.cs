using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMotor : MonoBehaviour
{
    private Player player;
    private PlayerGrowingMotor growingMotor;

    private Vector3 startLocalPosition;
    private Vector3 startLocalEulerAngle;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        growingMotor = GetComponentInParent<PlayerGrowingMotor>();

        startLocalPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        startLocalEulerAngle = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    private void Update()
    {
        float value = (float)player.score / 2 ;
        transform.localPosition = new Vector3(startLocalPosition.x , startLocalPosition.y + value / 2, startLocalPosition.z - value / 2);
    }
}
