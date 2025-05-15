using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 40f;
    [SerializeField] private float _explosionForce = 100f;
    [SerializeField] private float _upwardsModifier = 0.4f;

    public void Exploded()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        
        foreach(var hit in overlappedColliders)
        {
            Rigidbody rigidbody = hit.attachedRigidbody;

            if (rigidbody)
            {
                float distance = Vector3.Distance(transform.position, rigidbody.position);

                float normalizedDistance = 1f - Mathf.Clamp01(distance / _explosionRadius);

                float sizeFactor = 1f / Mathf.Max(0.1f, hit.bounds.size.magnitude);

                float force = _explosionForce * normalizedDistance * sizeFactor;

                rigidbody.AddExplosionForce(force, transform.position, _explosionRadius, _upwardsModifier, ForceMode.Impulse);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius / 2f);
    }
}
