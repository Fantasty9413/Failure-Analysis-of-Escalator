clear all;
close all;

%% 0.import data
ccd = pwd;      %save current path
cd Data
load test_data.mat;
% cd ..         %return
cd (ccd)        %return

% load test_data.mat;
start_point = 1;
end_point = 8000;
L = end_point - start_point + 1;       % length of signal
t = t(start_point:end_point)';
y = data(start_point:end_point)';      % value of signal
Fs = 10^4;      % frequence of sample

figure          % plot of time domain
plot(t,y);
title('Time domain figure');
xlabel('Time(s)');
ylabel('Amplitude of signal');
axis on, grid on;

%% 1.analysis of amplitude and frequency
Y = fft(y);
% f = (0:L-1)*Fs/L;
% figure 
% plot(f,y)

P2 = abs(Y/L);      % two-sided spectrum
P1 = P2(1:L/2+1);   % single-sided spectrum
P1(2:end-1) = 2*P1(2:end-1);
f = Fs*(0:(L/2))/L;

figure              % amplitude-frequency figure
plot(f,P1)  
title('The amplitude-frequency figure');
xlabel('Frequency(Hz)');
ylabel('Amplitude');
axis on, grid on;

%% 2.analysis of power spectrum
window = 500;       % the length of each section
noverlap = 300;     % the num of elements overlapped
nfft = 500;         % the points for fft
[pxx,f] = pwelch(y,window,noverlap,nfft,Fs);     % welch method 

figure              % figure of power spectrum
plot(f,10*log10(pxx));
title('Power Spectrum');
xlabel('Frequency(Hz)')
ylabel('Magnitude(dB)')
axis on, grid on;

%% 3.analysis of cepstrum
y_rceps = rceps(y);     % real cepstrum
y_cceps = cceps(y);     % complex cepstrum

figure
plot(t,y_rceps)
title('Real Cepstrum')
xlabel('Time(s)')
axis on,grid on;