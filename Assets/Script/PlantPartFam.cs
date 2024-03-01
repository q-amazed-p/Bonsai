using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantPartFam : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected SpriteRenderer mySprite;
    protected Collider2D _myCollider;

    [SerializeField] protected SpriteRenderer outline;

    bool mature = false;                        public void Maturate() => mature = true;

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

    protected int generation = 0;
    public int GetGeneraton() { return generation; }
    bool generationSet = false;
    public void SetGeneration()
    {
        if (!generationSet) 
        { 
            generation = parentStem.GetGeneraton() + 1;
            generationSet = true;
        }
    }

    protected int lvl = 1;
    protected float growthRate;
    protected float lvlUpCost;
    protected float maxStorage;
    [SerializeField]protected float growthStored;

    protected float inheritedBoost=1;        //is modified on level up and on new plantPart creation
    protected void InitializeBoost(float InitialBoost) => inheritedBoost = InitialBoost; 
    public void ReadjustBoost(float prev, float next) => inheritedBoost = inheritedBoost * next / prev; 

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
        else return false;
    }

    protected virtual bool GainGrowrth(float gain)
    {
        bool capacityAvailable = growthStored + inheritedBoost * gain < maxStorage;
        if (capacityAvailable)
        {
            growthStored += inheritedBoost*gain;
        }

        return capacityAvailable;
    }

    protected virtual float CheapestBuy() => lvlUpCost;

    protected virtual void RevealBuy() 
    {
        openForBusiness = true;
        ToggleShine(true);
    }
    protected virtual void RevokeBuy()
    {
        ToggleShine(false);
        openForBusiness = false;
    }

    public virtual void BuyGeneral(int i) { }

    public virtual List<bool> GetAffordances() 
    {
        return new List<bool> { growthStored+GrowthPoolSingleton.Instance.Growth > lvlUpCost };
    }

    bool openForBusiness = false;

    protected virtual bool BuyLevelUp()
    {
        bool paySuccessful = TryPayGrowth(lvlUpCost);
        if (paySuccessful)
        {
            lvl++;
            lvlUpCost = StemStats.LvlCost.AdvanceStat(lvl, generation, LibrarySingleton.Instance.GenerationScalingForCosts);
            growthRate = StemStats.Growth.AdvanceStat(lvl);
            maxStorage = StemStats.Storage.AdvanceStat(lvl);
        }
        return paySuccessful;
    }

    protected void Prune()
    {
        GrowthPoolSingleton.Instance.Growth += ScoreByPrunning();
        parentStem.CutOffChild(this);                                       //exploit on base stem
        Destroy(gameObject);
    }

    public virtual float ScoreByPrunning()           //Stems override to sum up from children
    {
        return growthStored;
    }

    //IPOINTER
    protected bool highlighted = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        ToggleHighlight(true);
        highlighted = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToggleHighlight(false);
        highlighted = false;
    }

    public void ToggleHighlight(bool state)
    {
        outline.color = new Color(outline.color.r, outline.color.g, outline.color.b, state ? 1 : 0.4f);
    }

    public void ToggleShine(bool state)
    {
        _myCollider.enabled = state;
        outline.enabled = state;
    }

    //WIND
    public virtual void SwayTime(float x) { }


    private void Awake()
    {
        growthStored = 0;
    }

    //MONOBEHAVIOR
    protected virtual void Start()
    {
        _myCollider = GetComponent<Collider2D>();
        Invoke("Maturate", 100 / SunSingleton.Instance.GetSun());
    }

    void Update()
    {
        if (SunSingleton.Instance.Tick && mature)
        {
            GainGrowrth(SunSingleton.Instance.GetSun() * growthRate);
            if (!openForBusiness)
            {
                if(growthStored + GrowthPoolSingleton.Instance.Growth >= CheapestBuy())
                {
                    RevealBuy();
                }
            }
        }
        
    }
}
