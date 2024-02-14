using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeafScript : PlantPartFam, IPointerClickHandler
{
    static float baseGrowthRate = 1;
    static float baseLvlUpCost = 50;
    static float baseMaxStorage = 100;

    [SerializeField] GameObject myParticles;

    void BuyLevelUp()
    {
        if (TryPayGrowth(lvlUpCost))
        {
            lvl++;
            lvlUpCost = StemStats.LvlCost.AdvanceStat(lvl);
            growthRate = StemStats.Growth.AdvanceStat(lvl);
            maxStorage = StemStats.Storage.AdvanceStat(lvl);
        }
    }

    bool storageFull = false;
    protected override bool GainGrowrth(float gain)
    {
        bool capacityAvailable = base.GainGrowrth(gain);
        if (!capacityAvailable && !storageFull)
        {
            storageFull = true;
            myParticles.SetActive(true);
            _myCollider.enabled = true;
        }
        return capacityAvailable;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if (storageFull) 
            {
                GrowthPoolSingleton.Instance.Growth += growthStored;
                growthStored = 0;
                myParticles.SetActive(false);
                _myCollider.enabled = false;
                storageFull = false;
            }

        }
    }

    private void Awake()
    {


    }

    protected override void Start()
    {
        base.Start();
        lvl = 1;
        lvlUpCost = StemStats.LvlCost.AdvanceStat(lvl);
        growthRate = StemStats.Growth.AdvanceStat(lvl);
        maxStorage = StemStats.Storage.AdvanceStat(lvl);
    }

    
}
