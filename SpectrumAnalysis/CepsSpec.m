function [ CS_amp, CS_f ] = CepsSpec( TD_amp, Fs )
%   FreSpec: to get ceps spectrum
%
%   Input:
%       TD_amp: the amplitude of TD signal
%       Fs: sample frequency of signal
%   Output:
%       CS_amp: amplitude of ceps spectrum
%       CS_f; pseudo frequency of ceps spectrum

% [ FD_amp, FD_f ] = FreSpec( TD_amp, Fs );
% CS_amp = real(ifft(log(abs(FD_amp))));
% CS_f = 1:1:length(CS_amp);

CS_amp = rceps(TD_amp);
CS_f = 0:1:length(CS_amp)-1;

end

