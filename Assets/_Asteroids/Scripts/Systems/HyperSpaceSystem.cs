using _Asteroids.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Asteroids.Scripts.Systems
{
    [AlwaysUpdateSystem]
    public class HyperSpaceSystem : SystemBase
    {
        private int screenWidth;
        private int screenHeight;
        private float3 ScreenEnds;

        private EndSimulationEntityCommandBufferSystem _endSimulationEntityCommandBufferSystem;

        protected override void OnStartRunning()
        {
            screenWidth = Screen.width;
            screenHeight = Screen.height;
            ScreenEnds = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth,screenHeight, 0f));

            _endSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            var ecb = _endSimulationEntityCommandBufferSystem.CreateCommandBuffer();

            Entities.ForEach((Entity e, in UniformScaleTag uniformScale) =>
            {
                var newScale = new Scale() {Value = 1};
                ecb.AddComponent(e, newScale);
                ecb.SetComponent(e, newScale);
            }).Run();
        }

        protected override void OnUpdate()
        {
            var deltaTime = Time.DeltaTime;
            var randomX = Random.Range(0, ScreenEnds.x);
           var randomY = Random.Range(0, ScreenEnds.y);
            Entities.ForEach((ref Translation playerTrans, ref Scale playerScale,
                ref HyperSpaceComponent hyperSpaceComponent, in PlayerInputData inputData) =>
            {
                hyperSpaceComponent.HyperSpaceCurrentCooldown -= deltaTime;
                Debug.Log("Update Entities");

                if (Input.GetKeyDown(inputData.HyperSpace) && hyperSpaceComponent.HyperSpaceCurrentCooldown < 0)
                {
                    hyperSpaceComponent.HyperSpaceCurrentCooldown = hyperSpaceComponent.HyperSpaceCooldown;
                    hyperSpaceComponent.IsHyperSpaceTravelling = true;
                    hyperSpaceComponent.IsShrinking = true;
                    Debug.Log("Begin hyperspace");
                }

                if (!hyperSpaceComponent.IsHyperSpaceTravelling) return;

                if (hyperSpaceComponent.IsShrinking)
                {
                    playerScale.Value -= hyperSpaceComponent.SizeChangeSpeed * deltaTime;

                    if (playerScale.Value > 0) return;

                    playerScale.Value = 0;

                    hyperSpaceComponent.IsShrinking = false;
                    hyperSpaceComponent.ChangePositionQueued = true;
                    return;
                }

                if (hyperSpaceComponent.ChangePositionQueued)
                {
                    playerTrans.Value = new float3(randomX,randomY, playerTrans.Value.z);
                    hyperSpaceComponent.ChangePositionQueued = false;
                    hyperSpaceComponent.IsGrowing = true;
                }
         
               

                if (hyperSpaceComponent.IsGrowing)
                {
                    playerScale.Value += hyperSpaceComponent.SizeChangeSpeed * deltaTime;

                    if (playerScale.Value < 1) return;

                    playerScale.Value = 1;
                    hyperSpaceComponent.IsGrowing = false;
                    hyperSpaceComponent.IsHyperSpaceTravelling = false;
                }
            }).Run();
        }

        /*
        private void HyperSpace(ref Translation playerTrans, ref Scale playerScale, ref HyperSpaceComponent hsComponent,
            in float deltaTime)
        {
            while (playerScale.Value > 0)
            {
                playerScale.Value -= deltaTime * hsComponent.SizeChangeSpeed;
            }

            playerScale.Value = 0;

            var randomX = UnityEngine.Random.Range(0, screenWidth);
            var randomY = UnityEngine.Random.Range(0, screenHeight);
            playerTrans.Value = new float3(randomX, randomY, 0);
            while (playerScale.Value < 1)
            {
                playerScale.Value += deltaTime * hsComponent.SizeChangeSpeed;
            }

            playerScale.Value = 1;
        }*/
    }
}