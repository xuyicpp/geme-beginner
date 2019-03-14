using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net.Sockets;
using System.Net;
using System.Text;
using MiniJSON;


public class NewBehaviourScript : MonoBehaviour
{
    private static Socket tcpSocket;
    // Start is called before the first frame update
    void Start()
    {
        //创建socket
        tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //
        Debug.Log("连接服务器1");
        //连接服务器
        tcpSocket.Connect(IPAddress.Parse("10.14.112.223"), 6666);


        //接收消息
        //byte[] bt = new byte[1024];
        //int messgeLength = tcpSocket.Receive(bt);
        //Debug.Log(ASCIIEncoding.UTF8.GetString(bt));

        Debug.Log("连接服务器2");
    }

    public static void sendInfo(string json)
    {

        tcpSocket.Send(Encoding.UTF8.GetBytes(Encoding.UTF8.GetBytes(json).Length.ToString()));
        //Debug.Log(Encoding.UTF8.GetBytes("你好").Length);
        tcpSocket.Send(Encoding.UTF8.GetBytes(json));
        byte[] bt = new byte[1024];
        int messgeLength = tcpSocket.Receive(bt);
        //Debug.Log(Encoding.UTF8.GetString(bt));
        string str = Encoding.UTF8.GetString(bt);
        Debug.Log(str);
        Dictionary<string, object> dict = Json.Deserialize(str) as Dictionary<string, object>;
        Debug.Log(dict["ErrorDesc"]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}



