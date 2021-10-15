


%% 0.import data
ccd = pwd;      %save current path
cd ..\Data
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

figure(1)          % plot of time domain
plot(t,y);
title('Time domain figure');
xlabel('Time(s)');
ylabel('Amplitude of signal');
axis on, grid on;

%% 1.spectrum analysis
% frequency spectrum
[ FD_amp, FD_f ] = FreSpec( y, Fs );

Spec_y = FD_amp;
Spec_x = FD_f;

figure(2)
plot(Spec_x, Spec_y);
title('Spectrum of Frequency');
xlabel('Hz');
ylabel('Amplitude');
axis on, grid on;

% envelope spectrum
[ ES_amp, ES_f ] = EnveSpec( y, Fs );

Spec_y = ES_amp;
Spec_x = ES_f;

figure(3)
plot(Spec_x, Spec_y);
title('Spectrum of Envelope');
xlabel('Hz');
ylabel('Amplitude');
xlim([1,100]);
axis on, grid on;

% cepstrum spectrum
[ CS_amp, CS_f ] = CepsSpec( y, Fs );

Spec_y = CS_amp;
Spec_x = CS_f;

figure(3)
plot(Spec_x, Spec_y);
title('Spectrum of Cesptrum');
xlabel('Hz');
ylabel('Amplitude');
xlim([1,100]);
axis on, grid on;

% power spectrum
[ PS_amp, PS_f ] = PowSpec( y, Fs );

Spec_y = PS_amp;
Spec_x = PS_f;

figure(3)
plot(Spec_x, Spec_y);
title('Spectrum of power');
xlabel('Hz');
ylabel('Amplitude');
axis on, grid on;
