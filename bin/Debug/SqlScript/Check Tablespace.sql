select d.TABLESPACE_NAME TABLESPACE,
       trunc(d.maxbytes / 1024 / 1024) MB_TOTALI,
       trunc(decode(d.maxbytes,
                    0,
                    f.bytes / 1024 / 1024,
                    decode(nvl(f.bytes / 1024 / 1024, 0),
                           0,
                           (d.maxbytes / 1024 / 1024 - d.bytes / 1024 / 1024),
                           (d.maxbytes / 1024 / 1024 - d.bytes / 1024 / 1024 +
                           f.bytes / 1024 / 1024)))) MB_LIBERI
  from (select tablespace_name,
               sum(BYTES) as bytes,
               sum(BLOCKS) as blocks,
               sum(decode(maxbytes, 0, bytes, maxbytes)) as maxbytes, --byte totali SEMPRE
               sum(decode(maxblocks, 0, BLOCKS, maxblocks)) as maxblocks --blocks totali SEMPRE
          from dba_data_files
         group by TABLESPACE_NAME) d,
       (select tablespace_name, sum(BYTES) as bytes, sum(BLOCKS) as blocks
          from dba_free_space
         group by TABLESPACE_NAME) f
 where d.TABLESPACE_NAME = f.TABLESPACE_NAME(+);

--Spazio libero su Rac:
--select name,usable_file_mb from gv$asm_diskgroup 
--Per vedere poi dove è collocato fisicamente il datafile e la dimensione in MB:
--select  t.USER_BYTES/1024/1024 AS MB, t.*  from dba_data_files t where tablespace_name = '<_____>';
--Ridimensionare datafile:
--ALTER DATABASE DATAFILE '<path del datafile>/<nome>.dbf' RESIZE <dimensione>M;
--Aggiungere datafile:
--aALTER TABLESPACE <nome tablespace> ADD DATAFILE '<path del datafile>/<nome>.dbf' size <dimensione>M;

------------------
--AUTOEXTEND:--
------------------
--Per vedere dove è collocato fisicamente il datafile e la dimensione in MB:
-- select  t.BYTES/1024/1024 AS MB, file_name, maxbytes/1024/1024 from dba_data_files t where tablespace_name = '<_____>';
--Per allargare il datafile:
-- ALTER DATABASE DATAFILE '<path del datafile>/<nome>.dbf' autoextend on maxsize <dimensione>M;
--Per aggiungere il datafile:
-- ALTER TABLESPACE <nome tablespace> ADD DATAFILE  '<path del datafile>/<nome>.dbf'  SIZE <dimensione> AUTOEXTEND ON NEXT 10M  MAXSIZE <dimensione>M

/*
Maxbytes rappresenta la massima dimensione per cui il datafile si allarghera’ da solo, per cui non bisogna esagerare dando dei valori troppo alti, i valori devono essere comunque sensati.
Se trovate maxbytes minore di bytes disabilitate l’autoextend con questo comando:
alter database datafile ‘<file_name.dbf>’ autoextend off’;
L’autoextend e’ un attributo del DATAFILE e non della TABLESPACE, quindi una tablespace puo’ tranquillamente avere un datafile in autoextend ed uno no
 
Il monitor (sia skynet che dnmonitor) tiene in considerazione l’autoextend
*/
