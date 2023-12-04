using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public bool isTyping;
    public bool isInMenu;

    public List<Menu> menus;

    public void Start()
    {
        instance = this;
    }

    public void changeIsType()
    {
        isTyping = !isTyping;
    }
    public void ChangeMenu(Menu menu)
    {
        if(!isTyping){
            for (int i = 0; i < menus.Count; i++)
            {
                menus[i].Disable();
            }
            menu.Enable();
        }
    }
    public void ChangeMenu(string menu)
    {
        if(!isTyping){
            for (int i = 0; i < menus.Count; i++)
            {
                menus[i].Disable();
            }
            
            for (int i = 0; i < menus.Count; i++)
            {
                if(menus[i].menuName == menu){
                    menus[i].Enable();
                }
            }
        }
    }

    public void CloseMenus()
    {
        for (int i = 0; i < menus.Count; i++)
        {
            menus[i].Disable();
        }    
    }
    public void ChangeMenuWithPause(Menu menu)
    {
        if(!isTyping){
            ToolTipSystem.instance.tooltip.gameObject.SetActive(false);
            ToolTipSystem.instance.itemTooltip.gameObject.SetActive(false);
            ToolTipSystem.instance.spellToolTip.gameObject.SetActive(false);
            bool hadMenuOpen = false;
            bool storedMenuOpen = menu.open;
            for (int i = 0; i < menus.Count; i++)
            {
                if(menus[i].open){
                    hadMenuOpen = true;
                }
                menus[i].Disable();
            }

            if(!hadMenuOpen){
                if(!storedMenuOpen)
                {
                    menu.Enable();
                    GameManager.instance.Pause(menu.menuName);
                    isInMenu = true;
                }else{
                    menu.Disable();
                    GameManager.instance.Unpause();  
                    isInMenu = false;   
                }
            }else{
                isInMenu = false;   
                GameManager.instance.Unpause();    
            }
        }
    }
    
}
