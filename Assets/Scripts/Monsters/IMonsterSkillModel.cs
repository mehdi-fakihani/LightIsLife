using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{
    public abstract class IMonsterSkillModel : ISkillModel
    {
        [SerializeField] public float range;

        public abstract float priority();
        public abstract void cast(SkillManager manager, IEnumerable<GameObject> players, IEnumerable<MonsterManager> monsters);

        public override void cast(SkillManager manager)
        {

        }
    }
}
