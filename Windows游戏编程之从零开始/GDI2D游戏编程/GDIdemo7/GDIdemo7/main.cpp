//------------------------------【程序说明】-----------------------------
// 程序名称：GDIdemo7
// 2018年10月Creat by XY
// 描述：游戏动画技巧之 定时器动画显示 示例程序
//-----------------------------------------------------------------------


//------------------------------【头文件包含部分】-----------------------
//描述：包含程序所依赖的头文件
//-----------------------------------------------------------------------
#include <windows.h>
#include <tchar.h>//使用swprintf_s函数所需的头文件

//-----------------------------------【库文件包含部分】---------------------------------------
//	描述：包含程序所依赖的库文件
//------------------------------------------------------------------------------------------------
#pragma comment(lib,"winmm.lib")  //调用PlaySound函数所需库文件
#pragma  comment(lib,"Msimg32.lib")  //添加使用TransparentBlt函数所需的库文件

//------------------------------【宏定义部分】---------------------------
//描述：定义一些辅助宏
//-----------------------------------------------------------------------
#define WINDOW_WIDTH 800
#define WINDOW_HEIGHT 600
#define WINDOW_TITLE L"【致我们永不熄灭的游戏开发梦想】程序核心框架"	//为窗口标题定义的宏

//------------------------------【全局变量声明部分】---------------------
//描述：全局变量声明
//-----------------------------------------------------------------------
HDC g_hdc = nullptr,g_mdc = nullptr;	//全局设备环境句柄与全局内存DC句柄
HBITMAP g_hSprite[12];	
DWORD g_tPre = 0, g_tNow= 0;		//声明两个变量来记录时间
int g_iNum = 0;		//记录目前显示的图号

//------------------------------【全局函数声明部分】---------------------
//描述：全局函数声明，防止“未声明的标识”系列错误
//-----------------------------------------------------------------------
LRESULT CALLBACK  WndProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lparam);	//窗口过程函数
BOOL Game_Init(HWND hwnd);		//在此函数中进行资源的初始化
VOID Game_Paint(HWND hwnd);		//在此函数中进行绘图代码的书写
BOOL Game_CleanUp(HWND hwnd);	//在此函数中进行资源的清理

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

	//游戏资源的初始化，若初始化失败，弹出一个消息框，并返回FALSE
	if(!Game_Init(hwnd))
	{
		MessageBox(hwnd,L"资源初始化失败",L"消息窗口",0);		//使用MessageBox函数，创建一个消息窗口
		return FALSE;
	}

	//【5】消息循环过程
	MSG msg = {0};	//定义并初始化msg
	while(msg.message != WM_QUIT)	//使用while循环，如果消息不是WM_QUIT，就继续循环
	{
		if(PeekMessage(&msg,0,0,0,PM_REMOVE))	//查看应用程序消息队列，有消息时将队列中的消息派发出去
		{
			TranslateMessage(&msg);	//将虚拟键消息转换为字符消息
			DispatchMessageW(&msg);	//分发一个消息给窗口程序
		}
		else
		{
			g_tNow = GetTickCount();		//获取当前系统时间
			if(g_tNow-g_tPre >= 100)		//当此次循环运行与上次绘图时间相差0.1秒时再近些重绘
				Game_Paint(hwnd);
		}
	}

	//【6】窗口类的注销
	UnregisterClass(L"ForTheDreamOfGameDevelop",wndClass.hInstance);	//程序准备结束，注销窗口类
	return 0;
}

//---------------------------------【WndProc()函数】-------------------------------------
//描述：窗口过程函数WndProc，对窗口消息进行处理
//---------------------------------------------------------------------------------------
LRESULT CALLBACK  WndProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lparam)
{
	switch(message)	//switch语句开始
	{

	case WM_KEYDOWN:
		if(wParam == VK_ESCAPE)	//如果按下的键是ESC
			DestroyWindow(hwnd);	//销毁窗口，并发送一条WM_DESTROY消息
		break;

	case WM_DESTROY:		//若是窗口销毁消息
		Game_CleanUp(hwnd);		//调用自定义的资源清理函数Game_CleanUp()进行退出前的资源清理

		PostQuitMessage(0);	//向系统表明有个线程有终止请求。用来响应WM_DESTROY消息
		break;		//跳出该switch语句

	default:	//若上述case条件都不符合
		return DefWindowProcW(hwnd,message,wParam,lparam);	//调用默认的窗口过程
	}

	return 0;
}

//---------------------------------【Game_Init()函数】-------------------------------------
//描述：初始化函数，进行一些简单的初始化
//-----------------------------------------------------------------------------------------
BOOL Game_Init(HWND hwnd)
{
	g_hdc = GetDC(hwnd);

	wchar_t filename[20];
	//载入各个萝莉位图
	for(int i = 0; i <12; ++i)
	{
		memset(filename, 0, sizeof(filename));		//filename的初始化
		swprintf_s(filename, L"%d.bmp",i);			//组装图片名称
		g_hSprite[i] = (HBITMAP)LoadImage(nullptr,filename,IMAGE_BITMAP,WINDOW_WIDTH,WINDOW_HEIGHT,LR_LOADFROMFILE);
	}

	//建立兼容DC
	g_mdc = CreateCompatibleDC(g_hdc);		//建立兼容设备环境的内存DC

	return TRUE;
}

//---------------------------------【Game_Paint()函数】------------------------------------
//描述：绘制函数，在此函数中进行绘制操作
//-----------------------------------------------------------------------------------------
VOID Game_Paint(HWND hwnd)
{
	//处理图号
	if(g_iNum == 11)
		g_iNum = 0;

	SelectObject(g_mdc,g_hSprite[g_iNum]);		//根据图号选入对应的位图
	BitBlt(g_hdc,0,0,WINDOW_WIDTH,WINDOW_HEIGHT,g_mdc,0,0,SRCCOPY);

	g_tPre = GetTickCount();		//记录此次绘图时间，供下次游戏循环中判断是否已经达到画面更新操作设定的时间间隔。
	g_iNum++;
}

//---------------------------------【Game_CleanUp()函数】----------------------------------
//描述：资源清理函数，在此函数中进行退出前资源的清理工作
//-----------------------------------------------------------------------------------------
BOOL Game_CleanUp(HWND hwnd)
{
	//释放资源对象
	for (int i =0; i < 12; ++i)
	{
		DeleteObject(g_hSprite[i]);
	}
	DeleteDC(g_mdc);
	ReleaseDC(hwnd,g_hdc);		//释放设备环境

	return TRUE;
}