function [ Nbf ] = TV_Nbf( FD, bf, N )
%   TV_ppv: get the Nbf value of typical value(TV)
%   Inputs:
%       FD: data struct of frequency domain(FD) signal
%       bf: basic frequency of escalator part
%       N;  one to N times bf
%   Outputs:
%       Nbf: amplitude in one to N times basic frequency
%            a matrix of 2*N, line 1 includes frequency, line 2 includes
%            amplitude in the corresponding frequency(round to integer 
%            times scale of TD.f.)

Nbf = zeros(2, N);
Nbf(1,:) = bf * (1:1:N);
FD_bf = FD.f(2);        % scale of basic frequency of FD
for i = 1:N
    temp = round(Nbf(1,i)/FD_bf) * FD_bf;
    Nbf(2,i) = FD.data(find(FD.f == temp));    
end

end

