using UnityEngine;

public class JumpCommand : ICommand
{
    #region Methods

    public void Execute(PlayerController playerController)
    {
        if (IsGrounded(playerController))
        {
            Rigidbody playerRigidbody = playerController.GetRigidbody();
            if (playerRigidbody != null)
            {
                playerRigidbody.AddForce(Vector2.up * playerController.JumpForce, ForceMode.Impulse);
            }
        }
    }

    private bool IsGrounded(PlayerController playerController)
    {
        var transform1 = playerController.transform;
        var forward = transform1.forward;
        var position = transform1.position;
        var right = transform1.right;

        Ray[] rays =
        {
            new(position + forward * 0.2f + (Vector3.up * 0.1f) , Vector3.down),
            new(position + -forward * 0.2f+ (Vector3.up * 0.1f), Vector3.down),
            new(position + right * 0.2f + (Vector3.up * 0.1f), Vector3.down),
            new(position + -right * 0.2f + (Vector3.up * 0.1f), Vector3.down),
        };

        foreach (var ray in rays)
        {
            if (!Physics.Raycast(ray, out var hit, 0.2f, playerController.GroundLayerMask)) continue;
            return true;
        }
        return false;
    }

    #endregion
}
