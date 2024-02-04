using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrowthPoolSingleton : MonoBehaviour
{
    private static GrowthPoolSingleton _instance;
    public static GrowthPoolSingleton Instance
    {
        get => _instance;
        private set => _instance = value;
    }

    double _growth;

    TMP_Text growthCounter;

    public double Growth
    {
        get => _growth;
        set
        {
            _growth = value;
            growthCounter.text = Mathf.Round((float) value).ToString();
        }
    }

    private void Awake()
    {
        _instance = this;
        growthCounter = GetComponent<TMP_Text>();
        Growth = 0;

    }
}
