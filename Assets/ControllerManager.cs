using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ControllerManager : MonoBehaviour
{

    public Text infoTextField;
    void Start()
    {
        infoTextField.text = "Starting";
    }

    // Update is called once per frame
    void Update()
    {
        HandleControllerUserInput();
    }

    private void HandleControllerUserInput()
    {
        /// TouchPad
        // if (GvrControllerInput.TouchDown)
        // GvrControllerInput --> GvrControllerInput.GetDevice(GvrControllerHand.Dominant)
        if ( GvrControllerInput.GetDevice(GvrControllerHand.Dominant).GetButtonDown(GvrControllerButton.TouchPadButton) )
        {
            // Is true for 1 frame after touchpad is touched.
            infoTextField.text = "TouchDown";
        }
        // if (GvrControllerInput.TouchUp)
        if (GvrControllerInput.GetDevice(GvrControllerHand.Dominant).GetButtonUp(GvrControllerButton.TouchPadButton))
        {
            // Is true for 1 frame after touchpad is released.
            infoTextField.text = "TouchUp";
        }
        //if (GvrControllerInput.IsTouching)
        if (GvrControllerInput.GetDevice(GvrControllerHand.Dominant).GetButton(GvrControllerButton.TouchPadTouch))
                
        {
            // Check IsTouching before retrieving TouchPos.
            // Vector2 touchPos = GvrControllerInput.TouchPos;
            Vector2 touchPos = GvrControllerInput.GetDevice(GvrControllerHand.Dominant).TouchPos;
            infoTextField.text = "touchPos: " + touchPos.x + "," + touchPos.y;
        }
        // Click Button(touchpad button)
        // if (GvrControllerInput.ClickButton)
        if (GvrControllerInput.GetDevice(GvrControllerHand.Dominant).GetButton(GvrControllerButton.TouchPadButton))
        {
                // True if the click button is currently being pressed.
                infoTextField.text = "ClickButton";
        }
        // if (GvrControllerInput.ClickButtonDown)
        if (GvrControllerInput.GetDevice(GvrControllerHand.Dominant).GetButtonDown(GvrControllerButton.TouchPadButton))
        {
            // True for one frame after click button pressed.
            infoTextField.text = "ClickButtonDown";
        }
        //if (GvrControllerInput.ClickButtonUp)
        if (GvrControllerInput.GetDevice(GvrControllerHand.Dominant).GetButtonUp(GvrControllerButton.TouchPadButton))
        {
            // True for one frame after click button released.
            infoTextField.text = "ClickButtonUp";
        }
        /// APP - BUTTON
        //if (GvrControllerInput.AppButton)
        if (GvrControllerInput.GetDevice(GvrControllerHand.Dominant).GetButton(GvrControllerButton.App))
        {
                // The App Button is currently being pressed
                infoTextField.text = "AppButton";
        }
        //if (GvrControllerInput.AppButtonDown)
        if (GvrControllerInput.GetDevice(GvrControllerHand.Dominant).GetButtonDown(GvrControllerButton.App))
        {
            // True for 1 frame after App Button has been pressed.
            infoTextField.text = "AppButtonDown";
        }
        //if (GvrControllerInput.AppButtonUp)
        if (GvrControllerInput.GetDevice(GvrControllerHand.Dominant).GetButtonUp(GvrControllerButton.App))
        {
            // True for 1 frame after App Button has been released.
            infoTextField.text = "AppButtonUp";
        }
        /// Home - Button
        //if (GvrControllerInput.HomeButtonDown)
        if (GvrControllerInput.GetDevice(GvrControllerHand.Dominant).GetButtonDown(GvrControllerButton.System))
        {
            // Home button is currently down, always false in the emulator
            infoTextField.text = "HomeButtonDown";
        }
/* is there a new version of this? Or obsolete? Can't prevent it anyway.
        if (GvrControllerInput.HomeButtonState)
        //if (GvrControllerInput.GetDevice(GvrControllerHand.Dominant).GetButton(GvrControllerButton.System))
        {
            // always false in the emulator
            infoTextField.text = "HomeButtonState true";
        }
*/
    }

}
