using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Escape.Levels.Level2
{
    public class PuzzleManager : MonoBehaviour
    {
        [Range(2, 6)]
        [SerializeField] private int Difficulty = 4;
        [SerializeField] private Texture2D Puzzle;
        [SerializeField] private Transform GameHolder;
        [SerializeField] private Transform PiecePrefab;
        [SerializeField] private GameManager_Escape GameManager;
        [SerializeField] private PlayerInput Input;
        [SerializeField] private GameObject Printer3D;
        [SerializeField] private BoxCollider PuzzleSulBancone;
        [SerializeField] private SwitchCameraObject SwitchCameraObject;
        [SerializeField] private ObjectCallMethods ObjectCallMethodsCanvas;
        [SerializeField] private MethodListener PuzzleMethodListener;
        [SerializeField] private LevelHint LevelHint;
        [SerializeField] private int HintNumber;
        [SerializeField] private GameObject SecondDialog;
        private List<PuzzlePiece> _pieces;
        private int _sectionX;
        private int _sectionY;
        private Vector2Int _dimensions;
        private int _piecesCorrect;

        public void EndDrag(PuzzlePiece pieceDragged, Vector3 releasePoint)
        {
            Snap(pieceDragged, releasePoint);
        }
        private void Snap(PuzzlePiece piece, Vector3 releasePoint)
        {
            var piecePosition = new Vector3((int) Math.Round(releasePoint.x / _sectionX) * _sectionX,
                (int) Math.Round(releasePoint.y / _sectionY) * _sectionY, 0f);
            if (_dimensions.x % 2 == 0)
            {
                // ReSharper disable once PossibleLossOfFraction
                piecePosition.x += _sectionX / 2 * (releasePoint.x < 0 ? -1 : 1);
            }

            if (_dimensions.y % 2 == 0)
            {
                // ReSharper disable once PossibleLossOfFraction
                piecePosition.y += _sectionY / 2 * (releasePoint.y < 0 ? -1 : 1);
            }
            var newPosition = piece.SetPosition(piecePosition);
            if (newPosition == piece.GetTargetPosition())
            {
                piece.DisableDragging();
                _piecesCorrect++;
                if (_piecesCorrect == _pieces.Count)
                {
                    PuzzleCompleted();
                }
            }
            piece.transform.SetAsFirstSibling();
        }
        private void PuzzleCompleted()
        {
            foreach (var piece in _pieces)
            {
                Destroy(piece.gameObject);
            }
            GameManager.isSeeing = true;
            Input.enabled = true;
            Input.SwitchCurrentActionMap("Player");
            ObjectCallMethodsCanvas.CloseInterface();
            SwitchCameraObject.ReturnToPrimaryCamera();
            SwitchCameraObject.isInteractable = false;
            SwitchCameraObject.isActive = false;
            gameObject.SetActive(false);
            DatiPersistenti.istanza.methodsListeners.Add(PuzzleMethodListener.methodListenerID);
            Printer3D.SetActive(true);
            GameManager.printer = Printer3D.GetComponent<Printer3DController>();
            LevelHint.nextHint(HintNumber);
            SecondDialog.SetActive(true);
        }
        private void OnEnable()
        {
            StartPuzzle();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void StartPuzzle()
        {
            _pieces = new List<PuzzlePiece>();
            _dimensions = GetDimensions();
            _piecesCorrect = 0;
            CreatePuzzlePieces();
            Scatter();
        }
        private Vector2Int GetDimensions()
        {
            var dimensions = Vector2Int.zero;
            if (Puzzle.width < Puzzle.height)
            {
                dimensions.x = Difficulty;
                dimensions.y = Difficulty * Puzzle.height / Puzzle.width;
            }
            else
            {
                dimensions.x = Difficulty * Puzzle.width / Puzzle.height;
                dimensions.y = Difficulty;
            }
            return dimensions;
        }

        private void CreatePuzzlePieces()
        {
            _sectionX = Puzzle.width / _dimensions.x;
            _sectionY = Puzzle.height / _dimensions.y;
            for (var row = 0; row < _dimensions.x; row++)
            {
                for (var col = 0; col < _dimensions.y; col++)
                {
                    var puzzlePiece = Instantiate(PiecePrefab, GameHolder).gameObject.GetComponent<PuzzlePiece>();
                    puzzlePiece.SetPuzzleManager(this);
                    puzzlePiece.SetObjectName($"Piece {col * _dimensions.x + row}");
                    puzzlePiece.SetScale(new Vector3(_sectionX, _sectionY));
                    puzzlePiece.ComputeTargetPosition(row, col, _sectionX, _sectionY, _dimensions);
                    puzzlePiece.GenerateTexture(Puzzle, _sectionX, _sectionY, row, col);
                    _pieces.Add(puzzlePiece);
                }
            }
        }
        private void Scatter()
        {
            foreach (var piece in _pieces)
            {
                piece.SetPosition(new Vector3(
                    Random.Range((_sectionX - Puzzle.width) / 2, (Puzzle.width - _sectionX) / 2),
                    Random.Range((_sectionY - Puzzle.height) / 2, (Puzzle.height - _sectionY) / 2), 0f));
            }
        }
    }
}
