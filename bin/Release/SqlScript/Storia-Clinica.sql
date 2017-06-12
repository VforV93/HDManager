select p.name,
       p.prename,
       p.id_patient,
       p.id_pat_hosp,
       p.current_location,
       v.id_visit,
       v.id_visit_hosp,
       v.cancellation as visit_cancellation,
       e.id_event,
       e.cancellation as event_cancellation,
       e.event_type,
       e.location,
       org1.name_short as LOCATION_NAME,
       e.prior_location as PRIOR_LOCATION_NAME,
       org2.name_short,
       e.event_day
  from visit             v,
       event             e,
       patient           p,
       organisation_unit org1,
       organisation_unit org2
 where p.id_patient = v.id_patient
   and v.id_visit = e.id_visit(+)
   and e.location = org1.id_unit(+)
   and e.prior_location = org2.id_unit(+)
   and p.id_patient = '<ID_PATIENT>'
 order by id_visit, event_day;