using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSingleton : MonoBehaviour
{
    private static SunSingleton _instance;

    public static SunSingleton Instance => _instance;

    [SerializeField] private float sun = 1;          public float GetSun() { return sun; }


    //WIND
    int timeSinceWind = 0;
    [SerializeField] StemScript treeBase;

    void MotivateWind()
    {
        if (timeSinceWind > 25)
        {
            if (Random.Range(0, 200) < timeSinceWind)
            {
                treeBase.SwayTime();
                timeSinceWind = 0;
            }
            else
            { timeSinceWind++; }
        }
        else
        { timeSinceWind++; }
    }


    //TICKER
    private bool _tick;
    public bool Tick
    {
        private set => _tick = value;
        get => _tick;
    }
    float timeSinceTick = 0;

    private void Awake()
    {
        _instance = this;
    }


    void Update()
    {
        if(timeSinceTick >= 1)
        {
            _tick = true;
            timeSinceTick = 0;
        }
        else
        {
            _tick = false;
        }
        timeSinceTick += Time.deltaTime;

        if (_tick)
        {
            MotivateWind();
        }
        
    }
}
