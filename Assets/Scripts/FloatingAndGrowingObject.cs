using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FloatingAndGrowingObject : MonoBehaviour
{
    public float speed = 50f; // Скорость подъема объекта
    public float growthRate = 0.1f; // Скорость увеличения размера объекта
    public float fadeDuration = 1.0f; // Время для полного исчезновения объекта

    private float elapsedTime = 0f; // Время с момента создания объекта
    private TextMeshProUGUI image; // Ссылка на компонент Image

    [SerializeField]
    private AudioClip textAudio;
    void Start()
    {
        // Получаем компонент Image
        image = GetComponent<TextMeshProUGUI>();
        if (image.text != "")
            this.GetComponent<AudioSource>().PlayOneShot(textAudio);
    }

    void Update()
    {
        // Перемещение объекта вверх
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Увеличение размера объекта
        transform.localScale += Vector3.one * growthRate * Time.deltaTime;

        // Увеличиваем время, прошедшее с момента создания объекта
        elapsedTime += Time.deltaTime;

        // Изменяем прозрачность объекта
        if (image != null)
        {
            Color color = image.color;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            image.color = color;
        }

        // Уничтожаем объект, если он полностью исчез
        if (elapsedTime >= fadeDuration)
        {
            Destroy(gameObject);
        }
    }
}