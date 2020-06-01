using CG.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CG.UI
{
	public class PlaceableObjectUI : MonoBehaviour
	{
		bool mouseOverComponent = false;

		public void RefillShooterAmmunition()
		{
			try
			{
				GetComponent<Shooter>().RefillAmmo();
			}
			catch (System.Exception)
			{

				Debug.LogError(gameObject.name + ": Could not find Shooter Component");
			}
		}

		private void OnMouseExit()
		{
			mouseOverComponent = false;
			gameObject.SetActive(false);
		}

		private void OnMouseEnter()
		{
			mouseOverComponent = true;
		}

		public void Bulldoze()
		{
			Destroy(GetComponentInParent<PlaceableObject>().gameObject);
		}

		internal void SetUIActive(bool isActive)
		{
			if (isActive)
			{
				gameObject.SetActive(isActive);
			}
			else if (mouseOverComponent)
			{
				return;
			}
			else
			{
				gameObject.SetActive(isActive);
			}
		}
	}
}

