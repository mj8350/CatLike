using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;
    private ParticleSystem At1;
    private ParticleSystem At2;

    private GameObject obj;
    private GameObject obj1;
    private GameObject obj2;


    private Vector3 moveDir = Vector3.zero;
    private Vector3 MousePosition;
    private Vector3 bowPos;

    private float angle;
    private float moveSpeed;
    private bool move;
    private bool miss;
    private bool bow;
    private bool knife01;
    private bool knife02;
    private bool flag;

    public bool stance;//어떤 무기를 들고있는지(true: bow, false: knife)
    public float stamina;
    public float stamina_f;
    public float stamina_max;
    public float stamina_heal;

    void Start()
    {
        if (!TryGetComponent<Animator>(out anim))
            Debug.Log("Player.cs - Start() - anim 참조 오류");
        obj1 = GameObject.Find("AtPt1");
        obj2 = GameObject.Find("AtPt2");
        if (!obj1.TryGetComponent<ParticleSystem>(out At1))
            Debug.Log("Player.cs - Start() - At1 참조 오류");
        if (!obj2.TryGetComponent<ParticleSystem>(out At2))
            Debug.Log("Player.cs - Start() - At2 참조 오류");



        moveSpeed = 5f;
        move = true;
        miss = true;
        bow = false;
        stance = true;
        knife01 = true;
        knife02 = false;
        flag = false;
        stamina_max = 100;
        stamina_heal = 1;
        stamina = stamina_max;
        stamina_f = stamina;

        At1.Stop();
        At2.Stop();

        StartCoroutine("stdown");
        StartCoroutine("stup");
    }

    void Update()
    {
        Debug.Log(stamina);

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8.5f, 8.5f), Mathf.Clamp(transform.position.y, -4.9f, 3.8f));
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");

        if (!bow && Input.GetKeyDown(KeyCode.R))
        {
            if (stance)
                stance = false;
            else
                stance = true;
        }

        if (stance && stamina>2 &&Input.GetMouseButton(0))
        {
            if (stamina < 3)
            {
                bow = false;
                anim.SetBool("BowAttack", false);

            }
            else
            {
                GetMouse();
                STdown();
                bow = true;
                anim.SetBool("BowAttack", true);
            }
        }
        if (stance && Input.GetMouseButtonUp(0))
        {
            bow = false;
            anim.SetBool("BowAttack", false);
        }
        

        if(stamina >3&&!stance && knife01&& Input.GetMouseButtonDown(0))
        {
            move = false;
            knife01 = false;
            GetMouse();
            //anim.SetFloat("MouseDir", MouseResult());
            anim.SetBool("KnifeAttack01", true);
            StartCoroutine("Knife01");
            At1.Play();
        }
        if(stamina > 3 && knife02 && Input.GetMouseButtonDown(0))
        {
            knife02 = false;
            GetMouse();
            anim.SetBool("KnifeAttack02", true);
            StartCoroutine("Knife02");
            At2.Play();
        }

        

        if (!knife01 || knife02)
        {
            if (flag)
                transform.position += (MousePosition - transform.position).normalized * Time.deltaTime * 15f;
        }
        else if (move && bow)
        {
            transform.position += moveDir.normalized * Time.deltaTime * (moveSpeed - 3f);
            Space(moveDir);

        }
        else if (move)
        {
            if (moveDir.x != 0 || moveDir.y != 0)
            {
                anim.SetBool("Move", true);
                transform.position += moveDir.normalized * Time.deltaTime * moveSpeed;

                anim.SetFloat("MotionDir", MoveResult());
                Space(moveDir);
            }
            else
                anim.SetBool("Move", false);
        }


        if (stamina_f == stamina&&stamina_f<stamina_max)
        {
            //StartCoroutine("stup");
        }


    }

    private Vector3 hitSize = new Vector3(2.5f, 4f, 0f);
    private Vector3 hitPos;
    private void Hit(float damage)
    {
        hitPos = (MousePosition - transform.position).normalized;
        hitPos *= 1.5f;

        Collider2D[] hit = Physics2D.OverlapBoxAll(hitPos, hitSize,angle);
        foreach(Collider2D col in hit)
        {
            //if(col.TryGetComponent<IDamage>(out IDamege idamage)) { }
        }
    }

    private int MoveResult()
    {
        if (moveDir.x > 0 && moveDir.y == 0)
            return 0;
        else if (moveDir.x > 0 && moveDir.y > 0)
            return 1;
        else if (moveDir.x == 0 && moveDir.y > 0)
            return 2;
        else if (moveDir.x < 0 && moveDir.y > 0)
            return 3;
        else if (moveDir.x < 0 && moveDir.y == 0)
            return 4;
        else if (moveDir.x < 0 && moveDir.y < 0)
            return 5;
        else if (moveDir.x == 0 && moveDir.y < 0)
            return 6;
        else //if (moveDir.x > 0 && moveDir.y < 0)
            return 7;
    }

    private void GetMouse()
    {
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePosition.z = 0f;
        angle = Mathf.Atan2(MousePosition.y - transform.position.y, MousePosition.x - transform.position.x) * Mathf.Rad2Deg;
        if (!bow)
        {
            obj1.transform.rotation = Quaternion.Euler(180f, 0f, -angle + 180f);
            obj2.transform.rotation = Quaternion.Euler(0f, 0f, angle + 180f);
        }
        anim.SetFloat("MouseDir", MouseResult());
        anim.SetFloat("MotionDir", MouseResult());
    }

    private int MouseResult()
    {
        if (angle >= -22.5f && angle < 22.5f)
            return 0;
        else if (angle >= 22.5f && angle < 67.5f)
            return 1;
        else if (angle >= 67.5f && angle < 112.5f)
            return 2;
        else if (angle >= 112.5f && angle < 157.5f)
            return 3;
        else if (angle >= 157.5f || angle < -157.5f)
            return 4;
        else if (angle >= -157.5f && angle < -112.5f)
            return 5;
        else if (angle >= -112.5f && angle < -67.5f)
            return 6;
        else //if (angle >= -67.5f && angle < -22.5f)
            return 7;
    }

    private void Space(Vector3 vector)
    {
        if (miss && stamina>10 && Input.GetKeyDown(KeyCode.Space))
        {
            miss = false;
            move = false;
            STdown(10f);
            anim.SetTrigger("Miss");
            StartCoroutine("MoveOn");
            StartCoroutine(Miss(vector));
        }
    }

    private void STdown(float st)
    {
        //StopCoroutine("stup");
        if (stamina > 0)
        {
            stamina -= st;
            //stamina_f = stamina;
        }
        else
            Debug.Log("stamina 부족");
        //StartCoroutine("stup");
    }
    private void STdown()
    {
        //StopCoroutine("stup");

        //StartCoroutine("stdown");

    }
    //여기부터 작업해야됨---------------------스태미나 보우 0.5초당 1씩 닳도록 구현
    private IEnumerator stdown()
    {
        while (true)
        {
            if (bow&&stamina>2)
            {
                //Debug.Log("공격");
                stamina -= 1;
                obj = PoolManager.Inst.pools[0].Pop();
                bowPos = transform.position;
                bowPos.y += 0.5f;
                obj.transform.position = bowPos;
                obj.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                yield return YieldInstructionCache.WaitForSeconds(0.5f);
            }
            else
            {
                //StartCoroutine("stup");
                yield return null;
            }
        }
    }

    /*private void STup()
    {
        StartCoroutine("stup");
    }*/

    private IEnumerator stup()
    {
        while (true)
        {
            stamina_f = stamina;
            yield return YieldInstructionCache.WaitForSeconds(1f);

            if (stamina_f == stamina && stamina_f < stamina_max)
            {
                //yield return YieldInstructionCache.WaitForSeconds(1f);

                while (stamina < stamina_max)
                {
                    stamina += stamina_heal;
                    stamina_f = stamina;
                    yield return YieldInstructionCache.WaitForSeconds(0.05f);
                    if (stamina_f != stamina)
                        break;
                }
                if (stamina > stamina_max)
                {
                    stamina = stamina_max;
                    stamina_f = stamina;
                }
            }
            else
                yield return null;
        }
    }

    private IEnumerator MoveOn()
    {
        yield return YieldInstructionCache.WaitForSeconds(0.3f);
        move = true;
        yield return YieldInstructionCache.WaitForSeconds(0.05f);
        miss = true;
    }

    private IEnumerator Miss(Vector3 vec)
    {
        knife01 = false;
        while (!move)
        {
            transform.position += vec.normalized * Time.deltaTime * (moveSpeed + 5f);
            yield return null;
        }
        knife01 = true;
    }

    private IEnumerator Knife01()
    {
        StartCoroutine(OnFlag(0.01f));
        yield return YieldInstructionCache.WaitForSeconds(0.15f);
        knife02 = true;
        yield return YieldInstructionCache.WaitForSeconds(0.2f);
        if (!anim.GetBool("KnifeAttack02"))
        {
            anim.SetBool("KnifeAttack01", false);
            knife02 = false;
            knife01 = true;
            move = true;
        }
    }

    private IEnumerator Knife02()
    {
        StartCoroutine(OnFlag(0.02f));
        yield return YieldInstructionCache.WaitForSeconds(0.4f);
        anim.SetBool("KnifeAttack01", false);
        anim.SetBool("KnifeAttack02", false);
        
        move = true;
        yield return YieldInstructionCache.WaitForSeconds(0.1f);
        knife01 = true;
    }

    private IEnumerator OnFlag(float t)
    {
        STdown(2f);
        flag = true;
        yield return YieldInstructionCache.WaitForSeconds(t);
        flag = false;
        
    }
}
