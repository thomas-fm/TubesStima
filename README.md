# NasiPadang
Tugas Besar II IF2211 Strategi Algoritma
(Pengaplikasian Algoritma BFS dan DFS dalam Fitur People You May Know
Jejaring Sosial Facebook)

## Penjelasan Singkat Algoritma BFS dan DFS
* Breadth First Search (BFS)
BFS adalah algoritma yang digunakan untuk menelusuri graph. Pada dasarnya, BFS merupakan algoritma yang mencari tetangga dari suatu node, kemudian menandai bahwa node tetangga tersebut sudah dikunjungi dan menyimpannya ke dalam suatu Queue. Lalu akan diambil kembali node dari Queue dan diulanglah proses tersebut sampai seluruh node pada graph selesai dikunjungi (Queue kosong).

* Depth First Search (DFS)
DFS juga merupakan algoritma untuk menelusuri graph. Hal yang membedakan dengan BFS adalah bagaimana cara algoritma ini menelusuri suatu graph. DFS menelusuri setiap simpul tetangga, dan ketika ditemukan simpul tetangga yang belum pernah dikunjungi sebelumnya, DFS akan langsung memasukan simpul tetangg tersebut ke dalam Stack, kemudian menelusuri kembali tetangga dari simpul tetangga yang sebelumnya. Hal ini diulangi sampai seluruh node pada graph selesai dikunjungi.

## Requirements
* Operating System: Windows (tested on Windows 10)
* Visual Studio 2019

## Cara menggunakan program
1. Clone repository ini
2. Jalankan file NasiPadang.exe yang ada pada folder bin
3. pilih file yang ingin dijadikan sebagai testcase yang ada di folder bin
4. Pilih algoritma yang diinginkan (BFS/DFS)
5. Pilih akun _utama_ dan akun _target_ apabila ingin menjalankan Explore Friends
6. Anda juga dapat menjalankan fitur Friend Reccomendation untuk mendapatkan rekomendasi teman akun _utama_


## Authors
* Thomas Ferdinand Martin - 13519099
* Muhammad Akram Al Bari - 13519142
* Azmi Muhammad Syazwana - 13519151
