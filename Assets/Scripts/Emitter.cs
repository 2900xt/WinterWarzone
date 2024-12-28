using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    [SerializeField]
    private Vector3 emissionColor;
    [SerializeField]
    private bool isRandom, isNightOnly;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        Vector3 cur = emissionColor;
        meshRenderer.material.color = new Color(cur.x, cur.y, cur.z, 1.0f);
    }
}