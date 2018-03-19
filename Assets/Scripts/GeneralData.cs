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

    // Class 
    [System.Serializable]
    public class SkillClass
    {
        public string name;
        public Color color;
        public SkillClass(string name, Color color)
        {
            this.name = name;
            this.color = color;
        }
    }


    // Skills in addition of being items and having name, description and isUsed they have level damage and a list of effects
    [System.Serializable]
    public class Skill
    {
        public string name;
        public SkillClass _class;
        public bool isUsed;
        public string description;
        public bool deblocked;
        public int CapPointsToUnlock;
        public int damage;
        public List<Skill> dependency;
        public List<Effect> effects;
        public string spritePath;

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
    private static Skill[] usedSkills = new Skill[] { null, null, null, null };
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

    //---------------------------------  Skill Class  ---------------------------------
    public static SkillClass getClassByName(string name)
    {
        SkillClass _class = null;
        switch (name)
        {
            case "Wizard":
                _class = new SkillClass("Wizard", new Color32(249, 230, 107,255));
                break;
            case "Warrior":
                _class = new SkillClass("Warrior", new Color32(9, 142, 187,255));
                break;
            case "Assassin":
                _class = new SkillClass("Assassin", new Color32(158, 26, 21,255));
                break;
        }
        return _class;
    }


    //---------------------------------  Experience  ---------------------------------

    // Get the experience
    public static int GetExperience()
    {
        return curExperience;
    }

    // Add the experience
    public static void IncrExperience(int amount)
    {
        curExperience += amount;

        Save();
    }




    //---------------------------------  Skills  ---------------------------------

    // Get the list of all Skills
    public static List<Skill> GetAllSkills()
    {
        return skills;
    }

    // Get a Skill by its name
    public static Skill GetSkillByName(string skillName)
    {
        return skills.Find(c => c.name == skillName);
    }

    // Check if the skill is available (all dependencies are good)
    public static bool isAvailable(string name)
    {
        Skill skill = GetSkillByName(name);
        bool available = true;
        if (skill.dependency != null)
        {
            for (int i = 0; i < skill.dependency.Count; i++)
            {
                if (!skill.dependency[i].deblocked)
                {
                    available = false;
                    break;
                }
            }
        }
        return available;
    }

    // Change used Skill
    public static void ChangeUsedSkill(string newSkillName,int pos)
    {
        skills.Find(c => c.isUsed == true).isUsed = false;
        skills.Find(c => c.name == newSkillName).isUsed = true;

        Save();
    }

    // Get current used Skills
    public static Skill[] GetCurrentSkills()
    {
        Skill[] currentSkills = new Skill[] { null, null, null, null};
        int j = 0;
        foreach (Skill skill in skills)
        {
            if (skill.name != "Sword" && skill.isUsed)
            {
                currentSkills[j] = skill;
                j++;
            }
        }

        return currentSkills;
    }

    // Init the skill list
    public static void initSkillsList()
    {
        // Wizard Skills : ------------------------------------------------------------------------------------------------------
        Skill Fireball = new Skill { name = "Fireball", _class = getClassByName("Wizard"), isUsed = true, deblocked = true,
            description = "Used to hit the enemy from a distance.\nQuality : Ranged attack,\n" +
                "3 charges\nFlaw : 0.5s of immobilization", CapPointsToUnlock = 1, spritePath = "SkillSprite/Wizard/Fireball",
            dependency = null };
        Skill Icyblast = new Skill { name = "IcyBlast", _class = getClassByName("Wizard"), isUsed = false, deblocked = true,
            description = "Used to slow down the enemy by throwing Icy balls in a cone.\nQuality : " +
                "Slow down the enemy 2-3s\nCause Damage", CapPointsToUnlock = 2, dependency = new List<Skill>() { Fireball },
            spritePath = "SkillSprite/Wizard/IcyBlast"
        };
        Skill TimeStamp =new Skill { name = "Rollback", _class =  getClassByName("Wizard"), isUsed = false, deblocked = false,
                    description = "Used to avoid difficult situations.\n1st use : place a mark at the player's position"+
                "(duration 3s)\n2nd use : replaced on the mark and recover the reloading time and health", CapPointsToUnlock =3,
            dependency = new List<Skill>() { Fireball }, spritePath = "SkillSprite/Wizard/Rollback"
        };
        Skill Levitation = new Skill { name = "Levitation", _class =  getClassByName("Wizard"), isUsed = false, deblocked = false,
                    description = "Used to get rid of the side effects.\nFor 3s attaking or using skills "+
                "does not slow down nor immobilize the player", CapPointsToUnlock =4,
            dependency = new List<Skill>() { Fireball }, spritePath = "SkillSprite/Wizard/Levitation"
        };

        // Warrior Skills : ------------------------------------------------------------------------------------------------------
        Skill Charge = new Skill { name = "Charge", _class =  getClassByName("Warrior"), isUsed = false, deblocked = true,
                    description = "A Warrior skill used to surprise the enemy by moving so fast.\nQuality : "+
                "invulnerable while attacking\nDamage and silence the enemy", CapPointsToUnlock =1, dependency = null,
            spritePath = "SkillSprite/Warrior/Charge"
        };
        Skill Storm = new Skill { name = "Storm", _class = getClassByName("Warrior"), isUsed = false, deblocked = false,
                    description = "Used to attack close enemies.\n1st use : inflicts damage (by 0.5s) to nearby enemies "+
                "but can only move 2-3x slower\n2nd use : inflect damage for 3s to affected enemies", CapPointsToUnlock =2,
            dependency = new List<Skill>() { Charge }, spritePath = "SkillSprite/Warrior/Storm"
        };
        Skill Provocation = new Skill { name = "Provocation", _class = getClassByName("Warrior"), isUsed = false, deblocked = false,
                    description = "Used to get rid of the effects.\nRemoves effects on the warrior"+
                "\nAttracts surrounding enemies for 2-3s", CapPointsToUnlock =3,
            dependency = new List<Skill>() { Charge }, spritePath = "SkillSprite/Warrior/Provocation"
        };
        Skill Reflection = new Skill { name = "Reflect", _class = getClassByName("Warrior"), isUsed = false, deblocked = false,
                    description = "Used for protection against ranged attacks.\nFor 0.5s, returns the projectiles"+
                "\nMakes the warrior invulnerable", CapPointsToUnlock =4,
            dependency = new List<Skill>() { Charge, Provocation }, spritePath = "SkillSprite/Warrior/Reflect"
        };

        // Assassin Skills : ------------------------------------------------------------------------------------------------------
        Skill BladesDance = new Skill { name = "BladesDance", _class = getClassByName("Assassin"), isUsed = false, deblocked = true,
                    description = "An Assassin skill used to disappear and hit the enemy.\nQuality : "+
                "Strong damage\nUseful to escape from enemies", damage = 3, CapPointsToUnlock =1, dependency = null,
            spritePath = "SkillSprite/Assassin/BladesDance"
        };
        Skill ShadowDance = new Skill { name = "ShadowDance", _class = getClassByName("Assassin"), isUsed = false, deblocked = false,
                    description = "Used to move freely and attack violently.\nDisappear for a short while and move faster"+
                "\nNext sword attack before the end of the effect, stuns (1s) and inflect additional damage.", CapPointsToUnlock =2,
            dependency = new List<Skill>() { Fireball}, spritePath = "SkillSprite/Assassin/ShadowDance" };
        Skill Poison = new Skill { name = "Poison", _class = getClassByName("Assassin"), isUsed = false, deblocked = false,
            description = "Used to poison enemies.\nSword attacks apply a poison that hurts the affected enemies for (4s)"
                , CapPointsToUnlock = 3, dependency = new List<Skill>() { ShadowDance }, spritePath = "SkillSprite/Assassin/Poison" };
        Skill Adrenaline = new Skill { name = "Adrenaline", _class = getClassByName("Assassin"), isUsed = false, deblocked = false,
            description = "Used to lower the reloading time.\nSword attacks mark touched enemies (max 3)" +
                "\nHit an enemy marked with a skill lower the realoding time", CapPointsToUnlock = 4,
            dependency = new List<Skill>() { BladesDance }, spritePath = "SkillSprite/Assassin/Adrenaline"
        };


        skills = new List<Skill>(){
            // Wizard Skills :
                Fireball, Icyblast, TimeStamp, Levitation,
            // Warrior Skills :
                Charge, Storm, Provocation, Reflection,
            // Assassin Skills :
                BladesDance, ShadowDance, Poison, Adrenaline,
            // Basic Attack : 
                new Skill { name = "Sword", isUsed = true,
                    description = "The Sword description here", damage = 3, CapPointsToUnlock =0 }
            };

        usedSkills = GetCurrentSkills();

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
