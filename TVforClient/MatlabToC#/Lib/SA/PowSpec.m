function [ PS_amp, PS_f ] = PowSpec( TD_amp, Fs )
%   FreSpec: to get frequency spectrum
%
%   Input:
%       TD_amp: the amplitude of TD signal
%       Fs: sample frequency of signal
%   Output:
%       PS_amp: amplitude of power spectrum
%       PS_f; frequency of power spectrum

y = TD_amp;

window = 500;       % the length of each section
noverlap = 300;     % the num of elements overlapped
nfft = 500;         % the points for fft
[pxx,f] = pwelch(y,window,noverlap,nfft,Fs);     % welch method 

PS_f = f;
PS_amp = 10*log10(pxx);

end

