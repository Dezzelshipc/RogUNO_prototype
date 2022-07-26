using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class ActionsData : MonoBehaviour
{
    public GameObject actionPrefab;
    private GameManager gm;
    public Dictionary<string, Data> actionsData = new();

    public class Data
    {
        public string name;
        public string description;
        public int[] cost = new int[4];
        public float[] numbers;
        public string tags;

        public IActionType actionType;

        public Data(string _name, string _desctription, string _tags, int[] _cost, float[] _numbers, IActionType _actionType)
        {
            name = _name;
            description = _desctription;
            tags = _tags;
            cost = _cost;
            numbers = _numbers;
            actionType = _actionType;
        }
    }

    public interface IActionType
    {
        public void Action(float[] numbers);
    }

    class DamageEnemyAction : IActionType
    {
        GameManager gm;
        public DamageEnemyAction(GameManager _gm)
        {
            gm = _gm;
        }

        public void Action(float[] numbers)
        {
            gm.enemy.Damage((int)numbers[0]);
        }
    }

    class HealPlayerAction : IActionType
    {
        GameManager gm;
        public HealPlayerAction(GameManager _gm)
        {
            gm = _gm;
        }

        public void Action(float[] numbers)
        {
            gm.player.Heal((int)numbers[0]);
        }
    }

    class ResistPlayerAction : IActionType
    {
        GameManager gm;
        public ResistPlayerAction(GameManager _gm)
        {
            gm = _gm;
        }

        public void Action(float[] numbers)
        {
            gm.player.resist += (int)numbers[0];
        }
    }

    class SkipEnemyAction : IActionType
    {
        GameManager gm;
        public SkipEnemyAction(GameManager _gm)
        {
            gm = _gm;
        }

        public void Action(float[] numbers)
        {
            gm.enemy.Skip((int)numbers[0]);
        }
    }

    private string GetDescription(string key)
    {
        return LocalizationSettings.StringDatabase.GetLocalizedString("ActionsText", key);
    }

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();

        actionsData.Add("Damage1", new Data("Damage1",
                                            GetDescription("Damage"),
                                            "e_damage",
                                            new int[] { 2, 2, 2, 2 },
                                            new float[] { 1f },
                                            new DamageEnemyAction(gm)));
        actionsData.Add("Damage5", new Data("Damage5",
                                            GetDescription("Damage"),
                                            "e_damage",
                                            new int[] { 20, 0, 0, 0 },
                                            new float[] { 5f },
                                            new DamageEnemyAction(gm)));

        actionsData.Add("Heal5", new Data("Heal1",
                                          GetDescription("Heal"),
                                          "p_heal",
                                          new int[] { 0, 0, 30, 0 },
                                          new float[] { 5f },
                                          new HealPlayerAction(gm)));

        actionsData.Add("Resist1", new Data("Resist1",
                                            GetDescription("Resist"),
                                            "p_resist",
                                            new int[] { 0, 0, 0, 15 },
                                            new float[] { 1f },
                                            new ResistPlayerAction(gm)));
        actionsData.Add("Skip1", new Data("Skip1",
                                          GetDescription("Skip"),
                                          "e_skip",
                                          new int[] { 0, 30, 0, 0 },
                                          new float[] { 1f },
                                          new SkipEnemyAction(gm)));
    }
}
