//------------------------------【程序说明】-----------------------------
// 程序名称：GameCore
// 2018年10月Creat by XY
// 描述：用代码勾勒出游戏开发所需的程序框架
//-----------------------------------------------------------------------


//------------------------------【头文件包含部分】-----------------------
//描述：包含程序所依赖的头文件
//-----------------------------------------------------------------------
#include <windows.h>

//------------------------------【宏定义部分】---------------------------
//描述：定义一些辅助宏
//-----------------------------------------------------------------------
#define WINDOW_WIDTH 800
#define WINDOW_HEIGHT 600
#define WINDOW_TITLE L"【致我们永不熄灭的游戏开发梦想】程序核心框架"	//为窗口标题定义的宏

//------------------------------【全局函数声明部分】---------------------
//描述：全局函数声明，防止“未声明的标识”系列错误
//-----------------------------------------------------------------------
LRESULT CALLBACK  WndProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lparam);	//窗口过程函数

//------------------------------【WinMain（）函数】-----------------------
//描述：Windows应用程序的入口函数，我们的程序从这里开始
//------------------------------------------------------------------------
int WINAPI WinMain( HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nShowCmd )
{
	//【1】窗口创建四部曲之一：开始设计一个完整的窗口类
	WNDCLASSEX wndClass = { 0 };	//用WINDCLASSEX定义一个窗口类
	wndClass.cbSize = sizeof(WNDCLASSEX);	//设置结构体的字节数大小
	wndClass.style = CS_HREDRAW | CS_VREDRAW;	//设置窗口的样式
	wndClass.lpfnWndProc = WndProc;		//设置指向窗口过程函数的指针
	wndClass.cbClsExtra = 0;		//窗口类的附加内存，取0就可以了
	wndClass.cbWndExtra = 0;		//窗口的附加内存，依然取0就可以了
	wndClass.hInstance = hInstance;	//指定包含窗口过程的程序的实例句柄
	wndClass.hIcon = (HICON)::LoadImage(NULL,L"icon.ico",IMAGE_ICON,0,0,LR_DEFAULTSIZE|LR_LOADFROMFILE);	//本地加载自定义ico图标
	wndClass.hCursor = LoadCursor(NULL, IDC_ARROW);		//指定窗口类的光标句柄
	wndClass.hbrBackground = (HBRUSH)GetStockObject(GRAY_BRUSH); 	//为hbrBackground成员指定一个灰色画刷句柄
	wndClass.lpszMenuName = NULL;	//用一个以空终止的字符串，指定菜单资源的名字
	wndClass.lpszClassName = L"ForTheDreamOfGameDevelop";	//用一个以空终止的字符串，指定窗口类的名字。

	//【2】窗口创建四部曲之二：注册窗口类
	 if( !RegisterClassEx(&wndClass))
		 return -1;

	 //【3】窗口创建四部曲之三：正式创建窗口
	 HWND hwnd = CreateWindow(L"ForTheDreamOfGameDevelop",WINDOW_TITLE,	//创建窗口函数CreateWindow
		 WS_OVERLAPPEDWINDOW,CW_USEDEFAULT,CW_USEDEFAULT,WINDOW_WIDTH,
		 WINDOW_HEIGHT,NULL,NULL,hInstance,NULL);

	 //【4】窗口创建四步曲之四：窗口的移动、显示与更新
	 MoveWindow(hwnd,250,80,WINDOW_WIDTH,WINDOW_HEIGHT,true);	//调整窗口显示时的位置，使窗口左上角位于(250,80)处
	 ShowWindow(hwnd,nShowCmd);	//调用ShowWindow函数来显示窗口
	 UpdateWindow(hwnd);	//对窗口进行更新，就像我们买了房子要装修一样

	 //【消息】循环过程
	 MSG msg = {0};	//定义并初始化msg
	 while(msg.message != WM_QUIT)	//使用while循环，如果消息不是WM_QUIT，就继续循环
	 {
		if(PeekMessage(&msg,0,0,0,PM_REMOVE))	//查看应用程序消息队列，有消息时将队列中的消息派发出去
		{
			TranslateMessage(&msg);	//将虚拟键消息转换为字符消息
			DispatchMessage(&msg);	//分发一个消息给窗口程序
		}
	 }

	 //【6】窗口类的注销
	 UnregisterClass(L"ForTheDreamOfGameDevelop",wndClass.hInstance);	//程序准备结束，注销窗口类
	 return 0;
}

//---------------------------------【WndProc()函数】-------------------------------------
//描述：窗口过程函数WndProc，对窗口消息进行处理
//---------------------------------------------------------------------------------------
LRESULT CALLBACK  WndProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	switch(message)	//switch语句开始
	{
	case WM_PAINT:	//若是客户区重绘消息
		ValidateRect(hwnd,NULL);	//更新客户区的显示
		break;	
	case WM_KEYDOWN:
		if(wParam == VK_ESCAPE)	//如果按下的键是ESC
			DestroyWindow(hwnd);	//销毁窗口，并发送一条WM_DESTROY消息
		break;

	case WM_DESTROY:
		PostQuitMessage(0);	//向系统表明有个线程有终止请求。用来响应WM_DESTROY消息
		break;		//跳出该switch语句

	default:	//若上述case条件都不符合
		return DefWindowProcW(hwnd,message,wParam,lParam);	//调用默认的窗口过程
	}

	return 0;
}