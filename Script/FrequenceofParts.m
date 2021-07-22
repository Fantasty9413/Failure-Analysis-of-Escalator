%*************************************************************************%
% 处理扶梯参数，计算各零部件的运动频率，并存储至escalator_parametersmat文件数据
% 中，以便后续分析。
% 参考论文：《频率分析在扶梯调试中的应用》
%*************************************************************************%
clear all;
close all;

%% 0.导入数据
cd ..
cd Data\
load('escalator_parameters.mat');
cd ..
cd Script\

%% 1.计算零部件运动频率
for j = 1:length(escalator)
    temp = escalator(j);

    v1 = temp.ladder(1);
    i = temp.ladder(2);
    n_m = temp.ladder(3);
    n_d = temp.ladder(4);
    n_s = temp.ladder(5);
    d_m = temp.ladder(6);
    d_s = temp.ladder(7);
    d_h = temp.ladder(8);
    n_md = temp.ladder(9);
    n_hd = temp.ladder(10);
    d_r1 = temp.ladder(11);
    d_r2 = temp.ladder(12);
    d_ml = temp.ladder(13);
    P = temp.motor(1);
    U = temp.motor(2);
    I = temp.motor(3);
    n_motor = temp.motor(4);

    d_r = d_r2;

    frequence.f_motor = 1000*v1*n_d*i / (d_s*n_m*n_s);
    frequence.f_m = 1000*v1*n_d / (d_s*n_s*n_m);
    frequence.f_d = 1000*v1 / (d_s*n_s);
    frequence.f_md = 1000*v1 / (d_s*n_s);
    frequence.f_hd = 1000*v1*n_md / (d_s*n_s*n_hd);
    frequence.f_r = 1000*v1 / (pi*d_r);
    frequence.f_dp = 1000*v1*n_d*n_m / (d_s*n_s*n_m);
    frequence.f_sp = 1000*v1*n_s / (d_s*n_s);
    frequence.f_hp = 1000*v1*n_md / (d_s*n_s);

    escalator(j).frequence = frequence;
end

%% 2.保存数据
cd ..
cd Data\
save('escalator_parameters.mat', 'escalator');
cd ..
cd Script\
