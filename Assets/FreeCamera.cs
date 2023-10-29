using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    public float sensitivityX = 2.0f;
    public float sensitivityY = 2.0f;
    public Texture2D cursorTexture;  // The texture for the custom cursor

    private float rotationX = 0;
    private float rotationY = 0;
    private Vector2 cursorHotspot;  // Center of the cursor texture
    public float rotationSpeed = 30.0f; // Rotation speed when floating

    private float rotationX = 0;
    private float rotationY = 0;
    private bool isCameraControllable = true; // Flag to control camera rotation
    private bool isFloating = false; // Flag to control floating behavior
    private Vector3 randomRotationAxis;

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
        if (isCameraControllable)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            if (!isFloating)
            {
                rotationX -= mouseY * sensitivityY;
                rotationY += mouseX * sensitivityX;
            }
            else
            {
                // Rotate the camera continuously
                float randomX = Random.Range(-360f, 360f);
                float randomY = Random.Range(-1f, 1f);
                rotationX += randomX * rotationSpeed * Time.deltaTime;
                rotationY += randomY * rotationSpeed * Time.deltaTime;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // Stop floating and resume normal camera control
                    isFloating = false;
                }
            }

            //rotationX = Mathf.Clamp(rotationX, -90, 90);
            rotationY %= 360;
        }

        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.parent.localRotation = Quaternion.Euler(0, rotationY, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Toggle camera control or initiate floating
            if (!isFloating)
            {
                isCameraControllable = !isCameraControllable;
            }
            else
            {
                // Start floating with random rotation axes
                randomRotationAxis = Random.onUnitSphere;
                isFloating = !isFloating;
            }
        }
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
