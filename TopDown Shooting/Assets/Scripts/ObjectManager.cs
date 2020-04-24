using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager: MonoBehaviour
{
    //pool 생성
    GameObject[] rifle_bullet;
    GameObject[] targetPool;

    public GameObject rifle_bullet_Prefab = null;

    void Start()
    {
        rifle_bullet = new GameObject[100];
        Generate();
    }

    void Generate()
    {
        for (int i = 0; i < rifle_bullet.Length; i++) // Pool에 프리팹 저장후 setActive(false)
        {
            rifle_bullet[i] = Instantiate(rifle_bullet_Prefab);
            rifle_bullet[i].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "rifle_bullet":
                targetPool = rifle_bullet;
                break;

                // --> 오브젝트추가
        }

        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }
        return null;
    }

    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "rifle_bullet":
                targetPool = rifle_bullet;
                break;

                // --> 추가
        }
        return targetPool;
    }



}
