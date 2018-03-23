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

    [System.Serializable]
    public class Player
    {
        public int playerNum;
        public List<Skill> skills;
        public Skill[] usedSkills;
        public int experience;
        public int capacityPoints;
        public float[] pos;

        public Player(int _playerNum, List<Skill> _skills, Skill[] _usedSkills, int _experience, int _capacityPoints, float[] _pos)
        {
            playerNum = _playerNum;
            skills = _skills;
            usedSkills = _usedSkills;
            experience = _experience;
            capacityPoints = _capacityPoints;
            pos = _pos;
        }

    }

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
        public byte[] color;
        public SkillClass(string name, byte[] color)
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
    private static List<Player> players;
    private static List<Skill> skills1;
    private static List<Skill> skills2;
    private static List<Effect> effects;
    public static bool multiplayer;
    private static Skill[] usedSkills1 = new Skill[] { null, null, null, null };
    private static Skill[] usedSkills2 = new Skill[] { null, null, null, null };
    //private static List<Tuto> tutos;
    private static int curExperience1;
    private static int curCapacityPoints1;
    private static int curExperience2;
    private static int curCapacityPoints2;

    //Magic Trick to load data at the creation of the static class
    //-------------------------------------------------------------
    private static int staticClassInit = StaticClassInit();
 

    private static int StaticClassInit()
    {
        Load();

        return 0;
    }
    //-------------------------------------------------------------

    //------------------------------------  Player  -----------------------------------
    public static void initPlayersList()
    {
        players = new List<Player>();
        List<Skill> skills1 = initSkillsList();
        Player player1 = new Player(1, skills1, new Skill[] { null, null, null, null }, 0, 10, new float[] { 0f, 0f, 0f });
        
        players.Add(player1);

        multiplayer = true;
        if (multiplayer)
        {
            List<Skill> skills2 = initSkillsList();
            Player player2 = new Player(2, skills2, new Skill[] { null, null, null, null }, 0, 10, new float[] { 5f, 0f, 0f });
            Debug.Log("Num : " + player2.playerNum + " Skills size : " + player2.skills.Count + " usedskills size : " + player2.usedSkills.Length +
            " experience : " + player2.experience + " capacitypoints : " + player2.capacityPoints + " pos : " + player2.pos.ToString());
            players.Add(player2);

        }
    }
    public static Player getPlayerbyNum(int playerNum)
    {
        return players.Find(p => p.playerNum == playerNum);
    }


    //---------------------------------  Skill Class  ---------------------------------
    public static SkillClass getClassByName(string name)
    {
        SkillClass _class = null;
        switch (name)
        {
            case "Wizard":
                _class = new SkillClass("Wizard", new byte[] { 249, 230, 107, 255 });
                break;
            case "Warrior":
                _class = new SkillClass("Warrior", new byte[] { 9, 142, 187, 255 });
                break;
            case "Assassin":
                _class = new SkillClass("Assassin", new byte[] { 158, 26, 21, 255 });
                break;
        }
        return _class;
    }


    //---------------------------------  Experience  ---------------------------------

    // Get the experience
    public static int GetExperience(int playerNum)
    {
        return getPlayerbyNum(playerNum).experience;
    }

    // Add the experience
    public static void IncrExperience(int amount, int playerNum)
    {
        getPlayerbyNum(playerNum).experience += amount;
        Save();
    }


    //---------------------------------  Capacity Points  ---------------------------------

    // Get the capacityPoints
    public static int GetCapacityPoints(int playerNum)
    {
        return getPlayerbyNum(playerNum).capacityPoints;
    }

    // Add the capacityPoints
    public static void UpdateCapacitypoints(int amount, int playerNum)
    {
        getPlayerbyNum(playerNum).capacityPoints += amount;
        Save();
    }


    //---------------------------------  Skills  ---------------------------------

    // Get the list of all Skills
    public static List<Skill> GetAllSkills(int playerNum)
    {
        return getPlayerbyNum(playerNum).skills;
    }

    // Get a Skill by its name
    public static Skill GetSkillByName(string skillName, int playerNum)
    {
        return getPlayerbyNum(playerNum).skills.Find(c => c.name == skillName);
    }

    // Get index of a skill by name
    public static int GetSkillIndexByName(string skillName, int playerNum)
    {
        int index = 0;

        for(int i = 0; i < getPlayerbyNum(playerNum).skills.Count; i++)
        {
            if (getPlayerbyNum(playerNum).skills[i].name == skillName)
            {
                index = i;
                break;
            }
        }
        return index;
    }

    // Select skill
    public void Selectskill(int playerNum, string skillName)
    {
       // List<Player>
    }

    // Check if the skill is available (all dependencies are good)
    public static bool isAvailable(string name, int playerNum)
    {
        
        Skill skill = GetSkillByName(name, playerNum);
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
    public static void ChangeUsedSkill(string newSkillName, int pos, int playerNum)
    {
        Skill skill = GetSkillByName(newSkillName, playerNum);
        skill.isUsed = true;

        getPlayerbyNum(playerNum).usedSkills[pos].isUsed = false;
        getPlayerbyNum(playerNum).usedSkills[pos] = skill;

        Save();
    }


    // Get current skills
    public static Skill[] GetCurrentSkills(int playerNum)
    {
        return getPlayerbyNum(playerNum).usedSkills;
    }


    // Init the skill list of the player 1
    public static List<Skill> initSkillsList()
    {
        // Wizard Skills : ------------------------------------------------------------------------------------------------------
        Skill Fireball = new Skill { name = "Fireball", _class = getClassByName("Wizard"), isUsed = false, deblocked = false,
            description = "Used to hit the enemy from a distance.\nQuality : Ranged attack,\n" +
                "3 charges\nFlaw : 0.5s of immobilization", CapPointsToUnlock = 1, spritePath = "SkillSprite/Wizard/Fireball",
            dependency = null };
        Skill Icyblast = new Skill { name = "IcyBlast", _class = getClassByName("Wizard"), isUsed = false, deblocked = false,
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
        Skill Charge = new Skill { name = "Charge", _class =  getClassByName("Warrior"), isUsed = false, deblocked = false,
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
        Skill BladesDance = new Skill { name = "BladesDance", _class = getClassByName("Assassin"), isUsed = false, deblocked = false,
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


        List<Skill> skills = new List<Skill>(){
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

        return skills;
    }


    public static void deblockSkill(int playerNum, string skillName)
    {
        Skill skill = GetSkillByName(skillName, playerNum);

        if (GetCapacityPoints(playerNum) >= skill.CapPointsToUnlock)
        {
            Debug.Log(playerNum + " : " + skillName + " deblocked ");
            int index = GetSkillIndexByName(skillName, playerNum);
            UpdateCapacitypoints(-skill.CapPointsToUnlock, playerNum);
            Debug.Log(2 + " : " + skillName + " is deblocked " + getPlayerbyNum(2).skills[GetSkillIndexByName(skillName, 2)].deblocked.ToString());
            getPlayerbyNum(playerNum).skills[index].deblocked = true;
            Debug.Log(2 + " : " + skillName + " is deblocked " + getPlayerbyNum(2).skills[GetSkillIndexByName(skillName, 2)].deblocked.ToString());
        }

        Save();
    }


    // ---------------------------------  Save Data  ---------------------------------


    public static void Save()
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;

        file = File.Create(Application.persistentDataPath + "/game.sav");
        bf.Serialize(file, players);
        file.Close();
    }


    //---------------------------------------------------------------------------------------
    // Load GeneralData
    //---------------------------------------------------------------------------------------
    public static void Load()
    {

        if (File.Exists(Application.persistentDataPath + "/game.sav"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/game.sav", FileMode.Open);
            players = (List<Player>)bf.Deserialize(file);
            file.Close();
            //IF there is not data, default values
        }
        else initPlayersList();

        Save();
    }
}
