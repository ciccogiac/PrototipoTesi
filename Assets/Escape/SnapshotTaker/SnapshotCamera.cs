using UnityEngine;

namespace Escape.SnapshotTaker
{
    [RequireComponent(typeof(Camera))]
    public class SnapshotCamera : MonoBehaviour
    {
        private Camera _snapCam;
        private int _resWidth = 1920;
        private int _resHeight = 1080;
        private void Awake()
        {
            _snapCam = GetComponent<Camera>();
            if (_snapCam.targetTexture == null)
            {
                _snapCam.targetTexture = new RenderTexture(_resWidth, _resHeight, 32);
            }
            else
            {
                var targetTexture = _snapCam.targetTexture;
                _resWidth = targetTexture.width;
                _resHeight = targetTexture.height;
            }
            _snapCam.gameObject.SetActive(false);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F12))
            {
                CallTakeSnapshot();
            }
        }
        public void CallTakeSnapshot()
        {
            _snapCam.gameObject.SetActive(true);
        }

        private void LateUpdate()
        {
            if (_snapCam.gameObject.activeInHierarchy)
            {
                var snapshot = new Texture2D(_resWidth, _resHeight, TextureFormat.RGB24, false);
                _snapCam.Render();
                RenderTexture.active = _snapCam.targetTexture;
                snapshot.ReadPixels(new Rect(0, 0, _resWidth, _resHeight), 0, 0);
                var bytes = snapshot.EncodeToPNG();
                var fileName = SnapshotName();
                System.IO.File.WriteAllBytes(fileName, bytes);
                Debug.Log("Snapshot taken");
                _snapCam.gameObject.SetActive(false);
            }
        }
        private string SnapshotName()
        {
            return
                $"{Application.dataPath}/Snapshots/snap_{_resWidth}x{_resHeight}_{System.DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
        }
    }
}
