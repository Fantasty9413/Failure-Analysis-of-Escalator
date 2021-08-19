clear all;

%% 0.import data
ccd = pwd;      %save current path
datapath = 'E:\Code_master\Risk Assessment of Escalator\Data';
cd (datapath);

load test_data.mat;
load escalator_parameters.mat

cd (ccd)        %return

%% 1.signal in time and frequency domain
signal.escalator = escalator(1);

% signal value and parameter in time domain
signal.TD.Fs = 10^4;        % sample frequency
signal.TD.L = 8092;         % data length
signal.TD.t = t(1:signal.TD.L)';
signal.TD.data = data(1:signal.TD.L)';

% signal value and parameter in frequency domain
[signal.FD.data, signal.FD.f] = fft_ss(signal.TD.data, signal.TD.Fs); 

%% 2. test for bug fix
basic_frequency = signal.escalator.frequence.f_motor;
[ Nbf_amp ] = TV_Nbf( signal.FD.data, signal.FD.f, basic_frequency);