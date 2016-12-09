using UnityEngine;

public class MoveController : MonoBehaviour
{

    public DogLogicController[] DogLogics;

    public float Speed = 100;

    private int _currentDogId = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Смена собаки
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            _currentDogId++;
            if (_currentDogId >= DogLogics.Length)
            {
                _currentDogId = 0;
            }
        }

        // Управление собакой

        if (Input.GetKey(KeyCode.DownArrow))
        {
            DogLogics[_currentDogId].MoveDogWithPony(Vector3.down * Time.deltaTime * Speed);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            DogLogics[_currentDogId].MoveDogWithPony(Vector3.up * Time.deltaTime * Speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            DogLogics[_currentDogId].MoveDogWithPony(Vector3.left * Time.deltaTime * Speed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            DogLogics[_currentDogId].MoveDogWithPony(Vector3.right * Time.deltaTime * Speed);
        }


    }
}
