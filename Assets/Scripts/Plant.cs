using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    // Start is called before the first frame update
    public float health = 100;
    protected float currentHealth;
    protected Animator animator;
    protected bool start;
    protected BoxCollider2D boxCollider2D;
    protected virtual void Start()
    {
        currentHealth = health;
        start = false;
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator.speed = 0;
        boxCollider2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ��ֲ��ɺ�����ֲ��
    public void SetPlantStart() 
    {
        start = true;
        animator.speed = 1;
        boxCollider2D.enabled = true;
    }

    // �ı�Ѫ���ķ�����ȡ��������
    public virtual float ChangeHealth(float num)
    {

        currentHealth = Mathf.Clamp(currentHealth + num, 0, health);
        print(currentHealth);
        if (currentHealth <= 0)
        {
            GameObject.Destroy(gameObject);
        }
        return currentHealth;
    }
}
