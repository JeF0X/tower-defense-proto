using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{

    private float points = 0;
    private float totalPoints = 0;
    public float totalScore = 100;

    float homebaseHealth = 0;

    public void SetHomebaseHealth(float health)
    {
        homebaseHealth = health;
    }

    public float GetHomeBaseHealth()
    {
        return homebaseHealth;
    }

    public void IncreasePoints(float pointsToIncrease)
    {
        points += pointsToIncrease;
        totalScore += pointsToIncrease;
    }

    public void DecreasePoints(float pointsToDecrease)
    {
        totalScore -= pointsToDecrease;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    public float GetTotalScore()
    {
        return totalScore;
    }
}
