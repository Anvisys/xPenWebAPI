
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/18/2024 00:40:19
-- Generated from EDMX file: C:\aaWork\Products\XPenWebApi\xPenWebAPi\GAS\GASModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [XPen];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Accounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts];
GO
IF OBJECT_ID(N'[dbo].[Activity]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Activity];
GO
IF OBJECT_ID(N'[dbo].[AdvanceItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AdvanceItem];
GO
IF OBJECT_ID(N'[dbo].[CatchUpUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CatchUpUser];
GO
IF OBJECT_ID(N'[dbo].[ExpenseItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExpenseItem];
GO
IF OBJECT_ID(N'[dbo].[Expenses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Expenses];
GO
IF OBJECT_ID(N'[dbo].[GST]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GST];
GO
IF OBJECT_ID(N'[dbo].[Organization]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Organization];
GO
IF OBJECT_ID(N'[dbo].[Project]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Project];
GO
IF OBJECT_ID(N'[dbo].[ProjectStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectStatus];
GO
IF OBJECT_ID(N'[dbo].[PurchaseInvoice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PurchaseInvoice];
GO
IF OBJECT_ID(N'[dbo].[ReportedIssue]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReportedIssue];
GO
IF OBJECT_ID(N'[dbo].[SellInvoice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SellInvoice];
GO
IF OBJECT_ID(N'[dbo].[TDS]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TDS];
GO
IF OBJECT_ID(N'[dbo].[Transaction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Transaction];
GO
IF OBJECT_ID(N'[dbo].[UserImage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserImage];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[NewViewActivity]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[NewViewActivity];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewAccount]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewAccount];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewActivity]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewActivity];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewActivityNames]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewActivityNames];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewActivityStatus]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewActivityStatus];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewActivitySummary]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewActivitySummary];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewAdvance]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewAdvance];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewAdvanceItemNames]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewAdvanceItemNames];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewAdvanceStatus]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewAdvanceStatus];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewExpenseItemDailyStatus]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewExpenseItemDailyStatus];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewExpenseItemStatusActivity]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewExpenseItemStatusActivity];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewGST]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewGST];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewMonthCumulativeExpense]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewMonthCumulativeExpense];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewOrganization]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewOrganization];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewPaymentGiven]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewPaymentGiven];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewPaymentReceived]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewPaymentReceived];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewProject]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewProject];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewProjectStatus]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewProjectStatus];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewPurchaseInvoice]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewPurchaseInvoice];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewSellInvoice]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewSellInvoice];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewTDS]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewTDS];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewTransaction]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewTransaction];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewTransactionDailyAccount]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewTransactionDailyAccount];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewTransactionDailyAccountBalance]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewTransactionDailyAccountBalance];
GO
IF OBJECT_ID(N'[GASModelStoreContainer].[ViewTransactionDailySummary]', 'U') IS NOT NULL
    DROP TABLE [GASModelStoreContainer].[ViewTransactionDailySummary];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [UserLogin] varchar(50)  NULL,
    [OrganizationID] int  NULL,
    [Password] varchar(50)  NULL,
    [Role] varchar(20)  NULL,
    [UserName] varchar(20)  NULL,
    [UserEmail] varchar(50)  NULL,
    [UserMobile] varchar(20)  NULL,
    [Status] varchar(20)  NULL,
    [RegisterDate] datetime  NOT NULL,
    [OrgName] varchar(50)  NULL,
    [SolutionType] varchar(20)  NULL
);
GO

-- Creating table 'ViewOrganizations'
CREATE TABLE [dbo].[ViewOrganizations] (
    [OrganizationID] int  NOT NULL,
    [OrganizationName] varchar(50)  NULL,
    [Employee] int  NULL,
    [Address] varchar(100)  NULL,
    [ContactEmail] varchar(50)  NULL,
    [ContactNumber] varchar(20)  NULL,
    [ContactPerson] varchar(20)  NULL,
    [UserID] int  NULL,
    [AdminName] varchar(20)  NULL,
    [UserEmail] varchar(50)  NULL,
    [UserMobile] varchar(20)  NULL
);
GO

-- Creating table 'Transactions'
CREATE TABLE [dbo].[Transactions] (
    [TransID] int IDENTITY(1,1) NOT NULL,
    [TransName] varchar(20)  NOT NULL,
    [AccID] int  NOT NULL,
    [Deposit] int  NOT NULL,
    [Withdraw] int  NOT NULL,
    [EntryDate] datetime  NOT NULL,
    [Balance] int  NULL,
    [AccountBalance] int  NULL,
    [ProjectID] int  NULL,
    [ActivityID] int  NOT NULL,
    [TransactionID] int  NOT NULL,
    [TransactionRemarks] varchar(50)  NULL,
    [OrgID] int  NOT NULL,
    [TransType] varchar(20)  NULL,
    [InvoiceID] int  NOT NULL,
    [TransactionDate] datetime  NOT NULL
);
GO

-- Creating table 'GSTs'
CREATE TABLE [dbo].[GSTs] (
    [GSTId] int IDENTITY(1,1) NOT NULL,
    [OrgID] int  NOT NULL,
    [GSTReceived] int  NOT NULL,
    [GSTInput] int  NOT NULL,
    [PreviousGSTDues] int  NOT NULL,
    [Penalty] int  NOT NULL,
    [GSTPayable] int  NOT NULL,
    [TaxMonth] datetime  NOT NULL
);
GO

-- Creating table 'ViewGSTs'
CREATE TABLE [dbo].[ViewGSTs] (
    [GSTId] int  NOT NULL,
    [OrgID] int  NOT NULL,
    [GSTReceived] int  NOT NULL,
    [GSTInput] int  NOT NULL,
    [PreviousGSTDues] int  NOT NULL,
    [Penalty] int  NOT NULL,
    [GSTPayable] int  NOT NULL,
    [TaxMonth] datetime  NOT NULL,
    [GST_Paid] int  NOT NULL,
    [UpdateDate] datetime  NOT NULL,
    [TransactionRemarks] varchar(50)  NOT NULL,
    [TransactionDate] datetime  NOT NULL
);
GO

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [AccID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(20)  NOT NULL,
    [Type] varchar(20)  NOT NULL,
    [Number] varchar(20)  NOT NULL,
    [AccountDescription] varchar(50)  NOT NULL,
    [OrgID] int  NOT NULL
);
GO

-- Creating table 'Activities'
CREATE TABLE [dbo].[Activities] (
    [ActivityID] int IDENTITY(1,1) NOT NULL,
    [ActivityName] varchar(20)  NOT NULL,
    [EmployeeID] int  NOT NULL,
    [ProjectID] int  NOT NULL,
    [CreatedBy] int  NULL,
    [ActivityDescription] varchar(50)  NOT NULL,
    [CreationDate] datetime  NOT NULL,
    [SelectedRow] bit  NOT NULL,
    [OrgID] int  NOT NULL,
    [ApproverID] int  NULL
);
GO

-- Creating table 'AdvanceItems'
CREATE TABLE [dbo].[AdvanceItems] (
    [AdvanceID] int IDENTITY(1,1) NOT NULL,
    [ActivityID] int  NOT NULL,
    [AdvanceName] varchar(20)  NOT NULL,
    [RequestAmount] int  NOT NULL,
    [ReceiveAmount] int  NOT NULL,
    [AdvanceRemarks] varchar(50)  NOT NULL,
    [CreationDate] datetime  NOT NULL,
    [SelectedRow] bit  NOT NULL,
    [Status] varchar(20)  NOT NULL
);
GO

-- Creating table 'CatchUpUsers'
CREATE TABLE [dbo].[CatchUpUsers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Image] varbinary(max)  NOT NULL,
    [MobileNumber] varchar(15)  NOT NULL,
    [Email] varchar(30)  NULL,
    [Password] varchar(50)  NULL,
    [GCMCode] varchar(250)  NOT NULL,
    [Location] varchar(250)  NOT NULL,
    [Function] varchar(10)  NOT NULL,
    [Date] datetime  NOT NULL
);
GO

-- Creating table 'Expenses'
CREATE TABLE [dbo].[Expenses] (
    [ExpenseID] int IDENTITY(1,1) NOT NULL,
    [ExpenseType] varchar(20)  NOT NULL,
    [ExpenseAmount] int  NOT NULL,
    [ExpenseDescription] varchar(50)  NOT NULL,
    [ExpenseDate] datetime  NOT NULL,
    [SelectedRow] bit  NOT NULL,
    [Status] varchar(20)  NOT NULL,
    [UserID] int  NOT NULL
);
GO

-- Creating table 'Organizations'
CREATE TABLE [dbo].[Organizations] (
    [OrganizationID] int IDENTITY(1,1) NOT NULL,
    [OrganizationNumber] varchar(20)  NULL,
    [OrganizationName] varchar(50)  NULL,
    [Employee] int  NULL,
    [Address] varchar(100)  NULL,
    [ContactPerson] varchar(20)  NULL,
    [ContactNumber] varchar(20)  NULL,
    [ContactEmail] varchar(50)  NULL,
    [Status] varchar(20)  NULL,
    [StartDate] datetime  NOT NULL
);
GO

-- Creating table 'ProjectStatus'
CREATE TABLE [dbo].[ProjectStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [WorkCompletion] int  NULL,
    [Remarks] varchar(200)  NULL,
    [UpdateDate] datetime  NOT NULL,
    [ProjectID] int  NOT NULL,
    [Status] varchar(20)  NOT NULL
);
GO

-- Creating table 'PurchaseInvoices'
CREATE TABLE [dbo].[PurchaseInvoices] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [InvoiceNumber] nvarchar(50)  NOT NULL,
    [VendorName] nvarchar(50)  NOT NULL,
    [OrgId] int  NOT NULL,
    [ProjectId] int  NOT NULL,
    [ServiceCost] float  NOT NULL,
    [CGST] float  NOT NULL,
    [SGST] float  NOT NULL,
    [IGST] float  NOT NULL,
    [TDS] float  NOT NULL,
    [InvoiceDate] datetime  NOT NULL,
    [InvoiceType] tinyint  NOT NULL
);
GO

-- Creating table 'ReportedIssues'
CREATE TABLE [dbo].[ReportedIssues] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [DeviceID] varchar(50)  NOT NULL,
    [Lattitude] decimal(18,6)  NOT NULL,
    [Longitude] decimal(18,6)  NOT NULL,
    [Type] int  NOT NULL,
    [severity] int  NOT NULL,
    [Description] varchar(250)  NOT NULL,
    [Image] varbinary(max)  NOT NULL,
    [ModifiedAt] datetime  NOT NULL
);
GO

-- Creating table 'SellInvoices'
CREATE TABLE [dbo].[SellInvoices] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [InvoiceNumber] nvarchar(50)  NOT NULL,
    [OrgId] int  NOT NULL,
    [ProjectId] int  NOT NULL,
    [ServiceCost] float  NOT NULL,
    [CGST] float  NOT NULL,
    [SGST] float  NOT NULL,
    [IGST] float  NOT NULL,
    [TDS] float  NOT NULL,
    [InvoiceDate] datetime  NOT NULL,
    [InvoiceType] tinyint  NOT NULL
);
GO

-- Creating table 'TDS'
CREATE TABLE [dbo].[TDS] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [OrgID] int  NOT NULL,
    [TDSDeducted] int  NOT NULL,
    [TDSPayable] int  NOT NULL,
    [PreviousTDS] int  NOT NULL,
    [Penalty] float  NOT NULL,
    [TaxMonth] datetime  NOT NULL
);
GO

-- Creating table 'UserImages'
CREATE TABLE [dbo].[UserImages] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NOT NULL,
    [Profile_image] varbinary(max)  NULL
);
GO

-- Creating table 'ViewAccounts'
CREATE TABLE [dbo].[ViewAccounts] (
    [AccID] int  NOT NULL,
    [AccountNumber] varchar(20)  NOT NULL,
    [AccountName] varchar(20)  NOT NULL,
    [AccountType] varchar(20)  NOT NULL,
    [AccountDescription] varchar(50)  NOT NULL,
    [AccountBalance] int  NOT NULL,
    [BalanceOn] datetime  NOT NULL,
    [OrgID] int  NOT NULL
);
GO

-- Creating table 'ViewActivities'
CREATE TABLE [dbo].[ViewActivities] (
    [ActivityID] int  NOT NULL,
    [ActivityName] varchar(20)  NOT NULL,
    [ActivityDescription] varchar(50)  NOT NULL,
    [EmployeeID] int  NOT NULL,
    [Employee] varchar(20)  NULL,
    [ProjectID] int  NOT NULL,
    [ProjectName] varchar(50)  NOT NULL,
    [CreatedBy] int  NULL,
    [CreatedByName] varchar(20)  NULL,
    [Approver] int  NOT NULL,
    [ApproverName] varchar(20)  NULL,
    [ActivityStatus] varchar(20)  NULL,
    [Expenses] int  NULL,
    [Received] int  NULL,
    [Balance] int  NULL,
    [UpdatedOn] datetime  NULL,
    [Advance] int  NULL,
    [OrgID] int  NOT NULL
);
GO

-- Creating table 'ViewActivityNames'
CREATE TABLE [dbo].[ViewActivityNames] (
    [ActivityID] int  NOT NULL,
    [ActivityName] varchar(20)  NOT NULL,
    [ActivityDescription] varchar(50)  NOT NULL,
    [CreationDate] datetime  NOT NULL,
    [ProjectID] int  NOT NULL,
    [ProjectName] varchar(50)  NOT NULL,
    [Approver] int  NOT NULL,
    [ApproverName] varchar(20)  NULL,
    [EmployeeID] int  NOT NULL,
    [Employee] varchar(20)  NULL,
    [CreatedBy] int  NULL,
    [CreatedByName] varchar(20)  NULL,
    [OrgID] int  NOT NULL
);
GO

-- Creating table 'ViewActivityStatus'
CREATE TABLE [dbo].[ViewActivityStatus] (
    [ActivityID] int  NOT NULL,
    [ActivityStatus] varchar(20)  NULL,
    [UpdatedOn] datetime  NULL
);
GO

-- Creating table 'ViewActivitySummaries'
CREATE TABLE [dbo].[ViewActivitySummaries] (
    [RowID] bigint  NOT NULL,
    [OrgID] int  NOT NULL,
    [ActivityStatus] varchar(20)  NULL,
    [Expense] int  NULL,
    [Received] int  NULL
);
GO

-- Creating table 'ViewAdvances'
CREATE TABLE [dbo].[ViewAdvances] (
    [ActivityID] int  NOT NULL,
    [ActivityName] varchar(20)  NOT NULL,
    [ProjectID] int  NOT NULL,
    [ProjectName] varchar(50)  NOT NULL,
    [EmployeeID] int  NOT NULL,
    [Employee] varchar(20)  NULL,
    [CreatedBy] int  NULL,
    [CreatedByName] varchar(20)  NULL,
    [RequestAmount] int  NULL,
    [ReceivedAmount] int  NULL,
    [AdvanceName] varchar(20)  NULL,
    [AdvanceStatus] varchar(20)  NULL,
    [AdvanceModifiedDate] datetime  NULL,
    [Approver] int  NOT NULL,
    [ApproverName] varchar(20)  NULL,
    [OrgID] int  NOT NULL
);
GO

-- Creating table 'ViewAdvanceItemNames'
CREATE TABLE [dbo].[ViewAdvanceItemNames] (
    [AdvanceID] int  NOT NULL,
    [AdvanceName] varchar(20)  NOT NULL,
    [RequestAmount] int  NOT NULL,
    [ReceiveAmount] int  NOT NULL,
    [AdvanceRemarks] varchar(50)  NOT NULL,
    [CreationDate] datetime  NOT NULL,
    [ActivityID] int  NOT NULL,
    [Status] varchar(20)  NOT NULL,
    [ActivityName] varchar(20)  NOT NULL,
    [ProjectName] varchar(50)  NOT NULL,
    [Employee] varchar(20)  NULL,
    [CreatedByName] varchar(20)  NULL,
    [Approver] int  NOT NULL,
    [ApproverName] varchar(20)  NULL
);
GO

-- Creating table 'ViewAdvanceStatus'
CREATE TABLE [dbo].[ViewAdvanceStatus] (
    [ActivityNumber] int  NOT NULL,
    [AdvanceName] varchar(20)  NULL,
    [AdvanceStatus] varchar(20)  NULL,
    [AdvanceModifiedDate] datetime  NULL
);
GO

-- Creating table 'ViewMonthCumulativeExpenses'
CREATE TABLE [dbo].[ViewMonthCumulativeExpenses] (
    [UserID] int  NOT NULL,
    [year] int  NULL,
    [month] int  NULL,
    [expense] int  NULL
);
GO

-- Creating table 'ViewPaymentGivens'
CREATE TABLE [dbo].[ViewPaymentGivens] (
    [ProjectId] int  NULL,
    [InvoiceId] int  NOT NULL,
    [PaidAmount] int  NULL,
    [PaidDate] datetime  NULL
);
GO

-- Creating table 'ViewPaymentReceiveds'
CREATE TABLE [dbo].[ViewPaymentReceiveds] (
    [ProjectId] int  NULL,
    [InvoiceId] int  NOT NULL,
    [ReceivedAmount] int  NULL,
    [ReceivedDate] datetime  NULL
);
GO

-- Creating table 'ViewProjectStatus'
CREATE TABLE [dbo].[ViewProjectStatus] (
    [ProjectID] int  NOT NULL,
    [StatusOn] datetime  NOT NULL,
    [WorkCompletion] int  NULL,
    [StatusRemark] varchar(200)  NULL,
    [Status] varchar(20)  NOT NULL
);
GO

-- Creating table 'ViewTDS'
CREATE TABLE [dbo].[ViewTDS] (
    [ID] int  NOT NULL,
    [OrgID] int  NOT NULL,
    [TDSDeducted] int  NOT NULL,
    [TDSPayable] int  NOT NULL,
    [PreviousTDS] int  NOT NULL,
    [Penalty] float  NOT NULL,
    [TaxMonth] datetime  NOT NULL,
    [TDS_Paid] int  NULL,
    [EntryDate] datetime  NULL,
    [TransactionRemarks] varchar(50)  NULL,
    [TransactionDate] datetime  NULL
);
GO

-- Creating table 'ViewTransactions'
CREATE TABLE [dbo].[ViewTransactions] (
    [EntryDate] datetime  NOT NULL,
    [TransactionDate] datetime  NOT NULL,
    [TransID] int  NOT NULL,
    [TransName] varchar(20)  NOT NULL,
    [AccID] int  NOT NULL,
    [Deposit] int  NOT NULL,
    [Withdraw] int  NOT NULL,
    [Balance] int  NULL,
    [AccountBalance] int  NULL,
    [ProjectID] int  NULL,
    [ActivityID] int  NOT NULL,
    [TransactionID] int  NOT NULL,
    [TransactionRemarks] varchar(50)  NULL,
    [OrgID] int  NOT NULL,
    [TransType] varchar(20)  NULL,
    [AccountName] varchar(20)  NULL,
    [ActivityName] varchar(20)  NOT NULL,
    [EmployeeID] int  NOT NULL
);
GO

-- Creating table 'ViewTransactionDailyAccounts'
CREATE TABLE [dbo].[ViewTransactionDailyAccounts] (
    [OrgID] int  NOT NULL,
    [AccID] int  NOT NULL,
    [AgrregateDate] datetime  NULL,
    [Deposit] int  NULL,
    [Withdraw] int  NULL,
    [Balance] int  NULL,
    [AccountBalance] int  NULL,
    [tTime] time  NULL
);
GO

-- Creating table 'ViewTransactionDailyAccountBalances'
CREATE TABLE [dbo].[ViewTransactionDailyAccountBalances] (
    [AccID] int  NOT NULL,
    [EntryDate] datetime  NOT NULL,
    [Balance] int  NULL,
    [AccountBalance] int  NULL,
    [tDate] datetime  NULL,
    [tTime] time  NULL
);
GO

-- Creating table 'ViewTransactionDailySummaries'
CREATE TABLE [dbo].[ViewTransactionDailySummaries] (
    [OrgID] int  NOT NULL,
    [AgrregateDate] datetime  NULL,
    [Balance] int  NULL,
    [Deposit] int  NULL,
    [Withdraw] int  NULL
);
GO

-- Creating table 'Projects'
CREATE TABLE [dbo].[Projects] (
    [ProjectID] int IDENTITY(1,1) NOT NULL,
    [ProjectNumber] varchar(20)  NULL,
    [ClientName] varchar(50)  NOT NULL,
    [ProjectName] varchar(50)  NOT NULL,
    [ProjectValue] int  NULL,
    [ProjectDescription] varchar(50)  NULL,
    [ProjectCreationDate] datetime  NOT NULL,
    [CreatedBy] int  NOT NULL,
    [OrgID] int  NOT NULL
);
GO

-- Creating table 'ViewProjects'
CREATE TABLE [dbo].[ViewProjects] (
    [ProjectID] int  NOT NULL,
    [ProjectNumber] varchar(20)  NULL,
    [ClientName] varchar(50)  NOT NULL,
    [ProjectName] varchar(50)  NOT NULL,
    [ProjectValue] int  NULL,
    [ProjectCreationDate] datetime  NOT NULL,
    [ProjectDescription] varchar(50)  NULL,
    [CreatedBy] int  NOT NULL,
    [CreatedByName] varchar(20)  NULL,
    [StatusOn] datetime  NOT NULL,
    [WorkCompletion] int  NULL,
    [StatusRemark] varchar(200)  NULL,
    [Status] varchar(20)  NOT NULL,
    [Spent] int  NOT NULL,
    [Received] int  NOT NULL,
    [OrgID] int  NOT NULL
);
GO

-- Creating table 'ViewExpenseItemStatusActivities'
CREATE TABLE [dbo].[ViewExpenseItemStatusActivities] (
    [ActivityID] int  NOT NULL,
    [ItemName] varchar(50)  NOT NULL,
    [ExpenseAmount] int  NOT NULL,
    [ReceiveAmount] int  NOT NULL,
    [ExpenseDescription] varchar(50)  NOT NULL,
    [ExpenseDate] datetime  NOT NULL,
    [ItemAction] varchar(20)  NOT NULL,
    [ActivityStatus] varchar(20)  NULL,
    [UpdatedOn] datetime  NULL,
    [ActivityName] varchar(20)  NOT NULL,
    [EmployeeID] int  NOT NULL,
    [ProjectID] int  NOT NULL,
    [CreatedBy] int  NULL,
    [ApproverID] int  NULL,
    [OrgID] int  NOT NULL,
    [ProjectName] varchar(50)  NULL,
    [ProjectOwner] varchar(20)  NULL
);
GO

-- Creating table 'ViewPurchaseInvoices'
CREATE TABLE [dbo].[ViewPurchaseInvoices] (
    [OrgId] int  NOT NULL,
    [ProjectId] int  NOT NULL,
    [VendorName] nvarchar(50)  NOT NULL,
    [InvoiceId] int  NOT NULL,
    [InvoiceNumber] nvarchar(50)  NOT NULL,
    [InvoiceDate] datetime  NOT NULL,
    [ServiceCost] float  NOT NULL,
    [CGST] float  NOT NULL,
    [SGST] float  NOT NULL,
    [IGST] float  NOT NULL,
    [TDS] float  NOT NULL,
    [Payable] float  NOT NULL,
    [PaidAmount] int  NOT NULL,
    [PaidDate] datetime  NOT NULL,
    [ProjectName] varchar(50)  NOT NULL,
    [ProjectNumber] varchar(20)  NULL,
    [ClientName] varchar(50)  NOT NULL,
    [CreatedBy] int  NOT NULL
);
GO

-- Creating table 'ViewSellInvoices'
CREATE TABLE [dbo].[ViewSellInvoices] (
    [OrgId] int  NOT NULL,
    [ProjectId] int  NOT NULL,
    [InvoiceId] int  NOT NULL,
    [InvoiceNumber] nvarchar(50)  NOT NULL,
    [InvoiceDate] datetime  NOT NULL,
    [ServiceCost] float  NOT NULL,
    [CGST] float  NOT NULL,
    [SGST] float  NOT NULL,
    [IGST] float  NOT NULL,
    [TDS] float  NOT NULL,
    [Receivable] float  NOT NULL,
    [ReceivedAmount] int  NOT NULL,
    [ReceivedDate] datetime  NOT NULL,
    [ProjectName] varchar(50)  NOT NULL,
    [ProjectNumber] varchar(20)  NULL,
    [ClientName] varchar(50)  NOT NULL,
    [CreatedBy] int  NOT NULL
);
GO

-- Creating table 'ViewExpenseItemDailyStatus'
CREATE TABLE [dbo].[ViewExpenseItemDailyStatus] (
    [OrgID] int  NOT NULL,
    [ProjectID] int  NOT NULL,
    [ActivityID] int  NOT NULL,
    [EmployeeID] int  NOT NULL,
    [CreatedBy] int  NULL,
    [ApproverID] int  NULL,
    [ExpensesDate] datetime  NULL,
    [ActivityStatus] varchar(20)  NULL,
    [Expense] int  NULL,
    [Received] int  NULL
);
GO

-- Creating table 'NewViewActivities'
CREATE TABLE [dbo].[NewViewActivities] (
    [ActivityID] int  NOT NULL,
    [ActivityName] varchar(20)  NOT NULL,
    [ActivityDescription] varchar(50)  NOT NULL,
    [EmployeeID] int  NOT NULL,
    [Employee] varchar(20)  NULL,
    [ProjectID] int  NOT NULL,
    [ProjectName] varchar(50)  NOT NULL,
    [CreatedBy] int  NULL,
    [CreatedByName] varchar(20)  NULL,
    [Approver] int  NOT NULL,
    [ApproverName] varchar(20)  NULL,
    [Settlement] int  NOT NULL,
    [PaidDate] datetime  NOT NULL,
    [Expenses] int  NULL,
    [Received] int  NULL,
    [Balance] int  NULL,
    [ActivityStatus] varchar(20)  NULL,
    [Advance] int  NOT NULL,
    [OrgID] int  NOT NULL
);
GO

-- Creating table 'ExpenseItems'
CREATE TABLE [dbo].[ExpenseItems] (
    [ItemID] int IDENTITY(1,1) NOT NULL,
    [ActivityID] int  NOT NULL,
    [ItemName] varchar(50)  NOT NULL,
    [ExpenseAmount] int  NOT NULL,
    [ReceiveAmount] int  NOT NULL,
    [ExpenseDescription] varchar(50)  NOT NULL,
    [ExpenseDate] datetime  NOT NULL,
    [SelectedRow] bit  NOT NULL,
    [Action] varchar(20)  NOT NULL,
    [OrganizationId] int  NOT NULL,
    [AccountId] int  NOT NULL,
    [ProjectID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [UserID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [OrganizationID] in table 'ViewOrganizations'
ALTER TABLE [dbo].[ViewOrganizations]
ADD CONSTRAINT [PK_ViewOrganizations]
    PRIMARY KEY CLUSTERED ([OrganizationID] ASC);
GO

-- Creating primary key on [TransID] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [PK_Transactions]
    PRIMARY KEY CLUSTERED ([TransID] ASC);
GO

-- Creating primary key on [GSTId] in table 'GSTs'
ALTER TABLE [dbo].[GSTs]
ADD CONSTRAINT [PK_GSTs]
    PRIMARY KEY CLUSTERED ([GSTId] ASC);
GO

-- Creating primary key on [GSTId], [OrgID], [GSTReceived], [GSTInput], [PreviousGSTDues], [Penalty], [GSTPayable], [TaxMonth], [GST_Paid], [UpdateDate], [TransactionRemarks], [TransactionDate] in table 'ViewGSTs'
ALTER TABLE [dbo].[ViewGSTs]
ADD CONSTRAINT [PK_ViewGSTs]
    PRIMARY KEY CLUSTERED ([GSTId], [OrgID], [GSTReceived], [GSTInput], [PreviousGSTDues], [Penalty], [GSTPayable], [TaxMonth], [GST_Paid], [UpdateDate], [TransactionRemarks], [TransactionDate] ASC);
GO

-- Creating primary key on [AccID] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [PK_Accounts]
    PRIMARY KEY CLUSTERED ([AccID] ASC);
GO

-- Creating primary key on [ActivityID] in table 'Activities'
ALTER TABLE [dbo].[Activities]
ADD CONSTRAINT [PK_Activities]
    PRIMARY KEY CLUSTERED ([ActivityID] ASC);
GO

-- Creating primary key on [AdvanceID] in table 'AdvanceItems'
ALTER TABLE [dbo].[AdvanceItems]
ADD CONSTRAINT [PK_AdvanceItems]
    PRIMARY KEY CLUSTERED ([AdvanceID] ASC);
GO

-- Creating primary key on [ID] in table 'CatchUpUsers'
ALTER TABLE [dbo].[CatchUpUsers]
ADD CONSTRAINT [PK_CatchUpUsers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ExpenseID] in table 'Expenses'
ALTER TABLE [dbo].[Expenses]
ADD CONSTRAINT [PK_Expenses]
    PRIMARY KEY CLUSTERED ([ExpenseID] ASC);
GO

-- Creating primary key on [OrganizationID] in table 'Organizations'
ALTER TABLE [dbo].[Organizations]
ADD CONSTRAINT [PK_Organizations]
    PRIMARY KEY CLUSTERED ([OrganizationID] ASC);
GO

-- Creating primary key on [ID] in table 'ProjectStatus'
ALTER TABLE [dbo].[ProjectStatus]
ADD CONSTRAINT [PK_ProjectStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'PurchaseInvoices'
ALTER TABLE [dbo].[PurchaseInvoices]
ADD CONSTRAINT [PK_PurchaseInvoices]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ReportedIssues'
ALTER TABLE [dbo].[ReportedIssues]
ADD CONSTRAINT [PK_ReportedIssues]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SellInvoices'
ALTER TABLE [dbo].[SellInvoices]
ADD CONSTRAINT [PK_SellInvoices]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'TDS'
ALTER TABLE [dbo].[TDS]
ADD CONSTRAINT [PK_TDS]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'UserImages'
ALTER TABLE [dbo].[UserImages]
ADD CONSTRAINT [PK_UserImages]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [AccID], [AccountNumber], [AccountName], [AccountType], [AccountDescription], [AccountBalance], [BalanceOn], [OrgID] in table 'ViewAccounts'
ALTER TABLE [dbo].[ViewAccounts]
ADD CONSTRAINT [PK_ViewAccounts]
    PRIMARY KEY CLUSTERED ([AccID], [AccountNumber], [AccountName], [AccountType], [AccountDescription], [AccountBalance], [BalanceOn], [OrgID] ASC);
GO

-- Creating primary key on [ActivityID], [ActivityName], [ActivityDescription], [EmployeeID], [ProjectID], [ProjectName], [OrgID] in table 'ViewActivities'
ALTER TABLE [dbo].[ViewActivities]
ADD CONSTRAINT [PK_ViewActivities]
    PRIMARY KEY CLUSTERED ([ActivityID], [ActivityName], [ActivityDescription], [EmployeeID], [ProjectID], [ProjectName], [OrgID] ASC);
GO

-- Creating primary key on [ActivityID], [ActivityName], [ActivityDescription], [CreationDate], [ProjectID], [ProjectName], [EmployeeID], [OrgID] in table 'ViewActivityNames'
ALTER TABLE [dbo].[ViewActivityNames]
ADD CONSTRAINT [PK_ViewActivityNames]
    PRIMARY KEY CLUSTERED ([ActivityID], [ActivityName], [ActivityDescription], [CreationDate], [ProjectID], [ProjectName], [EmployeeID], [OrgID] ASC);
GO

-- Creating primary key on [ActivityID] in table 'ViewActivityStatus'
ALTER TABLE [dbo].[ViewActivityStatus]
ADD CONSTRAINT [PK_ViewActivityStatus]
    PRIMARY KEY CLUSTERED ([ActivityID] ASC);
GO

-- Creating primary key on [RowID], [OrgID] in table 'ViewActivitySummaries'
ALTER TABLE [dbo].[ViewActivitySummaries]
ADD CONSTRAINT [PK_ViewActivitySummaries]
    PRIMARY KEY CLUSTERED ([RowID], [OrgID] ASC);
GO

-- Creating primary key on [ActivityID], [ActivityName], [ProjectID], [ProjectName], [EmployeeID], [OrgID] in table 'ViewAdvances'
ALTER TABLE [dbo].[ViewAdvances]
ADD CONSTRAINT [PK_ViewAdvances]
    PRIMARY KEY CLUSTERED ([ActivityID], [ActivityName], [ProjectID], [ProjectName], [EmployeeID], [OrgID] ASC);
GO

-- Creating primary key on [AdvanceID], [AdvanceName], [RequestAmount], [ReceiveAmount], [AdvanceRemarks], [CreationDate], [ActivityID], [Status], [ActivityName], [ProjectName] in table 'ViewAdvanceItemNames'
ALTER TABLE [dbo].[ViewAdvanceItemNames]
ADD CONSTRAINT [PK_ViewAdvanceItemNames]
    PRIMARY KEY CLUSTERED ([AdvanceID], [AdvanceName], [RequestAmount], [ReceiveAmount], [AdvanceRemarks], [CreationDate], [ActivityID], [Status], [ActivityName], [ProjectName] ASC);
GO

-- Creating primary key on [ActivityNumber] in table 'ViewAdvanceStatus'
ALTER TABLE [dbo].[ViewAdvanceStatus]
ADD CONSTRAINT [PK_ViewAdvanceStatus]
    PRIMARY KEY CLUSTERED ([ActivityNumber] ASC);
GO

-- Creating primary key on [UserID] in table 'ViewMonthCumulativeExpenses'
ALTER TABLE [dbo].[ViewMonthCumulativeExpenses]
ADD CONSTRAINT [PK_ViewMonthCumulativeExpenses]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [InvoiceId] in table 'ViewPaymentGivens'
ALTER TABLE [dbo].[ViewPaymentGivens]
ADD CONSTRAINT [PK_ViewPaymentGivens]
    PRIMARY KEY CLUSTERED ([InvoiceId] ASC);
GO

-- Creating primary key on [InvoiceId] in table 'ViewPaymentReceiveds'
ALTER TABLE [dbo].[ViewPaymentReceiveds]
ADD CONSTRAINT [PK_ViewPaymentReceiveds]
    PRIMARY KEY CLUSTERED ([InvoiceId] ASC);
GO

-- Creating primary key on [ProjectID], [StatusOn], [Status] in table 'ViewProjectStatus'
ALTER TABLE [dbo].[ViewProjectStatus]
ADD CONSTRAINT [PK_ViewProjectStatus]
    PRIMARY KEY CLUSTERED ([ProjectID], [StatusOn], [Status] ASC);
GO

-- Creating primary key on [ID], [OrgID], [TDSDeducted], [TDSPayable], [PreviousTDS], [Penalty], [TaxMonth] in table 'ViewTDS'
ALTER TABLE [dbo].[ViewTDS]
ADD CONSTRAINT [PK_ViewTDS]
    PRIMARY KEY CLUSTERED ([ID], [OrgID], [TDSDeducted], [TDSPayable], [PreviousTDS], [Penalty], [TaxMonth] ASC);
GO

-- Creating primary key on [EntryDate], [TransactionDate], [TransID], [TransName], [AccID], [Deposit], [Withdraw], [ActivityID], [TransactionID], [OrgID], [ActivityName], [EmployeeID] in table 'ViewTransactions'
ALTER TABLE [dbo].[ViewTransactions]
ADD CONSTRAINT [PK_ViewTransactions]
    PRIMARY KEY CLUSTERED ([EntryDate], [TransactionDate], [TransID], [TransName], [AccID], [Deposit], [Withdraw], [ActivityID], [TransactionID], [OrgID], [ActivityName], [EmployeeID] ASC);
GO

-- Creating primary key on [OrgID], [AccID] in table 'ViewTransactionDailyAccounts'
ALTER TABLE [dbo].[ViewTransactionDailyAccounts]
ADD CONSTRAINT [PK_ViewTransactionDailyAccounts]
    PRIMARY KEY CLUSTERED ([OrgID], [AccID] ASC);
GO

-- Creating primary key on [AccID], [EntryDate] in table 'ViewTransactionDailyAccountBalances'
ALTER TABLE [dbo].[ViewTransactionDailyAccountBalances]
ADD CONSTRAINT [PK_ViewTransactionDailyAccountBalances]
    PRIMARY KEY CLUSTERED ([AccID], [EntryDate] ASC);
GO

-- Creating primary key on [OrgID] in table 'ViewTransactionDailySummaries'
ALTER TABLE [dbo].[ViewTransactionDailySummaries]
ADD CONSTRAINT [PK_ViewTransactionDailySummaries]
    PRIMARY KEY CLUSTERED ([OrgID] ASC);
GO

-- Creating primary key on [ProjectID] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [PK_Projects]
    PRIMARY KEY CLUSTERED ([ProjectID] ASC);
GO

-- Creating primary key on [ProjectID], [ClientName], [ProjectName], [ProjectCreationDate], [StatusOn], [Status], [Spent], [Received], [OrgID] in table 'ViewProjects'
ALTER TABLE [dbo].[ViewProjects]
ADD CONSTRAINT [PK_ViewProjects]
    PRIMARY KEY CLUSTERED ([ProjectID], [ClientName], [ProjectName], [ProjectCreationDate], [StatusOn], [Status], [Spent], [Received], [OrgID] ASC);
GO

-- Creating primary key on [ActivityID], [ItemName], [ExpenseAmount], [ReceiveAmount], [ExpenseDescription], [ExpenseDate], [ItemAction], [ActivityName], [EmployeeID], [ProjectID], [OrgID] in table 'ViewExpenseItemStatusActivities'
ALTER TABLE [dbo].[ViewExpenseItemStatusActivities]
ADD CONSTRAINT [PK_ViewExpenseItemStatusActivities]
    PRIMARY KEY CLUSTERED ([ActivityID], [ItemName], [ExpenseAmount], [ReceiveAmount], [ExpenseDescription], [ExpenseDate], [ItemAction], [ActivityName], [EmployeeID], [ProjectID], [OrgID] ASC);
GO

-- Creating primary key on [OrgId], [ProjectId], [VendorName], [InvoiceId], [InvoiceNumber], [InvoiceDate], [ServiceCost], [CGST], [SGST], [IGST], [TDS], [Payable], [PaidAmount], [PaidDate], [ProjectName], [ClientName], [CreatedBy] in table 'ViewPurchaseInvoices'
ALTER TABLE [dbo].[ViewPurchaseInvoices]
ADD CONSTRAINT [PK_ViewPurchaseInvoices]
    PRIMARY KEY CLUSTERED ([OrgId], [ProjectId], [VendorName], [InvoiceId], [InvoiceNumber], [InvoiceDate], [ServiceCost], [CGST], [SGST], [IGST], [TDS], [Payable], [PaidAmount], [PaidDate], [ProjectName], [ClientName], [CreatedBy] ASC);
GO

-- Creating primary key on [OrgId], [ProjectId], [InvoiceId], [InvoiceNumber], [InvoiceDate], [ServiceCost], [CGST], [SGST], [IGST], [TDS], [Receivable], [ReceivedAmount], [ReceivedDate], [ProjectName], [ClientName], [CreatedBy] in table 'ViewSellInvoices'
ALTER TABLE [dbo].[ViewSellInvoices]
ADD CONSTRAINT [PK_ViewSellInvoices]
    PRIMARY KEY CLUSTERED ([OrgId], [ProjectId], [InvoiceId], [InvoiceNumber], [InvoiceDate], [ServiceCost], [CGST], [SGST], [IGST], [TDS], [Receivable], [ReceivedAmount], [ReceivedDate], [ProjectName], [ClientName], [CreatedBy] ASC);
GO

-- Creating primary key on [OrgID], [ProjectID], [ActivityID], [EmployeeID] in table 'ViewExpenseItemDailyStatus'
ALTER TABLE [dbo].[ViewExpenseItemDailyStatus]
ADD CONSTRAINT [PK_ViewExpenseItemDailyStatus]
    PRIMARY KEY CLUSTERED ([OrgID], [ProjectID], [ActivityID], [EmployeeID] ASC);
GO

-- Creating primary key on [ActivityID], [ActivityName], [ActivityDescription], [EmployeeID], [ProjectID], [ProjectName], [Approver], [Settlement], [PaidDate], [Advance], [OrgID] in table 'NewViewActivities'
ALTER TABLE [dbo].[NewViewActivities]
ADD CONSTRAINT [PK_NewViewActivities]
    PRIMARY KEY CLUSTERED ([ActivityID], [ActivityName], [ActivityDescription], [EmployeeID], [ProjectID], [ProjectName], [Approver], [Settlement], [PaidDate], [Advance], [OrgID] ASC);
GO

-- Creating primary key on [ItemID] in table 'ExpenseItems'
ALTER TABLE [dbo].[ExpenseItems]
ADD CONSTRAINT [PK_ExpenseItems]
    PRIMARY KEY CLUSTERED ([ItemID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------