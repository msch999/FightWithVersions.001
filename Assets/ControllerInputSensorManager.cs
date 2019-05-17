using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerInputSensorManager : MonoBehaviour
{
    public Text gyroTextField;
    public Text accelTextField;
    public Text orientTextField;

    void Update()
    {
        HandleControllerSensors();
    }

    private void HandleControllerSensors()
    {
        // Retrieve the angular velocity
        Vector3 angVel = GvrControllerInput.GetDevice(GvrControllerHand.Dominant).Gyro;
        gyroTextField.text = "angVel: " + angVel.x + "," + angVel.y + "," + angVel.z;

        // Retrieve the acceleration from accelerometer
        Vector3 accel = GvrControllerInput.GetDevice(GvrControllerHand.Dominant).Accel;
        accelTextField.text = "accel: " + accel.x + "," + accel.y + "," + accel.z;

        // Retrieve the orientation of the controller
        Vector3 orient = GvrControllerInput.GetDevice(GvrControllerHand.Dominant).Orientation.eulerAngles;
        orientTextField.text = "orient: " + orient.x + "," + orient.y + "," + orient.z;
    }
}
