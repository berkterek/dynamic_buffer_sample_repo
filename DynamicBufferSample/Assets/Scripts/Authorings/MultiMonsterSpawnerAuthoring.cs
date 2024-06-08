using Components;
using Unity.Entities;
using UnityEngine;

namespace Sample.Authorings
{
    public class MultiMonsterSpawnerAuthoring : MonoBehaviour
    {
        public float MaxTime = 0.2f;
        public GameObject[] Prefabs;
        
        class MultiMonsterSpawnerBaker : Baker<MultiMonsterSpawnerAuthoring>
        {
            public override void Bake(MultiMonsterSpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                var buffer = AddBuffer<MultiMonsterBuffer>(entity);

                int length = authoring.Prefabs.Length;
                for (int i = 0; i < length; i++)
                {
                    buffer.Add(new MultiMonsterBuffer()
                    {
                        Entity = GetEntity(authoring.Prefabs[i], TransformUsageFlags.Dynamic)
                    });
                }
                AddComponent<MultiMonsterSpawnerTag>(entity);
                
                AddComponent(entity, new SpawnerTimeData()
                {
                    MaxTime = authoring.MaxTime,
                    CurrentTime = 0f
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