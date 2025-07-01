using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 playerInputs;
    CharacterController playerController;
    float playerVelocity = 10f;
    Transform cameraPlayer;


    bool onGround;
    [SerializeField] Transform playerFeet;
    LayerMask cityMasks;
    float verticalPlayerVelocity;
    void Awake()
    {
        cameraPlayer = Camera.main.transform;
        playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Gravity();
    }

    void Movement()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, cameraPlayer.eulerAngles.y, transform.eulerAngles.z);

        playerInputs = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        playerInputs = transform.TransformDirection(playerInputs);
        playerController.Move(Time.deltaTime * playerVelocity * playerInputs);
    }

    void Gravity()
    {
        onGround = Physics.CheckSphere(playerFeet.position, 0.3f, cityMasks);

        if (onGround && verticalPlayerVelocity < 0) verticalPlayerVelocity = -2f;

        verticalPlayerVelocity += -9.81f * Time.deltaTime;

        playerController.Move(new Vector3(0, verticalPlayerVelocity, 0) * Time.deltaTime);
    }
}
