using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public int sunNum;

    public GameObject bornParent;
    public GameObject zombiePrefab;
    public float createZombieTime;
    void Start()
    {
        instance = this;

        UIManager.instance.InitUI();

        CreateZombie();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeSunNum(int changeNum)
    {
        sunNum += changeNum;
        if (sunNum <= 0)
        {
            sunNum = 0;
        }
        // �������������ı䣬֪ͨ��Ƭѹ�ڵ�...
        UIManager.instance.UpdateUI();

    }

    public void CreateZombie()
    {
        StartCoroutine(DalayCreateZombie());
    }

    // �ȴ�һ��ʱ��������������һֻ��ʬ
    IEnumerator DalayCreateZombie()
    {
        // �ȴ�
        yield return new WaitForSeconds(createZombieTime);

        // ����
        GameObject zombie = Instantiate(zombiePrefab);
        int index = Random.Range(0, 5);
        Transform zombieLine = bornParent.transform.Find("born" + index.ToString());
        zombie.transform.parent = zombieLine;
        zombie.transform.localPosition = Vector3.zero;

        // �ٴ�������ʱ��
        StartCoroutine(DalayCreateZombie());
    }
}
