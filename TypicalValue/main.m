clear all;

%% 0.import data
ccd = pwd;      %save current path
cd ..
cd Data
load test_data.mat;
load escalator_parameters.mat
% cd ..         %return
cd (ccd)        %return

%% 1.
signal.escalator = escalator(1);

% signal value and parameter in time domain
signal.TD.Fs = 10^4;        % sample frequency
signal.TD.L = 8000;         % data length
signal.TD.t = t(1:signal.TD.L)';
signal.TD.data = data(1:signal.TD.L)';

% signal value and parameter in frequency domain
[signal.FD.data, signal.FD.f] = fft_ss(signal.TD.data, signal.TD.Fs); 

