function [ ppv ] = TV_ppv( TD )
%   TV_ppv: get the peak-to-peak value(ppv) of typical value(TV)
%   Inputs:
%       TD: data struct of time domain(TD) signal
%   Outputs:
%       ppv: peak-to-peak value

[ pv_max, t_max, pv_min, t_min ] = TV_pv( TD )
ppv = pv_max - pv_min;

end

