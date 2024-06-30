using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    public void Openurl(string url)
    {
        Application.OpenURL(url);
    }
}
