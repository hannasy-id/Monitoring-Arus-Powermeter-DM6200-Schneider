# Monitoring-Arus-Powermeter-DM6200-Schneider
Program untuk memonitoring arus motor 3 phase mixer beton menggunakan powermeter DM6200 Schneider,
Komunikasi menggunakan RS485 dengan protokol Modbus RTU,
Dibuat menggunakan Visual Basic 2010.

Register yang dipakai adalah register A1 dan A3:
- arus 1 => A1, dengan alamat = 43924 - 43925
- arus 3 => A3, dengan alamat = 43957 - 43958

Referensi:
- https://download.schneider-electric.com/files?p_enDocType=Catalog&p_File_Name=DM6000_UserManaul.pdf&p_Doc_Ref=PLSED309041EN_kr
- http://pusdiklatmigas.esdm.go.id/file/t5-_Medbus_Protokol_---_Nurpadmi.pdf
- https://www.youtube.com/watch?v=DRdkO6NYQpY&list=PLRCEJ0bGSS1aWsVlwpEthiufoux0eGxx9&index=4
- https://www.youtube.com/watch?v=8afbTaA-gOQ
- http://howtostartprogramming.com/vb-net/vb-net-tutorial-53-multithreading/
