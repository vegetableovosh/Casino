using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FloatingAndGrowingObject : MonoBehaviour
{
    public float speed = 50f; // �������� ������� �������
    public float growthRate = 0.1f; // �������� ���������� ������� �������
    public float fadeDuration = 1.0f; // ����� ��� ������� ������������ �������

    private float elapsedTime = 0f; // ����� � ������� �������� �������
    private TextMeshProUGUI image; // ������ �� ��������� Image

    [SerializeField]
    private AudioClip textAudio;
    void Start()
    {
        // �������� ��������� Image
        image = GetComponent<TextMeshProUGUI>();
        if (image.text != "")
            this.GetComponent<AudioSource>().PlayOneShot(textAudio);
    }

    void Update()
    {
        // ����������� ������� �����
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // ���������� ������� �������
        transform.localScale += Vector3.one * growthRate * Time.deltaTime;

        // ����������� �����, ��������� � ������� �������� �������
        elapsedTime += Time.deltaTime;

        // �������� ������������ �������
        if (image != null)
        {
            Color color = image.color;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            image.color = color;
        }

        // ���������� ������, ���� �� ��������� �����
        if (elapsedTime >= fadeDuration)
        {
            Destroy(gameObject);
        }
    }
}