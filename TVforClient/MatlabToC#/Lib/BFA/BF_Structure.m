function [ BF ] = BF_Structure( structure_parameters )
%   BF_Structure: basic frequency of parts in structure
%   Input:
%       structure_parameters: parameters of structure with a vector in
%       order
%
%   Output:
%       BF: basic frequency
%

    temp = structure_parameters;

    v1 = temp(1);
    i = temp(2);
    n_m = temp(3);
    n_d = temp(4);
    n_s = temp(5);
    d_m = temp(6);
    d_s = temp(7);
    d_h = temp(8);
    n_md = temp(9);
    n_hd = temp(10);
    d_r1 = temp(11);
    d_r2 = temp(12);
    d_ml = temp(13);

    d_r = d_r2;

    frequence.f_motor = 1000*v1*n_d*i / (d_s*n_m*n_s);
    frequence.f_m = 1000*v1*n_d / (d_s*n_s*n_m);
    frequence.f_d = 1000*v1 / (d_s*n_s);
    frequence.f_md = 1000*v1 / (d_s*n_s);
    frequence.f_hd = 1000*v1*n_md / (d_s*n_s*n_hd);
    frequence.f_r = 1000*v1 / (pi*d_r);
    frequence.f_dp = 1000*v1*n_d*n_m / (d_s*n_s*n_m);
    frequence.f_sp = 1000*v1*n_s / (d_s*n_s);
    frequence.f_hp = 1000*v1*n_md / (d_s*n_s);
    
    BF(1) = frequence.f_motor;
    BF(2) = frequence.f_m;
    BF(3) = frequence.f_d;
    BF(4) = frequence.f_md;
    BF(5) = frequence.f_hd;
    BF(6) = frequence.f_r;
    BF(7) = frequence.f_dp;
    BF(8) = frequence.f_sp;
    BF(9) = frequence.f_hp;

end

