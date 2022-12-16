using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteChanger : MonoBehaviour
{
    private SpriteRenderer mySR;

    public delegate void SpriteChangeDelegate(Sprite sprite);
    public static SpriteChangeDelegate SpriteChangeEvent;

    public delegate void FlashRedDelegate();
    public static FlashRedDelegate FlashRedEvent;

    private void Start()
    {
        SpriteChangeEvent += SpriteChange;
        FlashRedEvent += SpriteFlashRed;
    }
    private void Awake()
    {
        mySR = GetComponent<SpriteRenderer>();
    }

    private void SpriteChange(Sprite sprite)
    {
        mySR.sprite = sprite;
    }

    private void SpriteFlashRed()
    {
        StartCoroutine(FlashRedOnDamage());
    }

    private IEnumerator FlashRedOnDamage() {
        mySR.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(0.15f);
        mySR.color = new Color(1, 1, 1, 1);
    } 

    private void OnDestroy()
    {
        SpriteChangeEvent -= SpriteChange;
        FlashRedEvent -= SpriteFlashRed;
    }
}