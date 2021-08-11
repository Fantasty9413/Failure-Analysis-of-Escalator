function [ amplitude, f ] = fft_ss( y, Fs )
%
%   fft_ss: fft for single-sided spectrum
%
%   Input:
%       y: the amplitude of TD signal
%       t: the time of TD signal
%   Output:
%       amplitude: amplitude of single-sided TDS
%       f; frequency of single-sided TDS
%

L = length(y);
Y = fft(y);
P2 = abs(Y/L);      % two-sided spectrum
P1 = P2(1:L/2+1);   % single-sided spectrum
P1(2:end-1) = 2*P1(2:end-1);
amplitude = P1;     % amplitude of TDS(frequency domain spectrum)
f = Fs*(0:(L/2))/L;

end

