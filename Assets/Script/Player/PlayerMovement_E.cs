using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_E : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rbody;
    [SerializeField]
    private Transform tr;
    [SerializeField]
    private Animator ani;
    [SerializeField]
    private CapsuleCollider capCol;
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip swordClip;
    [SerializeField]
    private AudioClip[] footSteps;
    

    private float h = 0f, v = 0f;
    float lastAttackTime;
    float lastSkillTime;
    float lastDashTime;
    bool isAttack;
    bool isDash;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        ani = GetComponent<Animator>();
        capCol = GetComponent<CapsuleCollider>();
        source = GetComponent<AudioSource>();
        footSteps = Resources.LoadAll<AudioClip>("FootStep");
        swordClip = Resources.Load<AudioClip>("Sound/Skill_Sound");
        isAttack = false;
        isDash = false;
    }

    public void OnStickChanged(Vector2 stick)
    {
        h = stick.x;
        v = stick.y;
    }

    public void OnAttackDown()
    {
        isAttack = true;
        ani.SetBool("IsCombo", true);

        StartCoroutine(StartAttack());
    }

    IEnumerator StartAttack()
    {
        if(Time.time - lastAttackTime > 1.0f)
        {
            lastAttackTime = Time.time;
            while(isAttack)
            {
                ani.SetBool("IsCombo", true);
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
    public void OnAttackUp()
    {
        isAttack = false;
        ani.SetBool("IsCombo", false);
    }

    public void OnSkillDown()
    {
        if (Time.time - lastSkillTime > 1.0f)
        {
            ani.SetBool("IsSkill", true);
            lastSkillTime = Time.time;
        }
        if(!source.isPlaying)
        {
            source.clip = swordClip;
            source.PlayDelayed(0.8f);
            source.Play();
        }
    }

    public void OnSkillUp()
    {
        ani.SetBool("IsSkill", false);
    }

    public void OnDashDown()
    {
        if(Time.time - lastDashTime > 1.0f)
        {
            lastDashTime = Time.time;
            isDash = true;
            ani.SetTrigger("IsDash");
        }
    }

    public void OnDashUp()
    {
        isDash = false;
    }
    void FixedUpdate()
    {
        if(ani)
        {
            ani.SetFloat("Speed", (h * h + v * v));
            if(rbody)
            {
                Vector3 Speed = rbody.velocity;
                Speed.x = 5 * h;
                Speed.z = 5 * v;
                rbody.velocity = Speed;
                if(h!=0f && v!=0f)
                {
                    tr.rotation = Quaternion.LookRotation(new Vector3(h, 0f, v));
                    
                }
            }
        }
    }
}
