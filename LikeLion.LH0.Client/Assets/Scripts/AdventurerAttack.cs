using System;
using System.Collections;
using Platformer;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Platformer
{
    public class AdventurerAttack : MonoBehaviour, IDamageable
    {
        [HideInInspector] public InputType inputType;
        private SoundManager sound;
        private Animator anim;

        [SerializeField]
        private GameObject gameOverPanel;

        public static int monsterCount = 0;
        public static int monsterKillCount = 0;

        [SerializeField] private Slider hpSlider;
        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private Image hpBar;

        [SerializeField] private float hp = 100f;
        // [field: SerializeField] public float Hp { get; private set; }

        private float maxHp = 100f;
        [SerializeField] private float damage = 20f;

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
            if (hp <= 0)
                return;

            hp -= damage;
            if (hp < 0)
                hp = 0;

            SetHpUI();

            if (hp <= 0)
                Death();
        }

        public void Death()
        {
            //anim.SetTrigger("Death");

            //gameOverPanel.SetActive(true);

            //SceneManager.LoadScene(0);
            StartCoroutine(DeathRoutine());
        }
        private IEnumerator DeathRoutine()
        {
            // 1. 죽음 애니메이션 실행 (즉시)
            anim.SetTrigger("Death");

            // 2. 1초 대기
            yield return new WaitForSeconds(1f);

            // 3. 게임 오버 패널 활성화
            gameOverPanel.SetActive(true);

            // 4. 2초 대기
            yield return new WaitForSeconds(2f);

            // 5. 씬 로드 (인덱스 0)
            SceneManager.LoadScene(0);
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
            hpText.text = $"{hp} / {maxHp}";

            if (hp >= 7.5f)
                hpBar.color = Color.green;
            else if (hp < 7.5f && hp >= 2.5f)
                hpBar.color = Color.yellow;
            else if (hp < 2.5f)
                hpBar.color = Color.red;
        }
    }
}