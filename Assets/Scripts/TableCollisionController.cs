using UnityEngine;

public class TableCollisionController : MonoBehaviour
{

    public PancakeScoreBoardController scoreboard;

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("TableCollisionController: I am OnTriggerEnter()");
        if (collider.gameObject.tag == "PancakeTag")
        {
            Debug.Log("collider param is tagged PancakeTag");
            PancakeFlipped flipped = collider.gameObject.GetComponent<PancakeFlipped>();
            if (flipped.hasFlipped)
            {
                Debug.Log("And I have been flipped");
                scoreboard.PancakeFlipped();
            }
        }
    }
}