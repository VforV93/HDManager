select * from prz_out_patient where
paz_surn = 'ROMAGNOLI' and paz_name = 'GIORGIA';


select * from prz_out_event e where 
e.id_event in (
select id_event from prz_out_patient where
paz_surn = 'ROMAGNOLI' and paz_name = 'GIORGIA');


select e. * from prz_out_patient p, prz_out_event e where 
p.id_event = e.id_event and
paz_surn = 'ROMAGNOLI' and paz_name = 'GIORGIA';

