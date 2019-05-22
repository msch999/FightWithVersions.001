using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Chapter4
{

    public class HoverEffect : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
    {

        public bool shouldAnimate = true;
        public float popoutAmount = 50f;

        public bool shouldTint = false;
        public Color tintColor;

        public bool shouldScale = false;
        public float scaleAmount = 1.15f;

        private float initZ;
        private Color initColor;
        private float animationTime = 0.25f;

        void Start()
        {
            // Store the initial z position
            initZ = this.gameObject.transform.localPosition.z;

            // Store the inital color
            if (shouldTint)
            {
                initColor = gameObject.GetComponent<Image>().color;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (shouldAnimate)
            {
                LeanTween.moveLocalZ(gameObject, initZ - popoutAmount, animationTime).setEaseInOutCubic();
            }

            // Tint the element
            if (shouldTint)
            {
                gameObject.GetComponent<Image>().color = tintColor;
            }

            // Scale the element by the scaleAmount
            if (shouldScale)
            {
                LeanTween.scale(gameObject, new Vector3(scaleAmount, scaleAmount, scaleAmount), animationTime).setEaseInOutCubic();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (shouldAnimate)
            {
                LeanTween.moveLocalZ(gameObject, initZ, animationTime).setEaseInOutCubic();
            }

            // Tint the element back to default
            if (shouldTint)
            {
                gameObject.GetComponent<Image>().color = initColor;
            }

            // Scale back to 1
            if (shouldScale)
            {
                LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), animationTime).setEaseInOutCubic();
            }
        }
    }
}
