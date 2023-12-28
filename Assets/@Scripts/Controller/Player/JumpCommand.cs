using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : ICommand
{
    public void Execute(PlayerController playerController)
    {
        if (playerController.IsGrounded())
        {
            Rigidbody playerRigidbody = playerController.GetRigidbody();
            if (playerRigidbody != null)
            {
                playerRigidbody.AddForce(Vector2.up * playerController.JumpForce, ForceMode.Impulse);
            }
        }
    }

    public void SetInput(Vector2 input)
    {

    }
}
