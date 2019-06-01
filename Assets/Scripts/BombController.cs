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
        //Debug.Log("BombController.Start(), rend.material: " + rend.material);
    }

    void Update()
    {   // old to new Daydream API
        //if (GvrController.ClickButtonDown)
        if (GvrControllerInput.GetDevice(GvrControllerHand.Dominant).GetButtonDown(GvrControllerButton.TouchPadButton))
        {
            rend.material = bombMat;
            //Debug.Log("BombController.Update() Btn Dwn, rend.material: " + rend.material);

        }  // old to new Daydream API
        //else if (GvrController.ClickButtonUp)
        else if (GvrControllerInput.GetDevice(GvrControllerHand.Dominant).GetButtonUp(GvrControllerButton.TouchPadButton))
        {
            rend.material = transparentMat;
            //Debug.Log("BombController.Update() Btn Up, rend.material: " + rend.material);
            Vector3 bombPos = gameObject.transform.position;
            GameObject bombParticle = Instantiate(bombPrefab, bombPos, Quaternion.identity);
            Rigidbody rb = bombParticle.GetComponent<Rigidbody>();
            //rb.useGravity = true;
            rb.AddForce(throwVelocity, ForceMode.VelocityChange);
            BombExplosion explosion = bombParticle.GetComponent<BombExplosion>();
            explosion.Detonate();
        }

        //if (GvrController.ClickButton)
        if (GvrControllerInput.GetDevice(GvrControllerHand.Dominant).GetButton(GvrControllerButton.TouchPadButton))
        {
            Vector3 currentVelocity = (transform.position - previousPosition) / Time.deltaTime;
            //Debug.Log("BombController.Update(), current Velocity: " + currentVelocity) ;
            const int samples = 3;
            throwVelocity = throwVelocity * (samples - 1) / samples + currentVelocity / samples;
            previousPosition = transform.position;
        }
    }
}
