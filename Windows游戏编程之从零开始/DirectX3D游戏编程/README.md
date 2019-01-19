# DirectX3D游戏编程(DX9)
可以看到之前使用最广的DX9c发布至今已有10年了，这本书也是2013年出版的。随着硬件的发展，使用新的图形接口DX11已经成为了必然。但是了解一些DX9的知识，构建一些关于图形接口的基本概念，目前市面上真的是找不到比这本书更通俗易懂的了。在这里我还是简要介绍一下书中的各种基本概念，以便可以快速的回忆起书中的内容。

## 第三部分概览
- D3DdemoCore这个项目是D3D程序的通用框架。与GDIdemoCore并无太大差别，用Direct3D_Init和Objects_Init这两个函数替换了Game_Init函数；Direct3D_Render替换了Game_Paint；Direct3D_CleanUp替换了Game_CleanUp函数。
- Direct3D初始化四部曲：1、创建Direct3D接口对象。2、获取设备硬件信息。3、填充D3DPRESENT_PARAMETERS结构体。4、穿件Direct3D设备接口。简称：创接口、取信息、填内容、创设备。
- Direct3D渲染五部曲：1、清屏操作。2、开始场景。3、正式绘制。4、结束场景。5、翻转显示。

- 第12章：D3Ddemo3：D3D顶点缓存。D3Ddemo4：D3D索引缓存。
- 第13章：D3Ddemo5:D3D四大变换：世界变换、取景变换、投影变换和视口变换。
- 第14章：D3Ddemo6：D3D中几种几何体的快捷绘制。D3Ddemo7：光照与材质。
- 第15章：D3Ddemo8：游戏输入控制利器：DirectInput示例程序。D3Ddemo9：对DirectInput的封装。
- 第16章：D3Ddemo10、11：纹理映射。
- 第17章：D3Ddemo12：网络模型和X文件使用。
- 第18章：D3Ddemo13：Alpha混合技术示例程序。(透明效果)
- 第19章：D3Ddemo14：深度测试和Z缓存示例程序。(遮挡)
- 第20章：D3Ddemo15：模板技术示例程序。(镜子)
- 第21章：D3Ddemo16：第一人称摄像机的实现示例程序。
- 第22章：D3Ddemo17：三维地形系统。
- 第23章：D3Ddemo18：三维天空的实现。
- 第24章：D3Ddemo19：三维粒子系统。(雪花)
- 第25章：D3Ddemo20：多游戏模型的载入示例程序。

## 关键词
DX9、索引缓存、四大变换、材质光照、

## D3DdemoCore
### 什么是DirectX
通常游戏开发人员谈论起DirectX一般指，DirectX SDK(我们一般叫它DirectX开发包)或称DirectX API(DirectX应用程序可编程接口),用于开发高性能多媒体程序的应用程序接口。现在最新版本为DirectX 12(截止20190118)。
DirectX是微软公司提出的一种应用程序接口(API)，由C++编写它可以让Windows为平台的游戏或多媒体程序获得更高的执行效率，加强3D图形和声音效果。

### D3DdemoCore代码示例
- [D3DdemoCore完整代码](https://github.com/xuyicpp/geme-beginner/blob/master/Windows%E6%B8%B8%E6%88%8F%E7%BC%96%E7%A8%8B%E4%B9%8B%E4%BB%8E%E9%9B%B6%E5%BC%80%E5%A7%8B/DirectX3D%E6%B8%B8%E6%88%8F%E7%BC%96%E7%A8%8B/D3DdemoCore/D3DdemoCore/main.cpp)

## [D3D顶点缓存与索引缓存](https://github.com/xuyicpp/geme-beginner/blob/master/Windows%E6%B8%B8%E6%88%8F%E7%BC%96%E7%A8%8B%E4%B9%8B%E4%BB%8E%E9%9B%B6%E5%BC%80%E5%A7%8B/DirectX3D%E6%B8%B8%E6%88%8F%E7%BC%96%E7%A8%8B/D3Ddemo4/D3Ddemo4/main.cpp)
三角形是Direct3D中绘制图形的基本单元，我们绘制任何图形，都是用大量三角形组合起来。如果我们用顶点缓存绘制一个正方形，写两个三角形，然后进行绘制就可以了(大概需要6个顶点缓存)。众所周知一个正方形有4个顶点，所以造成更大的存储空间和更大的开销。
索引缓存保存了构成物体的顶点在顶点缓存的索引值，通过索引查找对应的顶点，以完成图形的绘制。

## [四大变换](https://github.com/xuyicpp/geme-beginner/blob/master/Windows%E6%B8%B8%E6%88%8F%E7%BC%96%E7%A8%8B%E4%B9%8B%E4%BB%8E%E9%9B%B6%E5%BC%80%E5%A7%8B/DirectX3D%E6%B8%B8%E6%88%8F%E7%BC%96%E7%A8%8B/D3Ddemo5/D3Ddemo5/main.cpp)
- 世界变换：根据物体模型的大小、方向以及其他模型之间的相对关系，世界变换将物体模型从自身的局部坐标系中转换到时间坐标系中，并将所有的物体模型组织为一个场景。世界变换包括平移、旋转和缩放变换，我们可以通过D3DX库中D3DXMatrixTranslation、D3DXMatrixRotation、D3DXMatrixSaling函数进行变换。
- 取景变换：设置Direct3D中的虚拟摄像机的位置和观察点。
- 投影变换：投影变换负责将位于视截体内的物体模型映射到投影窗口中。D3DX库中的D3DXMatrixPerspectiveForLH函数可以用来计算一个视截体，并根据该视截体的描述信息创建一个投影矩阵变换。
- 视口变换：用于将投影窗口中的图形转换到显示屏幕的程序窗口中。(在窗口中哪些地方显示)

## [光照与材质](https://github.com/xuyicpp/geme-beginner/blob/master/Windows%E6%B8%B8%E6%88%8F%E7%BC%96%E7%A8%8B%E4%B9%8B%E4%BB%8E%E9%9B%B6%E5%BC%80%E5%A7%8B/DirectX3D%E6%B8%B8%E6%88%8F%E7%BC%96%E7%A8%8B/D3Ddemo7/D3Ddemo7/main.cpp)
- 物体表面的材质属性（D3DMATERIAL9）：漫反射光、环境光、镜面反射光、自发光、镜面反射指数
- 三种光源类型：点光源、平行光源、聚光灯

## [对DirectInput的封装](https://github.com/xuyicpp/geme-beginner/blob/master/Windows%E6%B8%B8%E6%88%8F%E7%BC%96%E7%A8%8B%E4%B9%8B%E4%BB%8E%E9%9B%B6%E5%BC%80%E5%A7%8B/DirectX3D%E6%B8%B8%E6%88%8F%E7%BC%96%E7%A8%8B/D3Ddemo9/D3Ddemo9/main.cpp)
所以尽管当前DirectX的最新版本上升到了DirectX 11，DirectInput还是DirectX 8那个版本时代的老样子，API的内容和功能随着最近几个版本的更迭却原封不动，名称上也保留了8这个版本号，依然叫DirectInput 8。
https://blog.csdn.net/poem_qianmo/article/details/8547531

## [纹理映射](https://github.com/xuyicpp/geme-beginner/blob/master/Windows%E6%B8%B8%E6%88%8F%E7%BC%96%E7%A8%8B%E4%B9%8B%E4%BB%8E%E9%9B%B6%E5%BC%80%E5%A7%8B/DirectX3D%E6%B8%B8%E6%88%8F%E7%BC%96%E7%A8%8B/D3Ddemo11/D3Ddemo11/main.cpp)
- 纹理映射是一种将2D图像映射到3D物体上的技术
- 纹理使用四部曲：顶点的定义、顶点的访问、纹理的创建、纹理的启用。

## [第一人称摄像机的实现示例程序](https://github.com/xuyicpp/geme-beginner/blob/master/Windows%E6%B8%B8%E6%88%8F%E7%BC%96%E7%A8%8B%E4%B9%8B%E4%BB%8E%E9%9B%B6%E5%BC%80%E5%A7%8B/DirectX3D%E6%B8%B8%E6%88%8F%E7%BC%96%E7%A8%8B/D3Ddemo16/D3Ddemo16/main.cpp)
一个实现了虚拟摄像机的类

