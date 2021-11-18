using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class lab_3 : MonoBehaviour
{
    [SerializeField] TMP_InputField _InputField;
    [SerializeField] TMP_Text _operativeText;
    [SerializeField] TMP_Text _rulesText;
    [SerializeField] TMP_Text _outputText;
    private readonly int[] _actionVariantsCount = { 1, 2, 2, 2, 2 };
    private readonly int[] _firstWayId = { 0, 3, 6, 7 };
    private readonly int[] _secondWayId = { 0, 4, 5, 7 };
    private readonly int[] _conflictWayId = { 1, 2};
    private int[] _myWayId = new int [actionCapacity];
    private string _inputAction;
    private int _inputActionId;
    private const int actionCapacity = 4;
    private List<string> _inputActions = new List<string>();
    //private int _resultID;
    Dictionary<string, string> _firstWay = new Dictionary<string, string>(actionCapacity);
    Dictionary<string, string> _secondWay = new Dictionary<string, string>(actionCapacity);
    Dictionary<string, string> _conflictWay = new Dictionary<string, string>(actionCapacity);
    Dictionary<string, string> _myWay = new Dictionary<string, string>(actionCapacity);
    // Dictionary<string, string> _dump = new Dictionary<string, string>(actionCapacity*2);
    public Button button;
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public GameObject ConfPanel;
    readonly string[] _result =
    {
        "нужно приготовить блюдо",
        "выбрать чай",
        "заказать пиццу",
        "выбрать овощи",
        "выбрать мясо",
        "разделать мясо",
        "отчистить овощи",
        "окончить готовку"
    };
    readonly string[] _actions =
    {
        "выбрать между овощами и мясом",
        "поставить чайник",
        "дождаться пиццу",
        "отчистить овощи",
        "разделать мясо",
        "приступить к обжарке стейка",
        "перемешать нарезанные овощи и заправить их соусом, сделав салат",
        "приступить к трапезе"
    };
  
    private void Start()
    {
        ConfPanel.SetActive(false) ;
        for (int i = 0; i < actionCapacity; i++)
        {
            _firstWay.Add(_result[_firstWayId[i]], _actions[_firstWayId[i]]);
            _secondWay.Add(_result[_secondWayId[i]], _actions[_secondWayId[i]]);        

        }
        for (int i = 0; i < _conflictWayId.Length; i++)
        {
            _conflictWay.Add(_result[_conflictWayId[i]], _actions[_conflictWayId[i]]);
          //  Debug.Log(_conflictWay[_actions[_conflictWayId[i]]]);
        }
    }
    public void DataInput()
    {
        _inputAction = _InputField.text;
      
    }
    public void Build()
    {
        Debug.Log(_inputActions.Count);
        if (_conflictWay.ContainsKey(_inputAction) && _inputActions.Count == 1)
        {
            _outputText.text = "Конфликтный набор!";
            _rulesText.text += "ЕСЛИ " + _inputAction + ", ТО " + _conflictWay[_inputAction] + ".\n";
            ConfPanel.SetActive(true);
            button.enabled = false;
        }
        else
        {
            DisplayData_2(_result, _rulesText);
            ConfPanel.SetActive(false);
        }
    }
  

    private void DisplayData_2(string[] result, TMP_Text text)
    {

      
        if (_myWay.Count != 0)
        {
            if (_inputActions.Contains(_inputAction))
            {
                _outputText.text = "Уже было!";
            }
            else if (_myWay.ContainsKey(_inputAction))
            {
                if (_inputAction == _result[_myWayId[_inputActionId]])
                {
                    text.text += "ЕСЛИ " + _inputAction + ", ТО " + _myWay[_inputAction] + ".\n";
                    _outputText.text = "Решение: " + _myWay[_inputAction];
                    NextStep();
                    _inputActionId++;
                }
                else _outputText.text = "Слишком рано!";
            }
            else _outputText.text = "Данное значение не предусмотренно!";
        }
        else if (CheckAction() != null)
        {
            if (_inputAction == _result[_myWayId[_inputActionId]])
            {
                text.text += "ЕСЛИ " + _inputAction + ", ТО " + CheckAction() + ".\n";
                _outputText.text = "Решение: " + CheckAction();
                NextStep();
                _inputActionId++;
            }
            else _outputText.text = "Слишком рано!";
        }
        else _outputText.text = "Данное значение не предусмотренно!";
    }


    private void NextStep()
    {
      
        _operativeText.text +=CheckAction()+ "\n";
        _inputActions.Add(_inputAction);

    }
    private string CheckAction()
    {

        if (_firstWay.ContainsKey(_inputAction))
        {
            if (_actionVariantsCount[_inputActionId] > 1 && _myWay.Count == 0)
            {
                _myWay = _firstWay;
                _myWayId = _firstWayId;
            }
                return _firstWay[_inputAction];
            
        }
        else if (_secondWay.ContainsKey(_inputAction))
        {
            if (_actionVariantsCount[_inputActionId] > 1 && _myWay.Count == 0)
            {
                _myWay = _secondWay;
                _myWayId = _secondWayId;
            }
                return _secondWay[_inputAction];
        }
 


        return null;
    }
}
