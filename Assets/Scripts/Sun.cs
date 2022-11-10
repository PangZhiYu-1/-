using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    // Start is called before the first frame update
    public float duration;
    private float timer;
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > duration)
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        print("OnMouseDown: Sun");
        // TODO: �ɵ�UI̫������λ�ã�Ȼ������
        GameObject.Destroy(gameObject);
        // �����������������
        GameManager.instance.ChangeSunNum(25);
    }
}