using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    private Vector2 input;

    public void Execute(PlayerController playerController)
    {
        Rigidbody playerRigidbody = playerController.GetRigidbody();
        if (playerRigidbody != null)
        {
            Vector3 dir = playerController.transform.forward * input.y + playerController.transform.right * input.x;
            dir *= playerController.MoveSpeed;
            dir.y = playerRigidbody.velocity.y;

            playerRigidbody.velocity = dir;
        }
    }

    public void SetInput(Vector2 input)
    {
        this.input = input;
    }
}
