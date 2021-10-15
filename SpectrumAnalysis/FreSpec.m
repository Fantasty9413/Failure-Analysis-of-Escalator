function [ FD_amp, FD_f ] = FreSpec( TD_amp, Fs )
%   FreSpec: to get frequency spectrum
%
%   Input:
%       TD_amp: the amplitude of TD signal
%       Fs: sample frequency of signal
%   Output:
%       FD_amp: amplitude of single-sided TDS
%       FD_f; frequency of single-sided TDS

y = TD_amp;

L = length(y);
Y = fft(y);
P2 = abs(Y/L);      % two-sided spectrum
P1 = P2(1:L/2+1);   % single-sided spectrum
P1(2:end-1) = 2*P1(2:end-1);
FD_amp = P1;     % amplitude of TDS(frequency domain spectrum)
FD_f = Fs*(0:(L/2))/L;

end

