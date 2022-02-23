using Unity.Entities;
using UnityEngine;
using _Asteroids.Scripts.Components;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace _Asteroids.Scripts.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public class PlayerShootingSystem : SystemBase
    {
        private EntityManager _entityManager;
        private EndInitializationEntityCommandBufferSystem _endInitializationEntityCommandBufferSystem;

        protected override void OnCreate()
        {
            _endInitializationEntityCommandBufferSystem =
                World.GetOrCreateSystem<EndInitializationEntityCommandBufferSystem>();
        }

        protected override void OnStartRunning()
        {
            _entityManager = EntityManager;
        }

/*
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            

            //buffer.Playback(_entityManager);
            
            //buffer.Dispose();
            return default;
        }*/
        protected override void OnUpdate()
        {
            var deltaTime = Time.DeltaTime;
            var buffer = _endInitializationEntityCommandBufferSystem.CreateCommandBuffer();
            var bulletEntity = GetComponentDataFromEntity<BulletData>(true);
            Entities.ForEach(
                (ref ShootingData sData, in PlayerInputData inputData, in Rotation playerRotation,
                    in PhysicsVelocity playerVelocity,
                    in Translation playerPos) =>
                {
                    sData.CurrentFireRate -= deltaTime;
                    if (!Input.GetKey(inputData.Shoot) || sData.CurrentFireRate > 0)
                        return;


                    sData.CurrentFireRate = 1/sData.FireRatePerSecond;
                    var bullet = buffer.Instantiate(sData.BulletPrefab);

                    var pos = new Translation()
                    {
                        Value = playerPos.Value
                    };
                    var physicsSpeed = new PhysicsVelocity()
                    {
                        Linear = playerVelocity.Linear + new float3(1, 1, 0) *
                            math.mul(playerRotation.Value, new float3(0f, 1f, 0f)) * sData.BulletInitialVelocity
                    };

                    buffer.AddComponent(bullet, pos);
                    buffer.AddComponent(bullet, physicsSpeed);
                }
            ).Run();
            //  buffer.Playback(_entityManager);
        }
    }
}