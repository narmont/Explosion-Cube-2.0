using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 40f;
    [SerializeField] private float _explosionForce = 100f;
    [SerializeField] private float _upwardsModifier = 0.4f;
    [SerializeField] private int _maxColliders = 32;

    public void ApplyExplosionCube(Vector3 explosionCenter, List<Cube> newCubes)
    {
        foreach (Cube cube in newCubes)
        {
            if (cube != null && cube.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_explosionForce, explosionCenter, _explosionRadius);
            }
        }
    }

    public void ApplyExplosionAll(Vector3 explosionCenter)
    {
        Collider[] _colliders = new Collider[_maxColliders];

        int count = Physics.OverlapSphereNonAlloc(explosionCenter, _explosionRadius, _colliders);

        for (int i = 0; i < count; i++)
        {
            Collider collider = _colliders[i];

            if (collider != null && collider.TryGetComponent(out Rigidbody rigidbody))
            {
                Vector3 direction = rigidbody.position - explosionCenter;
                float sqrDistance = direction.sqrMagnitude;
                float sqrRadius = _explosionRadius * _explosionRadius;

                if (sqrDistance <= sqrRadius)
                {
                    float distanceFactor = 1f - (sqrDistance / sqrRadius);
                    float sizeFactor = 1f / Mathf.Max(0.1f, collider.bounds.size.magnitude);
                    float force = _explosionForce * distanceFactor * sizeFactor;
                    rigidbody.AddExplosionForce(force, explosionCenter, _explosionRadius, _upwardsModifier, ForceMode.Impulse);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        float DecriseExplosionRadius = 2f;
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius / DecriseExplosionRadius);
    }
}