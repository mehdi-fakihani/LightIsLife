using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using LIL.Inputs;


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

    /*-----------*/
    /* All datas */
    /*-----------*/
    private static List<Player> players;
    public static Game game;
    private static List<Skill> skills1;
    private static List<Skill> skills2;
    public static bool multiplayer;
    private static Skill[] usedSkills1 = new Skill[] { null, null, null, null };
    private static Skill[] usedSkills2 = new Skill[] { null, null, null, null };
    //private static List<Tuto> tutos;
    private static int curExperience1;
    private static int curCapacityPoints1;
    private static int curExperience2;
    private static int curCapacityPoints2;
    public static string fileName;
    public static string path = Application.persistentDataPath + "/Saves";
    public static ProfilsID inputPlayer1, inputPlayer2;
    public static Profile profile1, profile2;
    public static ProfileModel Keyboard1ProfileModel, Keyboard2ProfileModel, controller1ProfileModel, controller2ProfileModel;
    public static string sceneName;
    public static int mainMenuID;
    public static bool gamePaused;

    /*-------------------*/
    /* STRUCTURES / Enum */
    /*-------------------*/

    // Game
    [System.Serializable]
    public class Game
    {
        public List<Player> players;
        public bool multiplayer;

        public Game(bool _multiplayer, List<Player> _players)
        {
            multiplayer = _multiplayer;
            players = _players;
        }

    }

        // Player
        [System.Serializable]
    public class Player
    {
        public int playerNum;
        public List<Skill> skills;
        public Skill[] usedSkills;
        public int experience;
        public int level;
        public int capacityPoints;
        public float[] pos;

        public Player(int _playerNum, List<Skill> _skills, Skill[] _usedSkills, int _experience, int _level, int _capacityPoints, float[] _pos)
        {
            playerNum = _playerNum;
            skills = _skills;
            usedSkills = _usedSkills;
            experience = _experience;
            level = _level;
            capacityPoints = _capacityPoints;
            pos = _pos;
        }

    }

    // Skill's Class 
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


    // Skill
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
        public string spritePath;
        public float cooldown;

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




    //Magic Trick to load data at the creation of the static class
    //-------------------------------------------------------------
    /*private static int staticClassInit = StaticClassInit();
 

    private static int StaticClassInit()
    {
        Load(fileName);

        return 0;
    }*/
    //-------------------------------------------------------------

    //------------------------------------  Game  -----------------------------------
    public static void initGame()
    {
        initPlayersList();
        game = new Game(multiplayer, players);
    }

    //------------------------------------  Player  -----------------------------------
    public static void initPlayersList()
    {
        players = new List<Player>();
        List<Skill> skills1 = initSkillsList();
        Player player1 = new Player(1, skills1, new Skill[] { null, null, null, null }, 0, 1, 1, new float[] { 30f, 0f, 26f });
        
        players.Add(player1);


        if (multiplayer)
        {
            List<Skill> skills2 = initSkillsList();
            Player player2 = new Player(2, skills2, new Skill[] { null, null, null, null }, 0,1, 1, new float[] { 25f, 0f, 30f });
            
            players.Add(player2);
        }
    }

    public static Profile getProfile(int playerNum)
    {
        if (playerNum == 2) return profile2;
        else return profile1;
    }

    public static void setProfile(int playerNum, Profile profile)
    {
        if (playerNum == 2)  profile2 = profile;
        else  profile1 = profile;
    }

    public static Player getPlayerbyNum(int playerNum)
    {
        return game.players.Find(p => p.playerNum == playerNum);
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
        Save(fileName);
    }

    // incr Level
    public static void incrLevel(int playerNum)
    {
        getPlayerbyNum(playerNum).level++;
        UpdateCapacitypoints(1, playerNum);
    }

    // Get Level
    public static int getLevel(int playerNum)
    {
        return getPlayerbyNum(playerNum).level;
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
        Save(fileName);
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
        Save(fileName);
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
            dependency = new List<Skill>() { Charge, Storm }, spritePath = "SkillSprite/Warrior/Provocation"
        };
        Skill Reflection = new Skill { name = "Reflect", _class = getClassByName("Warrior"), isUsed = false, deblocked = false,
                    description = "Used for protection against ranged attacks.\nFor 0.5s, returns the projectiles"+
                "\nMakes the warrior invulnerable", CapPointsToUnlock =4,
            dependency = new List<Skill>() { Charge }, spritePath = "SkillSprite/Warrior/Reflect"
        };

        // Assassin Skills : ------------------------------------------------------------------------------------------------------
        Skill BladesDance = new Skill { name = "BladesDance", _class = getClassByName("Assassin"), isUsed = false, deblocked = false,
                    description = "An Assassin skill used to disappear and hit the enemy.\nQuality : "+
                "Strong damage\nUseful to escape from enemies", CapPointsToUnlock =1, dependency = null,
            spritePath = "SkillSprite/Assassin/BladesDance"
        };
        Skill ShadowDance = new Skill { name = "ShadowDance", _class = getClassByName("Assassin"), isUsed = false, deblocked = false,
                    description = "Used to move freely and attack violently.\nDisappear for a short while and move faster"+
                "\nNext sword attack before the end of the effect, stuns (1s) and inflect additional damage.", CapPointsToUnlock =2,
            dependency = null, spritePath = "SkillSprite/Assassin/ShadowDance" };
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
                    description = "The Sword description here", damage = 3, CapPointsToUnlock =0, cooldown=0.3f }
            };

        return skills;
    }


    public static void deblockSkill(int playerNum, string skillName)
    {
        Skill skill = GetSkillByName(skillName, playerNum);

        if (GetCapacityPoints(playerNum) >= skill.CapPointsToUnlock)
        {
            int index = GetSkillIndexByName(skillName, playerNum);
             UpdateCapacitypoints(-skill.CapPointsToUnlock, playerNum);
            getPlayerbyNum(playerNum).skills[index].deblocked = true;
        }

        Save(fileName);
    }


    // ---------------------------------  Save Game  ---------------------------------


    public static void Save(string fineName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        file = File.Open(path + "/" + fileName, FileMode.OpenOrCreate, FileAccess.Write);
        bf.Serialize(file, game);
        file.Close();
    }


    // ---------------------------------  Load Game  ---------------------------------
    public static void Load(string fileName)
    {

        if (File.Exists(path + "/" + fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path + "/" + fileName, FileMode.Open);
            game = (Game) bf.Deserialize(file);
            multiplayer = game.multiplayer;
            file.Close();
            //IF there is not data, default values

            Save(fileName);
        }

       
    }

    public static List<string> FilesToLoad()
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        DirectoryInfo info = new DirectoryInfo(path + "/");
        FileInfo[] files = info.GetFiles("*.sav").OrderBy(p => p.CreationTime).ToArray();
        List<string> filesName = new List<string>();
        int i = 0;
        foreach (FileInfo file in files)
        {
            i++;
            filesName.Add(file.Name);
        }
        return filesName;
    }

    public static string LastGamePlayed()
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        DirectoryInfo info = new DirectoryInfo(path + "/");
        FileInfo[] files = info.GetFiles("*.sav").OrderBy(p => p.LastWriteTime).ToArray();

        string fileToLoad = "Unkown";

        if (files != null && files.Length != 0)
            fileToLoad = files[files.Length - 1].Name;

        return fileToLoad;
    }

    // ---------------------------------  New Game  ---------------------------------
    public static void New()
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        DirectoryInfo info = new DirectoryInfo(path + "/");
        FileInfo[] files = info.GetFiles("*.sav").ToArray();
        fileName = "Game" + (files.Length + 1) + ".sav";
        initGame();
    }

   


}
