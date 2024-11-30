using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponMovement : MonoBehaviour
{
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    private float speed = 0;
    private float range = 0;
    private float timer = 0;
    private Direction dir;


    // 얘는 Player에서 알아서 해줄 거임.
    public void SetDefault(float speed, float range, string dir)
    {
        this.speed = speed;
        this.range = range;
        this.dir = (Direction)Enum.Parse(typeof(Direction), dir);
    }

    public void Movement()
    {
        if (dir == Direction.UP)
            transform.position += Vector3.up * speed * Time.deltaTime;
        else if(dir == Direction.DOWN)
            transform.position += Vector3.down * speed * Time.deltaTime;
        else if (dir == Direction.LEFT)
            transform.position += Vector3.left * speed * Time.deltaTime;
        else if (dir == Direction.RIGHT)
            transform.position += Vector3.right * speed * Time.deltaTime;
    }
    IEnumerator Delete()
    {
        yield return new WaitForSeconds(range);
        // 작동 안할 거 같은데
        WeaponObjPool.Instance.ReturnObject(gameObject);
    }
    //void Delete()
    //{
    //   if (timer > range)
    //    {
    //        Destroy(this.gameObject);
    //        timer = 0;
    //    }

    //    timer += Time.deltaTime;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyMovement>().hp -= 50;
            WeaponObjPool.Instance.ReturnObject(gameObject);
        } 
    }
    public void StartAction()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.BasicWeaponSound);
        StartCoroutine(Delete()); 
    }
    // Start is called before the first frame update
    void Start()
    {
        //SoundManager.Instance.PlaySound(SoundManager.Instance.BasicWeaponSound);
        //StartCoroutine(Delete()); 
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
}
