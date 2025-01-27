﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : FoodSource
{
    [HideInInspector]
    public GameObject _berries;
    public float _regrowMax = 30;
    public float _regrowMin = 0;

    public AudioSource Eating;

    private float _regrowTime;

    private void Start()
    {
        _berries = transform.GetChild(0).gameObject; //Berries :D
    }

    public override void OnEat()
    {
        base.OnEat();
        _berries.SetActive(false);
        Eating.Play(0);
        _regrowTime = Time.time + Random.Range(_regrowMin, _regrowMax); //Figure out when the berries will grow back
        Debug.Log(gameObject.name + " Will be fully grown at " + _regrowTime);
    }

    private void Update()
    {
        if (_berries.activeSelf == false && Time.time > _regrowTime && _servesRemaining == 0)
        {
            _berries.SetActive(true); //Show the berries again
            _servesRemaining = _foodStats._serves;
        }
    }
}
