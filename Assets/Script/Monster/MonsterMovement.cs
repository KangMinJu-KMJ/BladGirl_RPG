using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rbody;
    [SerializeField]
    private Transform _Path;
    [SerializeField]
    private Transform[] _PathTransforms;
    [SerializeField]
    private List<Transform> Nodes;
    [SerializeField]
    private Transform tr;
    [SerializeField]
    private CapsuleCollider capCol;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Animator ani;
    public int currentNode = 0;
    private float timePrev = 0;
    bool IsSearch;
    bool IsAttack;
    bool IsTrace;
    void Start()
    {
        ani = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        capCol = GetComponent<CapsuleCollider>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _Path = GameObject.Find("PatrolPoint").transform;
        _PathTransforms = _Path.GetComponentsInChildren<Transform>();
        Nodes = new List<Transform>();
        for(int i=0; i<_PathTransforms.Length; i++)
        {
            if(_PathTransforms[i] != _Path.transform)
            {
                Nodes.Add(_PathTransforms[i]);
            }
        }
        currentNode = 0;
        timePrev = Time.time;
        IsSearch = true;
        IsAttack = false;
        IsTrace = false;
    }


    void FixedUpdate()
    {
        if (Time.time - timePrev >= 5.0f)
        {
            if(IsSearch)
            {
                Search();
                CheckWayPointDistance();
            }
            else if(!IsSearch)
            {
                if (IsTrace)
                    Trace();
                else if (IsAttack)
                    Attack();
            }

        }
    
    }

    void Search()//플레이어를 발견하기 전의 상태 구현
    {
        ani.SetBool("IsRoaring", true);
        Vector3 arriveDist = Nodes[currentNode].position - tr.position;
        tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion.LookRotation(arriveDist)
            , Time.deltaTime * 5.0f);
        tr.Translate(Vector3.forward * Time.deltaTime * 5f);
        float dist = Vector3.Distance(tr.position, target.position);
        if(dist<15.0f)
        {
            IsSearch = false;
            IsTrace = true;
        }
    }

    void Trace()// 플레이어 발견 이후
    {//쫓아가야하는데 왜 함수가 ㅇ비어잇을가요 ?
        Vector3 traceDist = target.position - tr.position;
        tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion.LookRotation(traceDist)
            , Time.deltaTime * 5.0f);
        tr.Translate(Vector3.forward * Time.deltaTime * 3f);
        float dist = Vector3.Distance(tr.position, target.position);
        if(dist<=4.0f)
        {
            IsAttack = true;
            IsTrace = false;
        }
        else if(dist>=15.0f)
        {
            IsTrace = false;
            IsSearch = true;
        }
    }

    void Attack()//공격
    {
        Vector3 attackDist = target.position - tr.position;
        tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion.LookRotation(attackDist)
            , Time.deltaTime * 3.0f);
        float dist = Vector2.Distance(tr.position, target.position);
        ani.SetBool("IsAttack", true);
        ani.SetBool("IsRoaring", false);
        if (dist>4.0f)
        {
            ani.SetBool("IsAttack", false);
            ani.SetBool("IsRoaring", true);
            IsTrace = true;
            IsAttack = false;
        }
    }


    void CheckWayPointDistance()
    {
        if(Vector3.Distance(tr.position, Nodes[currentNode].position)<4.5f)
        {
            if(currentNode==Nodes.Count -1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }
}
