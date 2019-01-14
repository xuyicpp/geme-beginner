#include <math.h>

#include "MathUtil.h"
#include "Vector3.h"

const Vector3 kZeroVector(0.0f, 0.0f, 0.0f);

//通过加上适当的2pi倍数，将角度限制在-pi到pi的区间内
float wrapPi(float theta)		//弧度
{
	theta += kPi;
	theta -= floor(theta * k1Over2Pi) * k2Pi;		//向下取整
	theta -= kPi;
	return theta
}

//safeAcos
//和acos(x)相同，但如果x超出范围将返回最为接近的有效值
//返回值在0到pi之间，和c语言中的标准acos()函数相同
float safeAcos(float x)
{
	//检测边界条件
	if(x <= -1.0f)
		return kPi;
	if(x >= 1.0f)
		return 0.0f;

	//使用标准C函数
	return acos(x)
}