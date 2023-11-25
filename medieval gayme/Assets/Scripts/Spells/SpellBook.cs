using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using System.IO;
public class SpellBook : MonoBehaviour
{
    public static SpellBook instance;

    [BoxGroup("Saving")]
    public string spellSavePath;

    [BoxGroup("Saving")]
    public SpellSaveData spellSaveData = new SpellSaveData();
    SpellEffect saveIconSpell;

    [BoxGroup("References")]
    public MenuManager menuManager;
    [BoxGroup("References")]
    public Menu spellBookMenu;
    [BoxGroup("References")]
    public GameObject spellBookUI;
    [BoxGroup("References/Player")]    
    public PlayerHealthManager playerHealthManager;
    [BoxGroup("References/Player")]    
    public PlayerMovement playerMovement;
    [BoxGroup("References/Player")]    
    public PlayerEquipmentManager playerEquipmentManager;

#region Spells

    [BoxGroup("Spells")]
    public SpellDatabase spellDatabase;
    
    [BoxGroup("Spells")]
    public List<Spell> spells;
    [BoxGroup("Spells")]
    public List<SpellEffect> unlockedEffects;
    [BoxGroup("Spells")]
    public List<SpellSlot> unlockedEffectSlots;
#endregion

#region Creation UI
    [BoxGroup("Spells Creation UI")]
    public GameObject spellSlotPrefab;
    [BoxGroup("Spells Creation UI")]
    public GameObject createMenuButton;

    [BoxGroup("Spells Creation UI/Grids")]
    public Transform spellStuffGrid;
    [BoxGroup("Spells Creation UI/Grids")]
    public Transform spellGrid;
    [BoxGroup("Spells Creation UI/Grids")]
    public Transform spellTypeGrid;
    [BoxGroup("Spells Creation UI/Grids")]
    public Transform spellModifierGrid;
    [BoxGroup("Spells Creation UI/Grids")]
    public Transform spellEffectsGrid;

    [BoxGroup("Spells Creation UI/Create Menu")]
    public GameObject createMenu;
    [BoxGroup("Spells Creation UI/Create Menu")]
    public SpellSlot iconSlot;
    [BoxGroup("Spells Creation UI/Create Menu")]
    public TMP_InputField spellNameField;
    [BoxGroup("Spells Creation UI/Create Menu")]
    public bool inCreateMenu;
#endregion

#region Spell Creation
    [BoxGroup("Spell Creation")]
    public bool canCreateSpell = true;
    [BoxGroup("Spell Creation")]
    public List<SpellEffect> selectedSpellEffects;
    [BoxGroup("Spell Creation")]
    public List<SpellSlot> spellSlots;
#endregion
    
    [BoxGroup("Spell Usage")]
    public SpellUsage spellUsage;

    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        spellBookMenu.menu = spellBookUI;

        spellSavePath = Application.persistentDataPath + "/" + MainMenuManager.instance.playerName + 
            "_SpellSaveData.json";
            
        if(File.Exists(spellSavePath)){
            LoadSpells();
        }
        spellBookMenu.Enable();    
        UpdateSpellBookUI();
        spellBookMenu.Disable();
    }

    public void Update()
    {
        if(InputManager.instance.spellBook){
            InputManager.instance.spellBook = false;

            menuManager.ChangeMenuWithPause(spellBookMenu);
        }    
    }

    public void UnlockSpellEffect(SpellEffect effect)
    {
        unlockedEffects.Add(effect);

        UpdateSpellBookUI();
    }

    public void UpdateSpellBookUI()
    {
        for (int i = 0; i < unlockedEffectSlots.Count; i++)
        {
            Destroy(unlockedEffectSlots[i].gameObject);
        }
        unlockedEffectSlots.Clear();

        for (int i = 0; i < unlockedEffects.Count; i++)
        {
            GameObject newSpellSlot = Instantiate(spellSlotPrefab);
            switch (unlockedEffects[i].spellType)
            {
                case ESpellType.spellType:
                    newSpellSlot.transform.SetParent(spellTypeGrid);
                break;
                case ESpellType.SpellModifier:
                    newSpellSlot.transform.SetParent(spellModifierGrid);
                break;
                case ESpellType.spellEffect:
                    newSpellSlot.transform.SetParent(spellEffectsGrid);
                break;
            }
            newSpellSlot.GetComponent<SpellSlot>().effect = unlockedEffects[i];
            newSpellSlot.transform.localScale = Vector3.one;
            unlockedEffectSlots.Add(newSpellSlot.GetComponent<SpellSlot>());
        }
    }

    public void OpenSpellCreateMenu()
    {
        if(selectedSpellEffects.Count >= 2 && selectedSpellEffects[0].spellType == ESpellType.spellType)
        {
            createMenu.SetActive(false);
            inCreateMenu = true;

            for (int i = 0; i < spellSlots.Count; i++)
            {
                spellSlots[i].gameObject.SetActive(false);
            }

            createMenu.SetActive(true);
            createMenuButton.SetActive(false);
            canCreateSpell = false;
        }else{
            if(selectedSpellEffects[0].spellType != ESpellType.spellType){
                print("You must have a spell type in the first slot");
            }
            createMenu.SetActive(true);
            createMenuButton.SetActive(false);
            canCreateSpell = true;    
        }
    }

    public void CreateSpell()
    {
        if(selectedSpellEffects.Count >= 2)
        {
            if(iconSlot.effect){
                if(spellNameField){     
                    float totalManaCost = 0;

                    for (int i = 0; i < selectedSpellEffects.Count; i++)
                    {
                        totalManaCost += selectedSpellEffects[i].manaCost;
                    }

                    Spell createdSpell = new Spell(selectedSpellEffects, spellNameField.text, iconSlot.effect, totalManaCost);
                    spells.Add(createdSpell);
                    GameObject newSpellSlot = Instantiate(spellSlotPrefab);
                    newSpellSlot.transform.SetParent(spellGrid);
                    newSpellSlot.transform.localScale = Vector3.one;   

                    newSpellSlot.GetComponent<SpellSlot>().slotSpell.iconEffect = iconSlot.effect;
                    newSpellSlot.GetComponent<SpellSlot>().isSpell = true;
                    newSpellSlot.GetComponent<SpellSlot>().slotSpell = createdSpell;

                    spellNameField.text = "";
                    iconSlot.effect = null;


                    for (int i = 0; i < spellSlots.Count; i++)
                    {
                        spellSlots[i].gameObject.SetActive(true);
                        spellSlots[i].effect = null;
                    }

                    //selectedSpellEffects.Clear();

                    createMenu.SetActive(false);
                    createMenuButton.SetActive(true);
                    canCreateSpell = true;    
                    inCreateMenu = false;

                    SaveManager.instance.Save();

                    UpdateSpellBookUI();
                }
            }
        }else{
            print("One or less spell effect found");
        }
    }

    public void AddEffect(SpellEffect effect)
    {
        print("Add Effect Called");
        if(canCreateSpell){
            if(selectedSpellEffects.Count < 5){
                selectedSpellEffects.Add(effect);
            }
            spellSlots[selectedSpellEffects.Count - 1].effect = effect;
        }else{
            print("Cannot add effect");
        }
    }

    public void RemoveEffect(SpellEffect effects, SpellSlot slotFrom)
    {

        for (int i = 0; i < spellSlots.Count; i++)
        {
            if(spellSlots[i] == slotFrom){
                spellSlots[i].effect = null;
                selectedSpellEffects.RemoveAt(i);
                for (int s = 0; s < selectedSpellEffects.Count; s++)
                {
                    spellSlots[s].effect = selectedSpellEffects[s];

                    if(selectedSpellEffects.Count < spellSlots.Count){
                        int remainingSlots = spellSlots.Count - selectedSpellEffects.Count;

                        for (int a = 0; a < remainingSlots; a++)
                        {
                            spellSlots[a + selectedSpellEffects.Count].effect = null;
                        }
                    }
                }
            }
        }
    }

    [Button]
    public void SaveSpells()
    {
        for (int i = 0; i < spells.Count; i++)
        {
            List<string> effectsToString = new List<string>();
            for (int s = 0; s < spells[i].effects.Count; s++)
            {
                effectsToString.Add(spells[i].effects[s].effectName);
            }
            SaveSpell newSaveSpell = new SaveSpell(effectsToString, spells[i].spellName, spells[i].iconEffect.effectName, spells[i].manaCost);
            spellSaveData.savedSpells.Add(newSaveSpell);
        }

        for (int i = 0; i < unlockedEffects.Count; i++)
        {
            spellSaveData.savedEffects.Add(new SaveEffect(unlockedEffects[i].effectName));
        }
        string jsonString = JsonUtility.ToJson(spellSaveData, true);
        File.WriteAllText(spellSavePath, jsonString);

    }

    [Button]
    public void LoadSpells()
    {
        string fileContents = File.ReadAllText(spellSavePath);

        SpellSaveData newSaveData = JsonUtility.FromJson<SpellSaveData>(fileContents);

        for (int i = 0; i < newSaveData.savedSpells.Count; i++)
        {
            List<SpellEffect> effectList = new List<SpellEffect>();
            saveIconSpell = null;

            //Goes through every effect in the data base
            for (int e = 0; e < spellDatabase.effectsInDatabase.Count; e++)
            {
                //Goes through allthe effects saved in the current spell
                for (int se = 0; se < newSaveData.savedSpells[i].effects.Count; se++)
                {
                    //checks if the effects are the same
                    if(spellDatabase.effectsInDatabase[e].effectName == newSaveData.savedSpells[i].effects[se]){
                        //if they are then it adds it to a the list
                        effectList.Add(spellDatabase.effectsInDatabase[e]);
                    }   

                    //checks if the effect from the database and the icon effect is the same  
                    if(spellDatabase.effectsInDatabase[e].effectName == newSaveData.savedSpells[i].iconEffect){
                        //if it is then it sets the icon
                        saveIconSpell = spellDatabase.effectsInDatabase[e];
                    }          
                }
            }

            effectList.Reverse();

            Spell newSaveSpell = new Spell(effectList, newSaveData.savedSpells[i].spellName, saveIconSpell, newSaveData.savedSpells[i].manaCost);

            spells.Add(newSaveSpell);
            GameObject newSpellSlot = Instantiate(spellSlotPrefab);
            newSpellSlot.transform.SetParent(spellGrid);
            newSpellSlot.transform.localScale = Vector3.one;   

            
            newSpellSlot.GetComponent<SpellSlot>().slotSpell.iconEffect = newSaveSpell.iconEffect;
            newSpellSlot.GetComponent<SpellSlot>().isSpell = true;
            newSpellSlot.GetComponent<SpellSlot>().slotSpell = newSaveSpell;   
        }
    }
}

[System.Serializable]
public class SpellUsage
{
    [BoxGroup("Spell Usage")]
#region Spell Effects
    [BoxGroup("Spell Usage/Effects")]
    public bool isFrenzy;
    [BoxGroup("Spell Usage/Effects")]
    public bool isCalming;
    [BoxGroup("Spell Usage/Effects")]
    public bool isDamage;
    [BoxGroup("Spell Usage/Effects")]
    public bool isHealing;
    [BoxGroup("Spell Usage/Effects")]
    public bool isJump;
    [BoxGroup("Spell Usage/Effects")]
    public bool isBounce;
    [BoxGroup("Spell Usage/Effects")]
    public bool isLeaping;
    [BoxGroup("Spell Usage/Effects")]
    public bool isQuickening;
    [BoxGroup("Spell Usage/Effects")]
    public bool isSlowing;
    [BoxGroup("Spell Usage/Effects")]
    public bool isStrengthening;
    [BoxGroup("Spell Usage/Effects")]
    public bool isWeakening;
#endregion

#region Spell Modifiers
    [BoxGroup("Spell Usage/Modifiers")]
    public int acceleratingModifier = 1;
    [BoxGroup("Spell Usage/Modifiers")]
    public int delayModifier = 1;
    [BoxGroup("Spell Usage/Modifiers")]
    public int aoeModifier = 1;
    [BoxGroup("Spell Usage/Modifiers")]
    public int amplifyModifier = 1;
    [BoxGroup("Spell Usage/Modifiers")]
    public int lengthenModifier = 1;
    [BoxGroup("Spell Usage/Modifiers")]
    public int peirceModifier = 1;

#endregion
}