using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnPointScript : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] SpriteRenderer sproutSprite;
    [SerializeField] GameObject growChoicePref;

    StemScript origin;

    private void Awake()
    {
        origin = GetComponentInParent<StemScript>();
        sproutSprite = GetComponentInChildren<SpriteRenderer>(true);
    }

    void Start()
    {

    }

    public void Highlight()
    {
        sproutSprite.color += Color.black * 0.3f;
    }

    public void Lowlight()
    {
        sproutSprite.color -= Color.black * 0.3f;
    }

    public void MoveSprout(Vector3 distance)
    {
        sproutSprite.transform.position += distance;
    }

    public void ScaleSprout(float scale)
    {
        sproutSprite.transform.localScale *= scale;
    }

    public void ToggleSprout(bool state)
    {
        sproutSprite.gameObject.SetActive(state);
    }
  //  public void OnPointerEnter(PointerEventData eventData)
  //  {
 //       Highlight();
  //  }

  //  public void OnPointerExit(PointerEventData eventData)
 //   {
 //       Lowlight();
 //   }

  //  public void OnPointerClick(PointerEventData eventData)
  //  {
  //      GrowButtonScript[] buttons = Instantiate(growChoicePref, transform.position, Quaternion.identity).GetComponentsInChildren<GrowButtonScript>();
//
    //    foreach(GrowButtonScript button in buttons)
    //    {
    //        button.SetOrigin(origin);
    //    }
    //}
}