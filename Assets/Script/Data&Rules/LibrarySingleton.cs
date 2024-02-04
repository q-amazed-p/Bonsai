using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibrarySingleton : MonoBehaviour
{
    static LibrarySingleton _instance;
    public static LibrarySingleton Instance
    {
        get => _instance;
        private set => _instance = value;
    }

    [SerializeField] public GameObject GrowthChoice;


    [SerializeField] Color[] levelColour;
    public Color GetLvlColour(int lvl)
    {
        return levelColour[lvl-1];
    }

    private void Awake()
    {
        _instance = this;
    }
}
