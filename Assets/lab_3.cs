using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

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
    private string[] _inputActions = new string [30];
    //private int _resultID;
    Dictionary<string, string> _firstWay = new Dictionary<string, string>(actionCapacity);
    Dictionary<string, string> _secondWay = new Dictionary<string, string>(actionCapacity);
    Dictionary<string, string> _conflictWay = new Dictionary<string, string>(actionCapacity);
    Dictionary<string, string> _myWay = new Dictionary<string, string>(actionCapacity);
    // Dictionary<string, string> _dump = new Dictionary<string, string>(actionCapacity*2);
    public GameObject ConfPanel;
    readonly string[] _result =
    {
        "����� ����������� �����",
        "������� ���",
        "�������� �����",
        "������� �����",
        "������� ����",
        "��������� ����",
        "��������� �����",
        "�������� �������"
    };
    readonly string[] _actions =
    {
        "������� ����� ������� � �����",
        "��������� ������",
        "��������� �����",
        "��������� �����",
        "��������� ����",
        "���������� � ������� ������",
        "���������� ���������� ����� � ��������� �� ������, ������ �����",
        "���������� � �������"
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
        if (_conflictWay.ContainsKey(_inputAction))
        {
            _outputText.text = "����������� �����!";
            _rulesText.text += "���� " + _inputAction + ", �� " + _conflictWay[_inputAction] + ".\n";
            ConfPanel.SetActive(true);
        }
        else DisplayData_2(_result, _rulesText);
    }
  

    private void DisplayData_2(string[] result, TMP_Text text)
    {

      
        if (_myWay.Count != 0)
        {
            if (_inputActions.Contains(_inputAction))
            {
                _outputText.text = "��� ����!";
            }
            else if (_myWay.ContainsKey(_inputAction))
            {
                if (_inputAction == _result[_myWayId[_inputActionId]])
                {
                    text.text += "���� " + _inputAction + ", �� " + _myWay[_inputAction] + ".\n";
                    _outputText.text = "�������: " + _myWay[_inputAction];
                    NextStep();
                    _inputActionId++;
                }
                else _outputText.text = "������� ����!";
            }
            else _outputText.text = "������ �������� �� ��������������!";
        }
        else if (CheckAction() != null)
        {
            if (_inputAction == _result[_myWayId[_inputActionId]])
            {
                text.text += "���� " + _inputAction + ", �� " + CheckAction() + ".\n";
                _outputText.text = "�������: " + CheckAction();
                NextStep();
                _inputActionId++;
            }
            else _outputText.text = "������� ����!";
        }
        else _outputText.text = "������ �������� �� ��������������!";
    }


    private void NextStep()
    {
      
        _operativeText.text +=CheckAction()+ "\n";
        _inputActions[_inputActionId] = _inputAction;

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
