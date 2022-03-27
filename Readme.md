# Falut Assessment for Escalator

## 1.背景

* 这是一个扶梯健康诊断的项目，用于智慧扶梯系统的扶梯健康诊断模块。

  针对在城市地铁站中的扶梯，安装上一系列传感器来时时的收集扶梯的数据。这些数据可以**反应这部扶梯当前的工况**，具体主要包括扶梯各个关键部件的振动加速度、扶梯的负载大小、扶梯关键部件温度、扶梯运行噪声等。在**正常扶梯**和有**潜在故障扶梯**（如电机故障、齿轮轴承长期磨损甚至断裂等）上收集到的数据有一定差异性的。基于这些数据的差异性，就可以来**判断当前扶梯的工况**，从而**诊断扶梯的健康状况**，以保证扶梯的安全运行。

* 扶梯公司本身开发了一套软件配套硬件来支持智慧扶梯系统，**流程**如下：

  *扶梯+传感器+现场设备* ——> *传感器数据采集* ——> *数据存储到本地数据库* ------> **获取扶梯健康状态** ------> *数据上传云端平台集中* ——> *用于智慧扶梯系统*

* 在扶梯安全方面只能实现**原始传感器数据**的搜集和上传等流程，而我们需要**开发一个Demo作为扶梯健康诊断模块的核心**来获取扶梯健康状态。其不仅需要**实现基本的健康诊断功能**，并且要能稳定地**嵌入到其公司原有软件中使用**。

## 2.简介

* 项目目的：开发一个Demo作为扶梯健康诊断模块的核心。
* 项目任务：分为3个部分，机械部分+算法部分+软开部分。机械部分由我的partner完成，**算法和软开两个部分由我完成**。
  1. 机械部分：对扶梯工况的判断；利用Ansys和Solidworks等软件实现故障模拟、获取机械仿真数据。

  2. 算法部分：基于**Matlab平台**实现原始信号的信号处理算法、机器学习算法、综合判断算法。

  3. 软开部分：基于**C#语言**和**.Net平台**，集成机械仿真结果和算法，完成Demo的开发，并与公司原有软件进行**联调测试**。
* Demo功能：实现对扶梯健康状况的诊断。功能的实现具体包括三个步骤：
  1. 对原始信号数据进行**信号处理**和**特征提取**。
  2. 机器学习算法实现工况判断， **判定扶梯的潜在故障**。
  3. 综合判断给出一个扶梯健康指标。

## 3.技术细节

### 1.算法开发

* 算法的实现：基于**Matlab平台**，开发算法，并**以库函数的形式**实现，**开放API**，以供Demo中调用。

* 算法库：算法主要基于**信号处理、机械结构和机器学习**三个方面的知识，目前大体分为五部分：
  * 特征提取算法：基于傅里叶变换等算法，实现信号的频域变换，同时提取时域和频域下的数个**典型特征值**。
  * 基频分析算法：基于机械原理和扶梯机械机构，获取扶梯关键部件（如齿轮、链条、轴承等）的振动基频。
  * 谱分析算法：基于信号处理与频域变换，获取谱图数据，用于公司软件的**可视化**。
  * 机器学习算法：基于传统机器学习算法（如SVM分类器，决策树等）实现**对扶梯运行工况的识别和分类**。机器学习的模型训练依赖实测数据和机械仿真数据。
  * 综合判断算法：基于机器学习算法的识别结果，结果信号和机械方面的特征提取以及辅助传感器数据，综合判定当前扶梯的健康程度。

### 2.Demo开发

* Demo的开发是基于**.Net平台和Matlab平台**。开发的要求：1. **模块化编程**，以便二期工程优化升级； 2.**可移植性好**，以便集成到现有软件中。
* Demo任务**总体流程**：原始数据读入 ——> 数据处理 ——> 调用算法（算法调用次序和步骤参见第2节简介） ——> 输出健康指标+请求数据
* Demo开发**难点**：在.Net平台的C#编程中对Matlab算法的调用。解决这一难点的思路是**C#和Matlab的混合编程**。
* **Demo传送门**：[Demo](https://github.com/Fantasty9413/Failure-Analysis-of-Escalator/tree/main/TVforClient/TypicalValeExtraction)

### 3.C#与Matlab的混合编程

* C#与Matlab的混合编程的**主要思路**：**算法移植+算法封装**
* **算法移植**：将Matlab中实现的算法库移植到.Net平台。
  1. 在Matlab中**以库函数**的形式实现核心算法。
  2. 借助Matlab-deploytool工具箱子，将核心算法转为**动态链接库**，**嵌入Demo**以便使用。
  3. 配置环境，在.Net平台中进行封装、调用和调试。
* **算法封装**：对核心算法、数据处理、调用核心算法等进行封装，留出指定接口。其目的一是为了模块化编程，方便二次开发；二是为了解决.**Net和Matlab平台数据类型不同**的问题。
  * .Net平台下**使用C#语音编程**，数据类型是常见的int、float、string等。核心算法接口所需数据类型：MW，这是一种基于Matlab解释器的数据类型，有各种子类，囊括了结构体、数组和多维矩阵等数据。数据类型的差异性导致**C#方法无法直接调用移植后的算法**，需要通过**数据转换**来解决这一问题。
  * 转换方法：Matlab有相应的数据转换方法，已经集成为**动态链接库形式**，只需嵌入demo，即可**调用相应库**进行两种数据的相互转换。
  * Demo中**数据类型变化流程**概述：---（数据输入）---> *C#数据类型* ---（处理和封装）---> *MW数据类型* ---（调用核心算法）---> *MW数据类型*  ---（封装和处理）---> *C#数据类型* ---（数据输出）---> 

### 4.其他

* 环境配置：公司原软件运行在工控机上Windows环境下，Demo要从现有的装有Matlab的环境中迁移过去（要**脱离Matlab软件**，公司使用Matlab会收费）。
  * 解决方法：使用免费的MCR（Matlab Complier Runtime）实现对核心算法的编译运行。
  * 最终运行环境环境：Windows + MCR + .Net + Demo
* 测试：包括整套系统和开发细节两个方面。
  * 系统方面：整套系统的**难点是跨平台的算法调用**，需要保证系统能成功调用算法，满足时间和精度的要求。否则需要重新更改方案。
  * 开发方面：1. 开发移植中的调试；2. 集成到原软件后的联调。在调试中解决移植Bug和环境错误，目的主要是为了将整套开发流程摸索清楚，方便后续项目和二次开发。

* 文档：撰写相应文档，记录整个项目和Demo的开发、移植、使用。
  * [项目笔记文档](https://github.com/Fantasty9413/Failure-Analysis-of-Escalator/blob/main/ProgramNote.md)
  * [Demo文档](https://github.com/Fantasty9413/Failure-Analysis-of-Escalator/tree/main/TVforClient/TypicalValeExtraction/Readme.md)
  * [环境配置文档](https://github.com/Fantasty9413/Failure-Analysis-of-Escalator/blob/main/TVforClient/TypicalValeExtraction/Environment%20Configuration.md)
  * [Matlab2C#编程](https://github.com/Fantasty9413/Failure-Analysis-of-Escalator/blob/main/TVforClient/MatlabToC%23/Readme.md)
  * [机器学习算法_v1_测试记录](https://github.com/Fantasty9413/Failure-Analysis-of-Escalator/blob/main/FaultAssessment/Readme.md)
  * [机器学习算法_v2_测试记录](https://github.com/Fantasty9413/Failure-Analysis-of-Escalator/blob/main/FaultAssessment2/Readme.md)


## 4.项目展示

* 项目整体展示

  <div align=center>
  <img src="https://github.com/Fantasty9413/Failure-Analysis-of-Escalator/blob/main/Image/%E9%A1%B9%E7%9B%AE%E5%B1%95%E7%A4%BA.png">
  </div>

* Demo部分方法封装接口展示

  <div align=center>
  <img src="https://github.com/Fantasty9413/Failure-Analysis-of-Escalator/blob/main/Image/%E9%83%A8%E5%88%86%E5%B0%81%E8%A3%85%E6%8E%A5%E5%8F%A3%E5%B1%95%E7%A4%BA.png">
  </div>

* 前期算法移植测试对比

  <div align=center>
  <img src="https://github.com/Fantasty9413/Failure-Analysis-of-Escalator/blob/main/Image/%E7%AE%97%E6%B3%95%E7%A7%BB%E6%A4%8D%E6%B5%8B%E8%AF%95%E5%AF%B9%E6%AF%94.png">
  </div>

* 前期机器学习算法测试结果

  <div align=center>
  <img src="https://github.com/Fantasty9413/Failure-Analysis-of-Escalator/blob/main/Image/%E6%9C%BA%E5%99%A8%E5%AD%A6%E4%B9%A0%E7%AE%97%E6%B3%95%E6%B5%8B%E8%AF%95.png">
  </div>

* Demo嵌入智慧扶梯系统软件联调测试-1

  <div align=center>
  <img src="https://github.com/Fantasty9413/Failure-Analysis-of-Escalator/blob/main/Image/%E8%81%94%E8%B0%83%E6%B5%8B%E8%AF%95.png">
  </div>

* Demo嵌入智慧扶梯系统软件联调测试-2

  <div align=center>
  <img src="https://github.com/Fantasty9413/Failure-Analysis-of-Escalator/blob/main/Image/%E8%81%94%E8%B0%83%E8%BF%90%E8%A1%8C.jpg">
  </div>

