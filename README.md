# Note of Risk Assesment of Escalator

## 1.文件记录

m文件运行版本：**matlab2016b**

### 0.Scripts

记录一些脚本计算文件，一般用于一次性的数据处理。

* FrequenceofParts.m： 处理扶梯参数，计算各零部件的运动频率，并存储至escalator_parametersmat文件数据 中，以便后续分析。参考论文：《频率分析在扶梯调试中的应用》

### 1.Hilberts-wp

文件包含Hilbert分析衍生方法及MATLAB实现的源代码，[使用方法及参考见此]([希尔伯特谱、边际谱、包络谱、瞬时频率/幅值/相位——Hilbert分析衍生方法及MATLAB实现 - 知乎 (zhihu.com)](https://zhuanlan.zhihu.com/p/136447202))。

### 2.Data

* test_data.mat ：测试数据集，含有时间$t$和测试信号$singal$​（取自通道5），格式均为长度为10000的向量。

* escalator_parameters.xlsx：电梯的扶梯参数和电机参数，用于做零部件运动频率的计算。具体内容与方法参考文献：《频率分析在扶梯调试中的应用》。

* escalator_parameters.mat：escalator_parameters.xlsx的参数信息导入matlab，并调用`FrequenceofParts.m`文件计算了各零部件运动频率，再保存至mat数据中。

  目前数据结构为：

  ```c
  struct escalator
  {
  	string name;		// 电梯名或编号
  	double motor;		// 电机参数
  	double ladder;		// 扶梯参数
  	struct frequence;	// 各零部件频率
  };
  ```


* signal(with typical value).mat：信号结构体，用于存储一段信号的各种信息，包括时域信号、频域信号和各种特征值。

  ```c
  struct signal
  {
      struct escalator;	// 采样信号来自的采样电梯
      struct TD;			// 时域信号（time domian）：幅值（TD.data）和时间(TD.time)
      struct FD;			// 频域信号（frequency domain）：幅值（FD.data）和时间(FD.f)
      struct TV;			// 特征值
  };
  
  struct TV
  {
      double pv;			// 峰值
      double ppv;			// 峰峰值
      double kv;			// 峰度（波形陡峭度）
      double Nbf;			// 倍频值
      struct pvifds;		// 高中低频信号峰值
      double tfv;			// 通频值
  }
  ```


### 3.By_Hilberts

参考*Hilberts-wp*源代码，使用测试数据*test_data.mat*分析，绘制**Hilberts谱**和**包络谱**等。



### 4.TypicalValue

用于对信号进行特征值的计算与提取。其中`function`文件的概述如下：

| 函数 | 功能 | 说明与备注 |
| :----: | :----: | :----: |
| `[ tfv ] = TV_tfv( FD )` | 获取通频值 | tfv(through frequency value)，输入FD为频域信号结构体 |
| `[ pv, f ] = TV_pvifds( FD, fds )` | 获取高中低频段的信号峰值和对应的频率 | fds为频段选择：'low', 'mid', 'high' |
| `[ kv ] = TV_kv( TD )` | 获取时域波形峰度（即陡峭度） | 输入TD为时域信号结构体 |
| `[pv_max,t_max,pv_min,t_min] = TV_pv(TD)` | 获取时域信号的峰值及其对应时间 |  |
| `[ ppv ] = TV_ppv( TD )` | 获取时域信号的峰峰值 |  |
| `[ Nbf ] = TV_Nbf( FD, bf, N )` | 获取1至N倍基频时的倍频信号幅值 | 输入bf为基频值(basic frequency) |
| `[ amplitude, f ] = fft_ss( y, Fs )` | 获取时域信号单边fft变换的频域信号 | 输入Fs为采样频率 |

### 5.TVforClient

* TypicalValeExtraction\

  基于C#的特征值提取分析库。具体见相关文件下的Readme文档。

* Test\

  TypicalValeExtraction系统的测试开发。

* MatlabToC#\

  * Readme.m：记录了matlab算法转.net组件的方法与实现。
  * Lib\：
    * Algotithm\：基于`TypicalValue`中的算法，更改接口，用于打包生成.net组件作为C#分析的核心算法。
    * TypivalValueExtraction_v1.0\：TypivalValueExtraction lib的初始版本。
    * TypivalValueExtraction_v1.1\：修复了上一版本的Nbf方法的bug。

### 6.SpectrumAnalysis

​	用于谱分析的库。

|                    函数                     |      功能      | 说明与备注 |
| :-----------------------------------------: | :------------: | :--------: |
| `[ FD_amp, FD_f ] = FreSpec( TD_amp, Fs )`  |  获取频谱数据  |            |
| `[ PS_amp, PS_f ] = PowSpec( TD_amp, Fs )`  | 获取功率谱数据 |            |
| `[ ES_amp, ES_f ] = EnveSpec( TD_amp, Fs )` | 获取包络谱数据 |            |
| `[ CS_amp, CS_f ] = CepsSpec( TD_amp, Fs )` |  获取倒谱数据  |            |

## 2.实验进展与结果

仿真结果的截图记录在*Figure*文件夹中。

### 0.Main

* 倒谱

![实倒谱](E:\Code_master\Risk Assessment of Escalator\Figure\实倒谱.jpg)

==对故障信号做对比==

* 功率谱

![功率谱](E:\Code_master\Risk Assessment of Escalator\Figure\功率谱.jpg)

第一次波峰：160Hz左右；幅值最大处：440Hz左右

### 1.By_Hilberts

* 希尔伯特谱

![test_data的希尔伯特谱](E:\Code_master\Risk Assessment of Escalator\Figure\By_Hilberts\test_data的希尔伯特谱.jpg)

每间隔0.3s出现一次希尔伯特谱分量。

==猜测是否为周期性的共振信号？验证是否为0.8s一次数据采集；下一步测试换测试信号。==

* 包络谱

![test_data的包络谱](E:\Code_master\Risk Assessment of Escalator\Figure\By_Hilberts\test_data的包络谱.jpg)

频率为0处的分量最大，其次为频率为35Hz处存在一个较大的分量。

==考虑故障信号的对比实验。==

## 3.Note

### 3.1Spectrum

* 希尔伯特谱（Hilbert Spectrum）：一种时频谱，做非平稳信号（例如例子中的故障信号）的重要手段，使用这种类型的分析方法强调的就是“变化”，即特征在时间尺度上的改变——因为如果信号没有随时间发生变化，使用频域分析手段就够了。

  Hilbert-Huang 变换（HHT）：用于**特征提取**。反映的是信号的时频特征，可以获取频率成分随时间的“变化”（可以对**局部特征**进行反映，因为EMD可以自适应地进行时频局部化分析，有效提取原信号的特征信息）。==可以了解尝试下EMD==

  [参考]([希尔伯特-黄变换（HHT）的前世今生——一个从瞬时频率讲起的故事 - 知乎 (zhihu.com)](https://zhuanlan.zhihu.com/p/124257081))

  ==下一步考虑查看各个IMF分量==

* 包络谱：常机械产品故障诊断（尤其是轴承）。与频谱结果差异较大，包络谱更适用于做故障特征提取。

  包络谱的求法是：目标信号→希尔伯特变换→得到解析信号→求解析信号的模→得到包络信号→傅里叶变换→得到Hilbert包络谱。

  利用matlab的`envelope()`方法来求解，具体见参考1

  [参考1](https://blog.csdn.net/heavy_truck/article/details/115608961)

* 倒谱：倒谱分析可以分离输入信号和系统信号（即输入特性与系统特性的分离），还可以去除边带频率。

  横轴是~~时间~~伪频率，纵轴单位应该不变。我理解的其意义是将原信号按照性质分解成几个叠加信号。

  计算方法（定义）：对信号进行傅里叶变换，再对傅里叶谱进行对数变换，再进行傅里叶反变换。

  若傅里叶谱是复数，则得到复倒谱；若反变换后只取实数部分，则得到实倒谱。

  matlab中求倒谱可以用定义的方法（移植的话还是用定义来做，避免调用太多库）；也可以直接调用函数`rceps()`（实数谱）和`cceps()`（复数谱）。

  ```matlab
  function [y] = rceps(x)
  	y = real(ifft(log(abs(fft(x)))));
  end
  
  function [y] = cceps(x)
  	h = fft(x);
  	logh = log(abs(h)) + sqrt(-1)*rcunwrap(angle(h));
  	y = real(ifft(logh));
  end
  ```

  ==倒谱可以用来求包络线？（见参考2、3）==

  [参考1]([[转载\]倒谱分析的意义_meyyq-北京_新浪博客 (sina.com.cn)](http://blog.sina.com.cn/s/blog_69230c350102via8.html))	[参考2]([语音处理中的倒谱分析 - 知乎 (zhihu.com)](https://zhuanlan.zhihu.com/p/67707430))	[参考3]([语音信号处理基础（八）——同态处理、倒谱、复倒谱_张亚楠的博客-CSDN博客](https://blog.csdn.net/qq_40644291/article/details/103364136))

* 功率谱：功率密度谱，**显示信号率在频域的分布状况**（功率随频率变化而变化，横坐标频率，纵坐标功率）。功率谱所覆盖的面积在数值上等于信号的总功率。可以用于分析某一频率信号的强弱。

  要得到功率谱过程较为复杂，大致过程是先对信号做自相关，再做FFT运算。得到的是对功率谱的**估计值**。且随着对信号处理过程的参数的不同，得到的功率谱的结果也有所差异。

  求功率谱的具体方法有很多种：直接法、间接法、改进直接法（Bartlett法、Welch法）	[参考1]([MATLAB处理信号得到频谱、相谱、功率谱_慧联工作室的博客-CSDN博客_matlab求功率谱](https://blog.csdn.net/qq_38426337/article/details/81188455))	[参考2]([数字信号中功率谱估计相关方法简介及MATLAB实现_cqfdcw的博客-CSDN博客_matlab功率谱](https://blog.csdn.net/cqfdcw/article/details/84952301))	[参考3]([经典功率谱估计及Matlab仿真 - J博士 - 博客园 (cnblogs.com)](https://www.cnblogs.com/jacklu/p/5140913.html))

  * 直接法（周期图法）：把随机序列的所有数据视为一能量有限的序列，直接计算整个序列的离散傅立叶变换，再取幅值的平方，并除以数据长度，作为对真实功率谱的估计。
  * 间接法：间接法先由序列x(n)估计出自相关函数R(n)，然后对R(n)进行傅立叶变换，便得到x(n)的功率谱估计。
  * Bartlett法（分段平均周期图法）：将长序列分段求周期图再平均。
  * Welch法（加窗平均周期图法）：相对Bartlett法有两点改进。
    * 改进1：加窗。在信号序列分段后，使用非矩形窗口对每一小段信号进行预处理（直接法用的是矩形框）。加窗后可使普估计非负。
    * 改进2：重叠。分段时可允许段间存在重叠，这样可以减少结果的方差。

  matlab中求功率谱调用`pwelch()`函数（定义来求太复杂了，`pwelch()`函数也很复杂，而且不是很清楚怎么选参数），具体见matlab的document。

  ```matlab
  [pxx,f] = pwelch(x,window,noverlap,nffts,fs);
  % window:the length of each section
  % noverlap:the num of elements overlapped
  % nffts:the points for fft
  plot(f,10*log10(pxx))
  ```

  

### 3.2 typical value
* 均值、方差与标准差

  均值：$\mu = \frac{\sum x}{n}$​​

  方差(variance)：$\sigma ^2$，标准差(standard deviation)：$\sigma$。
  $$
  \sigma ^2 = \frac{\sum(x- \overline {x})^2}{n}
  $$

* 波形陡峭度（峰度，kurtosis）

  **峰度**（Kurtosis）衡量实数随机变量概率分布的峰态。峰度高就意味着方差增大是由低频度的大于或小于平均值的极端差值引起的。

  通常定义为四阶累积量除以二阶累积量的平方或者四阶累积量除以二阶累积量的平方减3。
  $$
  kurt(x) = \frac{\mu ^4}{\sigma ^4}
  $$
  

