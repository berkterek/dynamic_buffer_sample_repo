using Components;
using Unity.Entities;
using UnityEngine;

namespace Sample.Authorings
{
    public class OneMonsterSpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public float MaxSpawnTime = 1f;

        class OneMonsterSpawnerBaker : Baker<OneMonsterSpawnerAuthoring>
        {
            public override void Bake(OneMonsterSpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent<OneMonsterSpawnerTag>(entity);

                AddComponent<SpawnerTimeData>(entity, new()
                {
                    CurrentTime = 0f,
                    MaxTime = authoring.MaxSpawnTime
                });

                AddComponent<OneMonsterEntityData>(entity, new()
                {
                    Entity = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic)
                });
                
                uint seed = (uint)Mathf.Abs(entity.Index + entity.Version + new System.Random().Next());
                AddComponent<RandomData>(entity, new()
                {
                    Random = Unity.Mathematics.Random.CreateFromIndex(seed)
                });
            }
        }
    }
}