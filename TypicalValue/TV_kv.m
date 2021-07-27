function [ kv ] = TV_kv( TD )
%   TV_kv: get kurtosis value(kv) of typical value(TV)
%   Inputs:
%       TD: data struct of time domain(TD) signal
%   Outputs:
%       kv: kurtosis value

avg = mean(TD.data(:));
var = mean((TD.data-avg).^2);
kv = avg^4 / var^2;

end

