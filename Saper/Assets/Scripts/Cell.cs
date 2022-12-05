using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class Cell : MonoBehaviour, IPointerClickHandler
{
    private bool _isBlocked;
    public bool IsBlocked => _isBlocked;

    protected Button CellButton;
    protected (int, int) Index;

    public Minefield Minefield { get; set; }

    private void Awake()
    {
        CellButton = GetComponent<Button>();
    }

    private void ChangeBlockState()
    {
        _isBlocked = !_isBlocked;

        CellButton.image.color = _isBlocked ? Color.gray : Color.white;
    }

    protected abstract void OpenCell();

    public void SetCellIndex((int, int) index)
    {
        Index = index;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //if (CellButton.interactable == false)
        //    return;

        if (eventData.button == PointerEventData.InputButton.Right)
            ChangeBlockState();

        if (eventData.button == PointerEventData.InputButton.Left && _isBlocked == false)
            OpenCell();
    }
}
