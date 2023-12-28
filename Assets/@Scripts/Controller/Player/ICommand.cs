using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICommand
{
    public void Execute(PlayerController playerController);
}
