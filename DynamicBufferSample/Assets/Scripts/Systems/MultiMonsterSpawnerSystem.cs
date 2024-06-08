using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Systems
{
    [BurstCompile]
    public partial struct MultiMonsterSpawnerSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (multiMonsterSpawnerTag, spawnerTimeData, randomData, multiMonsterBuffers) in SystemAPI.Query<RefRO<MultiMonsterSpawnerTag>, RefRW<SpawnerTimeData>, RefRW<RandomData>, DynamicBuffer<MultiMonsterBuffer>>())
            {
                spawnerTimeData.ValueRW.CurrentTime += deltaTime;
                
                if(spawnerTimeData.ValueRO.MaxTime > spawnerTimeData.ValueRO.CurrentTime) continue;

                spawnerTimeData.ValueRW.CurrentTime = 0f;

                int randomIndex = randomData.ValueRW.Random.NextInt(0, multiMonsterBuffers.Length);
                var newMonster = entityCommandBuffer.Instantiate(multiMonsterBuffers[randomIndex].Entity);
                var randomPosition2 = math.normalize(randomData.ValueRW.Random.NextFloat2());
                float3 randomPosition3 = new float3(randomPosition2.x, randomPosition2.y, 0f);
                entityCommandBuffer.SetComponent(newMonster, new MoveDirectionData()
                {
                    Direction = randomPosition3
                });
            }
            
            entityCommandBuffer.Playback(state.EntityManager);
            entityCommandBuffer.Dispose();
        }
    }
}