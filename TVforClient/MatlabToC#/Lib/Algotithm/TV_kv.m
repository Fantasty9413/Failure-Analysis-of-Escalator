function [ kv ] = TV_kv( TD_amp )
%   TV_kv: get kurtosis value(kv) of typical value(TV)
%   Inputs:
%       TD_amp: amplitude of data struct of time domain(TD) signal
%   Outputs:
%       kv: kurtosis value

avg = mean(TD_amp(:));
var = mean((TD_amp-avg).^2);
kv = avg^4 / var^2;

end

