using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public string menuName;
    public bool open;
    public void Disable()
    {
        open = false;
        menu.SetActive(false);
    }
    public void Enable()
    {
        open = true;
        menu.SetActive(true);
    }
}
