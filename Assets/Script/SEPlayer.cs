using UnityEngine;

public class SEPlayer : MonoBehaviour
{
    // AudioSourceコンポーネンスへの参照をInspectorから設定する
    public AudioSource myAudioSource;

    /// <summary>
    /// ゲーム開始音
    /// </summary>
    public AudioClip play01GameStartSE;

    /// <summary>
    /// ターンが変わる音
    /// </summary>
    public AudioClip play02TurnChangedSE;

    /// <summary>
    /// ボタン選択音
    /// </summary>
    public AudioClip play03ButtonClickSE;

    /// <summary>
    /// 特性を押した音
    /// </summary>
    public AudioClip play04SelectAbilitySE;

    /// <summary>
    /// コインを投げる音
    /// </summary>
    public AudioClip play05CoinTossSE;

    /// <summary>
    /// カードを引く音
    /// </summary>
    public AudioClip play06DrawCardSE;

    /// <summary>
    /// カードを置く音
    /// </summary>
    public AudioClip play07SetCardSE;

    /// <summary>
    /// カードをshuffleする音
    /// </summary>
    public AudioClip play08ShuffleSE;

    /// <summary>
    /// カードをアクティブに置いたときの音
    /// </summary>
    public AudioClip play09ActiveCardSE;

    /// <summary>
    /// カードを使用した音
    /// </summary>
    public AudioClip play10UsedCardSE;

    /// <summary>
    /// カードを触った音
    /// </summary>
    public AudioClip play11TouchCardSE;

    /// <summary>
    /// メンバーが進化した音
    /// </summary>
    public AudioClip play13EvoMemberSE;

    /// <summary>
    /// HPが減る音
    /// </summary>
    public AudioClip play14DownHPSE;

    /// <summary>
    /// 回復した音
    /// </summary>
    public AudioClip play15UpHPSE;

    /// <summary>
    /// ダメージ音
    /// </summary>
    public AudioClip play16AttackDamageSE;

    /// <summary>
    /// エネルギーを持つ音
    /// </summary>
    public AudioClip play18TouchEnergySE;

    /// <summary>
    /// エネルギーを置く音
    /// </summary>
    public AudioClip play19PutEnergySE;
    public static SEPlayer instance;
    void Awake()
    {
        // シングルトンパターンで、シーン上に常に1つだけ存在するようにする
        if (instance == null)
        {
            instance = this;
            // このGameObjectをシーン切り替え時に破棄しないようにする
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // すでに存在する場合は、新しい方を破棄
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 01.ゲーム開始音を流す
    /// </summary>
    public void Play01GameStartSoundEffect()
    {
        if (myAudioSource != null && play01GameStartSE != null)
        {
            // 設定したクリップを一度だけ再生
            myAudioSource.PlayOneShot(play01GameStartSE);
            // または、myAudioSource.clip = playSE; myAudioSource.Play(); でもOK
            // PlayOneShotは、既に別の音が再生中でも重ねて再生できるのでSEでよく使われる
        }
    }

    /// <summary>
    /// 02.ターンが変わる音を流す
    /// </summary>
    public void Play02TurnChangedSoundEffect()
    {
        if (myAudioSource != null && play02TurnChangedSE != null)
        {
            // 設定したクリップを一度だけ再生
            myAudioSource.PlayOneShot(play02TurnChangedSE);
            // または、myAudioSource.clip = playSE; myAudioSource.Play(); でもOK
            // PlayOneShotは、既に別の音が再生中でも重ねて再生できるのでSEでよく使われる
        }
    }

    /// <summary>
    /// 03.ボタンクリック音を流す
    /// </summary>
    public void Play03ButtonClickSoundEffect()
    {
        if (myAudioSource != null && play03ButtonClickSE != null)
        {
            // 設定したクリップを一度だけ再生
            myAudioSource.PlayOneShot(play03ButtonClickSE);
            // または、myAudioSource.clip = playSE; myAudioSource.Play(); でもOK
            // PlayOneShotは、既に別の音が再生中でも重ねて再生できるのでSEでよく使われる
        }
    }

    /// <summary>
    /// 04.特性を選択したときに流れる音
    /// </summary>
    public void Play04SelectAbilitySoundEffect()
    {
        if (myAudioSource != null && play04SelectAbilitySE != null)
        {
            // 設定したクリップを一度だけ再生
            myAudioSource.PlayOneShot(play04SelectAbilitySE);
            // または、myAudioSource.clip = playSE; myAudioSource.Play(); でもOK
            // PlayOneShotは、既に別の音が再生中でも重ねて再生できるのでSEでよく使われる
        }
    }

    /// <summary>
    /// 05.コイントスの音を流す
    /// </summary>
    public void Play05CoinTossSoundEffect()
    {
        if (myAudioSource != null && play05CoinTossSE != null)
        {
            // 設定したクリップを一度だけ再生
            myAudioSource.PlayOneShot(play05CoinTossSE);
            // または、myAudioSource.clip = playSE; myAudioSource.Play(); でもOK
            // PlayOneShotは、既に別の音が再生中でも重ねて再生できるのでSEでよく使われる
        }
    }

    /// <summary>
    /// 06.カードを引いたときに音を流す
    /// </summary>
    public void Play06DrawCardSoundEffect()
    {
        if (myAudioSource != null && play06DrawCardSE != null)
        {
            // 設定したクリップを一度だけ再生
            myAudioSource.PlayOneShot(play05CoinTossSE);
            // または、myAudioSource.clip = play06DrawCardSE; myAudioSource.Play(); でもOK
            // PlayOneShotは、既に別の音が再生中でも重ねて再生できるのでSEでよく使われる
        }
    }

    /// <summary>
    /// 07.カードを置く音を流す
    /// </summary>
    public void Play07SetCardSoundEffect()
    {
        if (myAudioSource != null && play07SetCardSE != null)
        {
            // 設定したクリップを一度だけ再生
            myAudioSource.PlayOneShot(play07SetCardSE);
            // または、myAudioSource.clip = playSE; myAudioSource.Play(); でもOK
            // PlayOneShotは、既に別の音が再生中でも重ねて再生できるのでSEでよく使われる
        }
    }

    /// <summary>
    /// 08.カードをshuffleする音を流す
    /// </summary>
    public void Play08ShuffleSoundEffect()
    {
        if (myAudioSource != null && play08ShuffleSE != null)
        {
            // 設定したクリップを一度だけ再生
            myAudioSource.PlayOneShot(play08ShuffleSE);
            // または、myAudioSource.clip = playSE; myAudioSource.Play(); でもOK
            // PlayOneShotは、既に別の音が再生中でも重ねて再生できるのでSEでよく使われる
        }
    }

    /// <summary>
    /// 09.カードをアクティブに置いた音を流す
    /// </summary>
    public void Play09ActiveCardSoundEffect()
    {
        if (myAudioSource != null && play09ActiveCardSE != null)
        {
            // 設定したクリップを一度だけ再生
            myAudioSource.PlayOneShot(play09ActiveCardSE);
            // または、myAudioSource.clip = playSE; myAudioSource.Play(); でもOK
            // PlayOneShotは、既に別の音が再生中でも重ねて再生できるのでSEでよく使われる
        }
    }

    /// <summary>
    /// 10.カードを使用した音を流す
    /// </summary>
    public void Play10UsedCardSoundEffect()
    {
        if (myAudioSource != null && play10UsedCardSE != null)
        {
            // 設定したクリップを一度だけ再生
            myAudioSource.PlayOneShot(play10UsedCardSE);
            // または、myAudioSource.clip = playSE; myAudioSource.Play(); でもOK
            // PlayOneShotは、既に別の音が再生中でも重ねて再生できるのでSEでよく使われる
        }
    }

    /// <summary>
    /// 11.カードを触った時の音を流す
    /// </summary>
    public void Play11TouchCardSoundEffect()
    {
        if (myAudioSource != null && play11TouchCardSE != null)
        {
            // 設定したクリップを一度だけ再生
            myAudioSource.PlayOneShot(play11TouchCardSE);
            // または、myAudioSource.clip = playSE; myAudioSource.Play(); でもOK
            // PlayOneShotは、既に別の音が再生中でも重ねて再生できるのでSEでよく使われる
        }
    }

    /// <summary>
    /// 13.メンバーが進化したときの音を流す
    /// </summary>
    public void Play13EvoMemberSoundEffect()
    {
        if (myAudioSource != null && play13EvoMemberSE != null)
        {
            // 設定したクリップを一度だけ再生
            myAudioSource.PlayOneShot(play13EvoMemberSE);
            // または、myAudioSource.clip = playSE; myAudioSource.Play(); でもOK
            // PlayOneShotは、既に別の音が再生中でも重ねて再生できるのでSEでよく使われる
        }
    }

    /// <summary>
    /// 14.HPが減ったときの音を流す
    /// </summary>
    public void Play14DownHPSoundEffect()
    {
        if (myAudioSource != null && play14DownHPSE != null)
        {
            // 設定したクリップを一度だけ再生
            myAudioSource.PlayOneShot(play14DownHPSE);
            // または、myAudioSource.clip = playSE; myAudioSource.Play(); でもOK
            // PlayOneShotは、既に別の音が再生中でも重ねて再生できるのでSEでよく使われる
        }
    }

    /// <summary>
    /// 15.HPが増えたときの音を流す
    /// </summary>
    public void Play15UpHPSoundEffect()
    {
        if (myAudioSource != null && play15UpHPSE != null)
        {
            // 設定したクリップを一度だけ再生
            myAudioSource.PlayOneShot(play15UpHPSE);
            // または、myAudioSource.clip = playSE; myAudioSource.Play(); でもOK
            // PlayOneShotは、既に別の音が再生中でも重ねて再生できるのでSEでよく使われる
        }
    }

    /// <summary>
    /// 16.攻撃時のダメージ音を流す
    /// </summary>
    public void Play16AttackDamageSoundEffect()
    {
        if (myAudioSource != null && play16AttackDamageSE != null)
        {
            // 設定したクリップを一度だけ再生
            myAudioSource.PlayOneShot(play16AttackDamageSE);
            // または、myAudioSource.clip = playSE; myAudioSource.Play(); でもOK
            // PlayOneShotは、既に別の音が再生中でも重ねて再生できるのでSEでよく使われる
        }
    }

    /// <summary>
    /// 18.エネルギーを掴んだときの音を流す
    /// </summary>
    public void Play18TouchEnergySoundEffect()
    {
        if (myAudioSource != null && play18TouchEnergySE != null)
        {
            // 設定したクリップを一度だけ再生
            myAudioSource.PlayOneShot(play18TouchEnergySE);
            // または、myAudioSource.clip = playSE; myAudioSource.Play(); でもOK
            // PlayOneShotは、既に別の音が再生中でも重ねて再生できるのでSEでよく使われる
        }
    }

    /// <summary>
    /// 19.エネルギーを置いた時の音を流す
    /// </summary>
    public void Play19PutEnergySoundEffect()
    {
        if (myAudioSource != null && play19PutEnergySE != null)
        {
            // 設定したクリップを一度だけ再生
            myAudioSource.PlayOneShot(play19PutEnergySE);
            // または、myAudioSource.clip = playSE; myAudioSource.Play(); でもOK
            // PlayOneShotは、既に別の音が再生中でも重ねて再生できるのでSEでよく使われる
        }
    }
}
