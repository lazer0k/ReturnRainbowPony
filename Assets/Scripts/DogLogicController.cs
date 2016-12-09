using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DogLogicController : MonoBehaviour
{

    public PonyCreationController PonyCreation; // Ссылка на клас - создания понни, нужна для подсчёта активных понни
    private List<Transform> _ponyTransformList = new List<Transform>(); // Лист текущих понни, которые бегут за сабакой

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Передвижение собаки, принимает Вектор 3 (можно и по стрелочкам и по клику мышки и как хочешь)
    /// </summary>
    /// <param name="newPos"></param>
    public void MoveDogWithPony(Vector3 newPos)
    {
        Vector3 lastPos = transform.position;
        transform.position += newPos;
        newPos = transform.position;



        for (int i = 0; i < _ponyTransformList.Count; i++)
        {
            float movedDistance = Vector3.Distance(_ponyTransformList[i].position, newPos) - Vector3.Distance(_ponyTransformList[i].position, lastPos);
            lastPos = _ponyTransformList[i].position;
            _ponyTransformList[i].position = Vector3.MoveTowards(_ponyTransformList[i].position, newPos, movedDistance);
            newPos = _ponyTransformList[i].position;

        }
    }

    /// <summary>
    /// Добавление понни к группе с собакой
    /// </summary>
    /// <param name="ponyTransform"></param>
    private void _addNewPony(Transform ponyTransform)
    {

        ponyTransform.SetParent(transform.parent);
        if (_ponyTransformList.Count == 0) // установка на позицию в  колонне. Не хотел писать километровую строчку - разбил на 3
        {
            ponyTransform.position = transform.position + (Vector3.down * 30);
        }
        else
        {
            if (_ponyTransformList.Count > 1)
            {
                ponyTransform.position = _ponyTransformList.Last().position +
                                         (_ponyTransformList.Last().position -
                                          _ponyTransformList[_ponyTransformList.Count - 2].position);
            }
            else
            {
                ponyTransform.position = _ponyTransformList.Last().position +
                                       (_ponyTransformList.Last().position -
                                        transform.position);

            }
        }
        _ponyTransformList.Add(ponyTransform);
    }


    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.name.Contains("Pony"))
        {
            coll.enabled = false;
            _addNewPony(coll.transform);
        }

        if (coll.gameObject.name.Equals("SafeZone")) // Заходим в зону - оставляем овечек там
        {
          
            for (int i = 0; i < _ponyTransformList.Count; i++)
            {
                PonyCreation.PonyCatch();
                PoolBoss.Despawn(_ponyTransformList[i]);
            }
            _ponyTransformList = new List<Transform>();
        }

    }
}
