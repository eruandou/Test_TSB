using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;
using _Asteroids.Scripts.Components;
using Unity.Mathematics;
using Unity.Rendering;

namespace _Asteroids.Scripts.Systems
{
    [AlwaysSynchronizeSystem]
    public class PlayerMovementSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var deltaTime = Time.DeltaTime;

            Entities.ForEach(
                (ref Translation playerTrans, ref Rotation playerRotation,
                    in ShipMovementData moveData) =>
                {
                    var direction = math.mul(playerRotation.Value, new float3(0f, 1f, 0f));
                    playerTrans.Value += moveData.Speed * moveData.MoveForward * deltaTime * direction;
                    playerRotation.Value = math.mul(playerRotation.Value,
                        quaternion.RotateZ(math.radians(moveData.RotateDir * moveData.RotateSpeed * deltaTime)));
                }).Run();


            return default;
        }
    }
}