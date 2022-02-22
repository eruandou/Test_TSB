using Unity.Entities;
using _Asteroids.Scripts.Components;
using Unity.Jobs;

namespace _Asteroids.Scripts.Systems
{
    public class BulletDestroySystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem _endSimulationEntityCommandBufferSystem;

        protected override void OnCreate()
        {
            _endSimulationEntityCommandBufferSystem =
                World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }


        protected override void OnUpdate()
        {
            var deltaTime = Time.DeltaTime;
            var buffer = _endSimulationEntityCommandBufferSystem.CreateCommandBuffer();
            Entities.ForEach((ref BulletData data, in Entity entity) =>
            {
                data.Lifetime -= deltaTime;

                if (data.Lifetime < 0)
                {
                    buffer.DestroyEntity(entity);
                }
            }).Schedule();


            CompleteDependency();
        }
    }
}