using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 10f;
        [SerializeField] ParticleSystem deathVFX = null;
        [SerializeField] ParticleSystem hitVFX = null;
        [SerializeField] AudioClip deathSFX = null;
        [SerializeField] float damage = 1f;
        [SerializeField] MeshRenderer bodyMesh = null;
        [SerializeField] List<Color> healthColors = null;
        [SerializeField] int bodyMaterialIndex = 0;
        [SerializeField] bool usesHealthColors = false;

        int colorIndex = 0;
        Material currentMaterial = null;
        float startHealth = 0;

        private void Start()
        {
            if (healthColors.Count == 0)
            {
                Debug.LogWarning(gameObject.name + ": No colors added to health component");
                healthColors.Add(bodyMesh.materials[bodyMaterialIndex].color);
            }
            bodyMesh.materials[bodyMaterialIndex].color = healthColors[colorIndex];
        }

        private void ChangeColor()
        {
            colorIndex++;
            if (colorIndex < healthColors.Count && usesHealthColors)
            {
                bodyMesh.materials[bodyMaterialIndex].color = healthColors[colorIndex];
                
            }
        }

        public void ProcessHit(float damageTaken)
        {
            healthPoints -= damageTaken;
            ChangeColor();
            try
            {
                hitVFX.Play();
            }
            catch (Exception)
            {
            }

            if (GetComponent<PlaceableObject>())
            {
                StartCoroutine(GetComponent<PlaceableObject>().ChangeColorOnHit());
            }
            
            if (healthPoints <= 0)
            {
                if (GetComponent<Enemy>())
                {
                    FindObjectOfType<Score>().IncreasePoints(GetComponent<Enemy>().GetPoints());
                }
                
                Die();
            }
           
        }

        public float GetDamage()
        {
            return damage;
        }

        public void Die()
        {
            if (deathSFX != null)
            {
                AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, 0.5f);
            }
            else
            {
                Debug.LogWarning(name + ": No deathSFX attached");
            }
            Instantiate(deathVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        private void OnParticleCollision(GameObject other)
        {
            if (GetComponentInParent<Enemy>() && other.GetComponentInParent<Enemy>())
            {
                return;
            }
            if (GetComponentInParent<PlaceableObject>() && other.GetComponentInParent<PlaceableObject>())
            {
                return;
            }
            float damageTaken = other.GetComponentInParent<Health>().GetDamage();
            ProcessHit(damageTaken);
        }

        public float GetHealth()
        {
            return healthPoints;
        }
    }
}
