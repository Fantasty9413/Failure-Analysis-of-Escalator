function [ ppv ] = TV_ppv( TD_amp, TD_t )
%   TV_ppv: get the peak-to-peak value(ppv) of typical value(TV)
%   Inputs:
%       TD_amp: amplitude of data struct of time domain(TD) signal
%       TD_t:   time of data struct of time domain(TD) signal
%   Outputs:
%       ppv: peak-to-peak value

[ pv_max, t_max, pv_min, t_min ] = TV_pv( TD_amp, TD_t );
ppv = pv_max - pv_min;

end

