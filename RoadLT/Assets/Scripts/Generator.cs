using System;
using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour
{

    public Transform[] partOfRoad;
    public GameObject car;
    

	// Use this for initialization
	void Awake ()
	{
        //Надо узнать какой метод выполняется в первую очередь и туда запихнуть получение машины
	    //car = Resources.Load("Car") as GameObject;
        Spawn();

    }
    
	// Update is called once per frame
	void Update () {
	
	}
    //instantiate all trass objects 
    void Spawn()
    {
        float lengthOfSprite = 0;
        for (int i = 0; i < partOfRoad.Length; i++)
        {
            float tmpSize = partOfRoad[i].GetComponent<RectTransform>().sizeDelta.x;
            Instantiate(partOfRoad[i], new Vector3(lengthOfSprite, 0, 0), Quaternion.identity);
            lengthOfSprite = lengthOfSprite + tmpSize;

            //тут пытался получить вершины каждого элемента дороги, надо будет додумать этот участок кода
            //for (int j = 0; j < partOfRoad[i].GetComponent<PolygonCollider2D>().points.Length; j++)
            //{
            //    foreach (var point in partOfRoad[i].GetComponent<PolygonCollider2D>().points)
            //    {
            //        Debug.Log(point.x);
            //    }
            //}
            
        }



        Instantiate(car, new Vector3(0, 5, 0), Quaternion.identity);
    }
}
