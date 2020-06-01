using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace CG.UI
{
    public class BulldozerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {        
        [SerializeField] Color highlightColor;
        [SerializeField] Color selectedColor;

        Color defaultColor;
        bool isInBulldozeMode = false;
        bool isMouseOver = false;
        Image buttonImage = null;
        GameUIHandler gameUIHandler = null;

        private void Start()
        {
            buttonImage = GetComponent<Image>();
            gameUIHandler = FindObjectOfType<GameUIHandler>();
            defaultColor = buttonImage.color;
        }

        private void Update()
        {
            if (LevelManager.Instance.GetState() == State.Game)
            {
                if (isMouseOver && Input.GetMouseButtonUp(0))
                {
                    isInBulldozeMode = !isInBulldozeMode;
                    LevelManager.Instance.ToggleBulldozeMode(isInBulldozeMode);
                }
                SetColor();
            }
            
        }


        public void OnPointerExit(PointerEventData eventData)
        {
            isMouseOver = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isMouseOver = true;
        }


        private void SetColor()
        {
            if (isInBulldozeMode)
            {
                buttonImage.color = selectedColor;
            }
            else if (isMouseOver && !isInBulldozeMode)
            {
                buttonImage.color = highlightColor;
            }
            else
            {
                buttonImage.color = defaultColor;
            }
        }
    }
}