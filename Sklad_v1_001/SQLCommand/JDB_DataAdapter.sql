USE [DataAdapter]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attributes](
	[ID] [int],
	[CompanyID] [tinyint],
	[Type] [tinyint],
	[Name] [nvarchar],
	[Value] [nvarchar],
	[AttributeGroup] [nvarchar],
	[AttributeSort] [int],
	[Visibility] [bit],
	[IsEnable] [bit],
	[ShopID] [smallint],
	[PublishedDate] [datetime],
	[Description] [nvarchar],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttributesN](
	[ID] [int],
	[Name] [varchar],
	[Description1] [varchar],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[ID] [int],
	[CompanyNumber] [tinyint],
	[Description] [nvarchar],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coupon](
	[CompanyID] [int],
	[ShopID] [int],
	[ID] [int],
	[Description] [nvarchar],
	[Discount] [float],
	[Quantity] [int],
	[CouponDateFrom] [datetime],
	[CouponDateTo] [datetime],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currency](
	[Code] [int],
	[ShortDescription] [nvarchar],
	[LongDescription] [nvarchar],
	[PublishedDate] [datetime],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KT](
	[CompanyID] [int],
	[ShopID] [int],
	[ID] [int],
	[MetalID] [int],
	[Description] [nvarchar],
	[PublishedDate] [datetime],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[CompanyID] [tinyint],
	[ShopID] [int],
	[RefLocationID] [int],
	[PublishedDate] [datetime],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogErr](
	[LogErrID] [int],
	[UserID] [int],
	[DtErr] [datetime],
	[ERROR_MESSAGE] [nvarchar],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metal](
	[CompanyID] [int],
	[ShopID] [int],
	[ID] [int],
	[Description] [nvarchar],
	[PublishedDate] [datetime],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MetalPriceRefreshInfo](
	[CompanyID] [int],
	[ShopID] [int],
	[PriceDat] [date],
	[DtMod] [datetime],
	[DtRefresh] [datetime],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MetalPrices](
	[CompanyID] [int],
	[ShopID] [int],
	[PriceDat] [date],
	[PriceType] [int],
	[KT] [int],
	[PriceValue] [smallmoney],
	[PublishedDate] [datetime],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Register](
	[ID] [int],
	[ScrapRegisterNumber] [bigint],
	[Status] [tinyint],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RelatedProductGuide](
	[Model] [nvarchar],
	[Supplier] [nvarchar],
	[Description] [nvarchar],
	[Price] [float],
	[ID] [int],
	[CompanyID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RevaluationDocument](
	[IDLocal] [int],
	[IDCenter] [int],
	[CompanyID] [int],
	[ShopID] [int],
	[Reason] [varchar],
	[CreatedDate] [datetime],
	[PublishedDate] [datetime],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RevaluationDocumentDetails](
	[RepriceRegisterID] [int],
	[ID] [int],
	[Price] [float],
	[Currency] [smallint],
	[SyncDateInShop] [datetime],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleDocument](
	[ID] [int],
	[DocumentNumber] [bigint],
	[Type] [int],
	[UserID] [int],
	[ClientCardNumber] [nvarchar],
	[ClientPhone] [nvarchar],
	[ClientEmail] [nvarchar],
	[SendCheck] [bit],
	[Currency] [nvarchar],
	[SyncDate] [datetime],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleDocumentAdditionalDiscount](
	[ID] [int],
	[DocumentID] [int],
	[ExtRefID] [bigint],
	[Amount] [float],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleDocumentCoupon](
	[ID] [int],
	[DocumentID] [int],
	[CouponID] [nvarchar],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleDocumentPayment](
	[ID] [int],
	[DocumentID] [int],
	[WorkShiftID] [int],
	[ExtReffID] [bigint],
	[PaymentType] [int],
	[Amount] [float],
	[CardType] [int],
	[CardNumber] [nvarchar],
	[CardDate] [nvarchar],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CreditNumber] [nvarchar],
	[CompanyID] [int],
	[ShopID] [int],
	[IsRemotePayment] [bit],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleDocumentPrepayment](
	[ID] [int],
	[Amount] [float],
	[DocumentID] [int],
	[UseDocumentID] [int],
	[Description] [nvarchar],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleDocumentProduct](
	[ID] [int],
	[DocumentID] [int],
	[LineDocument] [int],
	[ProductID] [int],
	[Quantity] [int],
	[TagPriceWithVAT] [float],
	[TagPriceWithoutVAT] [float],
	[PriceFromCRM] [float],
	[DiscountType] [int],
	[DiscountDescription] [nvarchar],
	[ReasonReturnType] [int],
	[ReasonReturnDescription] [nvarchar],
	[SalePriceWithVAT] [float],
	[SalePriceWithoutVAT] [float],
	[InternetNumber] [int],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleDocumentRelatedProduct](
	[ID] [int],
	[DocumentID] [int],
	[LineDocument] [int],
	[PartNumber] [int],
	[Model] [nvarchar],
	[Quantity] [int],
	[TagPriceWithVAT] [float],
	[TagPriceWithoutVAT] [float],
	[SalePriceWithVAT] [float],
	[SalePriceWithoutVAT] [float],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
	[Supplier] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleDocumentScrapDocument](
	[ID] [int],
	[DocumentID] [int],
	[ScrapDocumentID] [int],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleDocumentServices](
	[ID] [int],
	[DocumentID] [int],
	[Type] [int],
	[Amount] [float],
	[Description] [nvarchar],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalePlan](
	[ID] [int],
	[CompanyID] [int],
	[ShopID] [int],
	[Category] [int],
	[AmountPlan] [float],
	[AmountFact] [float],
	[SyncDate] [datetime],
	[Status] [int],
	[FromDate] [datetime],
	[ToDate] [datetime],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScrapDocument](
	[ID] [int],
	[ScrapDocumentNumber] [bigint],
	[Status] [tinyint],
	[VendorID] [int],
	[RegisterID] [int],
	[SyncDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[Type] [tinyint],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScrapDocumentDetails](
	[ID] [int],
	[KTID] [int],
	[Quantity] [int],
	[Weight] [float],
	[Description] [text],
	[Imennik] [nvarchar],
	[Impress] [nvarchar],
	[DocumentID] [int],
	[Summa] [float],
	[SinglePrice] [float],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[LastModifiedDate] [datetime],
	[CreatedDate] [datetime],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shop](
	[ID] [int],
	[Description] [nvarchar],
	[ShopNumber] [int],
	[IsActive] [bit],
	[CompanyID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tab1](
	[col1] [int],
	[col2] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_Attributes](
	[ID] [int],
	[CompanyID] [tinyint],
	[Type] [tinyint],
	[Name] [nvarchar],
	[Value] [nvarchar],
	[AttributeGroup] [nvarchar],
	[AttributeSort] [int],
	[Visibility] [bit],
	[IsEnable] [bit],
	[ShopID] [smallint],
	[PublishedDate] [datetime],
	[TransComplited] [bit],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_Company](
	[ID] [int],
	[CompanyNumber] [tinyint],
	[Description] [nvarchar],
	[TransComplited] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_DocumentSync](
	[IDCenter] [bigint],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_KT](
	[CompanyID] [int],
	[ShopID] [int],
	[ID] [int],
	[MetalID] [int],
	[Description] [nvarchar],
	[PublishedDate] [datetime],
	[TransComplited] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_Location](
	[CompanyID] [tinyint],
	[ShopID] [int],
	[RefLocationID] [int],
	[PublishedDate] [datetime],
	[TransComplited] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_Metal](
	[CompanyID] [int],
	[ShopID] [int],
	[ID] [int],
	[Description] [nvarchar],
	[PublishedDate] [datetime],
	[TransComplited] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_MetalPrices](
	[CompanyID] [int],
	[ShopID] [int],
	[PriceDat] [date],
	[PriceType] [int],
	[KT] [int],
	[PriceValue] [smallmoney],
	[PublishedDate] [datetime],
	[TransComplited] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_Register](
	[ID] [int],
	[ScrapRegisterNumber] [bigint],
	[Status] [tinyint],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CompanyID] [int],
	[ShopID] [int],
	[TransComplited] [smallint],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_RelatedDocumentSync](
	[TransferDocumentNumber] [bigint],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_RevaluationDocument](
	[IDLocal] [int],
	[IDCenter] [int],
	[CompanyID] [int],
	[ShopID] [int],
	[Reason] [varchar],
	[CreatedDate] [datetime],
	[TransComplited] [smallint],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_RevaluationDocumentDetails](
	[RepriceRegisterID] [int],
	[ID] [int],
	[Price] [float],
	[Currency] [smallint],
	[SyncDateInShop] [datetime],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_RevaluationSync](
	[IDCenter] [int],
	[CompanyID] [int],
	[ShopID] [int],
	[ProdID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_SaleDocument](
	[ID] [int],
	[DocumentNumber] [bigint],
	[Type] [int],
	[UserID] [int],
	[ClientCardNumber] [nvarchar],
	[ClientPhone] [nvarchar],
	[ClientEmail] [nvarchar],
	[SendCheck] [bit],
	[Currency] [nvarchar],
	[SyncDate] [datetime],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
	[TransComplited] [smallint],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_SaleDocumentAdditionalDiscount](
	[ID] [int],
	[DocumentID] [int],
	[ExtRefID] [bigint],
	[Amount] [float],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_SaleDocumentCoupon](
	[ID] [int],
	[DocumentID] [int],
	[CouponID] [nvarchar],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_SaleDocumentPayment](
	[ID] [int],
	[DocumentID] [int],
	[WorkShiftID] [int],
	[ExtReffID] [bigint],
	[PaymentType] [int],
	[Amount] [float],
	[CardType] [int],
	[CardNumber] [nvarchar],
	[CardDate] [nvarchar],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CreditNumber] [nvarchar],
	[CompanyID] [int],
	[ShopID] [int],
	[IsRemotePayment] [bit],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_SaleDocumentPrepayment](
	[ID] [int],
	[Amount] [float],
	[DocumentID] [int],
	[UseDocumentID] [int],
	[Description] [nvarchar],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_SaleDocumentProduct](
	[ID] [int],
	[DocumentID] [int],
	[LineDocument] [int],
	[ProductID] [int],
	[Quantity] [int],
	[TagPriceWithVAT] [float],
	[TagPriceWithoutVAT] [float],
	[PriceFromCRM] [float],
	[DiscountType] [int],
	[DiscountDescription] [nvarchar],
	[ReasonReturnType] [int],
	[ReasonReturnDescription] [nvarchar],
	[SalePriceWithVAT] [float],
	[SalePriceWithoutVAT] [float],
	[InternetNumber] [int],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_SaleDocumentRelatedProduct](
	[ID] [int],
	[DocumentID] [int],
	[LineDocument] [int],
	[PartNumber] [int],
	[Model] [nvarchar],
	[Quantity] [int],
	[TagPriceWithVAT] [float],
	[TagPriceWithoutVAT] [float],
	[SalePriceWithVAT] [float],
	[SalePriceWithoutVAT] [float],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
	[Supplier] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_SaleDocumentScrapDocument](
	[ID] [int],
	[DocumentID] [int],
	[ScrapDocumentID] [int],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_SaleDocumentServices](
	[ID] [int],
	[DocumentID] [int],
	[Type] [int],
	[Amount] [float],
	[Description] [nvarchar],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_ScrapDocument](
	[ID] [int],
	[ScrapDocumentNumber] [bigint],
	[Status] [tinyint],
	[VendorID] [int],
	[RegisterID] [int],
	[SyncDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[Type] [tinyint],
	[CompanyID] [int],
	[ShopID] [int],
	[TransComplited] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_ScrapDocumentDetails](
	[ID] [int],
	[KTID] [int],
	[Quantity] [int],
	[Weight] [float],
	[Description] [text],
	[Imennik] [nvarchar],
	[Impress] [nvarchar],
	[DocumentID] [int],
	[Summa] [float],
	[SinglePrice] [float],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[LastModifiedDate] [datetime],
	[CreatedDate] [datetime],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_Shop](
	[ID] [int],
	[Description] [nvarchar],
	[ShopNumber] [int],
	[IsActive] [bit],
	[CompanyID] [int],
	[Trans_Complited] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_TransferDocument](
	[ID] [int],
	[SyncDate] [datetime],
	[SenderID] [int],
	[ReceiverID] [int],
	[Contract] [nvarchar],
	[Status] [int],
	[TransferDocumentNumber] [bigint],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
	[TransComplited] [smallint],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_TransferDocumentDetails](
	[DocumentID] [int],
	[LineDocument] [int],
	[Model] [nvarchar],
	[ProductID] [int],
	[Type] [nvarchar],
	[KtID] [int],
	[Size] [nvarchar],
	[Color] [nvarchar],
	[Gender] [nvarchar],
	[SupplierID] [int],
	[SupplierName] [nvarchar],
	[Country] [nvarchar],
	[ThemedCollection] [nvarchar],
	[Weight] [float],
	[CostWithoutVAT] [float],
	[CostCurrency] [nvarchar],
	[TagPriceWithVAT] [float],
	[TagPriceCurrency] [nvarchar],
	[TagPriceExtra] [float],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[TagPriceWithOutVAT] [float],
	[DeclarationNumber] [nvarchar],
	[TnvedCode] [nvarchar],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_TransferDocumentStonesDetails](
	[ID] [int],
	[ProductID] [int],
	[DocumentID] [int],
	[Stone] [nvarchar],
	[Shape] [nvarchar],
	[Size] [nvarchar],
	[Color] [nvarchar],
	[Clarity] [nvarchar],
	[Quantity] [int],
	[Weight] [float],
	[Setting] [nvarchar],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_TransferRelatedProductDocument](
	[ID] [int],
	[SyncDate] [datetime],
	[SenderID] [int],
	[ReceiverID] [int],
	[Contract] [nvarchar],
	[Status] [int],
	[TransferDocumentNumber] [bigint],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
	[TransComplited] [smallint],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_TransferRelatedProductDocumentDetails](
	[DocumentID] [int],
	[LineDocument] [int],
	[PartNumber] [int],
	[Model] [nvarchar],
	[Supplier] [int],
	[Quantity] [int],
	[UnitPrice] [money],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_UserActiveHistory](
	[ID] [int],
	[Path] [varchar],
	[CreatedDate] [datetime],
	[CreatedByUserID] [int],
	[AttributeID] [int],
	[Type] [int],
	[CompanyID] [int],
	[ShopID] [int],
	[TransComplited] [bit],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_UsersWRK](
	[ID] [int],
	[UserID] [int],
	[Type] [tinyint],
	[DtAdd] [datetime],
	[CompanyID] [int],
	[ShopID] [int],
	[TransComplited] [bit],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_Vendor](
	[ID] [int],
	[Name] [nvarchar],
	[Address] [nvarchar],
	[Serial] [nvarchar],
	[Number] [nvarchar],
	[Issue] [nvarchar],
	[IssueDate] [datetime],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEMP_Versions](
	[ID] [int],
	[Number] [varchar],
	[SyncDate] [datetime],
	[CreatedByUserID] [int],
	[CreatedDate] [datetime],
	[CompanyID] [int],
	[ShopID] [int],
	[TransComplited] [bit],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransferDocument](
	[ID] [int],
	[SyncDateSender] [datetime],
	[SenderID] [int],
	[ReceiverID] [int],
	[Contract] [nvarchar],
	[Status] [int],
	[TransferDocumentNumber] [bigint],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
	[PublishedDate] [datetime],
	[SyncDateReciever] [datetime],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransferDocumentDetails](
	[DocumentID] [int],
	[LineDocument] [int],
	[Model] [nvarchar],
	[ProductID] [int],
	[Type] [nvarchar],
	[KtID] [int],
	[Size] [nvarchar],
	[Color] [nvarchar],
	[Gender] [nvarchar],
	[SupplierID] [int],
	[SupplierName] [nvarchar],
	[Country] [nvarchar],
	[ThemedCollection] [nvarchar],
	[Weight] [float],
	[CostWithoutVAT] [float],
	[CostCurrency] [nvarchar],
	[TagPriceWithVAT] [float],
	[TagPriceCurrency] [nvarchar],
	[TagPriceExtra] [float],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[TagPriceWithOutVAT] [float],
	[DeclarationNumber] [nvarchar],
	[TnvedCode] [nvarchar],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransferDocumentStonesDetails](
	[ID] [int],
	[ProductID] [int],
	[DocumentID] [int],
	[Stone] [nvarchar],
	[Shape] [nvarchar],
	[Size] [nvarchar],
	[Color] [nvarchar],
	[Clarity] [nvarchar],
	[Quantity] [int],
	[Weight] [float],
	[Setting] [nvarchar],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransferRelatedProductDocument](
	[ID] [int],
	[SenderID] [int],
	[ReceiverID] [int],
	[Contract] [nvarchar],
	[Status] [int],
	[TransferDocumentNumber] [bigint],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
	[PublishedDate] [datetime],
	[SyncDateSender] [datetime],
	[SyncDateReciever] [datetime],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransferRelatedProductDocumentDetails](
	[DocumentID] [int],
	[LineDocument] [int],
	[PartNumber] [int],
	[Model] [nvarchar],
	[Supplier] [int],
	[Quantity] [int],
	[UnitPrice] [money],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UpdID](
	[ID] [int],
	[CompanyID] [int],
	[ShopID] [int],
	[Description] [varchar],
	[PosUpdID] [int],
	[DtAdd] [datetime],
	[Res] [varchar],
	[DtRes] [datetime],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserActiveHistory](
	[ID] [int],
	[Path] [varchar],
	[CreatedDate] [datetime],
	[CreatedByUserID] [int],
	[AttributeID] [int],
	[Type] [int],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersWRK](
	[ID] [int],
	[UserID] [int],
	[Type] [tinyint],
	[DtAdd] [datetime],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendor](
	[ID] [int],
	[Name] [nvarchar],
	[Address] [nvarchar],
	[Serial] [nvarchar],
	[Number] [nvarchar],
	[Issue] [nvarchar],
	[IssueDate] [datetime],
	[CreatedDate] [datetime],
	[LastModifiedDate] [datetime],
	[CreatedByUserID] [int],
	[LastModifiedByUserID] [int],
	[CompanyID] [int],
	[ShopID] [int],
) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Versions](
	[ID] [int],
	[Number] [varchar],
	[SyncDate] [datetime],
	[CreatedByUserID] [int],
	[CreatedDate] [datetime],
	[CompanyID] [int],
	[ShopID] [int],
) 