//------------------------------【程序说明】-----------------------------
// 程序名称：GDIGameCore1
// 2018年10月Creat by XY
// 描述：GDI基本几何绘图示例程序
//-----------------------------------------------------------------------


//------------------------------【头文件包含部分】-----------------------
//描述：包含程序所依赖的头文件
//-----------------------------------------------------------------------
#include <windows.h>
#include <time.h>
#pragma comment(lib,"winmm.lib")		//链接使用PlaySound函数所需的winmm.lib库文件

//------------------------------【宏定义部分】---------------------------
//描述：定义一些辅助宏
//-----------------------------------------------------------------------
#define WINDOW_WIDTH 800
#define WINDOW_HEIGHT 600
#define WINDOW_TITLE L"【致我们永不熄灭的游戏开发梦想】程序核心框架"	//为窗口标题定义的宏

//------------------------------【全局变量声明部分】---------------------
//描述：全局变量声明
//-----------------------------------------------------------------------
HDC g_hdc = nullptr;	//全局设备环境句柄
HPEN g_hpen[7] = {0};	//定义画笔句柄的数组
HBRUSH g_hBrush[7] = {0};	//定义画刷句柄的数组
int g_iPenStyle[7] = {PS_SOLID,PS_DASH,PS_DOT,PS_DASHDOT,PS_DASHDOTDOT,PS_NULL,PS_INSIDEFRAME};	//定义画笔样式数组并初始化
int g_iBrushStyle[6] = {HS_VERTICAL,HS_HORIZONTAL,HS_CROSS,HS_DIAGCROSS,HS_FDIAGONAL,HS_BDIAGONAL};	//定义画刷样式数组并初始化

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

//	PlaySound(L"AIR - 夏影.wav", NULL, SND_FILENAME | SND_ASYNC|SND_LOOP); //循环播放背景音乐

	//【5】消息循环过程
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
LRESULT CALLBACK  WndProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lparam)
{
	PAINTSTRUCT paintStruct;		//定义一个PAINTSTRUCT结构体来记录一些绘制信息

	switch(message)	//switch语句开始
	{
	case WM_PAINT:	//若是客户区重绘消息
		g_hdc = BeginPaint(hwnd, &paintStruct);		//指定窗口进行绘图工作的准备，并用将和绘图有关的信息填充到paintStruct结构体中。
		Game_Paint(hwnd);
		EndPaint(hwnd, &paintStruct);		//EndPaint函数标记指定窗口的绘画过程结束

		ValidateRect(hwnd,NULL);	//更新客户区的显示
		break;	
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
	g_hdc = GetDC(hwnd);		//获取设备环境句柄
	srand((unsigned)time(NULL));	//初始化时间种子

	//随机初始化画笔和话刷的颜色值
	for(int i = 0;i <= 6; ++i)
	{
		g_hpen[i] = CreatePen(g_iPenStyle[i],1,RGB(rand()%256,rand()%256,rand()%256));

		if(i==6)
			g_hBrush[i] = CreateSolidBrush(RGB(rand()%256,rand()%256,rand()%256));
		else
			g_hBrush[i] = CreateHatchBrush(g_iBrushStyle[i],RGB(rand()%256,rand()%256,rand()%256));
	}
	Game_Paint(hwnd);
	ReleaseDC(hwnd,g_hdc);
	return TRUE;
}

//---------------------------------【Game_Paint()函数】------------------------------------
//描述：绘制函数，在此函数中进行绘制操作
//-----------------------------------------------------------------------------------------
VOID Game_Paint(HWND hwnd)
{
	//定义一个y坐标值
	int y=0;

	//一个for循环，用7种不同的画笔绘制线条
	for(int i=0; i<=6 ;++i)
	{
		y = (i+1)*70;

		SelectObject(g_hdc,g_hpen[i]);	//将对应的画笔选好
		MoveToEx(g_hdc,30,y,nullptr);	//“光标”移动到对应的(30,y)坐标处
		LineTo(g_hdc,100,y);			//从(30,y)坐标处向(100,y)绘制线段
	}

	//注意上面画完后y=420
	//定义两个x坐标值
	int x1 = 120;
	int x2 = 190;

	//用7中不同的画刷填充矩形
	for(int i=0;i<=6;++i)
	{
		SelectObject(g_hdc,g_hBrush[i]);	//选用画刷
		Rectangle(g_hdc,x1,70,x2,y);	
		x1+=90;
		x2+=90;
	}
}

//---------------------------------【Game_CleanUp()函数】----------------------------------
//描述：资源清理函数，在此函数中进行退出前资源的清理工作
//-----------------------------------------------------------------------------------------
BOOL Game_CleanUp(HWND hwnd)
{
	for (int i = 0; i <= 6; ++i)
	{
		DeleteObject(g_hpen[i]);
		DeleteObject(g_hBrush[i]);
	}
	return TRUE;
}