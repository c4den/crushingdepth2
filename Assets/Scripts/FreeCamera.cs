using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    public float sensitivityX = 2.0f;
    public float sensitivityY = 2.0f;
    public Texture2D cursorTexture;  // The texture for the custom cursor

    private float rotationX = 0;
    private float rotationY = 0;
    private Vector2 cursorHotspot;  // Center of the cursor texture

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;  // Hide the default system cursor

        cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
    }

    void Update()
    {
        HandleMouseMovement();
        HandleMouseClick();
    }

    void HandleMouseMovement()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotationX -= mouseY * sensitivityY;
        rotationX = Mathf.Clamp(rotationX, -90, 90);

        rotationY += mouseX * sensitivityX;
        rotationY %= 360;

        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.parent.localRotation = Quaternion.Euler(0, rotationY, 0);
    }

    void HandleMouseClick()
    {
        if (Input.GetMouseButtonDown(0)) // 0 is the button number for left mouse click
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object has the BlockInteraction script, and if so, call a method on it
                BlockInteraction block = hit.collider.GetComponent<BlockInteraction>();
                if (block != null)
                {
                    block.OnBlockClicked();
                }
            }
        }
    }

    void OnGUI()
    {
        // Draw the cursor at the center of the screen
        GUI.DrawTexture(new Rect((Screen.width - cursorTexture.width) / 2, (Screen.height - cursorTexture.height) / 2, cursorTexture.width, cursorTexture.height), cursorTexture);
    }
}
