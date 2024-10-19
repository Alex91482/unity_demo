using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	//конпка выхода из игры
	//public List<KeyCode> exsitButton;
	
	// Переменные для вывода на экран
	private int score = 0;
	private int waveNumber = 0;
	// Ссылки на текстовые объекты
	public TextMesh scoreText;
	public TextMesh waveText;
	
	// Создание переменной «враг»
	public Transform enemy;
	// Временные промежутки между событиями, кол-во врагов
	public float timeBeforeSpawning = 1.5f;
	public float timeBetweenEnemies = 0.25f;
	public float timeBeforeWaves = 2.0f;
	public int enemiesPerWave = 10;
	private int currentNumberOfEnemies = 0;
	
	// Start is called before the first frame update
	void Start(){
		StartCoroutine (SpawnEnemies());
	}

	// Update is called once per frame
	void Update(){
		if (Input.GetKey("escape"))  // если нажата клавиша Esc (Escape)
		{
			Application.Quit();    // закрыть приложение
		}
	}
	
	// Появление волн врагов
	IEnumerator SpawnEnemies()
	{
		// Начальная задержка перед первым появлением врагов
		yield return new WaitForSeconds (timeBeforeSpawning);
		// Когда таймер истекёт, начинаем производить эти действия
		while(true)
		{
			// Не создавать новых врагов, пока не уничтожены старые
			if (currentNumberOfEnemies <= 0)
			{
				waveNumber++;
				waveText.text = "Waves: " + waveNumber; 
				float randDirection;
				float randDistance;
				// Создать 10 врагов в случайных местах за экраном
				for (int i = 0; i < enemiesPerWave; i++)
					{
					// Задаём случайные переменные для расстояния и направления
					randDistance = Random.Range (10, 25);
					randDirection = Random.Range (0, 360);
					// Используем переменные для задания координат появления врага
					float posX = this. transform.position.x + (Mathf.Cos((randDirection) * Mathf.Deg2Rad) * randDistance);
					float posY = this. transform.position.y + (Mathf.Sin((randDirection) * Mathf.Deg2Rad) * randDistance);
					// Создаём врага на заданных координатах
					Instantiate (enemy, new Vector3 (posX, posY, 0), this.transform.rotation);
					currentNumberOfEnemies++;
					yield return new WaitForSeconds (timeBetweenEnemies);
				}
			}
			// Ожидание до следующей проверки
			yield return new WaitForSeconds (timeBeforeWaves);
		}
	}
	
	// Процедура уменьшения количества врагов в переменной
	public void KilledEnemy()
	{
		currentNumberOfEnemies--;
	}
	
	public void IncreaseScore(int increase)
	{
		score += increase;
		scoreText.text = "Score: " + score;
	}
}
