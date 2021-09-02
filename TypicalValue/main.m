clear all;

%% 0.import data
ccd = pwd;      %save current path
cd ..
cd Data
load test_data.mat;
load escalator_parameters.mat
% cd ..         %return
cd (ccd)        %return

%% 1.signal in time and frequency domain
signal.escalator = escalator(1);

% signal value and parameter in time domain
signal.TD.Fs = 10^4;        % sample frequency
signal.TD.L = 8192;         % data length
signal.TD.t = t(1:signal.TD.L)';
signal.TD.data = data(1:signal.TD.L)';

% signal value and parameter in frequency domain
[signal.FD.data, signal.FD.f] = fft_ss(signal.TD.data, signal.TD.Fs); 

%% 2.typical value
[ pv_max, t_max, pv_min, t_min ] = TV_pv(signal.TD);
signal.TV.pv = [ pv_max, t_max, pv_min, t_min ];

signal.TV.kv = TV_kv(signal.TD);

signal.TV.ppv = TV_ppv(signal.TD);

basic_frequency = signal.escalator.frequence.f_motor;
signal.TV.Nbf = TV_Nbf(signal.FD, basic_frequency, 3);

signal.TV.pvifds.high = TV_pvifds(signal.FD, 'high');
signal.TV.pvifds.mid = TV_pvifds(signal.FD, 'mid');
signal.TV.pvifds.low = TV_pvifds(signal.FD, 'low');

signal.TV.tfv = TV_tfv(signal.FD);

%% 3.save data
cd ..
cd Data\
save('signal(with typical value).mat', 'signal');
% cd ..
cd (ccd)
