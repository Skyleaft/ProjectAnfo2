program URecord;
uses crt,sysutils;
const
     maks=100;
type
    TPenjualan=record
                     kd_barang:string;
                     nama:string;
                     harga:integer;
                     qty:integer;
                     subtotal:real;
    end;
var
   data:Array[1..maks] of TPenjualan;
   bd,pil:integer;


procedure kotak(kiri,kanan,atas,bawah:integer;
                latar,tulisan:integer;judul:string);
var
   i:integer;
begin
     textcolor(tulisan);textbackground(latar);
     window(kiri,atas,kanan,bawah);clrscr;window(1,1,80,25);
     gotoxy(kiri,atas);write(chr(218));
     gotoxy(kanan,atas);write(chr($BF));
     gotoxy(kiri,bawah);write(#192);
     gotoxy(kanan,bawah);write(#217);
     for i:=kiri+1 to kanan-1 do
     begin
          gotoxy(i,atas);write(#196);gotoxy(i,bawah);write(#196);
     end;
     for i:=atas+1 to bawah-1 do
     begin
          gotoxy(kiri,i);write(#179);gotoxy(kanan,i);write(#179);
     end;
     gotoxy(((kanan-kiri)-length(judul)) div 2+kiri, atas+1);write(judul);
end;

procedure pemisah(kiri,kanan,atas:integer);
var
   i:integer;
begin
     gotoxy(kiri,atas);write(#195);
     for i:=kiri+1 to kanan-1 do
     begin
          gotoxy(i,atas);write(#196);
     end;
     gotoxy(kanan,atas);write(#180);
end;  

procedure tambah;
begin
     if bd<maks then
     begin
          clrscr;
          kotak(5,40,3,17,BLUE,WHITE,'Tambah Barang');
          pemisah(5,40,5);
          bd:=bd+1;
          gotoxy(7,6);writeln('Input Penjualan ke-',bd);
          gotoxy(7,7);write('Kode Barang : ');readln(data[bd].kd_barang);
          gotoxy(7,8);write('Nama Barang : ');readln(data[bd].nama);
          gotoxy(7,9);write('Harga       : ');readln(data[bd].harga);
          gotoxy(7,10);write('Qty         : ');readln(data[bd].qty);
          data[bd].subtotal:= data[bd].harga * data[bd].qty;

     end
     else
     begin
          writeln('Data Penuh. Tekan Enter Untuk Melanjutkan');readln;
     end;
end;

procedure tampil_data;
var
   i:integer;
begin
     clrscr;
     kotak(5,80,3,20,BLUE,WHITE,'Data Penjualan');
     pemisah(5,80,5);
     //writeln('-------------------------------------------------------');
     gotoxy(6,6);writeln('Kode Barang ',#179,' Nama Barang ',#179,' Harga ',#179,' Qty ',#179,' Sub Total ',#179);
     pemisah(5,80,8);
     for i:=1 to bd do
     begin
         writeln(data[i].kd_barang:10,'  ',data[i].nama:12,'  ',data[i].harga:8,'  ',data[i].qty:3,'  ',data[i].subtotal:11:0);
     end;
     readln;
end;

procedure pengurutan_subtotal();
var
   i,j:integer;
   temp:TPenjualan;
begin
   for i:=1 to bd-1 do
   begin
        for j:=bd downto i+1 do
        begin
             if data[j].subtotal < data[j-1].subtotal then
             begin
                  temp:=data[j];
                  data[j]:=data[j-1];
                  data[j-1]:=temp;
             end;
        end;
   end;
   writeln('Pengurutan Selesai. Tekan Enter untuk melanjutkan');readln;
end;

procedure pengurutan_qty();
var
   i,j:integer;
   temp:TPenjualan;
begin
   for i:=1 to bd-1 do
   begin
        for j:=bd downto i+1 do
        begin
             if data[j].qty < data[j-1].qty then
             begin
                  temp:=data[j];
                  data[j]:=data[j-1];
                  data[j-1]:=temp;
             end;
        end;
   end;
   writeln('Pengurutan Selesai. Tekan Enter untuk melanjutkan');readln;
end;

procedure pengurutan_harga();
var
   i,j:integer;
   temp:TPenjualan;
begin
   for i:=1 to bd-1 do
   begin
        for j:=bd downto i+1 do
        begin
             if data[j].harga < data[j-1].harga then
             begin
                  temp:=data[j];
                  data[j]:=data[j-1];
                  data[j-1]:=temp;
             end;
        end;
   end;
   writeln('Pengurutan Selesai. Tekan Enter untuk melanjutkan');readln;
end;

procedure pengurutan_nama();
var
   i,j:integer;
   temp:TPenjualan;
begin
   for i:=1 to bd-1 do
   begin
        for j:=bd downto i+1 do
        begin
             if data[j].nama < data[j-1].nama then
             begin
                  temp:=data[j];
                  data[j]:=data[j-1];
                  data[j-1]:=temp;
             end;
        end;
   end;
   writeln('Pengurutan Selesai. Tekan Enter untuk melanjutkan');readln;
end;

procedure pengurutan_kode();
var
   i,j:integer;
   temp:TPenjualan;
begin
   for i:=1 to bd-1 do
   begin
        for j:=bd downto i+1 do
        begin
             if data[j].kd_barang < data[j-1].kd_barang then
             begin
                  temp:=data[j];
                  data[j]:=data[j-1];
                  data[j-1]:=temp;
             end;
        end;
   end;
   writeln('Pengurutan Selesai. Tekan Enter untuk melanjutkan');readln;
end;

procedure pengurutan_kode_SS();
var
   i,j,imaks:integer;
   temp:TPenjualan;
begin
   for i:=bd to 2 do
   begin
        imaks:=1;
        for j:=2 to i do
        begin
             if data[j].kd_barang > data[imaks].kd_barang then
                imaks:=j;
        end;
        if imaks<>i then
             begin
                  temp:=data[j];
                  data[j]:=data[imaks];
                  data[imaks]:=temp;
             end;
   end;
   writeln('Pengurutan Selesai. Tekan Enter untuk melanjutkan');readln;
end;

procedure pengurutan;
var
   pil:integer;
begin
     clrscr;
     writeln('1. Pengurutan Berdasarkan Kode');
     writeln('2. Pengurutan Berdasarkan Nama Barang');
     writeln('3. Pengurutan Berdasarkan Harga Barang');
     writeln('4. Pengurutan Berdasarkan Qty');
     writeln('5. Pengurutan Berdasarkan Subtotal');
     writeln('----------------------------------------');
     write('Pilihan : ');readln(pil);
     case pil of
          1: pengurutan_kode_SS;
          2: pengurutan_nama;
          3: pengurutan_harga;
          4: pengurutan_qty;
          5: pengurutan_subtotal;
     end;
end;


procedure simpan;
var
   i:integer;
   f:file of TPenjualan;
begin
     assign(f,'penjualanif5.dat');
     rewrite(f);
     for i:=1 to bd do
         write(f,data[i]);
     writeln(bd,' Data Telah Disimpan Ke File');
     readln;
end;

procedure bacadarifile;
var
   f:file of TPenjualan;
begin
     if fileexists('penjualanif5.dat') then
     begin
          assign(f,'penjualanif5.dat');
          reset(f);
          while not eof(f) do
          begin
               bd:=bd+1;
               read(f,data[bd]);
          end;
          close(f);
          writeln('Baca data selesai. Terbaca ',bd,' record');
          readln;
     end
     else
     begin
          writeln('File Belum Ada. Tidak ada data yang terbaca');readln;
     end;


end;

procedure hapus_nama;
var
   cari:string;
   i,x:integer;
   temp:TPenjualan;
begin
     writeln('Masukan Kode Produk');readln(cari);
     i:=1;
     //cari dulu data yang mau dihapus
     while (data[i].kd_barang <> cari) and (i<bd) do
           i:=i+1;
     if data[i].kd_barang = cari then
     begin
        writeln(cari,' ditemukan di indeks ke-',i);
        writeln('nama barang : ',data[i].nama);
        data[i]:=data[bd+1];
        temp:=data[i+1];
        for x:=i to bd do
        begin
           data[x]:=temp;
           temp:=data[i+x+1];
        end;
        bd:=bd-1;
        //note bd=banyakdata
     end
     else
        writeln(cari,' tidak ditemukan');
     readln;
end;

procedure pencarian_kode;
var
   cari:string;
   i,x:integer;
   temp:TPenjualan;
begin
     writeln('Masukan Kode Produk');readln(cari);
     i:=1;
     while (data[i].kd_barang <> cari) and (i<bd) do
           i:=i+1;
     if data[i].kd_barang = cari then
     begin
        writeln(cari,' ditemukan di indeks ke-',i);
        writeln('nama barang : ',data[i].nama);

     end
     else
        writeln(cari,' tidak ditemukan');
     readln;
end;



procedure pencarian_nama;
var
   cari:string;
   L,R,T:integer;
   ketemu:boolean;
begin
     pengurutan_nama;
     writeln('Masukan Nama Produk');readln(cari);
     L:=1;
     R:=bd;
     T:=(L+R) div 2;
     while (upcase(data[T].nama) <> upcase(cari)) and (L<=R) do
     begin
          if upcase(data[T].nama)>upcase(cari) then
             R:=T-1
          else
              L:=T+1;
          T:=(L+R) div 2;
     end;
     if upcase(data[T].nama) = upcase(cari) then
     begin
        writeln(cari,' ditemukan di indeks ke-',T);
        writeln('nama barang : ',data[T].nama);
     end
     else
        writeln(cari,' tidak ditemukan');
     readln;
end;


procedure pencarian;
var
   pil:integer;
begin
     clrscr;
     writeln('1. pencarian Berdasarkan Kode');
     writeln('2. pencarian Berdasarkan Nama Barang');
     writeln('3. pencarian Berdasarkan Harga Barang');
     writeln('----------------------------------------');
     write('Pilihan : ');readln(pil);
     case pil of
     1: pencarian_kode;
     2:pencarian_nama;
     3:;
     end;
end;




begin
     bd:=0;
     bacadarifile;
     repeat
           clrscr;
           kotak(5,60,3,17,BLUE,WHITE,'Penjualan Barang');
           pemisah(5,60,5);
           gotoxy(7,6);writeln('1. Tambah Data');
           gotoxy(7,7);writeln('2. Tampilkan Data');
           gotoxy(7,8);writeln('3. Pengurutan');
           gotoxy(7,9);writeln('4. Pencarian');
           gotoxy(7,10);writeln('5. Simpan');
           gotoxy(7,11);writeln('0. Keluar');
           writeln;
           gotoxy(7,13);write('Pilih(1-5) : ');readln(pil);

           if pil = 1 then
              tambah
           else if pil = 2 then
                tampil_data
           else if pil = 3 then
                pengurutan
           else if pil = 4 then
                pencarian
           else if pil = 5 then
                simpan;
     until pil=0;
     readln;
     simpan;

end.
