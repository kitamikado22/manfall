using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(360, 640, false);
    }
}
