using System.Threading;
using UnityEngine;

namespace P1_Pooling
{
    public class Projectile : MonoBehaviour, IPoolObject<Projectile>
    {
        private float _totalTime;
        private ObjectPool<Projectile> _pool;    
    
        void Start()
        {
            ReturnToPool();
            FakeInitializeProjectile();
        }

        /// <summary>
        /// Setting up complex Prefabs containing Models, Sprites, Materials etc.
        /// Is Expensive. This Call simulates a more complex Object.
        /// Do not remove this Method invocation from Start.
        /// Instead, try to avoid `Start` being invoked in the first place. 
        /// </summary>
        void FakeInitializeProjectile()
        {
            Thread.Sleep(100);
        }
    
        // Update is called once per frame
        void Update()
        {
            this._totalTime += Time.deltaTime;
            this.transform.Translate(Vector3.up * Time.deltaTime);
            if (this._totalTime > 10f)
            {
                ReturnToPool();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Debug.Log("On Collision!");
            //Destroy(this.gameObject);
            ReturnToPool();
        }

        public void InitializePoolObject(ObjectPool<Projectile> pool)
        {
            _pool = pool;
        }

        public void ReturnToPool()
        {
            _pool.Return(this);
            _totalTime = 0f;
        }
    }
}
