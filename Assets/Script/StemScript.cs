using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.UI.Image;

public class StemScript : PlantPartFam, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] SpawnPointScript mySpawnPoint;

    static float CScale = 0.91f;

    Coroutine expansionRoutine;

    public void NewStem(Transform anchor, Quaternion angle) //to be callled by parent
    {
        StemScript newStem = Instantiate(PlantPartSingleton.Instance.getPart(0), anchor.position, angle, anchor).GetComponent<StemScript>();
        newStem.SetProportions(mySprite.size.x * CScale, mySprite.size.y * CScale, CScale);
        newStem.SetParent(this);
        childrenSprouts.Add(newStem);
    }

    public void NewLeaf(Transform anchor, Quaternion angle)
    {
        int orientation = Random.Range(0, 2);
        LeafScript newLeaf = Instantiate(PlantPartSingleton.Instance.getPart(1), anchor.position, angle, anchor).GetComponent<LeafScript>();
        newLeaf.SetParent(this);
        
        if (orientation > 0)
        {
            newLeaf.transform.localScale = new Vector3 (-1*newLeaf.transform.localScale.x, newLeaf.transform.localScale.y, newLeaf.transform.localScale.z);
            childrenSprouts.Add(null);
        }
        childrenSprouts.Add(newLeaf);
    }

    List<PlantPartFam> childrenSprouts = new List<PlantPartFam>();      // 0 or 1 index are taken buy left/right sprout (not standarised yet)

        public void CutOffChild(PlantPartFam child)
        {
            childrenSprouts.Remove(child);
        }


    bool proportionsSet = false;
    public void SetProportions(float x, float y, float budScale)
    {
        if (!proportionsSet)
        {
            //add generation
            width = x;
            height = y;
            proportionsSet = true;
            mySpawnPoint.ScaleSprout(budScale);
        }
        else
        {
            Debug.Log("Proportions cannot be initialised again");
        }
    }

    //EXPANSION
    [SerializeField] float width;
    [SerializeField] float height; public float GetHeight() { return height; }
    Color shade;

    IEnumerator Expander(bool colored)
    {
        float w = mySprite.size.x;
        float h = mySprite.size.y;
        Color c = mySprite.color;
        for (float i = 0; i < (100/SunSingleton.Instance.GetSun()); i += Time.deltaTime)
        {
            IncrementStem(new Vector2(width - w, height - h) * Time.deltaTime/ (100/SunSingleton.Instance.GetSun()));
            if (colored)
            {
                mySprite.color += (shade - c) * Time.deltaTime;
            }
            yield return null;
        }
        mySpawnPoint.ScaleSprout(1 / CScale);
        expansionRoutine = null;
    }

    void IncrementStem(Vector2 sizeChange)
    {
        mySprite.size += sizeChange;
        mySpawnPoint.transform.localPosition += 0.92f * sizeChange.y * Vector3.up;
    }

    public void ExpandWithChildren(float scale)
    {
        width *= scale;
        height *= scale;
        if (expansionRoutine != null)
        {
            StopCoroutine(expansionRoutine);
        }
        expansionRoutine = StartCoroutine(Expander(false));
        foreach (StemScript child in childrenSprouts)
        {
            child.ExpandWithChildren(scale);
        }
    }

    //GROWTH

    float splitCost;
    float leafCost;
    float boost;

    public override List<bool> GetAffordances()
    {
        List<bool> affordances = base.GetAffordances();
        if(childrenSprouts.Count > 0)
        {
            affordances.Add(false);     affordances.Add(false);
        }
        else
        {
            affordances.Add(growthStored + GrowthPoolSingleton.Instance.Growth > splitCost);
            affordances.Add(growthStored + GrowthPoolSingleton.Instance.Growth > leafCost);
        }
        return affordances;
    }

    protected override float CheapestBuy()
    {
        float cheap = splitCost;
        if (leafCost < cheap) { cheap = leafCost; }
        if (lvlUpCost < cheap) { cheap = lvlUpCost; }
        return cheap;
    }

    protected override void RevealBuy()
    {
        mySpawnPoint.ToggleSprout(true);
    }

    void ConcealBuy()
    {
        mySpawnPoint.ToggleSprout(false);
    }


    public override void BuyGeneral(int i)
    {
        switch (i)
        {
            case 0:
                BuyLevelUp();
                break;

            case 1:
                BuySplit();
                break;

            case 2:
                BuyLeaf();
                break;

            case 3:
                Prune();
                break;
        }
    }

    void BuySplit()
    {
        if (TryPayGrowth(splitCost))
        {
            NewStem(mySpawnPoint.transform, transform.rotation * Quaternion.Euler(0, 0, 25 + 20 * (Random.value) - 10));
            NewStem(mySpawnPoint.transform, transform.rotation * Quaternion.Euler(0, 0, -25 + 20 * (Random.value) - 10));
            if (CheapestBuy() > growthStored + GrowthPoolSingleton.Instance.Growth)
            {
                ConcealBuy();
            }
        }
    }

    void BuyLeaf()
    {
        if (TryPayGrowth(leafCost))
        {
            NewLeaf(mySpawnPoint.transform, transform.rotation * Quaternion.Euler(0, 0, -35 + 10 * (Random.value) - 5));
            if (CheapestBuy() > growthStored + GrowthPoolSingleton.Instance.Growth)
            {
                ConcealBuy();
            }
        }
    }

    void BuyLevelUp()
    {
        if (TryPayGrowth(lvlUpCost))
        {
            lvl++;
            lvlUpCost = StemStats.LvlCost.AdvanceStat(lvl);
            growthRate = StemStats.Growth.AdvanceStat(lvl);
            maxStorage = StemStats.Storage.AdvanceStat(lvl);
            splitCost = StemStats.BranchCost.AdvanceStat(lvl);
            leafCost = StemStats.LeafCost.AdvanceStat(lvl);
            boost = StemStats.Boost.AdvanceStat(lvl);
            // offer boost
            width *= 1 / CScale;
            height *= 1 / CScale;
            shade = LibrarySingleton.Instance.GetLvlColour(lvl);
            if (expansionRoutine != null)
            {
                StopCoroutine(expansionRoutine);
            }
            expansionRoutine = StartCoroutine(Expander(true));
            /*foreach(StemScript child in childrenSprouts)
            {
                child.ExpandWithChildren(1/CScale);
            }*/
        }
    }

    void Prune()
    {
        GrowthPoolSingleton.Instance.Growth += ScoreByPrunning();
        parentStem.CutOffChild(this);
        Destroy(this.gameObject);
    }

    public override float ScoreByPrunning()
    {
        float growthSum = base.ScoreByPrunning();
        foreach (PlantPartFam child in childrenSprouts)
        {
            growthSum += child.ScoreByPrunning();
        }
        return growthSum;
    }
    

//IPOINTER
bool highlighted = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Highlight();
        highlighted = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Lowlight();
        highlighted = false;
    }

    public void OnPointerClick(PointerEventData eventData) 
    {
        if (highlighted)
        {
            ChoiceUIScript newUI = Instantiate(LibrarySingleton.Instance.GrowthChoice, eventData.pointerCurrentRaycast.worldPosition, Quaternion.identity).GetComponent<ChoiceUIScript>();
            newUI.SetOrigin(this);

            GrowButtonScript[] buttons = newUI.GetComponentsInChildren<GrowButtonScript>();
            foreach (GrowButtonScript button in buttons)
            {
                button.SetOrigin(this);
            }
        }
    }

    //Wind
    [ContextMenu("Sway")]
    public void SwayTime()
    {
        float scale = Random.Range(7, 15);
        if (Random.value > 0.5) { scale *= -1; }
        StartCoroutine(Sway(scale));
        foreach(PlantPartFam p in childrenSprouts)
        {
            p.SwayTime(scale);
        }
    }
    public override void SwayTime(float scale)
    {
        StartCoroutine(Sway(scale));
        foreach (PlantPartFam p in childrenSprouts)
        {
            p.SwayTime(scale);
        }
    }

    IEnumerator Sway(float x)
    {
        float scale = Mathf.Abs(x);
        float sign = Mathf.Sign(x);
        for (float i = scale; i>=0; i -= Time.deltaTime)
        {
            transform.rotation *= Quaternion.Euler(0, 0, sign * 0.5f * Mathf.Cos(Mathf.PI*(i/scale + 1))*Time.deltaTime) ;
            yield return null;
        }

        //rebound
        scale *= 0.4f;
        for (float i = scale; i >= 0; i -= Time.deltaTime)
        {
            transform.rotation *= Quaternion.Euler(0, 0, sign * -0.15f*Mathf.Cos(Mathf.PI * (i / scale + 1)) * Time.deltaTime);
            yield return null;
        }
    }

    private void Awake()
    {
        
    }

    void Start()
    {
        lvl = 1;
        lvlUpCost = StemStats.LvlCost.AdvanceStat(lvl);
        growthRate = StemStats.Growth.AdvanceStat(lvl);
        maxStorage = StemStats.Storage.AdvanceStat(lvl);
        splitCost = StemStats.BranchCost.AdvanceStat(lvl);
        leafCost = StemStats.LeafCost.AdvanceStat(lvl);
        boost = StemStats.Boost.AdvanceStat(lvl);

        if (expansionRoutine != null)
        {
            StopCoroutine(expansionRoutine);
        }
        expansionRoutine = StartCoroutine(Expander(false));
    }
}
