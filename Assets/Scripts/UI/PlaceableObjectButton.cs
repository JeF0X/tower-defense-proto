using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using CG.Core;
using TMPro;

namespace CG.UI
{

    public class PlaceableObjectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] PlaceableObject placeableObjectPrefab = null;
        [SerializeField] GameObject overlay = null;
        [SerializeField] TMP_Text priceText = null;

        public bool isActive = false;

        ObjectManager objectManager = null;
        bool mouseOver = false;
        private bool isEnabled = false;
        Score score = null;

        private void Start()
        {
            score = FindObjectOfType<Score>();
            objectManager = FindObjectOfType<ObjectManager>();
            float cost = placeableObjectPrefab.GetComponent<PlaceableObject>().GetCost();
            priceText.text = Mathf.RoundToInt(cost).ToString() + "$";
        }

        private void Update()
        {
            if (LevelManager.Instance.GetState() == State.Game)
            {
                if (placeableObjectPrefab.GetComponent<Blocker>() && objectManager.blockers.Count >= objectManager.maxBlockers)
                {
                    isActive = false;
                    DisableButton();
                    return;
                }

                if (mouseOver && isEnabled && Input.GetMouseButtonUp(0))
                {

                    GetComponentInParent<GameUIHandler>().SetActiveButton(this);

                }
                if (isEnabled)
                {
                    overlay.SetActive(false);
                }

                if (isActive)
                {
                    GetComponent<Image>().color = Color.red;    
                }
                if (!isActive)
                {
                    GetComponent<Image>().color = Color.white;
                }

                if (placeableObjectPrefab.GetCost() > score.GetTotalScore())
                {
                    
                    DisableButton();
                }

                if (placeableObjectPrefab.GetCost() < score.GetTotalScore())
                {
                    EnableButton();
                }

                if (LevelManager.Instance.isInBulldozeMode)
                {
                    DisableButton();
                }
            }
        }

        public void EnableButton()
        {
            isEnabled = true;
        }

        public void DisableButton()
        {
            GetComponentInParent<GameUIHandler>().DeactivateButton(this);
            GetComponent<Image>().color = Color.gray;
            isEnabled = false;
            isActive = false;
        }

        public void SetActivePrefab()
        {
            FindObjectOfType<PlayerSpawner>().SetActivePlaceableObject(placeableObjectPrefab);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            mouseOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            mouseOver = false;
        }

        private bool CanBuy()
        {
            float cost = placeableObjectPrefab.GetComponent<PlaceableObject>().GetCost();

            if (score.GetTotalScore() > cost)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}