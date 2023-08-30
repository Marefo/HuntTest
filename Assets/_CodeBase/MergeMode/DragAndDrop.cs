using System.Collections.Generic;
using System.Linq;
using _CodeBase.Infrastructure.Services;
using _CodeBase.MergeMode.AnimalCode;
using _CodeBase.MergeMode.AnimalCode.StaticData;
using UnityEngine;
using Zenject;

namespace _CodeBase.MergeMode
{
  public class DragAndDrop : MonoBehaviour
  {
    [SerializeField] private LayerMask _planeLayer;
    [SerializeField] private LayerMask _cellLayer;
    [SerializeField] private Transform _dragPointObj;
    [Space(10)] 
    [SerializeField] private AnimalsData _animalsData;
    
    private InputService _inputService;
    private Field _field;
    private Camera _mainCamera;
    private bool _isTouching;
    private Cell _draggingStartCell;
    private MergeAnimal _draggingAnimal;
    private List<Cell> _cellsWithSameLvl = new List<Cell>();

    [Inject]
    private void Construct(InputService inputService, Field field)
    {
      _inputService = inputService;
      _field = field;
    }

    private void Awake()
    {
      _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
      _inputService.TouchEntered += OnTouchEnter;
      _inputService.TouchCanceled += OnTouchCancel;
    }

    private void OnDisable()
    {
      _inputService.TouchEntered -= OnTouchEnter;
      _inputService.TouchCanceled -= OnTouchCancel;
    }

    private void Update()
    {
      if(_isTouching == false) return; 
      MoveDragPointObj();
    }

    private void OnDrawGizmos()
    {
      Gizmos.color = Color.yellow;
      Gizmos.DrawWireSphere(_dragPointObj.position, 0.15f);
    }

    private void OnTouchEnter()
    {
      _isTouching = true;
      TryStartDraggingAnimal();
    }

    private void OnTouchCancel()
    {
      _isTouching = false;

      if (_draggingAnimal != null)
        HandleAnimalDropping();
    }

    private void MoveDragPointObj()
    {
      bool isHitPlane = Physics.Raycast(GetRayFromTouch(), out RaycastHit hit, 100, _planeLayer);
      if(isHitPlane == false) return;
      _dragPointObj.position = hit.point;
    }

    private void TryStartDraggingAnimal()
    {
      Cell cell = TryHitCell();
      if (cell == null || cell.HasAnimal == false) return;
      
      _draggingStartCell = cell;
      _draggingAnimal = cell.Animal;
      
      _draggingAnimal.transform.SetParent(_dragPointObj);
      _draggingAnimal.transform.localPosition = Vector3.zero + _draggingAnimal.Offset;
      _draggingAnimal.OnStartDragging();
      
      HighlightCellsToMerge();
    }

    private void HighlightCellsToMerge()
    {
      if(_draggingAnimal.Lvl == _animalsData.MaxAnimalLvl) return;
      _cellsWithSameLvl = _field.GetCellsByLvl(_draggingAnimal.Lvl)
        .Where(cellWithSameLvl => cellWithSameLvl != _draggingStartCell).ToList();
      _cellsWithSameLvl.ForEach(cellWithSameLvl => cellWithSameLvl.EnableHighlight());
    }

    private void HandleAnimalDropping()
    {
      Cell cell = TryHitCell();
      _draggingStartCell.ResetAnimal();

      if (cell != null)
        HandleAnimalDroppingOnCell(cell);
      else
        ResetDraggingAnimalToDefaultCell();

      _cellsWithSameLvl.ForEach(cellWithSameLvl => cellWithSameLvl.DisableHighlight());
      _cellsWithSameLvl.Clear();
      _draggingAnimal.OnFinishDragging();
      _draggingStartCell = null;
      _draggingAnimal = null;
    }

    private void HandleAnimalDroppingOnCell(Cell cell)
    {
      if (cell.HasAnimal == false)
      {
        cell.SetAnimal(_draggingAnimal);
      }
      else
      {
        if (_draggingAnimal.Lvl != _animalsData.MaxAnimalLvl && _draggingAnimal.Lvl == cell.Animal.Lvl)
          _field.MergeAnimals(_draggingAnimal, cell);
        else
          ResetDraggingAnimalToDefaultCell();
      }
    }

    private void ResetDraggingAnimalToDefaultCell() => _draggingStartCell.SetAnimal(_draggingAnimal);

    private Cell TryHitCell()
    {
      bool isHitCell = Physics.Raycast(GetRayFromTouch(), out RaycastHit hit, 100, _cellLayer);
      if (isHitCell == false) return null;
      Cell cell = hit.transform.GetComponent<Cell>();
      return cell;
    }
    
    private Ray GetRayFromTouch() => _mainCamera.ScreenPointToRay(Input.mousePosition);
  }
}