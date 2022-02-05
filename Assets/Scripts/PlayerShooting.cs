using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private RayGun rayGun;
    private PlayerInput playerInput;


    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (playerInput.actions["Shoot"].triggered)
        {
            rayGun.shootRay();
        }
    }
}
