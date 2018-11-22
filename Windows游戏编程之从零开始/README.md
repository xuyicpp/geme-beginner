# 逐梦旅程：Windows游戏编程之从零开始
正如这本书的书名所提到的这是一本讲解Windows游戏开发的入门图书。同时应该也是我第一本完整看完的游戏开发的书籍。在这里我主要是，秉着把书读薄的观点，对书中的知识点进行总结，以便自己之后能快速回忆起来，同时还对书中我比较困惑的地方进行补充。

## 本书的构成
下面这段话是作者经过多年的摸索，总结出来的从零开始的学习路线。这个路线可以分为如下几个阶段：
- 第1步：打牢C++编程语言基础
- 第2步：学习Windows API与Windows与生俱来的图形引擎GDI。注意这一步学习适量就可以了，Windows API这边主要学习窗口的创建相关的内容，GDI主要学习如何贴图、贴图的技巧、后备缓冲区思想等一些内容。学完这些内容，可以用GDI开发出一些小游戏了。
- 第3步：学习三维图形API并了解计算机图形学知识DirectX和OpenGL选择其一。这是迈向三维游戏编程的第一步，打好基础非常地重要。
- 第4步：学习三维游戏引擎。推荐选择一款开源的游戏引擎重点突破。
- 第5步：学开源的游戏（引擎）源码，比如QUAKE3、DOOM等等。

我认为这本书主要由三部分构成：分别是windows编程入门、GDI2D游戏编程、DirectX3D游戏编程
- [windows编程入门](https://github.com/xuyicpp/geme-beginner/tree/master/Windows%E6%B8%B8%E6%88%8F%E7%BC%96%E7%A8%8B%E4%B9%8B%E4%BB%8E%E9%9B%B6%E5%BC%80%E5%A7%8B/windows%E7%BC%96%E7%A8%8B%E5%85%A5%E9%97%A8)
这部分主要讲解了如何创建一个完整的windows窗口程序,为后面两部分的内容打好基础。
- [GDI2D游戏编程](https://github.com/xuyicpp/geme-beginner/tree/master/Windows%E6%B8%B8%E6%88%8F%E7%BC%96%E7%A8%8B%E4%B9%8B%E4%BB%8E%E9%9B%B6%E5%BC%80%E5%A7%8B/GDI2D%E6%B8%B8%E6%88%8F%E7%BC%96%E7%A8%8B)
这部分主要利用Windows自带的图形设备接口(Graphics Device Interface)进行一些2D贴图小游戏的开发。
- [DirectX3D游戏编程](https://github.com/xuyicpp/geme-beginner/tree/master/Windows%E6%B8%B8%E6%88%8F%E7%BC%96%E7%A8%8B%E4%B9%8B%E4%BB%8E%E9%9B%B6%E5%BC%80%E5%A7%8B/DirectX3D%E6%B8%B8%E6%88%8F%E7%BC%96%E7%A8%8B)
这部分主要利用Microsoft DirectX进行3D大型游戏的开发入门。同时也介绍了如何将DirectX的API接口进行封装，然后包装成游戏引擎的部分模块。(例如摄像机、地形、天气等)

## 实用网址
- [Windows文档的官网](https://docs.microsoft.com/en-us/previous-versions/windows/desktop)
遇到看不懂的函数就可以在官网上进行查询

## 参考文献
- [本书配套的CSDN博客专栏](https://blog.csdn.net/zhmxy555/column/info/vc-game-programming)
- [作者的BLOG](https://blog.csdn.net/poem_qianmo)
- [【书本源代码】《逐梦旅程：Windows游戏编程之从零开始》by浅墨](https://pan.baidu.com/s/13PVB3%E4%B8%8B%E8%BD%BD%E3%80%82)

