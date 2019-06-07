using UnityEngine;
public class BombExplosion : MonoBehaviour
{

    public void Detonate()
    {
        //Debug.Log("This is BombExplosion.Detonate()");
        Invoke("ExplodeBomb", 1.5f);
    }



    private void ExplodeBomb()
    {
        /*
            The ExplodeBomb() method uses the Physics.OverlapShere() method to
            retrieve all the colliders that are within a radius from the position of the explosion.
            A foreach loop is then used to cycle through each of the colliders, the Rigidbody
            associated with the collider is accessed, and an explosion force is added to it.
            AddExplosionForce takes power, position, radius, and upward modifier
            parameters. The upward modifier describes how much the explosion should lift the
            objects off the ground.
        */
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
