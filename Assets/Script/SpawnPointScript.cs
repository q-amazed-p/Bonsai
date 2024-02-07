using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnPointScript : MonoBehaviour
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
}