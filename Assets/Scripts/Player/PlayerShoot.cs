using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform SpawnTransform; // точка откуда начинается выстрел
    [SerializeField] private float AngleInDegrees; // угол выстрела

    [SerializeField] private GameObject _bullet; // префаб пули
    
    private List<GameObject> enemies = new List<GameObject>();
    
    private Move move;
    private GameObject currentEnemy;
    
    private bool isAttack = false;
    
    public float fireRate = 2f; // Задержка между выстрелами
    private float nextFire = 0.0f; // Время следующего выстрела
    private float g = Physics.gravity.y; // ускорение свободного падения, будет равна 9,8
    
    public float FireRate
    {
        get => fireRate;
        set => fireRate = value;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            currentEnemy = other.gameObject;
            enemies.Add(other.gameObject);
        }
    }
    private void Start()
    {
        move = FindObjectOfType<Move>();
    }
    private void Update()
    {
        if (currentEnemy != null && Time.time > nextFire)
        {
            isAttack = true;
            move.IsRun = false;
            if (isAttack)
            {
                StartCoroutine(Shoot());
                nextFire = Time.time + fireRate; // Изменение задержки между выстрелами
            }
        }
        if (currentEnemy != null && !currentEnemy.activeInHierarchy)
        {
            enemies.Remove(currentEnemy);
            if (enemies.Count > 0)
            {
                currentEnemy = enemies[0];
            }
            else if (enemies.Count == 0)
            {
                isAttack = false;
                move.IsRun = true;
            }
        }
    }
    
    private IEnumerator Shoot()
    {
        if (enemies.Count != 0)
        {
            SpawnTransform.localEulerAngles = new Vector3(-AngleInDegrees, 0f, 0f); //вращение пушки вверх, вниз
            Vector3 fromTo =
                enemies[0].transform.position -
                transform.position; // вектор направления от стреляющего объекта к цели
            Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z); // проекция вектора только по осям x,z

            transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up); // поворот вслед за целью

            float x = fromToXZ.magnitude - 1.1f; // расстояние от стреляющего, до цели в горизонтальной плоскости
            float y = fromTo.y; // координата y 

            float AngleInRadians = AngleInDegrees * Mathf.PI / 180; // перевод градусов в радианы, для тангенса 

            float v2 = (g * x * x) /
                       (2 * (y - Mathf.Tan(AngleInRadians) * x) *
                        Mathf.Pow(Mathf.Cos(AngleInRadians),
                            2)); // формула для нахождения v в квадрате рассчет скорости
            float
                v = Mathf.Sqrt(Mathf
                    .Abs(v2)); // извлечение корня, корень из отрицательного числа нельзя вычеслять, поэтому нужно брать модуль v2

            GameObject newBullet = Instantiate(_bullet, SpawnTransform.position, Quaternion.identity); // создание пули
            newBullet.GetComponent<Rigidbody>().velocity = SpawnTransform.forward * v; // задаем скорость пули
            yield return new WaitForSeconds(fireRate);
        }
    }
}