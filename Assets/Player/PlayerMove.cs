using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    Camera cam;

    [SerializeField]
    private float moveSpeed = 7f;
    private float moveMultiplier = 1f;

    [SerializeField]
    private float rotationMultiplier = 2f;

    [SerializeField]
    private float sprintCoolDown = 4f;

    [SerializeField]
    private float sprintTime = 5f;

    [SerializeField]
    private float sprintMuliplier = 2f;

    private bool canSprint = true;

    private void FixedUpdate()
    {
        float forwardMove = Input.GetAxis("Vertical");
        float horizontalMove = Input.GetAxis("Horizontal");

        Vector3 velocity = ((transform.forward * forwardMove) * (moveSpeed * moveMultiplier)) * Time.fixedDeltaTime;
        Vector3 rotation = new Vector3(0, (horizontalMove * rotationMultiplier) * moveMultiplier, 0);

        transform.eulerAngles += rotation;
        transform.position += velocity;

        if (Input.GetKeyDown(KeyCode.LeftShift) && canSprint)
        {
            StartCoroutine(Sprint());
        }
    }

    private IEnumerator Sprint()
    {
        StartCoroutine(SprintCooldown());
        moveMultiplier = sprintMuliplier;
        yield return new WaitForSeconds(sprintTime);
        moveMultiplier = 1;
    }

    private IEnumerator SprintCooldown()
    {
        canSprint = false;
        yield return new WaitForSeconds(sprintCoolDown + sprintTime);
        canSprint = true;
    }
}
