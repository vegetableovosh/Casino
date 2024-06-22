using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IPointerClickHandler
{
    private MineSweeper gameController;
    private int index;

    public void Setup(MineSweeper controller, int i)
    {
        gameController = controller;
        index = i;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gameController.OnCellClick(index);
    }
}
