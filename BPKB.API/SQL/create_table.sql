begin tran
print 'begin tran' ;
begin try

print 'create table ms_storage_location' ;
create table ms_storage_location
(
location_id varchar(10)
, location_name varchar(100)
, primary key (location_id asc)
);
print 'created' ;

print 'create table ms_user' ;
create table ms_user
(
user_id bigint
, user_name varchar(20)
, password varchar(50)
, is_active bit
primary key (user_id asc)
);
print 'created' ;

print 'create table tr_bpkb' ;
create table tr_bpkb
(
agreement_number varchar(100)
, bpkb_no varchar(100)
, branch_id varchar(10)
, bpkb_date datetime
, faktur_no varchar(100)
, faktur_date datetime
, location_id varchar(10)
, police_no varchar(20)
, bpkb_date_in datetime
, created_by varchar(20)
, created_on datetime
, last_updated_by varchar(20)
, last_updated_on datetime
, primary key (agreement_number asc)
, foreign key (location_id) references ms_storage_location (location_id)
);
print 'created' ;

commit tran ;
print 'commit tran' ;
end try
begin catch
rollback tran ;
print 'rollback tran' ;
end catch
