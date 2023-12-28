using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICommand
{
    void Execute(PlayerController playerController);
    void SetInput(Vector2 input);
}
