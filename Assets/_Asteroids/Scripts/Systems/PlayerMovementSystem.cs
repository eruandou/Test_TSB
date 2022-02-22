using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;
using _Asteroids.Scripts.Components;
using Unity.Mathematics;
using Unity.Physics;
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
                (ref PhysicsVelocity playerVelocity, ref Rotation playerRotation,
                    in ShipMovementData moveData) =>
                {
                    var direction = math.mul(playerRotation.Value, new float3(0f, 1f, 0f));
                    var limitSpeed = new float3(moveData.maxMovementSpeed, moveData.maxMovementSpeed, 0);
                    playerVelocity.Linear += moveData.Speed * moveData.MoveForward * deltaTime * direction;
                    playerVelocity.Linear = math.clamp(playerVelocity.Linear, -limitSpeed, limitSpeed );
                    playerRotation.Value = math.mul(playerRotation.Value,
                        quaternion.RotateZ(math.radians(moveData.RotateDir * moveData.RotateSpeed * deltaTime)));
                }).Run();


            return default;
        }
    }
}