using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Radio : Item {
    public bool Broken;
    public AudioSource Music;

    public override void Awake()
    {
        base.Awake();
        Music.Play();
        Music.Pause();
    }

    public override void Update()
    {
        base.Update();
        if (Broken)
            Music.Stop();
    }

    public override void Interact()
    {
        base.Interact();
        if (Broken)
            return;
        Music.UnPause();
    }

    public override void ItemReset()
    {
        base.ItemReset();
        if (Broken)
            return;
        Music.Pause();
    }
}
