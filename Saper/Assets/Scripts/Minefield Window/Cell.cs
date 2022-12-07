using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class Cell : MonoBehaviour, IPointerClickHandler
{
    private bool _isBlocked;

    private bool _isOpened;
    public bool IsOpened => _isOpened;

    public Button CellButton { get; private set; }
    public (int, int) CellIndex { get; set; }

    public Minefield Minefield { get; set; }

    private void Awake()
    {
        CellButton = GetComponent<Button>();
    }
   
    public void OpenCell()
    {
        if (_isOpened)
            return;

        _isOpened = true;

        CellButton.interactable = false;

        OpeningCell();
    }

    public virtual void Reset()
    {
        _isBlocked = false;
        _isOpened = false;
        CellButton.image.color = Color.white;
        CellButton.interactable = true;
    }

    protected abstract void OpeningCell();

    private void ChangeBlockState()
    {
        _isBlocked = !_isBlocked;

        CellButton.image.color = _isBlocked ? Color.gray : Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CellButton.interactable == false)
            return;

        if (eventData.button == PointerEventData.InputButton.Right)
            ChangeBlockState();

        if (eventData.button == PointerEventData.InputButton.Left && _isBlocked == false)
            OpenCell();
    }
}
