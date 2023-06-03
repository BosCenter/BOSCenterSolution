USE [master]
GO
/****** Object:  Database [BosCenter_DB]    Script Date: 08/04/2020 5:02:25 PM ******/
CREATE DATABASE [BosCenter_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BosCenter_DB', FILENAME = N'C:\Program Files (x86)\Plesk\Databases\MSSQL\MSSQL14.MSSQLSERVER2017\MSSQL\DATA\BosCenter_DB.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BosCenter_DB_log', FILENAME = N'C:\Program Files (x86)\Plesk\Databases\MSSQL\MSSQL14.MSSQLSERVER2017\MSSQL\DATA\BosCenter_DB_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BosCenter_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BosCenter_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BosCenter_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BosCenter_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BosCenter_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BosCenter_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [BosCenter_DB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BosCenter_DB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [BosCenter_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BosCenter_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BosCenter_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BosCenter_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BosCenter_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BosCenter_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BosCenter_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BosCenter_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BosCenter_DB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BosCenter_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BosCenter_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BosCenter_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BosCenter_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BosCenter_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BosCenter_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BosCenter_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BosCenter_DB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BosCenter_DB] SET  MULTI_USER 
GO
ALTER DATABASE [BosCenter_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BosCenter_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BosCenter_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BosCenter_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
USE [BosCenter_DB]
GO
/****** Object:  Table [dbo].[AutoNumber]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AutoNumber](
	[ClientID] [nvarchar](max) NULL,
	[Client_Prefix] [nvarchar](max) NULL,
	[SessionId] [nvarchar](max) NULL,
	[ReceiptId] [nvarchar](max) NULL,
	[VoucherNo] [nvarchar](250) NULL,
	[ProjectID] [nvarchar](250) NULL,
	[Project_Prefix] [nvarchar](250) NULL,
	[EmployeeId] [nvarchar](250) NULL,
	[Employee_Prefix] [nvarchar](250) NULL,
	[WeekOff] [nvarchar](250) NULL,
	[MsgSendDate] [date] NULL,
	[ItemType_ID] [nvarchar](max) NULL,
	[CallRefrenceNo] [nvarchar](300) NULL,
	[PlanCode] [nvarchar](250) NULL,
	[DistributorID_Prefix] [nvarchar](250) NULL,
	[Distributor_ID] [nvarchar](250) NULL,
	[ReturnID] [nvarchar](250) NULL,
	[ListID] [nvarchar](300) NULL,
	[ProductId] [nvarchar](250) NULL,
	[DistributorID] [nvarchar](300) NULL,
	[Distributor_Prefix] [nvarchar](300) NULL,
	[SubDistributorID] [nvarchar](300) NULL,
	[SubDistributor_Prefix] [nvarchar](300) NULL,
	[RetailerId] [nvarchar](300) NULL,
	[Retailer_Prefix] [nvarchar](300) NULL,
	[SMSAPI] [nvarchar](max) NULL,
	[SMSSenderId] [nvarchar](max) NULL,
	[ComplaintPrefix] [nvarchar](250) NULL,
	[ComplaintId] [nvarchar](250) NULL,
	[Recharge_APIPer] [nvarchar](250) NULL,
	[Flight_APIPer] [nvarchar](250) NULL,
	[PAN_APIPer] [nvarchar](250) NULL,
	[MoneyTransfer_APIPer] [nvarchar](250) NULL,
	[GST_APIPer] [nvarchar](250) NULL,
	[BusBooking_APIPer] [nvarchar](250) NULL,
	[Rail_APIPer] [nvarchar](250) NULL,
	[TransId] [nvarchar](250) NULL,
	[RefrenceID] [nvarchar](250) NULL,
	[ServiceCharge] [nvarchar](250) NULL,
	[Amt_Transfer_TransID] [nvarchar](250) NULL,
	[RechargeAPI_Status] [nvarchar](250) NULL,
	[PANCardAPI_Status] [nvarchar](250) NULL,
	[MoneyTransferAPI_Status] [nvarchar](250) NULL,
	[AEPS_API_Status] [nvarchar](250) NULL,
	[CompanyCode_Prefix] [nvarchar](250) NULL,
	[CompanyCode] [nvarchar](250) NULL,
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Customer_Prefix] [nvarchar](250) NULL,
	[CustomerID] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_Admin_BankAccount_Master]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_Admin_BankAccount_Master](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[AccountHolder_Name] [nvarchar](max) NULL,
	[AccountNo] [nvarchar](250) NOT NULL,
	[IFSC_Code] [nvarchar](250) NOT NULL,
	[Bank_Name] [nvarchar](max) NULL,
	[BranchName] [nvarchar](max) NULL,
	[AccountType] [nvarchar](max) NULL,
	[PanNo] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_AEPS_CheckStatus]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_AEPS_CheckStatus](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Txntype] [nvarchar](500) NULL,
	[APITimestamp] [nvarchar](500) NULL,
	[BcId] [nvarchar](500) NULL,
	[TerminalId] [nvarchar](500) NULL,
	[TransactionId] [nvarchar](500) NULL,
	[Amount] [nvarchar](500) NULL,
	[TxnStatus] [nvarchar](500) NULL,
	[BankIIN] [nvarchar](500) NULL,
	[TxnMedium] [nvarchar](500) NULL,
	[EndCustMobile] [nvarchar](500) NULL,
	[RecordDateTime] [datetime] NULL,
	[EntryBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_AEPS_UpdateStatus]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_AEPS_UpdateStatus](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[TransactionId] [nvarchar](500) NULL,
	[VenderId] [nvarchar](500) NULL,
	[BcCode] [nvarchar](500) NULL,
	[APIStatus] [nvarchar](500) NULL,
	[rrn] [nvarchar](500) NULL,
	[RecordDateTime] [datetime] NULL,
	[EntryBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_API_Log_Records]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_API_Log_Records](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[API_Name] [nvarchar](250) NULL,
	[Trans_ID] [nvarchar](250) NULL,
	[Trans_DateTime] [datetime] NULL,
	[Request_String] [nvarchar](max) NULL,
	[Response_String] [nvarchar](max) NULL,
	[AgentID] [nvarchar](250) NULL,
	[AgentType] [nvarchar](250) NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_APICommissionSettigs]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_APICommissionSettigs](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[AgentType] [nvarchar](250) NULL,
	[RegistrationID] [nvarchar](250) NULL,
	[Recharge_APIPer] [numeric](18, 2) NULL,
	[Flight_APIPer] [numeric](18, 2) NULL,
	[PAN_APIPer] [numeric](18, 2) NULL,
	[MoneyTransfer_APIPer] [numeric](18, 2) NULL,
	[GST_APIPer] [numeric](18, 2) NULL,
	[BusBooking_APIPer] [numeric](18, 2) NULL,
	[Rail_APIPer] [numeric](18, 2) NULL,
	[updatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_APIVSCategory_Master]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_APIVSCategory_Master](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ProductService] [nvarchar](250) NULL,
	[Category] [nvarchar](250) NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL,
	[CanChange] [nvarchar](250) NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_APIVSCategory_Master_SA]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_APIVSCategory_Master_SA](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ProductService] [nvarchar](250) NULL,
	[Category] [nvarchar](250) NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_BankDetails]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_BankDetails](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[AgentType] [nvarchar](250) NULL,
	[RegistrationId] [nvarchar](250) NULL,
	[RegistrationDate] [date] NULL,
	[BankName] [nvarchar](250) NULL,
	[BranchName] [nvarchar](250) NULL,
	[AccountType] [nvarchar](250) NULL,
	[IFSCCode] [nvarchar](250) NULL,
	[AccountNo] [nvarchar](250) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[RecordDateTime] [datetime] NULL,
	[SetDefault] [nvarchar](250) NULL,
	[AccountHolderName] [nvarchar](250) NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_ClientRegistration]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_ClientRegistration](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[RegisterationDate] [date] NULL,
	[CompanyCode] [nvarchar](250) NULL,
	[CompanyName] [nvarchar](400) NULL,
	[CompanyHead] [nvarchar](400) NULL,
	[ContactPerson] [nvarchar](400) NULL,
	[Companylogo] [nvarchar](500) NULL,
	[Address_1] [nvarchar](500) NULL,
	[Address_2] [nvarchar](500) NULL,
	[Address_3] [nvarchar](500) NULL,
	[PinCode] [nvarchar](250) NULL,
	[State] [nvarchar](250) NULL,
	[District] [nvarchar](250) NULL,
	[City] [nvarchar](250) NULL,
	[Country] [nvarchar](250) NULL,
	[PhoneNo_1] [nvarchar](250) NULL,
	[PhoneNo_2] [nvarchar](250) NULL,
	[Mobile_No] [nvarchar](250) NULL,
	[Email_ID] [nvarchar](250) NULL,
	[TinNo] [nvarchar](250) NULL,
	[CinNo] [nvarchar](250) NULL,
	[GSTNo] [nvarchar](250) NULL,
	[Status] [nvarchar](250) NULL,
	[ClientPassword] [nvarchar](250) NULL,
	[LastLogin] [datetime] NULL,
	[ClientRole] [nvarchar](250) NULL,
	[IsNewDatabase] [nvarchar](250) NULL,
	[DatabaseName] [nvarchar](250) NULL,
	[ChangeTheme] [nvarchar](400) NULL,
	[Record_DateTime] [datetime] NULL,
	[EmpCode] [nvarchar](250) NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL,
	[WebRedirectUrl] [nvarchar](250) NULL,
	[ClientPin] [nvarchar](250) NULL,
	[CreditBalnceLimit] [numeric](18, 0) NULL,
	[RechargeAPI_Status] [nvarchar](250) NULL,
	[PANCardAPI_Status] [nvarchar](250) NULL,
	[MoneyTransferAPI_Status] [nvarchar](250) NULL,
	[AEPS_API_Status] [nvarchar](250) NULL,
	[HoldAmt] [numeric](18, 2) NULL,
	[HoldAmtRemarks] [nvarchar](500) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_CommissionSlabwise]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_CommissionSlabwise](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[APIName] [nvarchar](250) NULL,
	[FromAmount] [numeric](18, 0) NULL,
	[ToAmount] [numeric](18, 0) NULL,
	[Dis_CommissionType] [nvarchar](250) NULL,
	[Dis_Commission] [numeric](18, 2) NULL,
	[Sub_Dis_CommissionType] [nvarchar](250) NULL,
	[Sub_Dis_Commission] [numeric](18, 2) NULL,
	[Retailer_CommissionType] [nvarchar](250) NULL,
	[Retailer_Commission] [numeric](18, 2) NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL,
	[CompanyCode] [nvarchar](250) NULL,
	[Customer_CommissionType] [nvarchar](250) NULL,
	[Customer_Commission] [numeric](18, 2) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_CommissionSlabwise_SA]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_CommissionSlabwise_SA](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[APIName] [nvarchar](250) NULL,
	[FromAmount] [numeric](18, 0) NULL,
	[ToAmount] [numeric](18, 0) NULL,
	[SA_CommissionType] [nvarchar](250) NULL,
	[SA_Commission] [numeric](18, 2) NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_CommissionSlabwiseVsAdmin_SA]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_CommissionSlabwiseVsAdmin_SA](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[AdminID] [nvarchar](250) NULL,
	[APIName] [nvarchar](250) NULL,
	[FromAmount] [numeric](18, 0) NULL,
	[ToAmount] [numeric](18, 0) NULL,
	[Admin_CommissionType] [nvarchar](250) NULL,
	[Admin_Commission] [numeric](18, 2) NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_Complaint_Master]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_Complaint_Master](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ComplaintDate] [date] NULL,
	[ComplaintID] [nvarchar](250) NULL,
	[kCode] [nvarchar](250) NULL,
	[kCodeType] [nvarchar](250) NULL,
	[Product] [nvarchar](250) NULL,
	[Problem] [nvarchar](250) NULL,
	[Complaint] [nvarchar](max) NULL,
	[Attachment] [nvarchar](250) NULL,
	[ComplaintStatus] [nvarchar](250) NULL,
	[Remarks] [nvarchar](250) NULL,
	[AllotedTo] [nvarchar](250) NULL,
	[AllotedDateTime] [datetime] NULL,
	[ClosedDateTime] [datetime] NULL,
	[ClosedBy] [nvarchar](250) NULL,
	[RecordDateTime] [datetime] NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_Complaint_Master_Chat]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_Complaint_Master_Chat](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ComplaintID] [nvarchar](250) NOT NULL,
	[MessageFrom] [nvarchar](250) NOT NULL,
	[Chat_Message] [nvarchar](max) NULL,
	[RecordDateTime] [datetime] NOT NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_Complaint_Master_Chat_SA]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_Complaint_Master_Chat_SA](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ComplaintID] [nvarchar](250) NOT NULL,
	[MessageFrom] [nvarchar](250) NOT NULL,
	[Chat_Message] [nvarchar](500) NULL,
	[RecordDateTime] [datetime] NOT NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_Complaint_Master_SA]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_Complaint_Master_SA](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ComplaintDate] [date] NULL,
	[ComplaintID] [nvarchar](250) NULL,
	[kCode] [nvarchar](250) NULL,
	[kCodeType] [nvarchar](250) NULL,
	[Product] [nvarchar](250) NULL,
	[Problem] [nvarchar](250) NULL,
	[Complaint] [nvarchar](500) NULL,
	[Attachment] [nvarchar](250) NULL,
	[ComplaintStatus] [nvarchar](250) NULL,
	[Remarks] [nvarchar](250) NULL,
	[AllotedTo] [nvarchar](250) NULL,
	[AllotedDateTime] [datetime] NULL,
	[ClosedDateTime] [datetime] NULL,
	[ClosedBy] [nvarchar](250) NULL,
	[RecordDateTime] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_ComplaintVSProblem_Master]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_ComplaintVSProblem_Master](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Product] [nvarchar](250) NULL,
	[ProductProblem] [nvarchar](250) NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_ComplaintVSProblem_Master_SA]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_ComplaintVSProblem_Master_SA](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Product] [nvarchar](250) NULL,
	[ProductProblem] [nvarchar](250) NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_Dis_SubDis_Retailer_Registration]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_Dis_SubDis_Retailer_Registration](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[AgentType] [nvarchar](250) NULL,
	[RegistrationId] [nvarchar](250) NULL,
	[RegistrationDate] [date] NULL,
	[AgencyName] [nvarchar](250) NULL,
	[PanCardNumber] [nvarchar](250) NULL,
	[FirstName] [nvarchar](250) NULL,
	[LastName] [nvarchar](250) NULL,
	[EmailID] [nvarchar](250) NULL,
	[MobileNo] [nvarchar](250) NULL,
	[DOB] [nvarchar](250) NOT NULL,
	[BusinessType] [nvarchar](250) NULL,
	[AlternateMobileNo] [nvarchar](250) NULL,
	[OfficeAddress] [nvarchar](max) NULL,
	[PermanentAddress] [nvarchar](max) NULL,
	[Pincode] [nvarchar](250) NULL,
	[State] [nvarchar](250) NULL,
	[City] [nvarchar](250) NULL,
	[AddharCardNo] [nvarchar](250) NULL,
	[GSTNO] [nvarchar](250) NULL,
	[WebSite] [nvarchar](250) NULL,
	[UploadPanCard] [nvarchar](500) NULL,
	[UploadAddharCard_Front] [nvarchar](500) NULL,
	[UploadAddharCard_Back] [nvarchar](500) NULL,
	[UploadOtherProof] [nvarchar](500) NULL,
	[UploadPhoto] [nvarchar](500) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[RecordDateTime] [datetime] NULL,
	[AgentPassword] [nvarchar](250) NULL,
	[ChangeTheme] [nvarchar](250) NULL,
	[RefrenceID] [nvarchar](250) NULL,
	[RefrenceType] [nvarchar](250) NULL,
	[CreditBalnceLimit] [numeric](18, 0) NULL,
	[TransactionPin] [nvarchar](250) NULL,
	[ActiveStatus] [nvarchar](250) NULL,
	[EmpCode] [nvarchar](250) NULL,
	[RechargeAPI_Status] [nvarchar](250) NULL,
	[PANCardAPI_Status] [nvarchar](250) NULL,
	[MoneyTransferAPI_Status] [nvarchar](250) NULL,
	[AEPS_API_Status] [nvarchar](250) NULL,
	[District] [nvarchar](250) NULL,
	[BcCode] [nvarchar](250) NULL,
	[StateID] [nvarchar](250) NULL,
	[DistrictID] [nvarchar](250) NULL,
	[CompanyCode] [nvarchar](250) NULL,
	[Ref_Code] [nvarchar](250) NULL,
	[HoldAmt] [numeric](18, 2) NULL,
	[HoldAmtRemarks] [nvarchar](500) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_Dis_SubDis_Retailer_Registration_Deleted]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_Dis_SubDis_Retailer_Registration_Deleted](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[AgentType] [nvarchar](250) NULL,
	[RegistrationId] [nvarchar](250) NULL,
	[RegistrationDate] [date] NULL,
	[AgencyName] [nvarchar](250) NULL,
	[PanCardNumber] [nvarchar](250) NULL,
	[FirstName] [nvarchar](250) NULL,
	[LastName] [nvarchar](250) NULL,
	[EmailID] [nvarchar](250) NULL,
	[MobileNo] [nvarchar](250) NULL,
	[DOB] [nvarchar](250) NOT NULL,
	[BusinessType] [nvarchar](250) NULL,
	[AlternateMobileNo] [nvarchar](250) NULL,
	[OfficeAddress] [nvarchar](500) NULL,
	[PermanentAddress] [nvarchar](500) NULL,
	[Pincode] [nvarchar](250) NULL,
	[State] [nvarchar](250) NULL,
	[City] [nvarchar](250) NULL,
	[AddharCardNo] [nvarchar](250) NULL,
	[GSTNO] [nvarchar](250) NULL,
	[WebSite] [nvarchar](250) NULL,
	[UploadPanCard] [nvarchar](500) NULL,
	[UploadAddharCard_Front] [nvarchar](500) NULL,
	[UploadAddharCard_Back] [nvarchar](500) NULL,
	[UploadOtherProof] [nvarchar](500) NULL,
	[UploadPhoto] [nvarchar](500) NULL,
	[AgentPassword] [nvarchar](250) NULL,
	[ChangeTheme] [nvarchar](250) NULL,
	[RefrenceID] [nvarchar](250) NULL,
	[RefrenceType] [nvarchar](250) NULL,
	[CreditBalnceLimit] [numeric](18, 0) NULL,
	[TransactionPin] [nvarchar](250) NULL,
	[ActiveStatus] [nvarchar](250) NULL,
	[EmpCode] [nvarchar](250) NULL,
	[RechargeAPI_Status] [nvarchar](250) NULL,
	[PANCardAPI_Status] [nvarchar](250) NULL,
	[MoneyTransferAPI_Status] [nvarchar](250) NULL,
	[AEPS_API_Status] [nvarchar](250) NULL,
	[District] [nvarchar](250) NULL,
	[BcCode] [nvarchar](250) NULL,
	[StateID] [nvarchar](250) NULL,
	[DistrictID] [nvarchar](250) NULL,
	[CompanyCode] [nvarchar](250) NULL,
	[Ref_Code] [nvarchar](250) NULL,
	[HoldAmt] [numeric](18, 2) NULL,
	[HoldAmtRemarks] [nvarchar](500) NULL,
	[UpdatedBy] [nvarchar](500) NULL,
	[UpdatedOn] [datetime] NULL,
	[RecordDateTime] [datetime] NULL,
	[DeletedBy] [nvarchar](500) NULL,
	[DeletedOn] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_GST_Refund_Details]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_GST_Refund_Details](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[RefrenceID] [nvarchar](250) NULL,
	[RegistrationId] [nvarchar](250) NULL,
	[GST_Month] [nvarchar](250) NULL,
	[FromDate] [date] NULL,
	[ToDate] [date] NULL,
	[Remarks] [nvarchar](500) NULL,
	[CommAmt] [numeric](18, 2) NULL,
	[GSTAmt] [numeric](18, 2) NULL,
	[GrossAmt] [numeric](18, 2) NULL,
	[TDS_Amt] [numeric](18, 2) NULL,
	[CreditBalance] [numeric](18, 2) NULL,
	[ApprovedBy] [nvarchar](250) NULL,
	[ApprovedDateTime] [datetime] NULL,
	[ApporvedStatus] [nvarchar](250) NULL,
	[ApporveRemakrs] [nvarchar](500) NULL,
	[RecordDateTime] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_InstaMojo_Gateway_Request_Details]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_InstaMojo_Gateway_Request_Details](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Request_DateTime] [datetime] NULL,
	[Request_Transaction_Id] [nvarchar](350) NULL,
	[Request_name] [nvarchar](350) NULL,
	[Request_email] [nvarchar](350) NULL,
	[Request_phone] [nvarchar](350) NULL,
	[Request_amount] [nvarchar](350) NULL,
	[Request_redirect_url] [nvarchar](350) NULL,
	[Request_CompanyCode] [nvarchar](350) NULL,
	[Request_Purpose] [nvarchar](350) NULL,
	[Request_AgentID] [nvarchar](350) NULL,
	[Request_TransID] [nvarchar](350) NULL,
	[Response_DateTime] [datetime] NULL,
	[Response_Payment_Id] [nvarchar](350) NULL,
	[Response_Payment_Status] [nvarchar](350) NULL,
	[Response_Id] [nvarchar](350) NULL,
	[Response_Transaction_Id] [nvarchar](350) NULL,
	[Response_Action_Taken] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[Reference_Id] [nvarchar](250) NULL,
	[Reference_Type] [nvarchar](250) NULL,
	[Ref_Plan_Code] [nvarchar](250) NULL,
	[TransIpAddress] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_MakePayemnts_Details]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_MakePayemnts_Details](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[RefrenceID] [nvarchar](250) NULL,
	[PaymentMode] [nvarchar](250) NULL,
	[PaymentDate] [date] NULL,
	[DepositBankName] [nvarchar](250) NULL,
	[BranchCode_ChecqueNo] [nvarchar](250) NULL,
	[Remarks] [nvarchar](250) NULL,
	[TransactionID] [nvarchar](250) NULL,
	[DocumentPath] [nvarchar](250) NULL,
	[RecordDateTime] [datetime] NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[ApprovedBy] [nvarchar](max) NULL,
	[ApprovedDateTime] [datetime] NULL,
	[ApporvedStatus] [nvarchar](250) NULL,
	[RegistrationId] [nvarchar](250) NULL,
	[ApporveRemakrs] [nvarchar](max) NULL,
	[Amount] [numeric](18, 2) NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_MakePayemnts_Details_SA]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_MakePayemnts_Details_SA](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[RefrenceID] [nvarchar](250) NULL,
	[PaymentMode] [nvarchar](250) NULL,
	[PaymentDate] [date] NULL,
	[DepositBankName] [nvarchar](250) NULL,
	[BranchCode_ChecqueNo] [nvarchar](250) NULL,
	[Remarks] [nvarchar](250) NULL,
	[TransactionID] [nvarchar](250) NULL,
	[DocumentPath] [nvarchar](250) NULL,
	[ApprovedBy] [nvarchar](250) NULL,
	[ApprovedDateTime] [datetime] NULL,
	[ApporvedStatus] [nvarchar](250) NULL,
	[RegistrationId] [nvarchar](250) NULL,
	[ApporveRemakrs] [nvarchar](500) NULL,
	[Amount] [numeric](18, 2) NULL,
	[CompanyCode] [nvarchar](250) NULL,
	[RecordDateTime] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_MoneyTransfer_API]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_MoneyTransfer_API](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[TransferDate] [date] NULL,
	[OrderNo] [nvarchar](250) NULL,
	[RefrenceNo] [nvarchar](250) NULL,
	[TranscationId] [nvarchar](250) NULL,
	[CustomerID] [nvarchar](250) NULL,
	[MobileNo] [nvarchar](250) NULL,
	[Amount] [numeric](18, 0) NULL,
	[BankName] [nvarchar](250) NULL,
	[Method] [nvarchar](250) NULL,
	[Process] [nvarchar](250) NULL,
	[RecordDateTime] [datetime] NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[APIStatus] [nvarchar](250) NULL,
	[APIMessage] [nvarchar](max) NULL,
	[TransId] [nvarchar](250) NULL,
	[IFSC] [nvarchar](250) NULL,
	[AccountNo] [nvarchar](250) NULL,
	[Receipent] [nvarchar](250) NULL,
	[ReceipentId] [nvarchar](250) NULL,
	[CompanyCode] [nvarchar](250) NULL,
	[TransIpAddress] [nvarchar](250) NULL,
	[Refund_Status] [nvarchar](250) NULL,
	[Refund_TransID] [nvarchar](250) NULL,
	[Refund_Req_Status] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Bos_Notification_Master]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bos_Notification_Master](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[NotificationID] [nvarchar](250) NULL,
	[NotificationDate] [date] NULL,
	[AgentType] [nvarchar](250) NULL,
	[ActiveStatus] [nvarchar](250) NULL,
	[NotificationPic] [nvarchar](350) NULL,
	[RecordDatetime] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Bos_Notification_Master_SA]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bos_Notification_Master_SA](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[NotificationID] [nvarchar](250) NULL,
	[NotificationDate] [date] NULL,
	[AgentType] [nvarchar](250) NULL,
	[ActiveStatus] [nvarchar](250) NULL,
	[NotificationPic] [nvarchar](350) NULL,
	[RecordDatetime] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_OperatorWiseCommission]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_OperatorWiseCommission](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[APIName] [nvarchar](250) NULL,
	[Category] [nvarchar](250) NULL,
	[Code] [nvarchar](250) NULL,
	[OperatorName] [nvarchar](250) NULL,
	[Dis_CommissionType] [nvarchar](250) NULL,
	[Dis_Commission] [numeric](18, 2) NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL,
	[ActiveStatus] [nvarchar](250) NULL,
	[Sub_Dis_CommissionType] [nvarchar](250) NULL,
	[Sub_Dis_Commission] [numeric](18, 2) NULL,
	[Retailer_CommissionType] [nvarchar](250) NULL,
	[Retailer_Commission] [numeric](18, 2) NULL,
	[CompanyCode] [nvarchar](250) NULL,
	[Customer_CommissionType] [nvarchar](250) NULL,
	[Customer_Commission] [numeric](18, 2) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_OperatorWiseCommission_Agents]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_OperatorWiseCommission_Agents](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[AgentType] [nvarchar](250) NULL,
	[RegistrationID] [nvarchar](250) NULL,
	[APIName] [nvarchar](250) NULL,
	[Category] [nvarchar](250) NULL,
	[Code] [nvarchar](250) NULL,
	[OperatorName] [nvarchar](250) NULL,
	[CommissionType] [nvarchar](250) NULL,
	[Commission] [numeric](18, 2) NULL,
	[RecordDatetime] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_OperatorWiseCommission_SA]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_OperatorWiseCommission_SA](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[APIName] [nvarchar](250) NULL,
	[Category] [nvarchar](250) NULL,
	[Code] [nvarchar](250) NULL,
	[OperatorName] [nvarchar](250) NULL,
	[SA_CommissionType] [nvarchar](250) NULL,
	[SA_Commission] [numeric](18, 2) NULL,
	[ActiveStatus] [nvarchar](250) NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_OperatorWiseCommissionVsAdmin_SA]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_OperatorWiseCommissionVsAdmin_SA](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[AdminID] [nvarchar](250) NULL,
	[APIName] [nvarchar](250) NULL,
	[Category] [nvarchar](250) NULL,
	[Code] [nvarchar](250) NULL,
	[OperatorName] [nvarchar](250) NULL,
	[Admin_CommissionType] [nvarchar](250) NULL,
	[Admin_Commission] [numeric](18, 2) NULL,
	[ActiveStatus] [nvarchar](250) NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_Pan_Card_API]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_Pan_Card_API](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[CoupanType] [nvarchar](250) NULL,
	[Amount] [numeric](18, 0) NULL,
	[TotalCoupan] [numeric](18, 0) NULL,
	[Remarks] [nvarchar](max) NULL,
	[RecordDateTime] [datetime] NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[TotalAmount] [numeric](18, 0) NULL,
	[API_Status] [nvarchar](250) NULL,
	[API_Message] [nvarchar](500) NULL,
	[TransId] [nvarchar](250) NULL,
	[AgentID] [nvarchar](250) NULL,
	[API_TransId] [nvarchar](250) NULL,
	[CompanyCode] [nvarchar](250) NULL,
	[TransIpAddress] [nvarchar](250) NULL,
	[Refund_Status] [nvarchar](250) NULL,
	[Refund_TransID] [nvarchar](250) NULL,
	[Refund_Req_Status] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_ProductServiceMaster]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_ProductServiceMaster](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Commission] [numeric](18, 2) NULL,
	[ActiveStatus] [nvarchar](250) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[ProductType] [nvarchar](250) NULL,
	[ContainCategory] [nvarchar](250) NULL,
	[CanChange] [nvarchar](250) NULL,
	[CommissionType] [nvarchar](250) NULL,
	[ServiceType] [nvarchar](250) NULL,
	[ServiceCharge] [numeric](18, 2) NULL,
	[Sub_Dis_CommissionType] [nvarchar](250) NULL,
	[Sub_Dis_Commission] [numeric](18, 2) NULL,
	[Retailer_CommissionType] [nvarchar](250) NULL,
	[Retailer_Commission] [numeric](18, 2) NULL,
	[SlabApplicable] [nvarchar](250) NULL,
	[CompanyCode] [nvarchar](250) NULL,
	[Customer_CommissionType] [nvarchar](250) NULL,
	[Customer_Commission] [numeric](18, 2) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_ProductServiceMaster_SA]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_ProductServiceMaster_SA](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[ProductType] [nvarchar](250) NULL,
	[ContainCategory] [nvarchar](250) NULL,
	[SlabApplicable] [nvarchar](250) NULL,
	[SA_CommissionType] [nvarchar](250) NULL,
	[SA_Commission] [numeric](18, 2) NULL,
	[ServiceType] [nvarchar](250) NULL,
	[ServiceCharge] [numeric](18, 2) NULL,
	[ActiveStatus] [nvarchar](250) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_ProductServiceVsAdmin_SA]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_ProductServiceVsAdmin_SA](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[AdminID] [nvarchar](250) NULL,
	[Title] [nvarchar](max) NULL,
	[ProductType] [nvarchar](250) NULL,
	[ContainCategory] [nvarchar](250) NULL,
	[SlabApplicable] [nvarchar](250) NULL,
	[Admin_CommissionType] [nvarchar](250) NULL,
	[Admin_Commission] [numeric](18, 2) NULL,
	[ServiceType] [nvarchar](250) NULL,
	[ServiceCharge] [numeric](18, 2) NULL,
	[ActiveStatus] [nvarchar](250) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_Recharge_API]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_Recharge_API](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Recharge_ServiceType] [nvarchar](250) NULL,
	[Recharge_Operator] [nvarchar](250) NULL,
	[Recharge_MobileNo_CaNo] [nvarchar](250) NULL,
	[Recharge_Amount] [nvarchar](250) NULL,
	[Recharge_PayableAmount] [nvarchar](250) NULL,
	[Recharge_Date] [datetime] NULL,
	[API_orderId] [nvarchar](250) NULL,
	[API_status] [nvarchar](250) NULL,
	[API_TransId] [nvarchar](250) NULL,
	[API_urid] [nvarchar](250) NULL,
	[API_mobile] [nvarchar](250) NULL,
	[API_amount] [nvarchar](250) NULL,
	[API_operatorId] [nvarchar](250) NULL,
	[API_error_code] [nvarchar](250) NULL,
	[API_service] [nvarchar](250) NULL,
	[API_bal] [nvarchar](250) NULL,
	[API_commissionBal] [nvarchar](250) NULL,
	[API_resText] [nvarchar](250) NULL,
	[API_billAmount] [nvarchar](250) NULL,
	[API_billName] [nvarchar](250) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[RetailerID] [nvarchar](250) NULL,
	[TransId] [nvarchar](250) NULL,
	[Refund_Status] [nvarchar](250) NULL,
	[Refund_TransID] [nvarchar](250) NULL,
	[Gateway] [nvarchar](250) NULL,
	[CompanyCode] [nvarchar](250) NULL,
	[TransIpAddress] [nvarchar](250) NULL,
	[Refund_Req_Status] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_Ref_Code_Master]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_Ref_Code_Master](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Ref_Code] [nvarchar](250) NULL,
	[AmtToDebit] [numeric](18, 2) NULL,
	[Dis_CommissionType] [nvarchar](250) NULL,
	[Dis_Commission] [numeric](18, 2) NULL,
	[Sub_Dis_CommissionType] [nvarchar](250) NULL,
	[Sub_Dis_Commission] [numeric](18, 2) NULL,
	[Retailer_CommissionType] [nvarchar](250) NULL,
	[Retailer_Commission] [numeric](18, 2) NULL,
	[Customer_CommissionType] [nvarchar](250) NULL,
	[Customer_Commission] [numeric](18, 2) NULL,
	[CompanyCode] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[instamojo_Pay_Link] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_Refund_Request_Master]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_Refund_Request_Master](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[RequestDate] [date] NULL,
	[RequestID] [nvarchar](250) NULL,
	[kCode] [nvarchar](250) NULL,
	[kCodeType] [nvarchar](250) NULL,
	[TransType] [nvarchar](250) NULL,
	[TransID] [nvarchar](250) NULL,
	[Amount] [numeric](18, 2) NULL,
	[Remarks] [nvarchar](250) NULL,
	[ApprovedBy] [nvarchar](250) NULL,
	[ApprovedDateTime] [datetime] NULL,
	[ApporvedStatus] [nvarchar](250) NULL,
	[ApporveRemarks] [nvarchar](500) NULL,
	[Refund_TransID] [nvarchar](250) NULL,
	[CompanyCode] [nvarchar](250) NULL,
	[RecordDateTime] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_TransferAmountToAgents]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_TransferAmountToAgents](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[TransactionDate] [date] NULL,
	[TransferFrom] [nvarchar](250) NOT NULL,
	[TransferTo] [nvarchar](250) NOT NULL,
	[TransferAmt] [numeric](18, 2) NULL,
	[RecordDateTime] [datetime] NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[Remark] [nvarchar](max) NULL,
	[TransferFromMsg] [nvarchar](250) NULL,
	[Amount_Type] [nvarchar](250) NULL,
	[TransferToMsg] [nvarchar](250) NULL,
	[Amt_Transfer_TransID] [nvarchar](250) NULL,
	[Actual_Commission_Amt] [numeric](18, 2) NULL,
	[GSTAmt] [numeric](18, 2) NULL,
	[Commission_Without_GST] [numeric](18, 2) NULL,
	[TDS_Amt] [numeric](18, 2) NULL,
	[Net_Commission_Amt] [numeric](18, 2) NULL,
	[Ref_TransID] [nvarchar](250) NULL,
	[Actual_Transaction_Amount] [numeric](18, 2) NULL,
	[API_TransId] [nvarchar](250) NULL,
	[CompanyCode] [nvarchar](250) NULL,
	[TransIpAddress] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOS_TransferAmountToAgents_New]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOS_TransferAmountToAgents_New](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[TransactionDate] [date] NULL,
	[TransferFrom] [nvarchar](250) NOT NULL,
	[TransferTo] [nvarchar](250) NOT NULL,
	[TransferAmt] [numeric](18, 2) NULL,
	[RecordDateTime] [datetime] NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[Remark] [nvarchar](max) NULL,
	[TransferFromMsg] [nvarchar](250) NULL,
	[Amount_Type] [nvarchar](250) NULL,
	[TransferToMsg] [nvarchar](250) NULL,
	[Amt_Transfer_TransID] [nvarchar](250) NULL,
	[Actual_Commission_Amt] [numeric](18, 2) NULL,
	[GSTAmt] [numeric](18, 2) NULL,
	[Commission_Without_GST] [numeric](18, 2) NULL,
	[TDS_Amt] [numeric](18, 2) NULL,
	[Net_Commission_Amt] [numeric](18, 2) NULL,
	[Ref_TransID] [nvarchar](250) NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_AreaMaster]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_AreaMaster](
	[Area_Name] [nvarchar](255) NULL,
	[Pincode] [nvarchar](255) NULL,
	[Country_Name] [nvarchar](255) NULL,
	[District_Name] [nvarchar](255) NULL,
	[State_Name] [nvarchar](255) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_BillingCycleMaster]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_BillingCycleMaster](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Billig_Cycle] [nvarchar](250) NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [date] NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_BranchMaster]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_BranchMaster](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](250) NOT NULL,
	[BranchName] [nvarchar](max) NULL,
	[Company] [nvarchar](max) NULL,
	[BranchHead] [nvarchar](max) NULL,
	[ContactPerson] [nvarchar](max) NULL,
	[Address_1] [nvarchar](max) NULL,
	[Address_2] [nvarchar](max) NULL,
	[Address_3] [nvarchar](max) NULL,
	[Country] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[District] [nvarchar](max) NULL,
	[Area] [nvarchar](max) NULL,
	[PinCode] [nvarchar](max) NULL,
	[PhoneNo_1] [nvarchar](max) NULL,
	[Phoneno_2] [nvarchar](max) NULL,
	[MobileNo] [nvarchar](max) NULL,
	[EmailID] [nvarchar](max) NULL,
	[FaxNo] [nvarchar](max) NULL,
	[UpdatedOn] [date] NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[Recordstatus] [nvarchar](250) NOT NULL,
	[CinNo] [nvarchar](250) NULL,
	[SubCompanyName] [nvarchar](max) NULL,
	[ReasonForDeletion] [nvarchar](max) NULL,
	[DeletedBy] [nvarchar](max) NULL,
	[DeletedOn] [datetime] NULL,
	[Login_IPAddress] [nvarchar](250) NULL,
	[Branch_Opening_Date] [date] NULL,
	[BranchType] [nvarchar](250) NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_CompanyTypeMaster]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_CompanyTypeMaster](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Company_Type] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_CountryMaster]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_CountryMaster](
	[Country_Name] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_DistrictMaster]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_DistrictMaster](
	[Country_Name] [nvarchar](max) NULL,
	[State_Name] [nvarchar](max) NULL,
	[District_Name] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_EmployeeSkillMaster]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_EmployeeSkillMaster](
	[Employee_Skill] [nvarchar](250) NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL,
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_EmployeeTypeMaster]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_EmployeeTypeMaster](
	[Employee_Type] [nvarchar](250) NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [datetime] NULL,
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_Group_Master]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_Group_Master](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Group_Name] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_Login_Details]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_Login_Details](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[User_Name] [nvarchar](300) NULL,
	[User_ID] [nvarchar](300) NULL,
	[User_Password] [nvarchar](300) NULL,
	[User_Type] [nvarchar](300) NULL,
	[AccountStatus] [nvarchar](300) NULL,
	[User_CreationDate] [datetime] NULL,
	[LoggedinStatus] [nvarchar](300) NULL,
	[RecordStatus] [nvarchar](300) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [nvarchar](300) NULL,
	[Fromtime] [nvarchar](max) NULL,
	[Totime] [nvarchar](max) NULL,
	[ImagePath] [nvarchar](max) NULL,
	[TotalTime] [numeric](18, 0) NULL,
	[LastLoginTime] [datetime] NULL,
	[EmailId] [nvarchar](max) NULL,
	[MobileNo] [nvarchar](max) NULL,
	[ChangeTheme] [nvarchar](max) NULL,
	[Profile] [nvarchar](max) NULL,
	[Designation] [nvarchar](max) NULL,
	[TargetAmuont] [numeric](18, 0) NULL,
	[MDPassword] [nvarchar](max) NULL,
	[EmpSkill] [nvarchar](250) NULL,
	[EmpType] [nvarchar](250) NULL,
	[Canlogin] [nvarchar](250) NULL,
	[CreditBalnceLimit] [numeric](18, 0) NULL,
	[TransactionPin] [nvarchar](250) NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_LoginDurationMaster]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_LoginDurationMaster](
	[User_Id] [nvarchar](500) NOT NULL,
	[LoginFromTime] [nvarchar](500) NULL,
	[LoginToTime] [nvarchar](500) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [nvarchar](500) NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_MailTemplateMaster]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_MailTemplateMaster](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Message] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_MainModule]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_MainModule](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ModuleName] [nvarchar](300) NOT NULL,
	[OrderNO] [numeric](18, 0) NULL,
	[UpdatedOn] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](300) NOT NULL,
	[Choice] [nvarchar](max) NULL,
	[url] [nvarchar](max) NULL,
	[CreateLink] [nvarchar](250) NULL,
	[FormName] [nvarchar](250) NULL,
	[Searching_Keyword] [nvarchar](250) NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_Module_Master]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_Module_Master](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[FrmSelected] [bit] NOT NULL,
	[FormName] [nvarchar](300) NOT NULL,
	[CanSave] [bit] NOT NULL,
	[CanSearch] [bit] NOT NULL,
	[CanUpdate] [bit] NOT NULL,
	[CanDelete] [bit] NOT NULL,
	[RefModule] [nvarchar](500) NULL,
	[RefSubModule] [nvarchar](500) NULL,
	[NavigationModule] [nvarchar](500) NULL,
	[UpdatedOn] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](300) NOT NULL,
	[OrderNo] [numeric](18, 0) NULL,
	[NavigationModule_Order] [numeric](18, 0) NULL,
	[RefSubModule_Order] [numeric](18, 0) NULL,
	[Choice] [nvarchar](max) NULL,
	[url] [nvarchar](max) NULL,
	[Searching_Keyword] [nvarchar](250) NULL,
	[CreateLink] [nvarchar](250) NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_ModuleMaster]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_ModuleMaster](
	[RID] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[FrmSelected] [bit] NOT NULL,
	[FormName] [nvarchar](300) NOT NULL,
	[RefModule] [nvarchar](500) NULL,
	[MenuText] [nvarchar](500) NULL,
	[UpdatedOn] [smalldatetime] NOT NULL,
	[UpdatedBy] [nvarchar](300) NOT NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_OpertaorLoginReport]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_OpertaorLoginReport](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[LoginId] [nvarchar](max) NULL,
	[SessionId] [nvarchar](max) NULL,
	[LoginTime] [datetime] NULL,
	[LogoutTime] [datetime] NULL,
	[OperatorName] [nvarchar](max) NULL,
	[TotalDuration] [nvarchar](max) NULL,
	[User_Type] [nvarchar](max) NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_Plan_Master]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_Plan_Master](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[PlanCode] [nvarchar](250) NULL,
	[PlanName] [nvarchar](250) NULL,
	[BillingCycle] [nvarchar](250) NULL,
	[GracePeriodInDays] [nvarchar](250) NULL,
	[UpdatedBy] [nvarchar](250) NULL,
	[UpdatedOn] [date] NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_SentSMSDetails]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_SentSMSDetails](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[SentDate] [datetime] NULL,
	[MobileNo] [nvarchar](max) NULL,
	[MessageDetails] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_SMSCategoryMaster]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_SMSCategoryMaster](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Category] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_SMSTemplateMaster]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_SMSTemplateMaster](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Category] [nvarchar](max) NULL,
	[Title] [nvarchar](max) NULL,
	[Message] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_StateMaster]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_StateMaster](
	[Country_Name] [nvarchar](max) NULL,
	[State_Name] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[StateCode] [nvarchar](250) NULL,
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_SubCategoryMaster]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_SubCategoryMaster](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ItemType_ID] [nvarchar](250) NULL,
	[ItemType_Group_ID] [nvarchar](250) NULL,
	[ItemType_SubGroup_ID] [nvarchar](250) NULL,
	[Item_Type] [nvarchar](250) NULL,
	[Group_Name] [nvarchar](250) NULL,
	[Sub_GroupName] [nvarchar](250) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_SubModule]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_SubModule](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ModuleName] [nvarchar](300) NOT NULL,
	[UpdatedOn] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](300) NOT NULL,
	[OrderNO] [numeric](18, 0) NULL,
	[RefSubModule] [nvarchar](max) NULL,
	[RefSubModule_Order] [numeric](18, 0) NULL,
	[url] [nvarchar](max) NULL,
	[Choice] [nvarchar](max) NULL,
	[CreateLink] [nvarchar](250) NULL,
	[FormName] [nvarchar](250) NULL,
	[Searching_Keyword] [nvarchar](250) NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CRM_UserRightsMaster]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CRM_UserRightsMaster](
	[User_Group] [nvarchar](300) NOT NULL,
	[FrmSelected] [bit] NOT NULL,
	[FormName] [nvarchar](300) NOT NULL,
	[CanSave] [bit] NOT NULL,
	[CanSearch] [bit] NOT NULL,
	[CanUpdate] [bit] NOT NULL,
	[CanDelete] [bit] NOT NULL,
	[RefModule] [nvarchar](500) NULL,
	[RefSubModule] [nvarchar](500) NULL,
	[NavigationModule] [nvarchar](500) NULL,
	[UpdatedOn] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](300) NOT NULL,
	[OrderNo] [numeric](18, 0) NULL,
	[NavigationModule_Order] [numeric](18, 0) NULL,
	[RefSubModule_Order] [numeric](18, 0) NULL,
	[Choice] [nvarchar](max) NULL,
	[url] [nvarchar](max) NULL,
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[CreateLinkModule] [nvarchar](250) NULL,
	[CreateLinkSubModule] [nvarchar](250) NULL,
	[CreateLink] [nvarchar](250) NULL,
	[Searching_Keyword] [nvarchar](250) NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MoneyTransferBankList]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MoneyTransferBankList](
	[Name] [nvarchar](255) NULL,
	[Code] [nvarchar](255) NULL,
	[BankID] [float] NULL,
	[NEFT] [float] NULL,
	[IMPS] [float] NULL,
	[Verification] [float] NULL,
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Recharge_API_DB_Info]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recharge_API_DB_Info](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[RecordDatetime] [datetime] NULL,
	[API_TransId] [nvarchar](250) NULL,
	[Recharge_TransId] [nvarchar](250) NULL,
	[API_status] [nvarchar](250) NULL,
	[API_resText] [nvarchar](500) NULL,
	[CompanyCode] [nvarchar](250) NULL,
	[DBName] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RechargeRefundCallback]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RechargeRefundCallback](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[API_OrderId] [nvarchar](250) NULL,
	[API_status] [nvarchar](300) NULL,
	[API_Msg] [nvarchar](max) NULL,
	[RecordDatetime] [datetime] NULL,
	[Refund_Status] [nvarchar](250) NULL,
	[Refund_TransID] [nvarchar](250) NULL,
	[CompanyCode] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TempDeletedTrans]    Script Date: 08/04/2020 5:02:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempDeletedTrans](
	[RID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[TransactionDate] [date] NULL,
	[TransferFrom] [nvarchar](250) NOT NULL,
	[TransferTo] [nvarchar](250) NOT NULL,
	[TransferAmt] [numeric](18, 2) NULL,
	[RecordDateTime] [datetime] NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[Remark] [nvarchar](max) NULL,
	[TransferFromMsg] [nvarchar](250) NULL,
	[Amount_Type] [nvarchar](250) NULL,
	[TransferToMsg] [nvarchar](250) NULL,
	[Amt_Transfer_TransID] [nvarchar](250) NULL,
	[Actual_Commission_Amt] [numeric](18, 2) NULL,
	[GSTAmt] [numeric](18, 2) NULL,
	[Commission_Without_GST] [numeric](18, 2) NULL,
	[TDS_Amt] [numeric](18, 2) NULL,
	[Net_Commission_Amt] [numeric](18, 2) NULL,
	[Ref_TransID] [nvarchar](250) NULL,
	[Actual_Transaction_Amount] [numeric](18, 2) NULL,
	[API_TransId] [nvarchar](250) NULL,
	[CompanyCode] [nvarchar](250) NULL,
	[TransIpAddress] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
USE [master]
GO
ALTER DATABASE [BosCenter_DB] SET  READ_WRITE 
GO
