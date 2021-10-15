function [ ES_amp, ES_f ] = EnveSpec( TD_amp, Fs )
%   FreSpec: to get envelope spectrum
%
%   Input:
%       TD_amp: the amplitude of TD signal
%       Fs: sample frequency of signal
%   Output:
%       ES_amp: amplitude of envelope spectrum
%       ES_f; frequency of envelope spectrum

y = envelope(TD_amp);

L = length(y);
N = 2^nextpow2(L);

z = abs(fft(y,N)/N);

ES_f = (0:1:floor(L/2)-1);
ES_amp = z(1:floor(L/2));

end

