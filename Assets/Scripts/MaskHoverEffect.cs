using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MaskHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IGvrPointerHoverHandler
{

    public Image image;

    [Range(0.01f, 0.2f)]
    [Tooltip("Tile forward distance when the pointer over the tile.")]
    public float hoverPositionZMeters = 0.125f;

    [Range(0.1f, 0.5f)]
    [Tooltip("Image scroll amount when the pointer over the tile.")]
    public float movementWeight = 0.15f;

    [Range(1.1f, 2.0f)]
    [Tooltip("Image scale amount when the pointer over the tile.")]
    public float scaleWeight = 1.4f;

    [Range(0.1f, 10.0f)]
    [Tooltip("Speed used for lerping the rotation/scale/position of the tile.")]
    public float interpolationSpeed = 8.0f;

    public float popoutAmount = 50f;

    // Ratio between meters (Unity Units) to the parent canvas that
    // this tile is part of.
    private float? metersToCanvasScale;

    private Vector3 originalMaskedPosition; // Start position when pointer is not on tile.
    private Vector3 maskedScrollOffset;
    private Vector2 originalImageSize;
    private Vector2 enlargedImageSize;
    private float desiredPositionZ;
    private bool isHovering = false;

    private float initZ;
    private float animationTime = 0.25f;

    // Use this for initialization
    void Start()
    {
        metersToCanvasScale = null;
        // Save size data.
        originalImageSize = image.rectTransform.sizeDelta;
        enlargedImageSize = originalImageSize;
        enlargedImageSize.x *= scaleWeight;
        enlargedImageSize.y *= scaleWeight;

        // Save position data.
        originalMaskedPosition = new Vector3(100f, -100f, 0);

        // Set data that varies.
        maskedScrollOffset = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScrollPosition();
        UpdateScale();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        desiredPositionZ = -hoverPositionZMeters / GetMetersToCanvasScale();

        LeanTween.moveLocalZ(gameObject, initZ - popoutAmount, animationTime).setEaseInOutCubic();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        maskedScrollOffset = Vector3.zero;
        desiredPositionZ = 0.0f;

        LeanTween.moveLocalZ(gameObject, initZ, animationTime).setEaseInOutCubic();
    }

    public void OnGvrPointerHover(PointerEventData eventData)
    {
        isHovering = true;
        Vector3 pos = eventData.pointerCurrentRaycast.worldPosition;

        RectTransform rectTransform = null;
        if (image)
        {
            rectTransform = image.GetComponent<RectTransform>();
        }

        if (!rectTransform)
        {
            return;
        }

        Rect rect = rectTransform.rect;
        Vector3 localCenter = rect.center;
        Vector3 worldCenter = image.transform.TransformPoint(localCenter);

        Vector3 localMin = new Vector3(rect.min.x, rect.min.y, 0.0f);
        Vector3 worldMin = image.transform.TransformPoint(localMin);

        worldCenter -= worldMin;
        pos -= worldMin;

        Vector3 direction = pos - worldCenter;
        maskedScrollOffset.x = (movementWeight * enlargedImageSize.x * direction.x);
        maskedScrollOffset.y = (movementWeight * enlargedImageSize.y * direction.y);
    }

    private void UpdateScrollPosition()
    {
        Vector3 desiredPosition = originalMaskedPosition;

        if (isHovering)
        {
            desiredPosition.x += maskedScrollOffset.x;
            desiredPosition.y += maskedScrollOffset.y;
        }

        Vector3 position = image.rectTransform.anchoredPosition3D;
        position = Vector3.Lerp(position, desiredPosition, Time.deltaTime * interpolationSpeed);
        image.rectTransform.anchoredPosition3D = position;
    }

    private float GetMetersToCanvasScale()
    {
        if (metersToCanvasScale == null)
        {
            metersToCanvasScale = GvrUIHelpers.GetMetersToCanvasScale(transform);
        }

        return metersToCanvasScale.Value;
    }

    private void UpdateScale()
    {
        Vector2 currentSize = image.rectTransform.sizeDelta;
        Vector2 desiredSize;

        if (isHovering)
        {
            desiredSize = enlargedImageSize;
        }
        else
        {
            desiredSize = originalImageSize;
        }

        currentSize = Vector2.Lerp(currentSize, desiredSize, Time.deltaTime * interpolationSpeed);
        image.rectTransform.sizeDelta = currentSize;
    }
}
