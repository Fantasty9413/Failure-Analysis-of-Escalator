function [ tfv ] = TV_tfv( FD_amp )
%   TV_ppv: get through-frequency value(tfv) of typical value(TV)
%   Inputs:
%       FD_amp: amplitude of frequency domain(FD) signal
%   Outputs:
%       tfv: through-frequency value

[pv_max, positon_max] = max(FD_amp);
% f_max = FD.t(positon_max);
[pv_min, positon_min] = min(FD_amp);
% f_min = FD.t(positon_min);

tfv = pv_max - pv_min;

end

