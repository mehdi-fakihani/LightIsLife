using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LIL
{
    public class DamagePopUp : MonoBehaviour
    {

        private Animator animator;
        private float clipLength;
        private Text damage;
        private HealthManager healthManager;


    // Use this for initialization
        void Start()
        {
            animator = this.GetComponent<Animator>();
            AnimatorClipInfo[] animInfo = animator.GetCurrentAnimatorClipInfo(0);
            clipLength = animInfo[0].clip.length;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetTextDamage(int _damage)
        {
            this.gameObject.SetActive(true);
            damage = GetComponent<Text>();
            damage.text = "-" + _damage;
            StartCoroutine(Disable());

        }

        IEnumerator Disable()
        {
            yield return new WaitForSeconds(clipLength);
            this.gameObject.SetActive(false);
        }
    }
}

