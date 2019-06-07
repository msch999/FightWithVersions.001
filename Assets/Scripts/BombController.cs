using UnityEngine;

public class BombController : MonoBehaviour
{

    public Material transparentMat;
    public Material bombMat;
    public GameObject bombPrefab;

    private MeshRenderer rend;
    private Vector3 throwVelocity;
    private Vector3 previousPosition;

    void Start()
    {
        rend = gameObject.GetComponentInChildren<MeshRenderer>();
        rend.material = transparentMat;
        Debug.Log("BombController.Start(), rend.material: " + rend.material);
    }

    void Update()
    {   // old to new Daydream API
        //if (GvrController.ClickButtonDown)
        if (GvrControllerInput.GetDevice(GvrControllerHand.Dominant).GetButtonDown(GvrControllerButton.TouchPadButton))
        {
            rend.material = bombMat;
            Debug.Log("BombController.Update() Button Down, rend.material: " + rend.material);
        } // old to new Daydream API
        //else if (GvrController.ClickButtonUp)
        else if (GvrControllerInput.GetDevice(GvrControllerHand.Dominant).GetButtonUp(GvrControllerButton.TouchPadButton))
        {
            rend.material = transparentMat;
            Vector3 bombPos = gameObject.transform.position;
            GameObject bombParticle = Instantiate(bombPrefab, bombPos, Quaternion.identity);
            Rigidbody rb = bombParticle.GetComponent<Rigidbody>();
            //rb.AddForce(throwVelocity, ForceMode.VelocityChange);

            rb.isKinematic = false; // false = Re-enables the physics engine.
            rb.useGravity = false;
            Vector3 throwVector = transform.position - previousPosition; // Get the direction that we're throwing
            rb.AddForce(throwVector * 10, ForceMode.Force); // Throws the ball by sending a force


            //Debug.Log("BombController.Update() Button Up, throwVelocity: " + throwVelocity);
            BombExplosion explosion = bombParticle.GetComponent<BombExplosion>();
            explosion.Detonate();
        }

        //if (GvrController.ClickButton)
        if (GvrControllerInput.GetDevice(GvrControllerHand.Dominant).GetButton(GvrControllerButton.TouchPadButton))
        {
            Vector3 currentVelocity = (transform.position - previousPosition) / Time.deltaTime;
            const int samples = 3;
            throwVelocity = throwVelocity * (samples - 1) / samples + currentVelocity / samples;
            //Debug.Log("BombController.Update(), GetButton, current Velocity: " + currentVelocity + " throwVelocity: " + throwVelocity);
            previousPosition = transform.position;
        }
    }
}
