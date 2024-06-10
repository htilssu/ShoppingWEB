create table AspNetRoles
(
    Id               varchar(255) not null
        primary key,
    Name             varchar(256),
    NormalizedName   varchar(256)
        constraint RoleNameIndex
            unique,
    ConcurrencyStamp varchar(max)
)
go

create table AspNetRoleClaims
(
    Id         int identity
        primary key,
    RoleId     varchar(255) not null
        constraint FK_AspNetRoleClaims_AspNetRoles_RoleId
            references AspNetRoles
            on delete cascade,
    ClaimType  varchar(max),
    ClaimValue varchar(max)
)
go

create index IX_AspNetRoleClaims_RoleId
    on AspNetRoleClaims (RoleId)
go

create table AspNetUsers
(
    Id                   varchar(255)  not null
        primary key,
    UserName             nvarchar(256) not null,
    FirstName            nvarchar(max),
    LastName             nvarchar(max),
    PhoneNumber          varchar(max),
    Email                varchar(256)  not null,
    BirthDay             datetime2(6),
    Gender               bit,
    NormalizedUserName   varchar(256)
        constraint UserNameIndex
            unique,
    NormalizedEmail      varchar(256),
    EmailConfirmed       bit           not null,
    PasswordHash         varchar(max),
    SecurityStamp        varchar(max),
    ConcurrencyStamp     varchar(max),
    PhoneNumberConfirmed bit           not null,
    TwoFactorEnabled     bit           not null,
    LockoutEnd           datetime2(6),
    LockoutEnabled       bit           not null,
    AccessFailedCount    int           not null,
    AvtPath              varchar(max)  not null
)
go

create table AspNetUserClaims
(
    Id         int identity
        primary key,
    UserId     varchar(255) not null
        constraint FK_AspNetUserClaims_AspNetUsers_UserId
            references AspNetUsers
            on delete cascade,
    ClaimType  varchar(max),
    ClaimValue varchar(max)
)
go

create index IX_AspNetUserClaims_UserId
    on AspNetUserClaims (UserId)
go

create table AspNetUserLogins
(
    LoginProvider       varchar(128) not null,
    ProviderKey         varchar(128) not null,
    ProviderDisplayName varchar(max),
    UserId              varchar(255) not null
        constraint FK_AspNetUserLogins_AspNetUsers_UserId
            references AspNetUsers
            on delete cascade,
    primary key (LoginProvider, ProviderKey)
)
go

create index IX_AspNetUserLogins_UserId
    on AspNetUserLogins (UserId)
go

create table AspNetUserRoles
(
    UserId varchar(255) not null
        constraint FK_AspNetUserRoles_AspNetUsers_UserId
            references AspNetUsers
            on delete cascade,
    RoleId varchar(255) not null
        constraint FK_AspNetUserRoles_AspNetRoles_RoleId
            references AspNetRoles
            on delete cascade,
    primary key (UserId, RoleId)
)
go

create index IX_AspNetUserRoles_RoleId
    on AspNetUserRoles (RoleId)
go

create table AspNetUserTokens
(
    UserId        varchar(255) not null
        constraint FK_AspNetUserTokens_AspNetUsers_UserId
            references AspNetUsers
            on delete cascade,
    LoginProvider varchar(128) not null,
    Name          varchar(128) not null,
    Value         varchar(max),
    primary key (UserId, LoginProvider, Name)
)
go

create index EmailIndex
    on AspNetUsers (NormalizedEmail)
go

create table Cart
(
    Id         varchar(255) not null
        primary key,
    CustomerId varchar(255)
        constraint Cart_ibfk_1
            references AspNetUsers
            on delete cascade
)
go

create index customer_Id
    on Cart (CustomerId)
go

create table Category
(
    Id           varchar(255) not null
        primary key,
    CategoryName nvarchar(255),
    ImagePath    varchar(255)
)
go

create table Coupons
(
    Id                varchar(255) not null
        primary key,
    Code              varchar(255),
    CouponDescription varchar(255),
    DiscountValue     int,
    Limited           int,
    StartAt           datetime2(0),
    EndAt             datetime2(0)
)
go

create table DeliveryType
(
    Id           varchar(255) not null
        primary key,
    ProviderName nvarchar(255),
    Price        float
)
go

create table Delivery_INFO
(
    Id          varchar(255) not null
        primary key,
    ReceiverId  varchar(255)
        constraint Delivery_INFO_ibfk_1
            references AspNetUsers,
    Street      varchar(255),
    Ward        varchar(255),
    District    varchar(255),
    City        varchar(255),
    PhoneNumber varchar(255)
)
go

create index Receiver
    on Delivery_INFO (ReceiverId)
go

create table PaymentMethod
(
    Id          varchar(255) not null
        primary key,
    PaymentName varchar(255)
)
go

create table Seller
(
    Id           varchar(255) not null
        primary key,
    SellerName   nvarchar(255),
    ReplyPercent int,
    JoinAt       date,
    Follower     int,
    Address      nvarchar(255)
)
go

create table Product
(
    Id               varchar(255) not null
        primary key,
    CategoryId       varchar(255)
        constraint Product_ibfk_1
            references Category
            on delete cascade,
    ProductName      nvarchar(255),
    Price            float,
    DiscountPercent  float,
    ShortDescription nvarchar(max),
    LongDescription  nvarchar(max),
    Publish          tinyint,
    Rating           int,
    Sold             int,
    SellerId         varchar(255)
        constraint Product_Seller_Id_fk
            references Seller
            on delete cascade,
    Style            nvarchar(50),
    MadeIn           nvarchar(50),
    Material         nvarchar(50)
)
go

create table ImageURL
(
    ImagePath varchar(255) not null
        primary key,
    ProductId varchar(255)
        constraint ImageURL_Product_Id_fk
            references Product
            on delete cascade,
    Thumbnail tinyint
)
go

create index CategoryId
    on Product (CategoryId)
go

create table TypeProduct
(
    Id        varchar(255) not null
        primary key,
    ProductId varchar(255)
        constraint TypeProduct_ibdk_1
            references Product
            on delete cascade,
    TypeName  nvarchar(255),
    ImagePath varchar(255)
)
go

create table Bill
(
    Id            varchar(255) not null
        primary key,
    Total         float,
    PaymentMethod varchar(255)
        constraint Bill_PaymentMethod_Id_fk
            references PaymentMethod,
    Quantity      int,
    Date          datetime2(0),
    DeliveryId    varchar(255)
        constraint Bill_DeliveryType_Id_fk
            references DeliveryType,
    UserId        varchar(255)
        constraint Bill_AspNetUsers_Id_fk
            references AspNetUsers,
)
go

create table BillItem
(
    Id            varchar(255) not null
        constraint BillItem_pk
            primary key,
    TypeProductId varchar(255)
        constraint BillItem_TypeProduct_Id_fk
            references TypeProduct,
    BillId        varchar(255)
        constraint BillItem_Bill_Id_fk
            references Bill,
    Quantity      int,
    SizeName      varchar(20),
    IsComment     tinyint,
    Total         float
)
go

create table CartItem
(
    Id            varchar(255) not null
        primary key,
    CartId        varchar(255)
        constraint CartItem_ibfk_2
            references Cart
            on delete cascade,
    Quantity      int,
    TypeProductId varchar(255)
        constraint CartItem_TypeProduct_Id_fk
            references TypeProduct
            on delete cascade,
    SizeType      nvarchar(255)
)
go

create index CartId
    on CartItem (CartId)
go

create table Comment
(
    Id            varchar(255) not null
        primary key,
    Content       nvarchar,
    UserId        varchar(255)
        constraint Comment_AspNetUsers_Id_fk
            references AspNetUsers,
    Time          datetime2(0),
    TypeProductId varchar(255)
        constraint Comment_TypeProduct_Id_fk
            references TypeProduct
            on delete cascade,
    Material      varchar(100),
    Color         varchar(20),
    LikeDetail    varchar(255)
)
go

create table Size
(
    TypeProductId varchar(255) not null
        constraint Size_TypeProduct_Id_fk
            references TypeProduct
            on delete cascade,
    SizeType      nvarchar(8)  not null,
    Quantity      int,
    constraint Size_pk
        primary key (SizeType, TypeProductId)
)
go

create table __EFMigrationsHistory
(
    MigrationId    nvarchar(150) not null
        constraint PK___EFMigrationsHistory
            primary key,
    ProductVersion nvarchar(32)  not null
)
go


