
# Dynamic Buffer Sample Repo

This repository demonstrates how to perform entity spawning (both single and multiple) using dynamic buffers in Unity's ECS (Entity Component System) DOTS (Data-Oriented Technology Stack) framework.

## Overview

In Unity's ECS DOTS framework, dynamic buffers provide a flexible way to handle collections of data associated with entities. This sample repository showcases:

- **Single Entity Spawning**: Demonstrates how to spawn a single entity using dynamic buffers.
- **Multiple Entity Spawning**: Shows how to spawn multiple entities using dynamic buffers efficiently.

## Getting Started

### Prerequisites

Ensure you have the following software installed:

- [Unity 2020.3 LTS](https://unity3d.com/unity/whats-new/2020.3.22)
- [Entities Package](https://docs.unity3d.com/Packages/com.unity.entities@0.17/manual/index.html)

### Installation

1. Clone this repository to your local machine:
    ```bash
    git clone https://github.com/berkterek/dynamic_buffer_sample_repo.git
    ```
2. Open the project in Unity.

## Project Structure

- **Scripts/**: Contains all the C# scripts used for entity spawning and dynamic buffer management.
- **Scenes/**: Contains the sample scenes demonstrating single and multiple entity spawning.

## Usage

### Single Entity Spawning

The `SingleEntitySpawner` script demonstrates how to spawn a single entity and use a dynamic buffer to manage its components.

```csharp
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
```

### Multiple Entity Spawning

The `MultipleEntitySpawner` script shows how to spawn multiple entities using dynamic buffers efficiently.

```csharp
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
```

## Video Tutorial

For a detailed explanation and walkthrough of this repository, watch the video tutorial:

[![Watch the video](https://img.youtube.com/vi/bQiHUAVD1Rk/hqdefault.jpg)](https://youtu.be/bQiHUAVD1Rk)

## Contributing

Contributions are welcome! Please feel free to submit a pull request or open an issue to improve this project.
