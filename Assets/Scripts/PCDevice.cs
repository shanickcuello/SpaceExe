using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCDevice : Device
{
    Light _light;

    protected override void Start()
    {
        base.Start();

        _light = transform.parent.GetComponentInChildren<Light>();
    }

    public override void OnEntityEnter(int selection)
    {
        base.OnEntityEnter(selection);

        if (_light != null)
            _light.enabled = false;
    }

    public override void OnEntityExit()
    {
        base.OnEntityExit();

        if (_light != null)
            _light.enabled = true;
    }

    public override void HackDevice()
    {
        base.HackDevice();
    }
}
