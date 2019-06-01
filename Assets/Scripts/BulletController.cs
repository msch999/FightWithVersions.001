using UnityEngine;

public class BulletController : MonoBehaviour
{

    public GameObject bullet;

    void Update()
    {   // old to new Daydream API
        //if (GvrController.ClickButtonDown)
        if  (GvrControllerInput.GetDevice(GvrControllerHand.Dominant).GetButtonDown(GvrControllerButton.TouchPadButton))
        {
            Vector3 bulletPos = gameObject.transform.position;
            bulletPos += gameObject.transform.forward * 0.13f;
            GameObject bulletParticle = Instantiate(bullet, bulletPos, Quaternion.identity);
            Rigidbody rb = bulletParticle.GetComponent<Rigidbody>();
            rb.AddForce(gameObject.transform.forward * 400f);
            Destroy(bulletParticle, 3f);
        }
    }
}
