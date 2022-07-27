using UnityEngine;

public class Controller
{
    private float gasForce = 0.5f;
    private float limit = .1f;
    private float gas;
    private float dumpInertion = 1f;

    private Vector3 inertion;
    private Transform player;
    private PlayerInputActions playerInputActions;
    private bool runing;

    public Controller(Transform obj, float gasForce, PlayerInputActions pIA)
    {
        runing = true;
        player = obj;
        this.gasForce = gasForce/100;
        playerInputActions = pIA;
        playerInputActions.Enable();
    }

    public void Update()
    {
        if (runing)
        {
            Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
            if (inputVector.y > 0)
            {
                gas = Mathf.Clamp(gas + inputVector.y * gasForce * Time.fixedDeltaTime, 0, 1);
                inertion += player.up * inputVector.y * gas * Time.fixedDeltaTime;
                inertion = Vector3.ClampMagnitude(inertion, limit);
                inertion *= dumpInertion;
            }

            player.Rotate(Vector3.forward, inputVector.x * -1f);
            player.Translate(inertion, Space.World);
        }
    }

    public void EndGame()
    {
        runing = false;
    }

    public void StartGame()
    {
        runing = true;
        inertion = Vector2.zero;
        gas = 0;
        player.position = Vector2.zero;
        player.rotation=new Quaternion();
    }

}
