%% ϣ�������ס��߼��ס������ס�˲ʱƵ��/��ֵ/��λ�ȼ��㻭ͼ����(������)
% ���иô���ǰ������ذ�װʱƵ�����������TFA_Toolbox����ȡ��ַ��http://www.khscience.cn/docs/index.php/2020/04/09/1/
% ������2020.08���ͣ��������¶��������������
%  ֪��ԭ�����ӣ�https://zhuanlan.zhihu.com/p/124257081
%               https://zhuanlan.zhihu.com/p/136447202
%  �������ַ��http://www.khscience.cn/docs/index.php/2020/08/08/hilbert/
%% 
clear all
close all
%% 1.��������ź�
[fs,t,signal] = ImportData();
figure;plot(t,signal);title('ԭʼ����')    %����ԭʼ�ź�ͼ��
%% 2.����ϣ��������
imf = kEMD(signal);  
data = imf(1,:);
hhtSpec(data',fs)
% �����ź�ϣ��������
% ���룺
% data�� �������ź�
% fs��   ����Ƶ�ʣ���fsδ֪ʱ�����Խ�fs����Ϊ�գ�[]��������type����Ϊ2����ʱhhtͼ���Ὣ������׼��
% type�� ��type��ֵΪ1ʱ�������Ȳ���MATLAB�Դ����е�hht�������л�ͼ�����õĻ�ͼ���Ҳ���Դ�hht��������һ�£���ʱhht�������÷���MATLAB�Դ�����һ�£�
%        ��type��ֵΪ2ʱ����ǿ�Ʋ��õ��������ļ��е�ϣ�������׺������л�ͼ���������ܻ�����ڴ治���޷�˳����ͼ�Ŀ���
%        ���typeû�д����������ú����ڽ�type����Ϊ1

% ����ϣ�����ر任��ϣ�������׸��������뿴���
% https://zhuanlan.zhihu.com/p/136447202
% https://zhuanlan.zhihu.com/p/124257081
%% 3.���Ʊ߼���
marginalSpec(imf,fs);
% ���Ʊ߼���
% [mgS,f] = marginalSpec(imf,fs,type)
% ���룺
% imf��  imf������ע�ⷽ��imf��ÿ��һ���źŷ����ľ���
% fs��   ����Ƶ��
% type�� ��type��ֵΪ1ʱ�������Ȳ���MATLAB�Դ����еĺ����������㣻
%        ��type��ֵΪ2ʱ����ǿ�Ʋ��õ��������ļ��еĺ����������㣬�������ܻ�����ڴ治���޷�˳����ͼ�Ŀ��ܣ����鳬��Ԥ�����������С��
%        ���typeû�д����������ú����ڽ�type����Ϊ1
%        �����������㷨��ͬ��ʹ�����ֿ⺯�������ı߼��״��ڷ�ֵ����
% �����
% mgS��  �߼��׷�ֵ���������ñ�����Ҫʹ����������룩
% f��    �߼��׵�Ƶ���ᡣ�������ñ�����Ҫʹ����������룩
%% 4.���ư�����
clear %��չ�����
% load dataEnv.mat  %������й����źţ��⻷����Ƶ��Ϊ83.33Hz
[fs,t,signal] = ImportData();
envSpec(signal,fs);  %���ư�����
% ���źŰ�����
% function [envS,f,xEnv] = envSpec(data,fs)
% ���룺
% data�� �������ź�
% fs��   ����Ƶ��
% �����
% envS�� ��������ֵ�������ñ�����Ҫʹ����������룩
% f��    ������Ƶ���ᣨ�����ñ�����Ҫʹ����������룩
% xEnv�� �����ߣ������ñ�����Ҫʹ����������룩
%% 5.˲ʱƵ��/��ֵ/��λ
imf = kEMD(signal);
[insF,insP,insA] = InsFPA(imf(1,:),fs);
% ���źŵ�˲ʱƵ�ʡ�˲ʱ��λ��˲ʱ��ֵ
% ���룺
% data�� Ŀ���ź�
% fs��   Ŀ���źŵĲ���Ƶ��
% �����
% insF�� ˲ʱƵ��
% insP�� ˲ʱ��λ
% insA�� ˲ʱ��ֵ

% ��ͼ��ע��InsFPA�������ǲ�������ͼ����ģ����軭ͼ���Բο�����д����
figure('Color','white');plot(t,insF,'k');title('˲ʱƵ��');
figure('Color','white');plot(t,insP,'k');title('˲ʱ��λ');
figure('Color','white');plot(t,insA,'k');title('˲ʱ��ֵ');

%% ������������룺
% ���������ĺ����ļ�Ϊp�ļ������Ա����ã����޷��鿴���롣
% �����������ȫ��Ϊm�ļ���m�ļ����Բ鿴Դ�벢�����޸ġ�
% �����Ҫ��װ�õ�ϣ�������ס��߼��ס������ס�˲ʱƵ��/��ֵ/��λ���ܺ���
% ����hhtSpec.m��marginalSpec.m��envSpec.m�� InsFPA.m��kEMD.m��pEMDandFFT.m��pEMD.m��Fb_FFT.m������ˮӡԴ�룬������ĩ���ӻ�ȡ��
% ���������Ҫ�õ������׺ͱ߼��׵ļ�������ֵҲ��Ҫ�������������Ŷ~
% ��̲��ף���л֧��~
% ���Ͱ͵�ѧ����������ϵ��������������Ż�~
% ���������ӣ�
% https://item.taobao.com/item.htm?spm=a1z10.1-c.w4004-23536000400.16.7cd94300T8SZLk&id=642855167993

%%
function [fs,t,signal] = ImportData()       %�������������
%     load test_data.mat          %test_data.mat ����ʱ��͵���ͨ�������ݣ�����Ϊ10000
    ccd = pwd;      %save current path
    DataPath = 'E:\Code_master\Risk Assessment of Escalator\Data';      
    cd (DataPath)       %�л���DataFile
    load test_data.mat; %��������
    cd (ccd)            %���ص�ǰ��ַ
    
    fs = 10^4;          %����Ƶ��
    t = t';             %����ʱ��
    signal = data';     %�����ź�
end