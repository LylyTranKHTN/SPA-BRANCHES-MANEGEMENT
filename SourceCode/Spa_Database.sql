--drop database spaCCH
use master
create database spaCCH
go
use spaCCH
go
create table Customer
(
	Avatar varchar(max),
	blackList int,
	DoB datetime,
	Email varchar(25),
	Gender int,
	ID int identity(1,1) NOT NULL,
	Name nvarchar(50),
	NRIC int,
	[passWord] varchar(20),
	Phone char(10),
	Profression int,
	Token varchar(max),
	Deleted int, --1 đã xóa, 0 là chưa xóa
	CreateDate datetime,
	UpdateDate datetime,
	RecordVersion decimal(10),
)

GO
create table Outlet
(
	ID int identity(1,1) NOT NULL,
	[address] nvarchar(150),
	[Image] varchar(max),
	Name nvarchar(50),
	Phone char(10),
	Start int,
	Deleted int default 0, --1 đã xóa, 0 là chưa xóa
	CreateDate datetime,
	UpdateDate datetime,
	RecordVersion decimal(10),
)
go
create table Staff
(
	Avatar varchar(max),
	[Description] nvarchar(500),
	DoB datetime,
	Experient varchar(10),
	Gender int,
	ID int identity(1,1) NOT NULL,
	Name nvarchar(50),
	[passWord] varchar(20),
	Phone char(10),
	Possition int,
	userName varchar(30),
	Deleted int default 0, --1 đã xóa, 0 là chưa xóa
	CreateDate datetime,
	UpdateDate datetime,
	RecordVersion decimal(10),
)
go
create table ReviewStaff
(
	Content nvarchar(500) NOT NULL,
	[Date] datetime,
	ID int identity(1,1) NOT NULL,
	Star int,
	Staff int NOT NULL,
	Customer int NOT NULL,
	Deleted int default 0, --1 đã xóa, 0 là chưa xóa
	CreateDate datetime,
	UpdateDate datetime,
	RecordVersion decimal(10),
)
go
create table ReviewOutlet
(
	Content nvarchar(500) NOT NULL,
	[Date] datetime,
	ID int identity(1,1) NOT NULL,
	Star int,
	Outlet int NOT NULL,
	Customer int NOT NULL,
	Deleted int, --1 đã xóa, 0 là chưa xóa
	CreateDate datetime,
	UpdateDate datetime,
	RecordVersion decimal(10),
)
go
create table Outlet_Staff
(
	Outlet int NOT NULL,
	Staff int NOT NULL,
	Deleted int default 0, --1 đã xóa, 0 là chưa xóa
	CreateDate datetime,
	UpdateDate datetime,
	RecordVersion decimal(10),
)
go
create table Room
(
	ID int identity(1,1) NOT NULL,
	Name nvarchar(50),
	numOfBed int,
	Outlet int,
	Deleted int default 0, --1 đã xóa, 0 là chưa xóa
	CreateDate datetime,
	UpdateDate datetime,
	RecordVersion decimal(10),
)
go
create table Bed
(
	ID int identity(1,1) NOT NULL,
	Room int,
	Deleted int default 0, --1 đã xóa, 0 là chưa xóa
	CreateDate datetime,
	UpdateDate datetime,
	RecordVersion decimal(10),
)
go
create table [Service]
(
	ID int identity(1,1) NOT NULL,
	[Image] varchar(max),
	Name nvarchar(50),
	numOfTimeSlot int,
	Price float NOT NULL,
	Star int,
	Term nvarchar(200),
	serviceType int,
	Deleted int default 0, --1 đã xóa, 0 là chưa xóa
	CreateDate datetime,
	UpdateDate datetime,
	RecordVersion decimal(10),
)
go
create table Service_Bed
(
	Bed int NOT NULL,
	[Service] int NOT NULL,
	Deleted int default 0, --1 đã xóa, 0 là chưa xóa
	CreateDate datetime,
	UpdateDate datetime,
	RecordVersion decimal(10),
)
go 
create table ServiceType
(
	ID int identity(1,1) NOT NULL,
	Name nvarchar(100),
	Deleted int default 0, --1 đã xóa, 0 là chưa xóa
	CreateDate datetime,
	UpdateDate datetime,
	RecordVersion decimal(10),
)
go
create table ReviewService
(
	Content nvarchar(500) NOT NULL,
	[Date] datetime,
	ID int identity(1,1) NOT NULL,
	Star int,
	[Service] int,
	Customer int NOT NULL,
	Deleted int default 0, --1 đã xóa, 0 là chưa xóa
	CreateDate datetime,
	UpdateDate datetime,
	RecordVersion decimal(10),
)
go
create table CustomerType
(
	[Description] nvarchar(50),
	ID int identity(1,1) NOT NULL,
	Name nvarchar(100),
	Deleted int default 0, --1 đã xóa, 0 là chưa xóa
	CreateDate datetime,
	UpdateDate datetime,
	RecordVersion decimal(10),
)
go
create table BufferTime
(
	bufferTime int,
	customerType int NOT NULL,
	[Service] int NOT NULL,
	Deleted int default 0, --1 đã xóa, 0 là chưa xóa
	CreateDate datetime,
	UpdateDate datetime,
	RecordVersion decimal(10),
	primary key([customerType],[Service])
)
go
create table Appointment
(
	bookingTime datetime,
	ID int identity(1,1) NOT NULL,
	[Status] int,
	Customer int,
	Deleted int default 0, --1 đã xóa, 0 là chưa xóa
	CreateDate datetime,
	UpdateDate datetime,
	RecordVersion decimal(10),
)
go
create table AppointmentDetail
(
	Appointment int NOT NULL,
	[Service] int NOT NULL,
	Cost float,
	CustomerSign nvarchar(200),
	[Date] datetime NOT NULL,
	feedBack nvarchar(500),
	imageAfter1 varchar(max),
	imageAfter2 varchar(max),
	imageBefore varchar(max),
	Note nvarchar(200),
	Bed int,
	timeSlot int NOT NULL,
	Staff int NOT NULL,
	Deleted int default 0, --1 đã xóa, 0 là chưa xóa
	CreateDate datetime,
	UpdateDate datetime,
	RecordVersion decimal(10),
)
go
create table TimeSlot
(
	[From] varchar(20),
	ID int identity(1,1) NOT NULL,
	[To] varchar(20),
	Deleted int default 0, --1 đã xóa, 0 là chưa xóa
	CreateDate datetime,
	UpdateDate datetime,
	RecordVersion decimal(10),
)
alter table TimeSlot drop column [From]
alter table TimeSlot drop column [To]
alter table TimeSlot add [From] time(0)
alter table TimeSlot add [To] time(0)

declare @From time(0) = '7:00'
declare @To time(0) = '7:15'
declare @maxTime time(0) = '23:00'
declare @slot int = 15

while @To < @maxTime
begin
	insert into TimeSlot([From],[To]) values (@From, @To);
	set @From = DateAdd(Minute, @slot, @From);
	set @To = DateAdd(Minute, @slot, @To);
end

go
create table Measurement
(
	Appointment int NOT NULL,
	[Service] int NOT NULL,
	[Time] int NOT NULL, -- truoc(1) hoac sau(0)
	BMI float,
	bodyMass float,
	fatContent float,
	Height float,
	muscleContent float,
	[Status] int,
	[Weight] float,
	Deleted int default 0, --1 đã xóa, 0 là chưa xóa
	CreateDate datetime,
	UpdateDate datetime,
	RecordVersion decimal(10),
)
-- Primary Key
alter table Customer add constraint PK_Customer Primary Key (ID);
alter table Outlet add constraint PK_Outlet Primary Key (ID);
alter table Staff add constraint PK_Staff Primary Key (ID);
alter table ReviewStaff add constraint PK_ReviewStaff Primary Key (ID);
alter table ReviewOutlet add constraint PK_ReviewOutlet Primary Key (ID);
alter table ReviewService add constraint PK_ReviewService Primary Key (ID);
alter table Outlet_Staff add constraint PK_Outlet_Staff Primary Key (Outlet,Staff);
alter table Bed add constraint PK_Bed Primary Key (ID);
alter table [Service] add constraint PK_Service Primary Key (ID);
alter table Service_Bed add constraint PK_Service_Bed Primary Key (Bed, [Service]);
alter table ServiceType add constraint PK_ServiceType Primary Key (ID);
alter table CustomerType add constraint PK_CustomerType Primary Key (ID);
alter table Appointment add constraint PK_Appointment Primary Key (ID);
alter table AppointmentDetail add constraint PK_AppointmentDetail Primary Key (Appointment,[Service]);
alter table Room add constraint PK_Room Primary Key (ID);
alter table TimeSlot add constraint PK_TimeSlot Primary Key (ID);
alter table Measurement add constraint PK_Measurement Primary Key (Appointment,[Service], [Time]);

go
-- Foreign Key
alter table ReviewStaff add
constraint FK_ReviewStaff_Customer foreign key (Customer) references Customer(ID),
constraint FK_ReviewStaff_Staff foreign key (Staff) references Staff(ID);
alter table Outlet_Staff add
constraint FK_Outlet_Staff_Staff foreign key (Staff) references Staff(ID),
constraint FK_Outlet_Staff_Outlet foreign key (Outlet) references Outlet(ID);
alter table ReviewOutlet add
constraint FK_ReviewOutlet_Customer foreign key (Customer) references Customer(ID),
constraint FK_ReviewOutlet_Outlet foreign key (Outlet) references Outlet(ID);
alter table Room add
constraint FK_Room_Outlet foreign key (Outlet) references Outlet(ID);
alter table Bed add
constraint FK_Bed_Room foreign key (Room) references Room(ID);
alter table AppointmentDetail add
constraint FK_AppointmentDetail_TimeSlot foreign key (TimeSlot) references TimeSlot(ID),
constraint FK_AppointmentDetail_Service foreign key ([Service]) references [Service](ID),
constraint FK_AppointmentDetail_Appointment foreign key (Appointment) references Appointment(ID),
constraint FK_AppointmentDetail_Bed foreign key (Bed) references Bed(ID),
constraint FK_AppointmentDetail_Staff foreign key (Staff) references Staff(ID);
alter table Measurement add
constraint FK_Measurement_AppointmentDetail foreign key (Appointment,[Service]) references AppointmentDetail(Appointment,[Service]);
alter table Appointment add
constraint FK_Appointment_Customer foreign key (Customer) references Customer(ID);
alter table [Service] add
constraint FK_Service_ServiceType foreign key (serviceType) references ServiceType(ID);
alter table BufferTime add
constraint FK_BufferTime_CustomerType foreign key (customerType) references CustomerType(ID),
constraint FK_BufferTime_Service foreign key ([Service]) references [Service](ID);
alter table ReviewService add
constraint FK_ReviewService_Customer foreign key (Customer) references Customer(ID),
constraint FK_ReviewService_Service foreign key ([Service]) references [Service](ID);
alter table Service_Bed add
constraint FK_Service_Bed_Bed foreign key (Bed) references Bed(ID),
constraint FK_Service_Bed_Service foreign key ([Service]) references [Service](ID);

--Update database
go
create table Staff_Service
(
	Staff int not null,
	Service int not null,
	Deleted int default 0, --1 đã xóa, 0 là chưa xóa
	CreateDate datetime,
	UpdateDate datetime,
	RecordVersion decimal(10),
	primary key(Staff,Service),
)

alter table Staff_Service add
constraint FK_Staff_Service_Staff foreign key (Staff) references Staff(ID),
constraint FK_Staff_Service_Service foreign key (Service) references Service(ID);

alter table Staff add Email varchar(50)

alter table AppointmentDetail add BufferTime int

alter table AppointmentDetail drop column imageAfter1
alter table AppointmentDetail drop column imageAfter2
alter table AppointmentDetail drop column imageBefore

alter table AppointmentDetail add imageAfter1 image
alter table AppointmentDetail add imageAfter2 image
alter table AppointmentDetail add imageBefore image

alter table Outlet drop column Image
alter table Outlet add Image image

alter table Customer drop column Avatar
alter table Customer add Avatar image

alter table Staff drop column Avatar
alter table Staff add Avatar image

alter table Service drop column Image
alter table Service add Image image

truncate table customer
alter table Customer drop column passWord
alter table Customer add passWord varchar(max)

alter table Customer alter column [passWord] varchar(32)
