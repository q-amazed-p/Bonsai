using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeafScript : PlantPartFam, IPointerClickHandler
{
    public static LeafScript NewLeaf(Transform anchor, StemScript parentStem, bool rightTilt, float initialBoost)
    {
        float rawAngle = -15 + 10 * (Random.value) - 5;
        LeafScript newLeaf = Instantiate(PlantPartSingleton.Instance.getPart(1), anchor.position, Quaternion.identity, anchor).GetComponent<LeafScript>();
        newLeaf.SetParent(parentStem);
        newLeaf.SetGeneration();
        newLeaf.InitializeInheritedBoost(initialBoost);

        if (rightTilt)
        {
            //newLeaf.transform.localScale = new Vector3 (-1*newLeaf.transform.localScale.x, newLeaf.transform.localScale.y, newLeaf.transform.localScale.z);
            newLeaf.transform.localRotation = Quaternion.Euler(0, 0, rawAngle);
        }
        else
        {
            newLeaf.transform.localRotation = Quaternion.Euler(0, 180, rawAngle);
        }
        return newLeaf;
    }

    [SerializeField] GameObject myParticles;

    /*protected bool BuyLevelUp()
    {
        if (TryPayGrowth(lvlUpCost))
        {
            lvl++;
            lvlUpCost = StemStats.LvlCost.AdvanceStat(lvl);
            growthRate = StemStats.Growth.AdvanceStat(lvl);
            maxStorage = StemStats.Storage.AdvanceStat(lvl);
        }
    }*/

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

    public override void BuyGeneral(int i)
    {
        switch (i)
        {
            case 0:
                BuyLevelUp();
                break;

            case 3:         //keep as the last option
                Prune();
                break;
        }
        if (CheapestBuy() > growthStored + GrowthPoolSingleton.Instance.Growth)
        {
            RevokeBuy();
            ToggleShine(false);
        }
    }

    protected override void BuyLevelUp()
    {
        if(TryPayGrowth(lvlUpCost))
        {
            lvl++;
            lvlUpCost = LeafStats.LvlCost.AdvanceStat(lvl, generation, LibrarySingleton.Instance.GenerationScalingForCosts);
            growthRate = LeafStats.Growth.AdvanceStat(lvl);
            maxStorage = LeafStats.Storage.AdvanceStat(lvl);
        }
    }


    //IPOINTER
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if (storageFull) 
            {
                GrowthPoolSingleton.Instance.Growth += growthStored;
                growthStored = 0;
                myParticles.SetActive(false);
                if(!outline.enabled) _myCollider.enabled = false;
                storageFull = false;
            }

        }
        else if (highlighted && eventData.button == PointerEventData.InputButton.Left)
        {
            ChoiceUIScript newUI = Instantiate(LibrarySingleton.Instance.GrowthChoice, eventData.pointerCurrentRaycast.worldPosition, Quaternion.identity).GetComponent<ChoiceUIScript>();
            newUI.SetOrigin(this);
        }
    }

    //MONOBEHAVIOR
    private void Awake()
    {


    }

    protected override void Start()
    {
        base.Start();
        lvl = 1;
        lvlUpCost = StemStats.LvlCost.AdvanceStat(lvl, generation, LibrarySingleton.Instance.GenerationScalingForCosts);
        growthRate = StemStats.Growth.AdvanceStat(lvl);
        maxStorage = StemStats.Storage.AdvanceStat(lvl);
    }

    
}
