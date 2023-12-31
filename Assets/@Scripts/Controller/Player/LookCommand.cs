using UnityEngine;

public class LookCommand : ICommand
{
    #region Methods

    public void Execute(PlayerController playerController)
    {
        playerController.UpdateCamCurXRot(playerController.MouseDelta.y * playerController.LookSensitivity);
        playerController.CameraContainer.localEulerAngles = new Vector3(-playerController.CamCurXRot, 0, 0);

        playerController.transform.eulerAngles += new Vector3(0, playerController.MouseDelta.x * playerController.LookSensitivity, 0);
    }

    #endregion
}
