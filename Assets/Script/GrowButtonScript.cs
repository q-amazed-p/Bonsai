using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrowButtonScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int buttonType;
    [SerializeField] Collider2D myCollider;
    [SerializeField] PlantPartFam origin;           public void SetOrigin(PlantPartFam o) { origin = o; }

    SpriteRenderer[] mySprites;

    private void Awake()
    {
        mySprites = GetComponentsInChildren<SpriteRenderer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        origin.BuyGeneral(buttonType);
        GetComponentInParent<DismisserScript>().DestroyGroup();
    }

    public void SetVisible(bool state)
    {
        foreach(SpriteRenderer sr in mySprites)
        {
            sr.enabled = state;
            myCollider.enabled = true;
        }
    }
}
