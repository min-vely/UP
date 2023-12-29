using UnityEngine;

public class MoveCommand : ICommand
{
    #region Fields

    private Vector2 input;

    #endregion

    #region Methods

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

    #endregion
}
