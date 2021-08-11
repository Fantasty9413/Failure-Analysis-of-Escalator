function [ Nbf_amp ] = TV_Nbf( FD_amp, FD_f, bf)
%   TV_ppv: get the Nbf value of typical value(TV)
%   Inputs:
%       FD_amp: amplitude of FD data
%       FD_f:   frequency of FD data
%       bf: basic frequency of escalator part
%       N;  one to N times bf. delete, now equal to 3.
%   Outputs:
%       Nbf_amp: only amplitude value of Nbf
%       Nbf: amplitude in one to N times basic frequency
%            a matrix of 2*N, line 1 includes frequency, line 2 includes
%            amplitude in the corresponding frequency(round to integer 
%            times scale of TD.f.)

N = 3;

Nbf = zeros(2, N);
Nbf(1,:) = bf * (1:1:N);
FD_bf = FD_f(2);        % scale of basic frequency of FD
for i = 1:N
    temp = round(Nbf(1,i)/FD_bf) * FD_bf;
    Nbf(2,i) = FD_amp(find(FD_f == temp));    
end

Nbf_amp = Nbf(2,:);

end

