using ImportantScripts.Managers;
using UnityEngine;

namespace UselessScripts
{
	public class CameraController : MonoBehaviour
	{

		public Vector3 Offset;

		public float ZoomSpeed = 4f;
		public float MinZoom = 5f;
		public float MaxZoom = 15f;
		public float Pitch = 2f;
		public float RotSpeed = 100f;

		private float _curZoom = 10f;
		private float _rotX;
	
		void Update ()
		{
			_curZoom -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
			_curZoom = Mathf.Clamp(_curZoom, MinZoom, MaxZoom);

			if (Input.GetMouseButton(2))
			{
				_rotX += Input.GetAxisRaw("Mouse X") * RotSpeed * Time.deltaTime;
				Cursor.visible = false;
			}

			else
			{
				Cursor.visible = true;
			}
		}

		private void LateUpdate()
		{
			transform.position = GameManager.GameManagerIn.Char.transform.position - Offset * _curZoom;
			transform.LookAt(GameManager.GameManagerIn.Char.transform.position + Vector3.up * Pitch);
			transform.RotateAround(GameManager.GameManagerIn.Char.transform.position, Vector3.up, _rotX);
		}
	}
}
