using UnityEngine;

public class PancakeFlipped : MonoBehaviour
{

    public bool hasFlipped;

    void Start()
    {
        hasFlipped = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "FryingPanTag")
        {
            bool topBottom = false;
            Vector3 projectedUp = Vector3.Project(transform.up, Vector3.up);
            hasFlipped = projectedUp.y < 0;
        }
    }
}
