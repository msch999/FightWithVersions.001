using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerInputSensorManager : MonoBehaviour
{
    public Text gyroTextField;

    void Update()
    {
        HandleControllerSensors();
    }

    private void HandleControllerSensors()
    {
        // Retrieve the angular velocity
        Vector3 angVel = GvrControllerInput.GetDevice(GvrControllerHand.Dominant).Gyro;
        // gyroTextField.text = "angVel: " + angVel.x + "," + angVel.y + "," + angVel.z;

    }
}
