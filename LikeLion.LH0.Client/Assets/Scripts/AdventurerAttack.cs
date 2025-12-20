using System;
using Platformer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class AdventurerAttack : MonoBehaviour, IDamageable
    {
        [HideInInspector] public InputType inputType;
        private SoundManager sound;
        private Animator anim;

        public static int monsterCount = 0;
        public static int monsterKillCount = 0;

        [SerializeField] private Slider hpSlider;
        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private Image hpBar;

        [SerializeField] private float hp = 10f;
        // [field: SerializeField] public float Hp { get; private set; }

        private float maxHp;
        [SerializeField] private float damage = 1f;

        private bool isAttack, isCombo, isFinal;

        void Awake()
        {
            sound = FindFirstObjectByType<SoundManager>();

            anim = GetComponent<Animator>();

            maxHp = hp;

            SetHpUI();
        }

        void Update()
        {
            if (inputType == InputType.Keyboard)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                    Attack();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var target = other.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(damage);

                // Debug.Log($"{gameObject.name}이 {other.name}에게 {damage}만큼의 데미지 적용");
            }
        }

        public void Attack()
        {
            anim.SetTrigger("Attack");
            //if (!isAttack) // 처음 공격키를 눌렀을 때
            //{
            //    //isAttack = true;

            //    //ClearCombo();
            //}
        }

        public void AttackSound(string clipName)
        {
            sound.SoundOneShot(clipName);
        }

        public void TakeDamage(float damage)
        {
            hp -= damage;

            SetHpUI();

            if (hp <= 0)
                Death();
        }

        public void Death()
        {
            Debug.Log($"{gameObject.name} 죽음");
        }

        public void Heal(float healPoint)
        {
            hp += healPoint;

            if (hp > maxHp)
                hp = maxHp;

            SetHpUI();
        }

        private void SetHpUI()
        {
            hpSlider.value = hp / maxHp;
            hpText.text = $"{hp * 10} / {maxHp * 10}";

            if (hp >= 7.5f)
                hpBar.color = Color.green;
            else if (hp < 7.5f && hp >= 2.5f)
                hpBar.color = Color.yellow;
            else if (hp < 2.5f)
                hpBar.color = Color.red;
        }
    }
}