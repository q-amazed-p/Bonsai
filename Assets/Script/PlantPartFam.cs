using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantPartFam : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected SpriteRenderer mySprite;
    public Collider2D _myCollider;

    [SerializeField] SpriteRenderer outline;

    bool mature = false;                        public void Maturate() { mature = true; }

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


    public void ToggleShine(bool state)
    {
        _myCollider.enabled = state;
        outline.enabled = state;
    }

    public void ToggleHighlight(bool state)
    {
        outline.color = new Color(outline.color.r, outline.color.g, outline.color.b, state? 1 : 0.4f);
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

    protected virtual void RevealBuy() 
    {
        openForBusiness = true;
    }
    protected virtual void RevokeBuy()
    {
        openForBusiness = false;
    }

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
