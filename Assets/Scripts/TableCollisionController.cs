using UnityEngine;

public class TableCollisionController : MonoBehaviour
{

    public PancakeScoreBoardController scoreboard;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "PancakeTag")
        {
            PancakeFlipped flipped = collider.gameObject.GetComponent<PancakeFlipped>();
            if (flipped.hasFlipped)
            {
                scoreboard.PancakeFlipped();
            }
        }
    }
}