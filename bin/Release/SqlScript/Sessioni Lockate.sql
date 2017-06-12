select  *  from dba_waiters
where holding_session not in (select waiting_session from dba_waiters);

--OPPURE

select * from v$session where sid in (select holding_session from dba_waiters
where holding_session not in (select waiting_session from dba_waiters));

--OPPURE

select l1.sid, ' IS BLOCKING ', l2.sid
  from v$lock l1, v$lock l2
  where l1.block =1 and l2.request > 0
  and l1.id1=l2.id1
  and l1.id2=l2.id2
  and l2.ctime > 120;