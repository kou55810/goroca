using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// メンバーカードビュー
/// </summary>
public class MemberView : CardView
{
    /// <summary>
    /// メンバーの体力
    /// </summary>
    [SerializeField] public TextMeshProUGUI hpText;

    /// <summary>
    /// メンバーの種類
    /// </summary>
    [SerializeField] public TextMeshProUGUI memberType;

    /// <summary>
    /// メンバーの種類
    /// </summary>
    [SerializeField] public TextMeshProUGUI energyCount;

    /// <summary>
    /// 逃げエネ数
    /// </summary>
    [SerializeField] public Image dropEnergyImage;

    /// <summary>
    /// 道具マーク
    /// </summary>
    // [SerializeField] public Image belongingsImage;

    /// <summary>
    /// 道具マーク
    /// </summary>
    [SerializeField] public BelongingsView belongingsView;

    /// <summary>
    /// 弱体
    /// </summary>
    [SerializeField] public TextMeshProUGUI weaknessText;

    /// <summary>
    /// 状態異常マーク
    /// </summary>
    [SerializeField] public Image specialConditionsImage;

    [SerializeField] public GameObject abilityPanel;
    [SerializeField] public GameObject attack1Panel;
    [SerializeField] public GameObject attack2Panel;

    /// <summary>
    /// カードの表示
    /// </summary>
    /// <param name="cardModel"></param>
    public void Show(MemberModel cardModel) // cardModelのデータ取得と反映
    {
        nameText.text = cardModel.name;
        cardImage.sprite = cardModel.cardImage;
        energyCount.text = cardModel.energyCount.ToString();
        typeFrame.sprite = cardModel.typeFrame;
        background.sprite = cardModel.background;
        backside.sprite = cardModel.backside;
        hpText.text = cardModel.restHP.ToString();
        memberType.text = cardModel.memberType.GetDescription();
        dropEnergyImage.sprite = cardModel.dropEnergyImage;
        weaknessText.text = cardModel.weakness.GetDescription();
        // 特性
        if (cardModel.ability is null)
        {
            abilityPanel.SetActive(false);
        }
        else
        {
            TextMeshProUGUI abilityName = abilityPanel.transform.Find("abilityName").GetComponent<TextMeshProUGUI>();
            abilityName.text = cardModel.ability.abilityName;
            TextMeshProUGUI abilityEffect = abilityPanel.transform.Find("abilityEffect").GetComponent<TextMeshProUGUI>();
            abilityEffect.text = cardModel.ability.abilityEffect;
        }

        // 技1
        if (cardModel.attack1 is null)
        {
            attack1Panel.SetActive(false);
        }
        else
        {
            SetAttack(cardModel.attack1, attack1Panel);
        }

        // 技2
        if (cardModel.attack2 is null)
        {
            attack2Panel.SetActive(false);
        }
        else
        {
            SetAttack(cardModel.attack2, attack2Panel);
        }
    }

    /// <summary>
    /// 道具をセットする
    /// </summary>
    /// <param name="id"></param>
    public void SetBelongingsImage(Guid guid, int id)
    {
        belongingsView.SetBelongingsImage(guid, id);
    }

    /// <summary>
    /// 状態異常をセットする
    /// </summary>
    /// <param name="id"></param>
    public void SetSpecialConditions(SpecialConditions specialConditions)
    {
        specialConditionsImage.sprite = Resources.Load<Sprite>($"Images/Card/specialConditions/{specialConditions.GetDescription()}_frame");
        specialConditionsImage.gameObject.SetActive(true);
    }

    /// <summary>
    /// 道具を無効化
    /// </summary>
    public void DisableBelongings()
    {
        belongingsView.DisableBelongings();
    }

    /// <summary>
    /// 状態異常の無効化
    /// </summary>
    public void DisableSpecialConditions()
    {
        specialConditionsImage.gameObject.SetActive(false);
    }

    /// <summary>
    /// 攻撃をセットする
    /// </summary>
    /// <param name="attackEntity"></param>
    /// <param name="attackPanel"></param>
    public void SetAttack(AttackEntity attackEntity, GameObject attackPanel)
    {
        for (int i = 1; i <= attackEntity.energy; i++)
        {
            Transform energy = attackPanel.transform.Find($"attackEnergy{i}");
            energy.GetComponent<CanvasGroup>().alpha = 100;
        }
        TextMeshProUGUI attackName = attackPanel.transform.Find("attackName").GetComponent<TextMeshProUGUI>();
        attackName.text = attackEntity.attackName;
        TextMeshProUGUI attackEffect = attackPanel.transform.Find("attackEffect").GetComponent<TextMeshProUGUI>();
        attackEffect.text = attackEntity.attackEffect;
        TextMeshProUGUI attackDamage = attackPanel.transform.Find("attackDamage").GetComponent<TextMeshProUGUI>();
        attackDamage.text = attackEntity.damage.ToString();
    }
}
