%% 希尔伯特谱、边际谱、包络谱、瞬时频率/幅值/相位等计算画图代码(公开版)
% 运行该代码前，请务必安装时频域分析工具箱TFA_Toolbox，获取地址：http://www.khscience.cn/docs/index.php/2020/04/09/1/
% 本程序2020.08定型，后续更新都在完整版代码中
%  知乎原文链接：https://zhuanlan.zhihu.com/p/124257081
%               https://zhuanlan.zhihu.com/p/136447202
%  本代码地址：http://www.khscience.cn/docs/index.php/2020/08/08/hilbert/
%% 
clear all
close all
%% 1.故障轴承信号
[fs,t,signal] = ImportData();
figure;plot(t,signal);title('原始数据')    %绘制原始信号图像
%% 2.绘制希尔伯特谱
imf = kEMD(signal);  
data = imf(1,:);
hhtSpec(data',fs)
% 绘制信号希尔伯特谱
% 输入：
% data： 待分析信号
% fs：   采样频率，当fs未知时，可以将fs设置为空（[]），并将type设置为2。此时hht图纵轴将经过标准化
% type： 当type的值为1时，则优先采用MATLAB自带库中的hht函数进行画图，采用的绘图风格也与自带hht函数保持一致，此时hht函数的用法与MATLAB自带函数一致；
%        当type的值为2时，则强制采用第三方库文件中的希尔伯特谱函数进行画图，不过可能会存在内存不够无法顺利画图的可能
%        如果type没有传入参数，则该函数内将type设置为1

% 关于希尔伯特变换及希尔伯特谱更多资料请看这里：
% https://zhuanlan.zhihu.com/p/136447202
% https://zhuanlan.zhihu.com/p/124257081
%% 3.绘制边际谱
marginalSpec(imf,fs);
% 绘制边际谱
% [mgS,f] = marginalSpec(imf,fs,type)
% 输入：
% imf：  imf分量，注意方向：imf是每行一个信号分量的矩阵
% fs：   采样频率
% type： 当type的值为1时，则优先采用MATLAB自带库中的函数进行运算；
%        当type的值为2时，则强制采用第三方库文件中的函数进行运算，不过可能会存在内存不够无法顺利画图的可能（数组超过预设的最大数组大小）
%        如果type没有传入参数，则该函数内将type设置为1
%        经测试由于算法不同，使用两种库函数做出的边际谱存在幅值差异
% 输出：
% mgS：  边际谱幅值。（导出该变量需要使用完整版代码）
% f：    边际谱的频率轴。（导出该变量需要使用完整版代码）
%% 4.绘制包络谱
clear %清空工作区
% load dataEnv.mat  %加载轴承故障信号，外环故障频率为83.33Hz
[fs,t,signal] = ImportData();
envSpec(signal,fs);  %绘制包络谱
% 求信号包络谱
% function [envS,f,xEnv] = envSpec(data,fs)
% 输入：
% data： 待分析信号
% fs：   采样频率
% 输出：
% envS： 包络谱数值（导出该变量需要使用完整版代码）
% f：    包络谱频率轴（导出该变量需要使用完整版代码）
% xEnv： 包络线（导出该变量需要使用完整版代码）
%% 5.瞬时频率/幅值/相位
imf = kEMD(signal);
[insF,insP,insA] = InsFPA(imf(1,:),fs);
% 求信号的瞬时频率、瞬时相位和瞬时幅值
% 输入：
% data： 目标信号
% fs：   目标信号的采样频率
% 输出：
% insF： 瞬时频率
% insP： 瞬时相位
% insA： 瞬时幅值

% 画图。注意InsFPA函数中是不包含画图程序的，如需画图可以参考下述写法：
figure('Color','white');plot(t,insF,'k');title('瞬时频率');
figure('Color','white');plot(t,insP,'k');title('瞬时相位');
figure('Color','white');plot(t,insA,'k');title('瞬时幅值');

%% 关于完整版代码：
% 公开版代码的函数文件为p文件，可以被调用，但无法查看代码。
% 完整版代码中全部为m文件，m文件可以查看源码并自由修改。
% 如果需要封装好的希尔伯特谱、边际谱、包络谱、瞬时频率/幅值/相位功能函数
% （即hhtSpec.m、marginalSpec.m、envSpec.m、 InsFPA.m、kEMD.m、pEMDandFFT.m、pEMD.m和Fb_FFT.m）的无水印源码，可在文末连接获取。
% 此外如果想要得到包络谱和边际谱的计算结果数值也需要购买完整版代码哦~
% 编程不易，感谢支持~
% 紧巴巴的学生党可以联系店主，店主会给优惠~
% 完整版链接：
% https://item.taobao.com/item.htm?spm=a1z10.1-c.w4004-23536000400.16.7cd94300T8SZLk&id=642855167993

%%
function [fs,t,signal] = ImportData()       %导入待分析数据
%     load test_data.mat          %test_data.mat 导入时间和第五通道的数据，数量为10000
    ccd = pwd;      %save current path
    DataPath = 'E:\Code_master\Risk Assessment of Escalator\Data';      
    cd (DataPath)       %切换到DataFile
    load test_data.mat; %导入数据
    cd (ccd)            %返回当前地址
    
    fs = 10^4;          %采样频率
    t = t';             %采样时间
    signal = data';     %采样信号
end