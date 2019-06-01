using UnityEngine;
public class BombExplosion : MonoBehaviour
{

    public void Detonate()
    {
        Debug.Log("This is BombExplosion.Detonate()");
        Invoke("ExplodeBomb", 1.5f);
    }

    private void ExplodeBomb()
    {
        float radius = 7.0F;
        float power = 500.0F;

        Vector3 explosionPos = gameObject.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, 100f, 20.0F);
            }
        }
        Destroy(gameObject);
    }
}