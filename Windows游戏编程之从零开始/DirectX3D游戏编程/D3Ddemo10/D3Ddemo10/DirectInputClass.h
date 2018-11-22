//=============================================================================
// Name: DirectInputClass.h
//	Des: 封装了DirectInput键盘输入处理类的头文件
// 2018年 11月  Create by XY
//=============================================================================

#pragma once		//只要在头文件的最开始加入这条杂注，就能够保证头文件只被编译一次。
#include "D3DUtil.h"
#define DIRECTINPUT_VERSION 0x0800
#include <dinput.h>

//DInputClass类定义开始
class DInputClass
{
private:
	IDirectInput8	*m_pDirectInput;		//IDirectInput8接口对象
	IDirectInputDevice8		*m_KeyboardDevice;		//键盘设备接口对象
	char			m_keyBuffer[256];		//用于键盘键值存储的数组

	IDirectInputDevice8		*m_MouseDevice;	//鼠标设备接口对象
	DIMOUSESTATE	m_MouseState;			//用于鼠标键值存储的一个结构体

public:
	DInputClass(void);						//构造函数
	~DInputClass(void);						//析构函数
public:
	//初始化DirectInput键盘及鼠标输入设备
	HRESULT Init(HWND hWnd,HINSTANCE hInstance,DWORD keyboardCoopFlags,DWORD mouseCoopFlags);
	void GetInput();	
	bool IsKeyDown(int iKey);
	bool IsMouseButtonDown(int button);		
	
	float MouseDX();		//
	float MouseDY();		//
	float MouseDZ();		//返回鼠标的Z轴坐标值
};