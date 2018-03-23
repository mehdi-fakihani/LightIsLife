using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace LIL
{
    public static class SaveLoad
    {
        /*public static List<GeneralData.Game> savedGames = new List<GeneralData.Game>();
        private static int staticClassInit = StaticClassInit();

        private static int StaticClassInit()
        {
            
            Load();
            return 0;
        }

        public static void Save()
        {
            savedGames.Add(GeneralData.Game.currentGame);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
            bf.Serialize(file, SaveLoad.savedGames);
            file.Close();

            //Debug.Log("savedGames : " + GeneralData.Game.currentGame.skills[0][0].description);
        }

        public static void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
                SaveLoad.savedGames = (List<GeneralData.Game>)bf.Deserialize(file);
                file.Close();
                Debug.Log("load - savedGames : " + savedGames.Count);
            }
            else
            {
                GeneralData.initGame();
                Save();
            }
        }*/
    }
}
    
    
    
