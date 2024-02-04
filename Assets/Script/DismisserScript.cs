using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DismisserScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool focused = false;
    private void Update()
    {
        if (Input.anyKeyDown && !focused)
        {
            DestroyGroup();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        focused = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        focused = false;
    }

    public void DestroyGroup()
    {
        Destroy(this.gameObject);
    }
}
