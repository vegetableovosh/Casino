using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Rocket : MonoBehaviour, IntefaceGame
{
    public GameObject RocketPrefab;
    private GameObject movingObject; // Объект, который будет двигаться по параболе
    public float speed = 1.0f; // Скорость движения по параболе
    public float amplitude = 1.0f; // Амплитуда параболы
    public float explosionMinTime = 2.0f; // Минимальное время до взрыва
    public float explosionMaxTime = 10.0f; // Максимальное время до взрыва
    public LineRenderer lineRenderer; // LineRenderer для рисования линии


    private Vector3 startPosition;
    private float elapsedTime;
    private float explosionTime;
    private List<Vector3> positions = new List<Vector3>();

    private Mesh mesh;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;

    private float rotationChangeInterval; // Интервал времени для изменения угла
    private float nextRotationChangeTime;
    private float currentRotationAngle; // Текущий угол поворота
    private float targetRotationAngle; // Целевой угол поворота
    private bool isActive = false;
    private TextMeshProUGUI textX;

    private void init()
    {
        if (movingObject != null || meshRenderer != null)
        {
            Explode();
            ClearLineAndMesh();
        }
        movingObject = Instantiate(RocketPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        if (movingObject == null)
        {
            Debug.LogError("Moving Object is not assigned.");
            enabled = false;
            return;
        }

        startPosition = new Vector3(0, 1, 0);
        elapsedTime = 0.0f;
        positions = new List<Vector3>();
        explosionTime = Random.Range(explosionMinTime, explosionMaxTime);
        nextRotationChangeTime = rotationChangeInterval;
        rotationChangeInterval = 3.0f;
        currentRotationAngle = -75.0f;
        targetRotationAngle = currentRotationAngle;

        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        lineRenderer.positionCount = 0;
        lineRenderer.sortingOrder = -1;

        // Create mesh components
        GameObject meshObject = new GameObject("AreaMesh");
        meshRenderer = meshObject.AddComponent<MeshRenderer>();
        meshFilter = meshObject.AddComponent<MeshFilter>();
        mesh = new Mesh();
        meshFilter.mesh = mesh;

        // Set material for the mesh
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
        meshRenderer.material.color = new Color32(138, 124, 228, 140);
        meshRenderer.sortingOrder = -2; // Ниже по порядку сортировки
        textX = GameObject.Find("TextX").GetComponent<TextMeshProUGUI>();
    }

    public void RunMain()
    {
        init();
        isActive = true;
    }

    public void StopGame()
    {
        isActive = false;
        float x = movingObject != null ? movingObject.transform.position.y : 0;
        this.GetComponent<Bet>().Return_rate(x);
    }

    void Update()
    {
        if(isActive){
        MakeNewPosition();
            textX.text = movingObject.transform.position.y.ToString("#.00" + "X");
        if (elapsedTime >= explosionTime || movingObject.transform.position.y >= 8)
        {
            Explode();
        }
        UpdateMesh();
        }

    }

    void MakeNewPosition()
    {
        if (movingObject == null) return;

        elapsedTime += Time.deltaTime;

        float x = elapsedTime * speed;
        float y = amplitude * Mathf.Pow(x, 2);
        Vector3 newPosition = new Vector3(startPosition.x + x, startPosition.y + y, startPosition.z);
        movingObject.transform.position = newPosition;

        RotateRocket();
        positions.Add(newPosition);
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());
    }

    void RotateRocket()
    {
        // Обновление угла поворота плавно каждые 5 секунд на 10 градусов
        if (elapsedTime >= nextRotationChangeTime)
        {
            targetRotationAngle = Mathf.Min(currentRotationAngle + 10.0f, 0.0f); // Обновляем целевой угол
            nextRotationChangeTime += rotationChangeInterval;
        }

        // Плавное изменение угла поворота
        currentRotationAngle = Mathf.Lerp(currentRotationAngle, targetRotationAngle, Time.deltaTime / rotationChangeInterval);
        movingObject.transform.rotation = Quaternion.Euler(0, 0, currentRotationAngle);
    }

    void Explode()
    {
        if (isActive)
            this.GetComponentInChildren<StopBet>().Stop();
        Debug.Log("Boom! Object exploded.");
        isActive = false;
        Destroy(movingObject);
    }

    void UpdateMesh()
    {
        if (positions.Count < 2) return;

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        for (int i = 0; i < positions.Count; i++)
        {
            Vector3 currentPosition = positions[i];
            vertices.Add(new Vector3(currentPosition.x, 0, currentPosition.z)); // точка на оси X
            vertices.Add(new Vector3(currentPosition.x, currentPosition.y, currentPosition.z)); // точка на траектории

            if (i < positions.Count - 1)
            {
                int baseIndex = i * 2;
                triangles.Add(baseIndex);
                triangles.Add(baseIndex + 1);
                triangles.Add(baseIndex + 2);

                triangles.Add(baseIndex + 1);
                triangles.Add(baseIndex + 3);
                triangles.Add(baseIndex + 2);
            }
        }

        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }

    public void ClearLineAndMesh()
    {
        positions.Clear();
        lineRenderer.positionCount = 0;
        mesh.Clear();
    }

    public void ChangeAreaColor(Color newColor)
    {
        if (meshRenderer != null)
        {
            meshRenderer.material.color = newColor;
        }
    }

    public void ChangeAreaMaterial(Material newMaterial)
    {
        if (meshRenderer != null)
        {
            meshRenderer.material = newMaterial;
        }
    }

    void OnDrawGizmos()
    {
        if (positions.Count < 2) return;

        Gizmos.color = Color.green;
        for (int i = 1; i < positions.Count; i++)
        {
            Vector3 previousPosition = positions[i - 1];
            Vector3 currentPosition = positions[i];
            //Gizmos.DrawLine(new Vector3(previousPosition.x, 0, previousPosition.z), new Vector3(currentPosition.x, 0, currentPosition.z));
        }

        // Рисование площади
        Gizmos.color = Color.red;
        for (int i = 0; i < positions.Count; i++)
        {
            Vector3 currentPosition = positions[i];
            //Gizmos.DrawLine(new Vector3(currentPosition.x, 0, currentPosition.z), new Vector3(currentPosition.x, currentPosition.y, currentPosition.z));
        }
    }
}
