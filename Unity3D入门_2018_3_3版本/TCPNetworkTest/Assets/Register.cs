using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MiniJSON;

public class Register : MonoBehaviour
{
    [SerializeField] private Text inputAccount;
    [SerializeField] private Text inputPassword;

    public void OnSubmitAccountInfo()
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        dict["BusinessType"] = 1;
        dict["Account"] = inputAccount.text;
        dict["Password"] = inputPassword.text;
        string json= Json.Serialize(dict);
        Debug.Log(json);
        NewBehaviourScript.sendInfo(json);

    }


}
