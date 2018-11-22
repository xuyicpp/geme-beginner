//=============================================================================
// Desc: D3DUtil.h头文件，用于公共辅助宏的定义
// 2018年 11月  Create by XY
//=============================================================================

#pragma once	//只要在头文件的最开始加入这条杂注，就能够保证头文件只被编译一次。

#ifndef HR		//先测试x是否被宏定义过,如果x没有被宏定义过，定义x，并编译
#define HR(x) hr = x; {if(FAILED(hr)) {return hr;}}	//自定义一个HR宏，方便执行错误的返回
#endif

#ifndef SAFE_DELETE
#define SAFE_DELETE(p)	{if(p) {delete(p);	(p)=NULL; }}	//自定义一个SAFE_RELEASE()宏,便于指针资源的释放
#endif

#ifndef SAFE_RELEASE
#define SAFE_RELEASE(p) {if(p) {(p)->Release(); (p)=NULL;}}	//自定义一个SAFE_RELEASE()宏,便于COM资源的释放
#endif

