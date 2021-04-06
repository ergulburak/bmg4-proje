using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BloodSplitter : MonoBehaviour
{
    public enum SplatLocation
    {
        Wall,
        Background,
    }

    public Color backgroundTint;
    public float minSizeMod = 0.8f;
    public float maxSizeMod = 1.5f;

    public Sprite[] splats;
    public Color[] colors;

    private SplatLocation m_SplatLocation;
    private SpriteRenderer m_SpriteRenderer;

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(SplatLocation splatLocation)
    {
        this.m_SplatLocation = splatLocation;
        SetSprite();
        SetSize();
        SetRotation();
        SetColor();
        SetLocationProperties();
    }

    private void SetSprite()
    {
        int randomIndex = Random.Range(0, splats.Length);
        m_SpriteRenderer.sprite = splats[randomIndex];
    }

    private void SetColor()
    {
        int randomIndex = Random.Range(0, splats.Length);
        m_SpriteRenderer.color = colors[randomIndex];
    }

    private void SetSize()
    {
        float sizeMod = Random.Range(minSizeMod, maxSizeMod);
        transform.localScale *= sizeMod;
    }

    private void SetRotation()
    {
        float randomRotation = Random.Range(-360f, 360f);
        transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);
    }

    private void SetLocationProperties()
    {
        switch (m_SplatLocation)
        {
            case SplatLocation.Background:
                m_SpriteRenderer.maskInteraction = SpriteMaskInteraction.None;
                m_SpriteRenderer.color = backgroundTint;
                m_SpriteRenderer.sortingOrder = -4;
                break;
            case SplatLocation.Wall:
                m_SpriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                m_SpriteRenderer.sortingOrder = 3;
                break;
        }
    }
}