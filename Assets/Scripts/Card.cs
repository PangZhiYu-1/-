using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
    public GameObject objectPrefab;  // ��Ƭ��Ӧ������Ԥ�Ƽ�
    private GameObject curGameObject;  // ��¼��ǰ��������������
    private GameObject darkBg;
    private GameObject progressBar;
    public float waitTime;
    public int useSun;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        // ��һ�ֻ�ȡ����ķ�ʽ����ȡ������
        darkBg = transform.Find("dark").gameObject;
        progressBar = transform.Find("progress").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        UpdateProgress();
        UpdateDarkBg();
    }

    void UpdateProgress()
    {
        float per = Mathf.Clamp(timer / waitTime, 0, 1);
        progressBar.GetComponent<Image>().fillAmount = 1 - per;
    }

    void UpdateDarkBg()
    {
        // ����ʱ����������̫�������㹻
        if (progressBar.GetComponent<Image>().fillAmount == 0 && GameManager.instance.sunNum >= useSun)
        {
            darkBg.SetActive(false);
        }
        else
        {
            darkBg.SetActive(true);
        }
    }



    // ��ק��ʼ�������µ�һ˲�䣩
    public void OnBeginDrag(BaseEventData data)
    {
        // �ж��Ƿ������ֲ, ѹ�ڴ������޷���ֲ
        if (darkBg.activeSelf)
        {
            return;
        }
        PointerEventData pointerEventData = data as PointerEventData;
        curGameObject = Instantiate(objectPrefab);
        curGameObject.transform.position = TranlateScreenToWorld(pointerEventData.position);
    }

    // ��ק���̣���갴��û�ſ���
    public void OnDrag(BaseEventData data)
    {
        if (curGameObject == null)
        {
            return;
        }
        PointerEventData pointerEventData = data as PointerEventData;
        // ��������ƶ���λ�ö�Ӧ�ƶ�����
        curGameObject.transform.position = TranlateScreenToWorld(pointerEventData.position);

    }

    // ��ק���������ſ���һ˲�䣩
    public void OnEndDrag(BaseEventData data)
    {
        if (curGameObject == null)
        {
            return;
        }
        PointerEventData pointerEventData = data as PointerEventData;
        // �õ��������λ�õ���ײ��
        Collider2D[] col = Physics2D.OverlapPointAll(TranlateScreenToWorld(pointerEventData.position));
        // ������ײ��
        foreach (Collider2D c in col)
        {
            // �ж�����Ϊ�����ء�������������û������ֲ��
            if (c.tag == "Land" && c.transform.childCount == 0)
            {
                // �ѵ�ǰ�������Ϊ���ص�������
                curGameObject.transform.parent = c.transform;
                curGameObject.transform.localPosition = Vector3.zero;
                // ����Ĭ��ֵ�����ɽ���
                curGameObject.GetComponent<Plant>().SetPlantStart();
                curGameObject = null;
                GameManager.instance.ChangeSunNum(-useSun);
                timer = 0;
                break;
            }
        }
        // ���û�з������������أ���curGameObject�������ţ���ô������.
        if (curGameObject != null)
        {
            GameObject.Destroy(curGameObject);
            curGameObject = null;
        }
    }

    public static Vector3 TranlateScreenToWorld(Vector3 position)
    {
        Vector3 cameraTranslatePos = Camera.main.ScreenToWorldPoint(position);
        return new Vector3(cameraTranslatePos.x, cameraTranslatePos.y, 0);
    }

}
