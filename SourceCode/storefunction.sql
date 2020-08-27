-- lê văn quang

use spaCCH
go

alter table Appointment
add [DateToOutlet] date


-----------------------------------------------------------------------------------------------------------
--hàm tìm thời gian bonus tùy theo từng loại khách hàng
--input: ID customer, ID service
--output:số lượng tiemslot được bonus

DROP Function GetBuffTime
go
CREATE FUNCTION GetBuffTime (@customer int,@service int)
RETURNS int
AS BEGIN
	declare @a int
    select @a= max(ISNULL(BufferTime.bufferTime,0)) from BufferTime, Customer
	where BufferTime.Service=@service and  BufferTime.customerType=Customer.CustomerType and Customer.ID=@customer
	and bufferTime.Deleted=0 and Customer.Deleted=0
	return @a 
END
--test hàm
select [dbo].[GetBuffTime](3,2)



--Hàm tính số lượng timeslot của 1 dịch vụ
--input :ID Service
--output: số lượng tiemslot 
-------------------------------------------------------------------------------------------------
drop Function GetNumTimeslot
go
create Function GetNumTimeslot(@service int)
returns int
as
begin
	return (select ISNULL(Service.numOfTimeSlot ,0)
	from Service 
	where Service.Deleted=0 and Service.ID=@service)
end
--test hàn
select [dbo].[GetNumTimeslot](2)

--Hàm kiểm tra khoảng thời gian hợp lệ giữa 2 khoảng thời gian nhập vào
--input : mốc thời gian 1, kéo dài của 1, mốc thời gian 2, kéo dài của 2
--output: 1 (nếu 2 khoảng này không giao nhau, 0 (nếu giao nhau)
--------------------------------------------------------------------------------------------------------

create FUNCTION CheckTimeBusy(@slotbegin int, @long int, @slotbegin1 int, @long1 int)
Returns int
AS BEGIN
	if @slotbegin between @slotbegin1 and( @slotbegin1+@long1) or (@slotbegin+@long) between @slotbegin1 and( @slotbegin1+@long1)
		return 0;
	return 1;
END   

--test hàm
select [dbo].[CheckTimeBusy](7,4,2,5)

--Hàm kiểm tra có còn giường nào trống với thời gian mà khách hàng chọn ứng với 1 dịch vụ của outlet hay không
--input : mốc thời gian bắt đầu ,số lượng tiemslot,ID service
--output: ID của 1 Bed hợp lệ nếu còn , hoặc NULL
------------------------------------------------------------------------------------ 
drop Function CheckBed
go
create Function CheckBed(@tiemslot int ,@long int, @service int)
returns int
As Begin
declare @bed int,@numslot int,@outlet int
	-- số time slot của 1 dịch vụ của 1 outlet
	SELECT @numslot =[dbo].[GetNumTimeslot](@service)
	select @outlet = Room.Outlet from Room,Bed,Service_Bed where Service_Bed.Service=@service and Service_Bed.Bed=Bed.ID and Bed.Room=Room.ID
	and Bed.Deleted=0 and Room.Deleted=0 and Service_Bed.Deleted=0
	select top(1)
	@bed=Bed.ID
	from Room, Bed, Service_Bed
	where Room.Outlet=@outlet and Bed.Room=Room.ID  and Service_Bed.Service=@service and Service_Bed.Bed=Bed.ID 
	and Room.Deleted=0 and Bed.Deleted=0 and Service_Bed.Deleted=0  and Bed.ID not in
	(select AppointmentDetail.Bed from AppointmentDetail where AppointmentDetail.Service=@service and [dbo].[CheckTimeBusy](@tiemslot,@long,AppointmentDetail.timeSlot,@numslot)=0)
	return @bed
end
--test hàm
select [dbo].[CheckBed](1,3,5)


---Hàm kiểm tra có còn therapist  nào  trống với thời gian mà khách hàng chọn ứng với 1 dịch vụ của outlet hay không
--input : mốc thời gian bắt đầu ,số lượng tiemslot,ID service
--output: một bảng các therapist hợp lệ (để customer lựa chọn )
------------------------------------------------------------------------------------------------------------------
drop Function CheckTherapist
go
create Function CheckTherapist(@tiemslot int ,@long int, @service int)

RETURNS @Therapists TABLE (
   IDTherapist int NOT NULL
) 
AS
BEGIN
	declare @numslot int,@outlet int
	select @outlet = Room.Outlet from Room,Bed,Service_Bed where Service_Bed.Service=@service and Service_Bed.Bed=Bed.ID and Bed.Room=Room.ID
	and Bed.Deleted=0 and Room.Deleted=0 and Service_Bed.Deleted=0
	SELECT @numslot =[dbo].[GetNumTimeslot](@service)

   INSERT into @Therapists(IDTherapist)
   select  Staff.ID
   from Staff,Staff_Service,Outlet_Staff
   where Staff.Possition=1 and Staff.ID=Outlet_Staff.Staff and Outlet_Staff.Outlet=@outlet and Staff_Service.Staff=Staff.ID and Staff_Service.Service=@service
   and Staff.Deleted=0 and Staff_Service.Deleted=0 and Outlet_Staff.Deleted=0
   and Staff.ID not in 
   (select AppointmentDetail.Staff from AppointmentDetail where AppointmentDetail.Service=@service and [dbo].[CheckTimeBusy](@tiemslot,@long,AppointmentDetail.timeSlot,@numslot)=0 and AppointmentDetail.Deleted=0)
   RETURN ;
END;

--test hàm
select * from [dbo].CheckTherapist(7,2,2)



--Hàm kiểm tra hợp lệ và trả về danh sách các therapsit có thể đặt cuộc hẹn
--input : ID customer,ID service,mốc thời gian bắt đầu ,
--output: một bảng các therapist hợp lệ (để customer lựa chọn )
---------------------------------------------------------------------------------------------------------------

drop function CheckAppointment
go
create function CheckAppointment(@customer int, @service int,@slotbegin int)
returns @Therapists TABLE(
	 IDTherapist int )
as
begin 
	declare @numslot int, @bufftiem int,@longtime int,@bed int
	SELECT @numslot =[dbo].[GetNumTimeslot](@service)
	SELECT @bufftiem =[dbo].[GetBuffTime](@customer,@service)
	set @longtime=@numslot+@bufftiem
	  select @bed=[dbo].CheckBed(@slotbegin,@longtime,@service)
	  if @bed is Null
		  begin
			return;
		  end
	  else
		  begin
			INSERT into @Therapists(IDTherapist)
			select * from [dbo].CheckTherapist(@slotbegin,@longtime,@service)
		  end
	return;
end
--test hàm
select * from CheckAppointment(2,2,3)


-- Hàm đưa ra gợi ý cho các khoảng thời gian hợp lệ
--input : ID customer, ID dịch vụ
--output: Một bảng các mốc timeslot hợp lệ và danh sách các therapist hợp lệ



drop function SuggestionsTimeSlot
go
Create function SuggestionsTimeSlot(@customer int,@service int)
returns @ListTimeSlot table
	(IDTherapist int,TimeSlotbegin int)
as
begin 
	DECLARE @indx INT = 1, @Therapists int
	WHILE @indx < 64
	BEGIN
		insert into @ListTimeSlot(IDTherapist,TimeSlotbegin)
		select *, @indx from CheckAppointment(@customer,@service,@indx)
		SET @indx = @indx + 1;
	END;
	delete from @ListTimeSlot where IDTherapist is null
	return;

end

--test hàm
select TimeSlotbegin,IDTherapist  from SuggestionsTimeSlot(2,2)
order by TimeSlotbegin



--
DROP PROC BookAppointment
go
create Procedure BookAppointment(@customer int, @service int, @slotbegin int,@bed int,@date date,@CustomerSign nvarchar(200), @Staff int)
AS
BEGIN TRAN
SET TRAN ISOLATION LEVEL READ COMMITTED 
	declare @outlet int, @cost float,@IDAppointment int
	SELECT @cost=Service.Price from Service where Service.ID=@service
	select @outlet = Room.Outlet from Room,Bed,Service_Bed where Service_Bed.Service=@service and Service_Bed.Bed=Bed.ID and Bed.Room=Room.ID
	and Bed.Deleted=0 and Room.Deleted=0 and Service_Bed.Deleted=0
	select @IDAppointment=Appointment.ID from Appointment where Customer=@customer and IDOutlet= @outlet and day([DateToOutlet]) =day(@date) and month([DateToOutlet]) =month(@date) and year([DateToOutlet]) =year(@date) 
	if(@IDAppointment is NULL)
	begin
		insert into Appointment(bookingTime,[CreateDate],[Customer],[IDOutlet],[DateToOutlet])
		values(SYSDATETIME(),SYSDATETIME(),@customer,@outlet,@date)
		select @IDAppointment=Appointment.ID from Appointment where Customer=@customer and IDOutlet= @outlet and day([DateToOutlet]) =day(@date) and month([DateToOutlet]) =month(@date) and year([DateToOutlet]) =year(@date)   
	end
	insert into AppointmentDetail([Appointment],[Service],[Cost],[CustomerSign],[Date],[Bed],[Staff],[TimeSlot],[CreateDate])
	values(@IDAppointment,@service,@cost,@CustomerSign,@date,@bed,@Staff,@slotbegin,SYSDATETIME())
COMMIT TRAN			
																					