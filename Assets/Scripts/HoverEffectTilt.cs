using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverEffectTilt : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IGvrPointerHoverHandler
{

    private const float _360_DEGREES = 360.0f;
    private const float _180_DEGREES = 180.0f;

    public bool shouldAnimate = true;
    public GameObject animationElement;
    public float popoutAmount = 50f;

    public bool shouldTint = false;
    public Color tintColor;

    public bool shouldScale = false;
    public float scaleAmount = 1.15f;

    [Range(0.0f, 5.0f)]
    [Tooltip("Maximum tile rotation towards the pointer.")]
    public float maximumRotationDegreesPointer = 3.0f;
    [Range(0.0f, 30.0f)]
    [Tooltip("Maximum tile rotation towards the camera.")]
    public float maximumRotationDegreesCamera = 15.0f;
    [Range(1.0f, 10.0f)]
    [Tooltip("Speed used for lerping the rotation/scale/position of the tile.")]
    public float interpolationSpeed = 8.0f;

    private float initZ;
    private Color initColor;
    private float animationTime = 0.25f;
    private Quaternion desiredRotation = Quaternion.identity;
    private bool isHovering = true;


    void Update()
    {
        UpdateRotation();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        isHovering = true;
        initZ = this.gameObject.transform.localPosition.z;

        // Store the inital color
        if (shouldTint)
        {
            if (animationElement != null)
            {
                initColor = animationElement.GetComponent<Image>().color;
            }
            else
            {
                initColor = gameObject.GetComponent<Image>().color;
            }
        }

        if (shouldAnimate)
        {
            // if no object is set to animate, animate the object this script is attached to
            if (animationElement != null)
            {
                LeanTween.moveLocalZ(animationElement, initZ - popoutAmount, animationTime).setEaseInOutCubic();
            }
            else
            {
                LeanTween.moveLocalZ(gameObject, initZ - popoutAmount, animationTime).setEaseInOutCubic();
            }
        }

        // Tint the element
        if (shouldTint)
        {
            if (animationElement != null)
            {
                animationElement.GetComponent<Image>().color = tintColor;
            }
            else
            {
                gameObject.GetComponent<Image>().color = tintColor;
            }
        }

        // Scale the element by the scaleAmount
        if (shouldScale)
        {
            if (animationElement != null)
            {
                /////// MAKE SURE SCALING WORKS WITH AN IMAGE --- otherwise leave this out
                LeanTween.scale(animationElement, new Vector3(scaleAmount, scaleAmount, scaleAmount), animationTime).setEaseInOutCubic();
            }
            else
            {
                LeanTween.scale(gameObject, new Vector3(scaleAmount, scaleAmount, scaleAmount), animationTime).setEaseInOutCubic();

            }

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;

        if (shouldAnimate)
        {
            if (animationElement != null)
            {
                LeanTween.moveLocalZ(animationElement, initZ, animationTime).setEaseInOutCubic();
            }
            else
            {
                LeanTween.moveLocalZ(gameObject, initZ, animationTime).setEaseInOutCubic();
            }
        }

        // Tint the element back to default
        if (shouldTint)
        {
            if (animationElement != null)
            {
                animationElement.GetComponent<Image>().color = initColor;
            }
            else
            {
                gameObject.GetComponent<Image>().color = initColor;
            }
        }

        // Scale back to 1
        if (shouldScale)
        {
            LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), animationTime).setEaseInOutCubic();
        }

        desiredRotation = Quaternion.identity;
    }

    public void OnGvrPointerHover(PointerEventData eventData)
    {
        isHovering = true;
        UpdateDesiredRotation(eventData.pointerCurrentRaycast.worldPosition);
    }

    private void UpdateRotation()
    {
        Quaternion finalDesiredRotation = desiredRotation;
        if (!isHovering)
        {
            finalDesiredRotation = Quaternion.identity;
        }

        if (finalDesiredRotation != transform.localRotation)
        {
            Quaternion localRotation = transform.localRotation;
            localRotation = Quaternion.Lerp(localRotation, finalDesiredRotation, Time.deltaTime * interpolationSpeed);
            transform.localRotation = localRotation;
        }
    }

    private void UpdateDesiredRotation(Vector3 pointerIntersectionWorldPosition)
    {
        Vector3 localCenter = CalculateLocalCenter();
        Vector3 worldCenter = transform.TransformPoint(localCenter);
        Vector2 localSize = CalculateLocalSize();

        Vector3 pointerLocalPositionOnTile = transform.InverseTransformPoint(pointerIntersectionWorldPosition);

        Vector3 pointerDiffFromCenter = pointerLocalPositionOnTile - localCenter;
        float pointerRatioX = pointerDiffFromCenter.x / localSize.x;
        float pointerRatioY = pointerDiffFromCenter.y / localSize.y;
        Vector2 pointerRatioFromCenter = new Vector2(pointerRatioX, pointerRatioY);

        float axisCoeff = maximumRotationDegreesPointer * 2.0f;

        Vector3 worldDirection = worldCenter - Camera.main.transform.position;
        Vector3 localDirection = transform.parent.InverseTransformDirection(worldDirection);
        Quaternion lookRotation = Quaternion.LookRotation(localDirection, Vector3.up);
        Vector3 lookEuler = clampEuler(lookRotation.eulerAngles, maximumRotationDegreesCamera);
        float eulerX = lookEuler.x - pointerRatioFromCenter.y * axisCoeff;
        float eulerY = lookEuler.y + pointerRatioFromCenter.x * axisCoeff;
        desiredRotation = Quaternion.Euler(eulerX, eulerY, lookEuler.z);
    }

    private Vector3 CalculateLocalCenter()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform)
        {
            Vector3 localCenter = rectTransform.rect.center;
            return localCenter;
        }
        return Vector3.zero;
    }

    private Vector2 CalculateLocalSize()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform)
        {
            Vector3 localMax = rectTransform.rect.max;
            Vector3 localMin = rectTransform.rect.min;
            return localMax - localMin;
        }
        return Vector2.zero;
    }

    private Vector3 clampEuler(Vector3 rotation, float maxDegrees)
    {
        rotation.x = clampDegrees(rotation.x, maxDegrees);
        rotation.y = clampDegrees(rotation.y, maxDegrees);
        rotation.z = clampDegrees(rotation.z, maxDegrees);
        return rotation;
    }

    private float clampDegrees(float degrees, float maxDegrees)
    {
        if (degrees > _180_DEGREES)
        {
            degrees -= _360_DEGREES;
        }

        return Mathf.Clamp(degrees, -maxDegrees, maxDegrees);
    }
}
