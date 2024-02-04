using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPartSingleton : MonoBehaviour
{
    private static PlantPartSingleton _instance;

    public static PlantPartSingleton Instance
    {
        get => _instance;
        private set => _instance = value;
    }

    private void Awake()
    {
        _instance = this;
        StemStats.Initialize(StemStorageBase, StemStorageScale, StemGrowthBase, StemGrowthScale, StemLevelCostBase, StemLevelCostScale,
                             StemBranchCostBase, StemBranchCostScale, StemLeafCostBase, StemLeafCostScale, StemBoostBase, StemBoostScale);
        LeafStats.Initialize(LeafStorageBase, LeafStorageScale, LeafGrowthBase, LeafGrowthScale, LeafLevelCostBase, LeafLevelCostScale);
        FlowerStats.Initialize(FlowerStorageBase, FlowerStorageScale, FlowerGrowthBase, FlowerGrowthScale, FlowerLevelCostBase,
                               FlowerLevelCostScale, FlowerFruitCostBase, FlowerFruitCostScale);
    }

    [SerializeField] GameObject[] plantParts;


    public GameObject getPart(int idx)
    {
        return plantParts[idx];
    }

    [SerializeField] float StemStorageBase;
    [SerializeField] float StemStorageScale;
    [SerializeField] float StemGrowthBase;
    [SerializeField] float StemGrowthScale;
    [SerializeField] float StemLevelCostBase;
    [SerializeField] float StemLevelCostScale;
    [SerializeField] float StemBranchCostBase;
    [SerializeField] float StemBranchCostScale;
    [SerializeField] float StemLeafCostBase;
    [SerializeField] float StemLeafCostScale;
    [SerializeField] float StemBoostBase;
    [SerializeField] float StemBoostScale;

    [SerializeField] float LeafStorageBase;
    [SerializeField] float LeafStorageScale;
    [SerializeField] float LeafGrowthBase;
    [SerializeField] float LeafGrowthScale;
    [SerializeField] float LeafLevelCostBase;
    [SerializeField] float LeafLevelCostScale;

    [SerializeField] float FlowerStorageBase;
    [SerializeField] float FlowerStorageScale;
    [SerializeField] float FlowerGrowthBase;
    [SerializeField] float FlowerGrowthScale;
    [SerializeField] float FlowerLevelCostBase;
    [SerializeField] float FlowerLevelCostScale;
    [SerializeField] float FlowerFruitCostBase;
    [SerializeField] float FlowerFruitCostScale;

    


}
