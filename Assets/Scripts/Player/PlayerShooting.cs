using UnityEngine;
using UnityEngine.UI;
 
public class PlayerShooting : MonoBehaviour
{
    public int gunMode = 1;
    public int damagePerShot = 30;
    public AudioClip[] gunAudioSrc = new AudioClip[4];
    public GameObject BowSliderIcon;
    public Slider BowSlider;
    private float timeBetweenBullets = 0.15f;
    private float timeBulletsShot = 0.15f;
    private float range = 100f;
    private float maxRange = 100f;
    float timer;
    Ray shootRay = new Ray();
    Ray[] shootRays = new Ray[5];
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    RaycastHit[] enemiesHit;

    void Awake ()
    {
        //GetMask
        shootableMask = LayerMask.GetMask ("Shootable");
        //Mendapatkan Reference component
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }

    void SetGunMode()
    {
        if (Input.GetKeyDown("1"))
        {
            gunMode = 1;
        }
        else if (Input.GetKeyDown("2"))
        {
            if (GameManager.gameManager.availableWeapon[1])
            {
                gunMode = 2;
            }
            else
            {
                GameManager.gameManager.hudManager.AlertMessage("Shotgun is not available yet");
            }
        }
        else if (Input.GetKeyDown("3"))
        {
            if (GameManager.gameManager.availableWeapon[2])
            {
                gunMode = 3;
            }
            else
            {
                GameManager.gameManager.hudManager.AlertMessage("Sword is not available yet");
            }
        }
        else if (Input.GetKeyDown("4"))
        {
            if (GameManager.gameManager.availableWeapon[3])
            {
                gunMode = 4;
            }
            else
            {
                GameManager.gameManager.hudManager.AlertMessage("Bow is not available yet");
            }
        }
        
    }

    void UpdateGunMode(int gunMode)
    {
        switch (gunMode) {
            case 1:
                timeBetweenBullets = 0.15f;
                range = 100f;
                break;
            case 2:
                timeBetweenBullets = 1f;
                range = 10f;
                break;
            case 3:
                timeBetweenBullets = 0.5f;
                range = 2f;
                break;
            case 4:
                timeBetweenBullets = 0.5f;
                range = 0f;
                break;
            default:
                timeBetweenBullets = 0.5f;
                range = 2f;
                break;
        }
        UpdateDamagePerShot();
    }

    void UpdateBowRange() {
        if (Input.GetMouseButton(1)) {
            range = range <= maxRange ? range + 0.5f : maxRange;
            BowSlider.value = range;
            Debug.Log("range: " + range);
        }
    }

    void Update ()
    {
        timer += Time.deltaTime;
        int prevGunMode = gunMode;
        SetGunMode();
        if (prevGunMode != gunMode) {
            UpdateGunMode(gunMode);
        } 
        if (gunMode == 4) {
            BowSlider.gameObject.SetActive(true);
            BowSliderIcon.SetActive(true);
            UpdateBowRange();
        }
        else {
            BowSlider.gameObject.SetActive(false);
            BowSliderIcon.SetActive(false);
        }

        if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0 && !GameManager.gameManager.isShopping)
        {
            Shoot ();
            if (gunMode == 4) {
                range = 0f;
            }
        }

        if(timer >= timeBulletsShot * effectsDisplayTime)
        {
            DisableEffects ();
        }

        UpdateDamagePerShot();
    }

    public void UpdateDamagePerShot() {
        switch (gunMode) {
            case 1:
                damagePerShot = CheatMenu.hitkill ? 999999 : 30;
                break;
            case 2:
                damagePerShot = CheatMenu.hitkill ? 999999 : 50;
                break;
            case 3:
                damagePerShot = CheatMenu.hitkill ? 999999 : 80;
                break;
            case 4:
                damagePerShot = CheatMenu.hitkill ? 999999 : 200;
                break;
            default:
                damagePerShot = CheatMenu.hitkill ? 999999 : 30;
                break;
        }
    }


    public void DisableEffects ()
    {
        //disable line renderer
        gunLine.enabled = false;

        //disable light
        gunLight.enabled = false;
    }

    void GunShoot() {
        timer = 0f;

        //Play audio
        gunAudio.clip = gunAudioSrc[0];
        gunAudio.Play ();

        //Play gun particle
        gunParticles.Stop ();
        gunParticles.Play ();

        //enable Line renderer
        gunLine.enabled = true;
        gunLine.positionCount = 2;
        gunLine.SetPosition (0, transform.position);

        // set shoot rays
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        //Lakukan raycast jika mendeteksi id nemy hit apapun
        if (Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            //Lakukan raycast hit hace component Enemyhealth
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();

            if(enemyHealth != null)
            {
                //Lakukan Take Damage
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }

            //Set line end position ke hit position
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            //set line end position ke range freom barrel
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }

    void ShotgunShoot() {
        timer = 0f;

        //Play audio
        gunAudio.clip = gunAudioSrc[1];
        gunAudio.Play ();

        //Play gun particle
        gunParticles.Stop ();
        gunParticles.Play ();

        //enable Line renderer
        gunLine.enabled = true;
        gunLine.positionCount = 10;

        //set shooting rays
        for (int i = 0; i < shootRays.Length; i++) {
            gunLine.SetPosition (2 * i, transform.position);
            shootRays[i].origin = transform.position;
            shootRays[i].direction = Quaternion.Euler(0, (-10 + i * 5), 0) * transform.forward;
            //Lakukan raycast jika mendeteksi id nemy hit apapun
            if (Physics.Raycast (shootRays[i], out shootHit, range, shootableMask))
            {
                //Lakukan raycast hit hace component Enemyhealth
                EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
    
                if(enemyHealth != null)
                {
                    //Lakukan Take Damage
                    enemyHealth.TakeDamage (damagePerShot, shootHit.point);
                }

                //Set line end position ke hit position
                gunLine.SetPosition (2*i+1, shootHit.point);
            }
            else
            {
                //set line end position ke range freom barrel
                gunLine.SetPosition (2*i+1, shootRays[i].origin + shootRays[i].direction * range);
            }
        }
    }

    void SwordAttack() {
        timer = 0f;

        gunAudio.clip = gunAudioSrc[2];
        gunAudio.Play ();

        //Play gun particle
        // gunParticles.Stop ();
        // gunParticles.Play ();

        //enable Line renderer
        gunLine.enabled = true;
        gunLine.positionCount = 20;

        for (int i = 0; i < 10; i++) {
            shootRay.origin = transform.position;
            shootRay.direction = Quaternion.Euler(0, (i * 36), 0) * transform.forward;
            gunLine.SetPosition (2*i, shootRay.origin + shootRay.direction * range);
            gunLine.SetPosition (2*i+1, shootRay.origin + Quaternion.Euler(0, 36, 0) * shootRay.direction * range);
        }

        enemiesHit = Physics.SphereCastAll(transform.position, range, transform.forward, 0, shootableMask);
        for (int i = 0; i < enemiesHit.Length; i++) {
            EnemyHealth enemyHealth = enemiesHit[i].collider.gameObject.GetComponent<EnemyHealth>();
            if(enemyHealth != null)
            {
                //Lakukan Take Damage
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }
        }
    }

    void BowAttack() {
        timer = 0f;

        //Play audio
        gunAudio.clip = gunAudioSrc[3];
        gunAudio.Play ();

        //Play gun particle
        // gunParticles.Stop ();
        // gunParticles.Play ();

        //enable Line renderer
        gunLine.enabled = true;
        gunLine.positionCount = 2;
        gunLine.SetPosition (0, transform.position);

        // set shoot rays
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        //Lakukan raycast jika mendeteksi id nemy hit apapun
        if (Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            //Lakukan raycast hit hace component Enemyhealth
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();

            if(enemyHealth != null)
            {
                //Lakukan Take Damage
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }

            //Set line end position ke hit position
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            //set line end position ke range freom barrel
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
        BowSlider.value = 0;
    }

    void Shoot ()
    {
        switch (gunMode) {
            case 1: GunShoot(); break;
            case 2: ShotgunShoot(); break;
            case 3: SwordAttack(); break;
            case 4: BowAttack(); break;
            default: GunShoot(); break;
        }
    }
}