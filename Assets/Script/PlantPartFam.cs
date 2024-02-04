using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantPartFam : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected SpriteRenderer mySprite;
    [SerializeField] SpriteRenderer outline;

    protected StemScript parentStem;
    protected bool parentSet = false;
    public void SetParent(StemScript par)
    {
        if (parentSet == false)
        {
            parentStem = par;
            parentSet = true;
        }
        else
        {
            Debug.Log("Parents cannot be initialised again");
        }
    }



    protected void Highlight()
    {
        outline.enabled = true;
    }

    protected void Lowlight()
    {
        outline.enabled = false;
    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    ShowOutline();
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    HideOutline();
    //}

    //obsolete for stems
    //public void GrowOn(int growthType, Vector3 anchor)
    //{
    //    Instantiate(PlantPartSingleton.Instance.getPart(growthType), anchor, Quaternion.identity, transform);
    //}

    protected int lvl = 1;
    protected float growthRate;
    protected float lvlUpCost;
    protected float maxStorage;
    [SerializeField]protected float growthStored;

    protected bool ChangeGrowthStored(float delta)
    {
        if (growthStored + delta < maxStorage && growthStored + delta > 0)
        {
            growthStored += delta;
            return true;
        }
        else { return false; }
    }

    protected bool TryPayGrowth(float price)
    {
        if(growthStored + GrowthPoolSingleton.Instance.Growth > price)
        {
            if(growthStored > price)
            {
                growthStored -= price;
            }
            else 
            {
                GrowthPoolSingleton.Instance.Growth -= price - growthStored;
                growthStored = 0;
            }
            return true;
        }
        else { return false; }
    }

    protected void GainGrowrth(float gain)
    {
        if(growthStored + gain < maxStorage)
        {
            growthStored += gain;
        }
        else
        {
            GrowthPoolSingleton.Instance.Growth += 0.75*(gain - maxStorage + growthStored);
            growthStored = maxStorage;
        }
    }

    protected virtual float CheapestBuy() { return lvlUpCost; }

    protected virtual void RevealBuy() { }

    public virtual void BuyGeneral(int i) { }

    public virtual List<bool> GetAffordances() 
    {
        return new List<bool> { growthStored+GrowthPoolSingleton.Instance.Growth > lvlUpCost };
    }

    bool openForBusiness = false;


    public virtual float ScoreByPrunning()           //Stems override to sum up from children
    {
        return growthStored;
    }

    public virtual void SwayTime(float x) { }


    private void Awake()
    {
        growthStored = 0;
    }

    void Update()
    {
        if (SunSingleton.Instance.Tick)
        {
            GainGrowrth(SunSingleton.Instance.GetSun() * growthRate);
            if (!openForBusiness)
            {
                if(growthStored+ GrowthPoolSingleton.Instance.Growth > CheapestBuy())
                {
                    RevealBuy();
                    openForBusiness = true;
                }
            }
        }
        
    }
}
