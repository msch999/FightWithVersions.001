using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ControllerManager : MonoBehaviour
{

    public Text infoTextField;

    // Update is called once per frame
    void Update()
    {
        HandleControllerUserInput();
    }

    private void HandleControllerUserInput()
    {
        /// TouchPad
        if (GvrControllerInput.TouchDown)
        {
            // Is true for 1 frame after touchpad is touched.
            infoTextField.text = "TouchDown";
        }
        if (GvrControllerInput.TouchUp)
        {
            // Is true for 1 frame after touchpad is released.
            infoTextField.text = "TouchUp";
        }
        if (GvrControllerInput.IsTouching)
        {
            // Check IsTouching before retrieving TouchPos.
            Vector2 touchPos = GvrControllerInput.TouchPos;
            infoTextField.text = "touchPos: " + touchPos.x + "," + touchPos.y;
        }
        /// Click Button(touchpad button)
        if (GvrControllerInput.ClickButton)
        {
            // True if the click button is currently being pressed.
            infoTextField.text = "ClickButton";
        }
        if (GvrControllerInput.ClickButtonDown)
        {
            // True for one frame after click button pressed.
            infoTextField.text = "ClickButtonDown";
        }
        if (GvrControllerInput.ClickButtonUp)
        {
            // True for one frame after click button released.
            infoTextField.text = "ClickButtonUp";
        }
        /// APP - BUTTON
        if (GvrControllerInput.AppButton)
        {
            // The App Button is currently being pressed
            infoTextField.text = "AppButton";
        }
        if (GvrControllerInput.AppButtonDown)
        {
            // True for 1 frame after App Button has been pressed.
            infoTextField.text = "AppButtonDown";
        }
        if (GvrControllerInput.AppButtonUp)
        {
            // True for 1 frame after App Button has been released.
            infoTextField.text = "AppButtonUp";
        }
        /// Home - Button
        if (GvrControllerInput.HomeButtonDown)
        {
            // Home button is currently down, always false in the emulator
            infoTextField.text = "HomeButtonDown";
        }
        if (GvrControllerInput.HomeButtonState)
        {
            // always false in the emulator
            infoTextField.text = "HomeButtonState true";
        }
    }

}
