using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Quaternion;

public class Weapon
{
    private PlayerInputActions playerInputActions;
    private Transform target;
    private GameObject AmmoFab;
    private Vector2 mousePos;
    private float speedAmmo;
    private int laserAttackMaxCount;
    private float laserCDconst;
    private bool runing;

    public int laserAttackCount { get; private set; }
    public float laserCD { get; private set; }
    public Weapon(Transform target,float speedAmmo,PlayerInputActions pIA,int lasercount, float laserCD)
    {
        runing = true;
        this.target = target;
        this.speedAmmo = speedAmmo/100;
        playerInputActions = pIA;
        playerInputActions.Enable();
        playerInputActions.Player.Fire.performed += Fire;
        playerInputActions.Player.MousePos.performed += MoveMouse;
        AmmoFab = Resources.Load<GameObject>("Fabs/Ammo");
        laserAttackMaxCount = lasercount;
        laserAttackCount = laserAttackMaxCount;
        this.laserCDconst = laserCD;
        this.laserCD = laserCDconst;
    }
    private void MoveMouse(InputAction.CallbackContext context)
    {
        mousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    public void Update()
    {
        if (runing)
        {
            Vector2 difference = mousePos - new Vector2(target.position.x, target.position.y);
            difference = difference.normalized;
            float rotation = (Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg) - 90;
            target.rotation = Euler(0, 0, rotation);
            if (laserAttackCount < laserAttackMaxCount)
            {
                laserCD -= Time.deltaTime;
                if (laserCD < 0)
                {
                    laserCD = laserCDconst;
                    laserAttackCount++;
                }

            }
        }
    }

    private void createAmmo(Ammo ammoType)
    {
        Vector2 dir = (mousePos - new Vector2(target.position.x,target.position.y)).normalized;
        Quaternion rot = Quaternion.Euler(0f,0f,Mathf.Atan2(dir.y,dir.x)*Mathf.Rad2Deg-90);
        AmmoController _ammo = PoolManager.Instance.Spawn(AmmoFab,target.position,rot).GetComponent<AmmoController>();
        _ammo.Init(target.position,speedAmmo,dir,ammoType);
    }

    private void Fire(InputAction.CallbackContext context)
    {
        if (runing)
        {
            switch (context.control.name)
            {
                case "leftButton":
                    createAmmo(new PointAmmo());
                    break;
                case "rightButton":
                    if (laserAttackCount > 0)
                    {
                        createAmmo(new LaserAmmo());
                        laserAttackCount--;
                    }

                    break;
            }
        }
    }

    public void EndGame()
    {
        runing = false;
    }

    public void StartGame()
    {
        runing = true;
    }

}
