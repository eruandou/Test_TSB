using _Asteroids.Scripts.Components;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace _Asteroids.Scripts.Systems
{
    [AlwaysSynchronizeSystem]
    public class PlayerInputProcessSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref ShipMovementData moveData, in PlayerInputData inputData) =>
                {
                    moveData.MoveForward = Input.GetKey(inputData.Thrust) ? 1 : 0;
                    moveData.RotateDir = Input.GetKey(inputData.RotateRight) ? 1 :
                        Input.GetKey(inputData.RotateLeft) ? -1 : 0;
                    
                }
            ).Run();
        }
    }
}