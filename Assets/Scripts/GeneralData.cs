using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


//------------------------------------------------------------------------
//
//  Name:   GeneralData.cs
//
//  Desc:   Manage all data and load saves
//
//  Attachment :    This class is called by several other classes whenever we need data
//
//  Creation :  06/02/2018
//
//  Last modification : Aub Ah - 06/02/2018
//
//------------------------------------------------------------------------


public class GeneralData : MonoBehaviour
{

    /*-------------------*/
    /* STRUCTURES / Enum */
    /*-------------------*/

    // Items regroup everything Skills, Effects, Tuto etc...
    [System.Serializable]
    public class Item
    {
        public string name;
        public bool isUsed;
        public string description;
    }


    // Skills in addition of being items and having name, description and isUsed they have level damage and a list of effects
    [System.Serializable]
    public class Skill : Item
    {
        public int level;
        public int damage;
        public List<Effect> effects;
    }

    // Effects in addtion Items' variables they have an effect
    [System.Serializable]
    public class Effect : Item
    {
        public float effect;
    }

    // TUTO FOR LATER
    /*[System.Serializable]
    public class Tuto
    {
        public string name;
        public string description;
        public string spritePath;
        public bool isSeen;
    }*/


    /*-----------*/
    /* All datas */
    /*-----------*/

    private static List<Skill> skills;
    private static List<Effect> effects;
    //private static List<Tuto> tutos;
    private static int curExperience;

    //Magic Trick to load data at the creation of the static class
    //-------------------------------------------------------------
    private static int staticClassInit = StaticClassInit();

    private static int StaticClassInit()
    {
        Load();
        return 0;
    }
    //-------------------------------------------------------------


    //  Players' Experience

    // Get the experience
    public static int GetExperience()
    {
        return curExperience;
    }

    // Add the experience
    public static void AddExperience(int amount)
    {
        curExperience += amount;

        Save();
    }




    //---------------------------------  Skills  ---------------------------------

    // Get the list of all Skills
    public static List<Skill> GetWeapons()
    {
        return skills;
    }

    // Get a Skill by its name
    public static Skill GetSkillByName(string skillName)
    {
        return skills.Find(c => c.name == skillName);
    }

    // Change used Skill
    public static void ChangeUsedSkill(string newSkillName)
    {
        skills.Find(c => c.isUsed == true).isUsed = false;
        skills.Find(c => c.name == newSkillName).isUsed = true;

        Save();
    }

    // Get current used Skills
    public static List<Skill> GetCurrentSkills()
    {
        List<Skill> currentSkills = new List<Skill>();

        foreach (Skill skill in skills)
        {
            if (skill.isUsed) currentSkills.Add(skill);
        }

        return currentSkills;
    }

    // Init the skill list
    public static void initSkillsList()
    {
        skills = new List<Skill>(){
                new Skill { name = "Fireball", isUsed = true, description = "The Fireball description here", damage = 3, level =1 }
                ,new Skill { name = "Sword", isUsed = false, description = "The Sword description here", damage = 3, level =1 }
            };
    }


    //---------------------------------  Effects  ---------------------------------

    // Get the list of all Effects
    public static List<Effect> GetEffects()
    {
        return effects;
    }

    // Get an Effect by its name
    public static Effect GetEffectByName(string effectName)
    {
        return effects.Find(c => c.name == effectName);
    }

    // Init Effects list
    public static void initEffectsList()
    {
        effects = new List<Effect>(){
                new Effect { name = "Silence", isUsed = true, description = "The Silence effect description here", effect = 1.0f}
                ,new Effect { name = "Delayed", isUsed = false, description = "The delayed effect description here", effect = 1.0f }
            };
    }



    // ---------------------------------  Save Data  ---------------------------------


    public static void Save()
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;

        /*--------------*/
        /* Save Skills */
        /*--------------*/
        file = File.Create(Application.persistentDataPath + "/skills.sav");
        bf.Serialize(file, skills);
        file.Close();

        /*-------------*/
        /* Save Effects */
        /*-------------*/
        file = File.Create(Application.persistentDataPath + "/effects.sav");
        bf.Serialize(file, effects);
        file.Close();


        /*--------------*/
        /* Save Experience */
        /*--------------*/
        file = File.Create(Application.persistentDataPath + "/experience.sav");
        bf.Serialize(file, curExperience);
        file.Close();
    }


    //---------------------------------------------------------------------------------------
    // Load GeneralData
    //---------------------------------------------------------------------------------------
    public static void Load()
    {

        /*---------------*/
        /*  Load Skills  */
        /*---------------*/
        if (File.Exists(Application.persistentDataPath + "/skills.sav"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/skills.sav", FileMode.Open);
            skills = (List<Skill>)bf.Deserialize(file);
            file.Close();
            //IF there is not data, default values
        }
        else initSkillsList();



        /*--------------*/
        /* Load Effects */
        /*--------------*/
        if (File.Exists(Application.persistentDataPath + "/effects.sav"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/effects.sav", FileMode.Open);
            effects = (List<Effect>)bf.Deserialize(file);
            file.Close();
        }
        //IF there is not data, default values
        else initEffectsList();


        /*-----------------*/
        /* Load Experience */
        /*-----------------*/

        if (File.Exists(Application.persistentDataPath + "/experience.sav"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/experience.sav", FileMode.Open);
            curExperience = (int)bf.Deserialize(file);
            file.Close();
        }
        //IF there is not data, default value
        else curExperience = 0;


    }
}
