using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Sample.Systems
{
    [BurstCompile]
    public partial struct OneMonsterSpawnSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (oneMonsterSpawnerTag, oneMonsterEntityData, spawnerTimeData, randomData) in SystemAPI.Query<RefRO<OneMonsterSpawnerTag>, RefRW<OneMonsterEntityData>, RefRW<SpawnerTimeData>, RefRW<RandomData>>())
            {
                spawnerTimeData.ValueRW.CurrentTime += deltaTime;
                
                if(spawnerTimeData.ValueRO.MaxTime > spawnerTimeData.ValueRO.CurrentTime) continue;

                spawnerTimeData.ValueRW.CurrentTime = 0f;

                var newMonster = entityCommandBuffer.Instantiate(oneMonsterEntityData.ValueRW.Entity);
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