using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 40f;
    [SerializeField] private float _explosionForce = 100f;
    [SerializeField] private float _upwardsModifier = 0.4f;

    public void ApplyExplosionForce(Vector3 explosionCenter)
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        
        foreach(var hit in overlappedColliders)
        {
            Rigidbody rigidbody = hit.attachedRigidbody;

            if (rigidbody)
            {
                Vector3 direction = rigidbody.position - explosionCenter;
                float sqrDistance = direction.sqrMagnitude;
                float sqrRadius = _explosionRadius * _explosionRadius;

                if (sqrDistance <= sqrRadius)
                {
                    float distanceFactor = 1f - (sqrDistance / sqrRadius);
                    float sizeFactor = 1f / Mathf.Max(0.1f, hit.bounds.size.magnitude);
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