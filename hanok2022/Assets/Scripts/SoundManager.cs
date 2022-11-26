using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static SoundManager _instance;
    public static SoundManager Instance { get => _instance; }

    [SerializeField] AudioSource BGMSource;
    [SerializeField] AudioSource SFXSource;

    [SerializeField] AudioLowPassFilter lowPassFilter;
    [SerializeField] AudioClip[] BGMClips;
    [SerializeField] AudioClip[] SFXClips;

    ESoundBGM currentBGM;
    ESoundFX currentFX;

    public enum ESoundBGM
    {
        None,
        Main,
    }

    public enum ESoundFX
    {
        None,
        Button,
        LightMove,
        WallBreak,
        Damaged,
        DuneoFULLGadong,
    }

    //string[] SoundNames = { "투핑거스 - 도전적인 미래", "Mario Jumping Sound", "Water Droplet Sound", "Pow 1", "Blast 6" };

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.LogError("Duplicate SoundManager Exists");
            Destroy(this.gameObject);
        }

        if (BGMSource != null)
        {
            BGMSource.loop = true;
        }

        if (SFXSource != null)
        {
            SFXSource.loop = false;
        }

        ActiveLowPassFilter(false);
    }

    void Update()
    {
        TestPlay();
    }

    public void ActiveLowPassFilter(bool active)
    {
        lowPassFilter.enabled = active;
    }

    public void ChangeBGM(ESoundBGM type)
    {
        if (type == ESoundBGM.None)
        {
            BGMSource.Stop();
            currentBGM = ESoundBGM.None;
            BGMSource.clip = null;
            return;
        }

        int index = (int)type - 1;
        var clip = BGMClips[index];

        if (currentBGM == type)
        {
            BGMSource.Stop();
            currentBGM = ESoundBGM.None;
            BGMSource.clip = null;

            Debug.Log($"Stop BGM, {type}");
            return;
        }

        currentBGM = type;
        BGMSource.clip = clip;
        BGMSource.Play();

        Debug.Log($"Change BGM, {type}");
    }

    public void ChangeSFX(ESoundFX type)
    {
        int index = (int)type - 1;
        var clip = SFXClips[index];

        currentFX = type;

        SFXSource.PlayOneShot(clip);

        Debug.Log($"Change SFX, {type}");
    }

    #region Test Func
    void TestPlay()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeBGM(ESoundBGM.Main);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeSFX(ESoundFX.Button);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeSFX(ESoundFX.LightMove);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeSFX(ESoundFX.WallBreak);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeSFX(ESoundFX.Damaged);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeSFX(ESoundFX.DuneoFULLGadong);
        }
    }

    #endregion Test Func
}
