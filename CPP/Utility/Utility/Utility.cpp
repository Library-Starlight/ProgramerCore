// Utility.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <iostream>

void FakeVeriCode();
void Net_GetVeriCode(uint8_t* tr, uint8_t* sr, uint8_t* vr);

int main()
{
	//std::cout << "Hello World!\n";

	/*
	int v = 1;
	int* p = &v;
	std::cout << p;
	std::cout << '\n';
	std::cout << *p;
	std::cout << '\n';*/

	//double arr[5] = { 1, 2, 3, 4, 5 };
	//std::cout << arr[1];
	//std::cout << '\n';
	//std::cout << *arr;
	//std::cout << '\n';

	//double* p = arr;
	//std::cout << *p;
	//std::cout << '\n';
	//p++;
	//std::cout << *p;

	FakeVeriCode();
}

/// <summary>
/// 伪造验证码
/// </summary>
void FakeVeriCode()
{
	//11112222:veri:aaaaaaaaaabbbbbbbbbbbbbbbbbbbb
	//12345678:veri:123ABC4560AA53A5B423F4147A223C
	uint8_t *tr, *sr, *vr;

	// 10位: 123ABC4560
	uint8_t trArr[10] = { 0x31, 0x32, 0x33, 0x41, 0x42, 0x43, 0x34, 0x35, 0x36, 0x30 };

	// 20位ascii码AA53A5B423F4147A223C
	// 转换为16进制 => 0xAA, 0x53, 0xA5, 0xB4, 0x23, 0xF4, 0x14, 0x7A, 0x22, 0x3C
	//uint8_t srArr[20] = { 0x41, 0x41, 0x35, 0x33, 0x41, 0x35, 0x42, 0x34, 0x32, 0x33, 0x46, 0x34, 0x31, 0x34, 0x37, 0x41, 0x32, 0x32, 0x33, 0x43 };
	uint8_t srArr[20] = { 0xAA, 0x53, 0xA5, 0xB4, 0x23, 0xF4, 0x14, 0x7A, 0x22, 0x3C };

	uint8_t vrArr[10] = {};

	tr = trArr;
	sr = srArr;
	vr = vrArr;

	Net_GetVeriCode(tr, sr, vr);
	for (int i = 0; i < 10; i++)
	{
		std::cout << (int)vr[i];
		std::cout << '\n';
	}

	//std::cout << (int)vr[0];
	//std::cout << (int)vr[1];
	//std::cout << (int)vr[2];
	//std::cout << (int)vr[3];
	//std::cout << (int)vr[4];

	//vr[0] = 5;
	//std::cout << (int)vr[0];
	//std::cout << (int)vr[1];
	//std::cout << (int)vr[2];
	//std::cout << (int)vr[3];
	//std::cout << (int)vr[4];

	// tr
	/*std::cout << (int)trArr[9];
	std::cout << '\n';
	
	tr = trArr;
	std::cout << (int)tr[5];*/
}

/// <summary>
/// 获取验证码
/// </summary>
/// <param name="tr">临时码</param>
/// <param name="sr">预设码</param>
/// <param name="vr">最终验证码</param>
void Net_GetVeriCode(uint8_t* tr, uint8_t* sr, uint8_t* vr)
{
	*vr = (uint8_t)((tr[0] * 3 + sr[3] * 5));vr++;
	*vr = (uint8_t)((tr[1] + sr[6]) / 3);vr++;
	*vr = (uint8_t)((~tr[2] + sr[0]) + 8);vr++;
	*vr = (uint8_t)((tr[3] + sr[7] + 38) / 7);vr++;
	*vr = (uint8_t)((tr[4] * 3 + sr[2] + 46) / 9);vr++;
	*vr = (uint8_t)((tr[5] * 7 + sr[1] + 27) / 13);vr++;
	*vr = (uint8_t)((uint8_t)(tr[6] + 8) ^ (uint8_t)(sr[5] + 7));vr++;
	*vr = (uint8_t)(tr[7] + tr[4] + sr[9] + sr[3]);vr++;
	*vr = (uint8_t)(~((uint8_t)(tr[8] + sr[8] + 21)));vr++;
	*vr = (uint8_t)((tr[9] + tr[6] + sr[4] + sr[7] + 3));vr++;
}

// 运行程序: Ctrl + F5 或调试 >“开始执行(不调试)”菜单
// 调试程序: F5 或调试 >“开始调试”菜单

// 入门使用技巧: 
//   1. 使用解决方案资源管理器窗口添加/管理文件
//   2. 使用团队资源管理器窗口连接到源代码管理
//   3. 使用输出窗口查看生成输出和其他消息
//   4. 使用错误列表窗口查看错误
//   5. 转到“项目”>“添加新项”以创建新的代码文件，或转到“项目”>“添加现有项”以将现有代码文件添加到项目
//   6. 将来，若要再次打开此项目，请转到“文件”>“打开”>“项目”并选择 .sln 文件
