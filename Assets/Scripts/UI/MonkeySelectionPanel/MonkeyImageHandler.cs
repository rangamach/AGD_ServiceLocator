using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ServiceLocator.UI
{
    public class MonkeyImageHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
    {
        private Image monkeyImage;
        private MonkeyCellController owner;
        private Sprite spriteToSet;
        private RectTransform rect_transform;
        private Vector3 original_position;
        private Vector3 original_anchor_position;

        public void ConfigureImageHandler(Sprite spriteToSet, MonkeyCellController owner)
        {
            this.spriteToSet = spriteToSet;
            this.owner = owner;
        }

        private void Awake()
        {
            monkeyImage = GetComponent<Image>();
            monkeyImage.sprite = spriteToSet;
            rect_transform = GetComponent<RectTransform>();
            original_position = rect_transform.position;
            original_anchor_position = rect_transform.anchoredPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rect_transform.anchoredPosition += eventData.delta;
            owner.MonkeyDraggedAt(rect_transform.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ResetMonkeyImage();
            owner.MonkeyDroppedAt(eventData.position);
        }

        private void ResetMonkeyImage()
        {
            monkeyImage.color = new Color(1, 1, 1, 1);
            monkeyImage.rectTransform.position = original_position;
            monkeyImage.rectTransform.anchoredPosition = original_anchor_position;
            GetComponent<LayoutElement>().enabled = false;
            GetComponent<LayoutElement>().enabled = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            monkeyImage.color = new Color(1, 1, 1, 0.6f);
        }
    }
}