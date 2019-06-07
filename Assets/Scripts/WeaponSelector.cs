using UnityEngine;

public class WeaponSelector : MonoBehaviour
{

    enum Weapon { GUN, BOMB };

    public GameObject gun;
    public GameObject bomb;

    private Weapon currentWeapon;

    void Start()
    {
        currentWeapon = Weapon.GUN;
        gun.SetActive(true);
        bomb.SetActive(false);
        Debug.Log("WeaponSelector.Start(), gun active");
    }

    void Update()
    {
        //if (GvrController.AppButtonDown)
        if (GvrControllerInput.GetDevice(GvrControllerHand.Dominant).GetButtonDown(GvrControllerButton.App))
        {
            if (currentWeapon == Weapon.GUN)
            {
                gun.SetActive(false);
                bomb.SetActive(true);
                currentWeapon = Weapon.BOMB;
                Debug.Log("WeaponSelector.Update(), bomb active");
            }
            else if (currentWeapon == Weapon.BOMB)
            {
                gun.SetActive(true);
                bomb.SetActive(false);
                currentWeapon = Weapon.GUN;
                Debug.Log("WeaponSelector.Update(), gun active");
            }
        }
    }
}
