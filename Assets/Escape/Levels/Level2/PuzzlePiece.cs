using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Escape.Levels.Level2
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Image))]
    public class PuzzlePiece : MonoBehaviour
    {
        private Vector3 _targetPosition;
        private RectTransform _rectTransform;
        private Image _image;
        private PuzzleManager _manager;
        private Vector3 _lastPosition;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _image = GetComponent<Image>();
        }
        public void SetScale(Vector2 scale)
        {
            _rectTransform.sizeDelta = scale;
        }
        public void SetObjectName(string objName)
        {
            gameObject.name = objName;
        }
        public Vector3 GetTargetPosition()
        {
            return _targetPosition;
        }
        public void ComputeTargetPosition(int row, int col, int sectionX,
            int sectionY, Vector2Int dimensions)
        {
            var xPos = row * sectionX - dimensions.x / 2 * sectionX;
            if (dimensions.x % 2 == 0) xPos += sectionX / 2;
            var yPos = col * sectionY - dimensions.y / 2 * sectionY;
            if (dimensions.y % 2 == 0) yPos += sectionY / 2; 
            _targetPosition = new Vector3(xPos, yPos, 0f);
        }
        public void SetPosition(Vector3 pos)
        {
            _rectTransform.localPosition = pos;
        }
        public void GenerateTexture(Texture2D puzzleTexture, int sectionX, int sectionY, int row, int col)
        {
            var texture = new Texture2D(sectionX, sectionY);
            texture.SetPixels(puzzleTexture.GetPixels(sectionX * row, sectionY * col, sectionX, sectionY));
            texture.Apply(true);
            _image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
        public void SetPuzzleManager(PuzzleManager puzzleManager)
        {
            _manager = puzzleManager;
        }

        [UsedImplicitly]
        public void BeginDrag(BaseEventData data)
        {
            _lastPosition = transform.localPosition;
        }
        [UsedImplicitly]
        public void PieceDrag(BaseEventData data)
        {
            var pointerData = (PointerEventData)data;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)_manager.gameObject.transform,
                pointerData.position,
                _manager.GetComponent<Canvas>().worldCamera,
                out var position);
            transform.position = _manager.gameObject.transform.TransformPoint(position);
        }
        [UsedImplicitly]
        public void PieceDrop(BaseEventData data)
        {
            _manager.EndDrag(this);
        }
        public void DisableDragging()
        {
            Destroy(gameObject.GetComponent<EventTrigger>());
        }
        public void ResetToLastPosition()
        {
            transform.localPosition = _lastPosition;
        }
    }
}
