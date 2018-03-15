using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace LIL.Inputs
{
    /// <summary>
    /// Stores the keys used by a player.
    /// Creates player profiles with a device number, to allow simultaneous multiple gamepads.
    /// </summary>
    public class ProfileModel : MonoBehaviour
    {
        [SerializeField] public ProfilsID id;
        [SerializeField] public Device device;
        [SerializeField] public Key up;
        [SerializeField] public Key down;
        [SerializeField] public Key left;
        [SerializeField] public Key right;
        [SerializeField] public Key attack;
        [SerializeField] public Key skill1;
        [SerializeField] public Key skill2;
        [SerializeField] public Key skill3;
        [SerializeField] public Key skill4;

        [SerializeField] public Key interaction;


        [SerializeField] public Key changeTorch;


        public readonly Dictionary<PlayerAction, Key> keys
            = new Dictionary<PlayerAction, Key>();
        
        void Awake()
        {
            // Check that all keys are different

            var keysList = new HashSet<Key>();
            Assert.IsTrue(keysList.Add(up));
            Assert.IsTrue(keysList.Add(down));
            Assert.IsTrue(keysList.Add(left));
            Assert.IsTrue(keysList.Add(right));
            Assert.IsTrue(keysList.Add(attack));
            Assert.IsTrue(keysList.Add(skill1));
            Assert.IsTrue(keysList.Add(skill2));
            Assert.IsTrue(keysList.Add(skill3));
            Assert.IsTrue(keysList.Add(skill4));

            Assert.IsTrue(keysList.Add(interaction));

            Assert.IsTrue(keysList.Add(changeTorch));
            

            // Reference keys with their actions

            keys.Add(PlayerAction.Up,     up);
            keys.Add(PlayerAction.Down,   down);
            keys.Add(PlayerAction.Left,   left);
            keys.Add(PlayerAction.Right,  right);
            keys.Add(PlayerAction.Attack, attack);
            keys.Add(PlayerAction.Skill1, skill1);
            keys.Add(PlayerAction.Skill2, skill2);
            keys.Add(PlayerAction.Skill3, skill3);
            keys.Add(PlayerAction.Skill4, skill4);

            keys.Add(PlayerAction.Interaction, interaction);

            keys.Add(PlayerAction.ChangeTorch, changeTorch);


            // Check that the keys correspond to the device used

            foreach (var key in keys.Values)
            {
                Assert.AreEqual(device, KeyGroup.DeviceOf(key),
                    "The keys of a profile must correspond to it's device");
            }

            Profile.Models.Add(id, this);
        }
    }
}
