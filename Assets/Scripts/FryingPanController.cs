using UnityEngine;

public class FryingPanController : MonoBehaviour
{
    private Rigidbody fryingPanRigidbody;
    void Start()
    {
        fryingPanRigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        //fryingPanRigidbody.MoveRotation(GvrControllerInput.Orientation);
        fryingPanRigidbody.MoveRotation(GvrControllerInput.GetDevice(GvrControllerHand.Dominant).Orientation);
    }
}