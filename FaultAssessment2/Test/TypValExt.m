function [ tv ] = TypValExt( time, amp )
%
%       TypValExt: typical value extraction
%
%

        % add file path
        lib_TV = '..\..\TypicalValue\';
        cdc = pwd;

        % 1.signal in time and frequency domain
        % signal.escalator = escalator(1);

        % signal value and parameter in time domain
        signal.TD.Fs = 10^4;        % sample frequency
        signal.TD.L = 8192;         % data length
        signal.TD.t = time;
        signal.TD.data = amp;

        % signal value and parameter in frequency domain
        cd (lib_TV);                % turn to lib
        [signal.FD.data, signal.FD.f] = fft_ss(signal.TD.data, signal.TD.Fs); 
        cd (cdc);                   % turn back

        % 2.typical value
        TD_amp = signal.TD.data ;
        TD_t = signal.TD.t;
        FD_amp = signal.FD.data;
        FD_f = signal.FD.f;

        cd (lib_TV);                % turn to lib
        
        [ pv_max, t_max, pv_min, t_min ] = TV_pv(TD_amp, TD_t);
        signal.TV.pv = [ pv_max, t_max, pv_min, t_min ];

        signal.TV.kv = TV_kv(TD_amp);

        signal.TV.ppv = TV_ppv(TD_amp, TD_t);

        basic_frequency = 20;
        bf = basic_frequency;
        signal.TV.Nbf = TV_Nbf(FD_amp, FD_f, bf);

        [signal.TV.pvifds.amp, signal.TV.pvifds.f] = TV_pvifds(FD_amp, FD_f);

        signal.TV.tfv = TV_tfv(FD_amp);

        cd (cdc)                    % turn back
        
        tv = [signal.TV.pv, signal.TV.kv, signal.TV.ppv, signal.TV.tfv, ...
              signal.TV.Nbf, signal.TV.pvifds.amp, signal.TV.pvifds.f];

end

