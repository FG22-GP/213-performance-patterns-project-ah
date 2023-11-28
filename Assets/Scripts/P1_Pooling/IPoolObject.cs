using UnityEngine;

namespace P1_Pooling
{
    public interface IPoolObject<T> where T : MonoBehaviour, IPoolObject<T>
    {
        void InitializePoolObject(ObjectPool<T> pool);
        void ReturnToPool();
    }
}
