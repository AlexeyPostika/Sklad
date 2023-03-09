using Sklad_v1_001.Control.FlexProgressBar;
using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.GlobalVariable;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace POS.DocumentsReport
{
    public class DocumentPrint
    {
        public String TransferDocumentNumber;
        public DateTime? SyncDate;
        public String Contract;
        public Double SummaSumm;
        public Int32 SenderID;
        public Int32 ReceiverID;
        public Double QuantitySumm;
        public Double WeightSumm;
        public String ContractNumberInternalLong;
    }

    public class LocaleRow
    {
        public Int32 ID;
        public String UIN;
        public String Model;
        public String Type;
        public String Country;
        public Double Weight;
        public Double TagPriceWithVAT;
        public Double TagPriceWithOutVAT;
        public String DeclarationNumber;
        public String TnvedCode;
        public String KtIDShortDescription;

        public String ModelDescription;
        public String PartNumber;
        public Int32 Quantity;
        public Double UnitPrice;
    }

    public class DocumentsReport
    {
        Numeric numeric;
        DataBaseData _locdataBase;
        public DocumentPrint documentPrint;
        ObservableCollection<LocaleRow> datalistdetails;

        DataRow myDataRow;

        Boolean isRelatedDocument;

        String companySenderNumber = "";
        String companyRecieverNumber = "";
        String shopRecieverFullAddress = "";
        String shopSenderFullAddress = "";
        String seniorAccountant = "";
        String generalDirector = "";

        Int32 numberDetails = 1;
        Int32 UnitCode = 163;
        Double WeightNettoSumm = 0;
        Double UnitQuantity = 1;
        Double UnitPriceSumm = 0;
        Double SummWithOutNds = 0;
        Double FullSummWithOutNds = 0;
        Double NdsValue = 0;
        Double NdsValueSumm = 0;
        Double NdsValueFullSumm = 0;
        String UnitDescription = "штука";
        String fullStoneDescription = "";
        Boolean isTransferDocument;

        public DocumentsReport(Numeric _numeric, DataBaseData _databasedata, Object _document, Object _objectcollection)
        {
            numeric = _numeric;
            _locdataBase = _databasedata;
          
            datalistdetails = new ObservableCollection<LocaleRow>();
            documentPrint = new DocumentPrint();
           
            ConvertDocument(_document, _objectcollection);
            documentPrint.QuantitySumm = 0;
        }

        private void ConvertDocument(Object _document, Object _objectcollection)
        {
            //Object document;

            //document = _document as Screens.TransferDocument.LocaleRow;
            //if (document != null)
            //{
            //    isRelatedDocument = false;
            //    isTransferDocument = true; 

            //    Screens.TransferDocument.LocaleRow doc = _document as Screens.TransferDocument.LocaleRow;
            //    documentPrint.TransferDocumentNumber = doc.TransferDocumentNumber.ToString();
            //    documentPrint.SyncDate = doc.CreatedDate;
            //    documentPrint.Contract = doc.Contract;
            //    documentPrint.SummaSumm = doc.SummaSumm;
            //    documentPrint.SenderID = doc.SenderID;
            //    documentPrint.ReceiverID = doc.ReceiverID;
            //    documentPrint.QuantitySumm = doc.QuantitySumm;
            //    documentPrint.WeightSumm = doc.WeightSumm;
            //    documentPrint.ContractNumberInternalLong = doc.ContractNumberInternalLong;
            //    isTransferDocument = true;

            //    ObservableCollection<Screens.TransferDocumentDetails.LocaleRow> objectcollection = _objectcollection as ObservableCollection<Screens.TransferDocumentDetails.LocaleRow>;
            //    foreach (Screens.TransferDocumentDetails.LocaleRow row in objectcollection)
            //    {
            //        LocaleRow localeRow = new LocaleRow();
            //        localeRow.ID = row.ProductID;
            //        localeRow.UIN = row.UIN;
            //        localeRow.Model = row.Model;
            //        localeRow.Type = row.Type;
            //        localeRow.Country = row.Country;
            //        localeRow.Weight = row.Weight;
            //        localeRow.TagPriceWithVAT = row.TagPriceWithVAT;
            //        localeRow.TagPriceWithOutVAT = row.TagPriceWithOutVAT;
            //        localeRow.DeclarationNumber = row.DeclarationNumber;
            //        localeRow.TnvedCode = row.TnvedCode;
            //        localeRow.KtIDShortDescription = row.KtIDShortDescription;
            //        datalistdetails.Add(localeRow);
            //    }
            //}

            //document = _document as Screens.Document.LocaleRow;
            //if (document != null)
            //{
            //    isRelatedDocument = false;
            //    isTransferDocument = false;

            //    Screens.Document.LocaleRow doc = _document as Screens.Document.LocaleRow;
            //    documentPrint.TransferDocumentNumber = doc.DocumentID.ToString();
            //    documentPrint.SyncDate = doc.LastModifiedDate;
            //    documentPrint.Contract = doc.Contract;
            //    documentPrint.SummaSumm = doc.SummaSumm;
            //    documentPrint.SenderID = doc.SenderID;
            //    documentPrint.ReceiverID = doc.ReceiverID;
            //    documentPrint.QuantitySumm = doc.QuantitySumm;
            //    documentPrint.WeightSumm = doc.WeightSumm;
            //    documentPrint.ContractNumberInternalLong = doc.ContractNumberInternalLong;

            //    ObservableCollection<Screens.DocumentDetails.LocaleRow> objectcollection = _objectcollection as ObservableCollection<Screens.DocumentDetails.LocaleRow>;
            //    foreach (Screens.DocumentDetails.LocaleRow row in objectcollection)
            //    {
            //        LocaleRow localeRow = new LocaleRow();
            //        localeRow.ID = row.ID;
            //        localeRow.UIN = row.UIN;
            //        localeRow.Model = row.Model;
            //        localeRow.Type = row.Type;
            //        localeRow.Country = row.Country;
            //        localeRow.Weight = row.Weight;
            //        localeRow.TagPriceWithVAT = row.TagPriceWithVAT;
            //        localeRow.TagPriceWithOutVAT = row.TagPriceWithOutVAT;
            //        localeRow.DeclarationNumber = row.DeclarationNumber;
            //        localeRow.TnvedCode = row.TnvedCode;
            //        localeRow.KtIDShortDescription = row.KtIDShortDescription;
            //        datalistdetails.Add(localeRow);
            //    }
            //}

            //document = _document as Screens.TransferRelatedDocument.LocaleRow;
            //if (document != null)
            //{
            //    isRelatedDocument = true;
            //    isTransferDocument = true;

            //    Screens.TransferRelatedDocument.LocaleRow doc = _document as Screens.TransferRelatedDocument.LocaleRow;
            //    documentPrint.TransferDocumentNumber = doc.DocumentNumber.ToString();
            //    documentPrint.SyncDate = doc.CreatedDate;
            //    documentPrint.Contract = doc.Contract;
            //    documentPrint.SummaSumm = doc.SummaTagPriceWithVAT;
            //    documentPrint.SenderID = doc.SenderID;
            //    documentPrint.ReceiverID = doc.ReceiverID;
            //    documentPrint.QuantitySumm = doc.QuantitySumm;
            //    documentPrint.WeightSumm = 0;
            //    documentPrint.ContractNumberInternalLong = "";

            //    ObservableCollection<Screens.TransferRelatedDocumentDetails.LocaleRow> objectcollection = _objectcollection as ObservableCollection<Screens.TransferRelatedDocumentDetails.LocaleRow>;
            //    foreach (Screens.TransferRelatedDocumentDetails.LocaleRow row in objectcollection)
            //    {
            //        LocaleRow localeRow = new LocaleRow();
            //        localeRow.ID = row.ID;
            //        localeRow.Model = row.Model;
            //        localeRow.Type = "";
            //        localeRow.Country = "";
            //        localeRow.Weight = 0;
            //        localeRow.TagPriceWithVAT = row.TagPriceWithVAT;
            //        localeRow.TagPriceWithOutVAT = row.TagPriceWithOutVAT;
            //        localeRow.DeclarationNumber = "";
            //        localeRow.TnvedCode = "";
            //        localeRow.KtIDShortDescription = "";
            //        localeRow.ModelDescription = row.ModelDescription;
            //        localeRow.PartNumber = row.PartNumber.ToString();
            //        localeRow.Quantity = row.Quantity;
            //        localeRow.UnitPrice = row.UnitPrice;
            //        datalistdetails.Add(localeRow);
            //    }
            //}

            //document = _document as Screens.RelatedProductDocument.LocaleRow;
            //if (document != null)
            //{
            //    isRelatedDocument = true;
            //    isTransferDocument = false;

            //    Screens.RelatedProductDocument.LocaleRow doc = _document as Screens.RelatedProductDocument.LocaleRow;
            //    documentPrint.TransferDocumentNumber = doc.DocumentID.ToString();
            //    documentPrint.SyncDate = doc.LastModifiedDate;
            //    documentPrint.Contract = doc.Contract;
            //    documentPrint.SummaSumm = doc.SummaSumm;
            //    documentPrint.SenderID = doc.SenderID;
            //    documentPrint.ReceiverID = convertdata.FlexDataConvertToInt32(_locnumeric.requisites[Properties.Resources.NumberShop]);
            //    documentPrint.QuantitySumm = doc.QuantitySumm;
            //    documentPrint.WeightSumm = 0;
            //    documentPrint.ContractNumberInternalLong = "";

            //    ObservableCollection<Screens.RelatedProductDocumentDetails.LocaleRow> objectcollection = _objectcollection as ObservableCollection<Screens.RelatedProductDocumentDetails.LocaleRow>;
            //    foreach (Screens.RelatedProductDocumentDetails.LocaleRow row in objectcollection)
            //    {
            //        LocaleRow localeRow = new LocaleRow();
            //        localeRow.ID = row.ID;
            //        localeRow.Model = row.Model;
            //        localeRow.Type = "";
            //        localeRow.Country = "";
            //        localeRow.Weight = row.Weight;
            //        localeRow.TagPriceWithVAT = row.TagPriceWithVAT;
            //        localeRow.TagPriceWithOutVAT = row.TagPriceWithOutVAT;
            //        localeRow.DeclarationNumber = "";
            //        localeRow.TnvedCode = "";
            //        localeRow.KtIDShortDescription = "";
            //        localeRow.ModelDescription = row.ModelDescription;
            //        localeRow.PartNumber = row.PartNumber.ToString();
            //        localeRow.Quantity = row.Quantity;
            //        localeRow.UnitPrice = row.UnitPrice;
            //        datalistdetails.Add(localeRow);
            //    }
            //}

            //DataTable datatable;
            //datatable = locationLogic.FillGridReport(documentPrint.ReceiverID);
            //foreach (DataRow row in datatable.Rows)
            //{
            //    Screens.Location.LocaleRow localeRow = new Screens.Location.LocaleRow();
            //    locationLogic.Convert(row, localeRow);
            //    companyRecieverNumber = localeRow.CompanyID.ToString();
            //    shopRecieverFullAddress = localeRow.FullAddress;
            //}

            //datatable = locationLogic.FillGridReport(documentPrint.SenderID);
            //foreach (DataRow row in datatable.Rows)
            //{
            //    Screens.Location.LocaleRow localeRow = new Screens.Location.LocaleRow();
            //    locationLogic.Convert(row, localeRow);
            //    shopSenderFullAddress = localeRow.FullAddress;
            //    companySenderNumber = localeRow.CompanyID.ToString();
            //}
        }

        void GetStoneDescription(Int32 id)
        {
            //fullStoneDescription = "";
            //Int32 numberStone = 1;

            //Screens.Stones.LocaleRow localrow;
            //Screens.Stones.LocaleFilter filter = new Screens.Stones.LocaleFilter();
            //filter.ProductID = id;
            //DataTable stonestable = documentStoneLogic.FillGrid(filter);

            //foreach (DataRow stonerow in stonestable.Rows)
            //{
            //    localrow = new Screens.Stones.LocaleRow();
            //    documentStoneLogic.Convert(stonerow, localrow);
            //    String stoneDescription = String.Concat(
            //        "   ",
            //        localrow.Quantity,
            //        localrow.Stone.Trim(), " ",
            //        localrow.Shape.Trim(),
            //        String.IsNullOrEmpty(localrow.Shape.Trim()) ? "" : "-",
            //        String.Format(CultureInfo.InvariantCulture, "{0:N2}", localrow.Weight, 2).ToString(),
            //        " ",
            //        localrow.Color.Trim(),
            //        String.IsNullOrEmpty(localrow.Color.Trim()) || String.IsNullOrEmpty(localrow.Clarity.Trim()) ? "" : "/",
            //        localrow.Clarity.Trim()
            //     );

            //    if (numberStone == stonestable.Rows.Count)
            //        fullStoneDescription = String.Concat(fullStoneDescription, stoneDescription);
            //    else
            //        fullStoneDescription = String.Concat(fullStoneDescription, stoneDescription, "\r\n");

            //    numberStone++;
            //}
        }

        public void PrintDocumentSchetFactura1(FlexProgressBarData progressBarData)
        {
            //Report.SchetFactura1Document.ReportData reportData = new Report.SchetFactura1Document.ReportData();

            //DataTable documentDataTable = new DataTable("documentDataTable");
            //for (Int32 i = 1; i <= 14; i++)
            //    documentDataTable.Columns.Add(String.Concat("Column", i.ToString()), typeof(string));

            //foreach (LocaleRow current in datalistdetails)
            //{
            //    NdsValue = (current.TagPriceWithVAT - current.TagPriceWithOutVAT) * 100 / current.TagPriceWithOutVAT;
            //    FullSummWithOutNds = FullSummWithOutNds + current.TagPriceWithOutVAT;
            //    NdsValueSumm = current.TagPriceWithVAT - current.TagPriceWithOutVAT;
            //    NdsValueFullSumm = NdsValueFullSumm + NdsValueSumm;

            //    myDataRow = documentDataTable.NewRow();

            //    if (isRelatedDocument == false)
            //    {
            //        GetStoneDescription(current.ID);
            //        myDataRow["Column1"] = String.Concat(current.Type, " ", current.Model, String.IsNullOrEmpty(fullStoneDescription.Trim()) ? "" : "\r\n" + fullStoneDescription);
            //        myDataRow["Column2"] = current.TnvedCode;
            //        myDataRow["Column3"] = UnitCode;
            //        myDataRow["Column4"] = UnitDescription;
            //        myDataRow["Column5"] = String.Format(CultureInfo.InvariantCulture, "{0:N3}", UnitQuantity).ToString();
            //        myDataRow["Column6"] = String.Format(CultureInfo.InvariantCulture, "{0:N3}", current.TagPriceWithVAT).ToString();
            //        myDataRow["Column7"] = String.Format(CultureInfo.InvariantCulture, "{0:N3}", current.TagPriceWithOutVAT).ToString();
            //        myDataRow["Column8"] = "";
            //        myDataRow["Column9"] = String.Format(CultureInfo.InvariantCulture, "{0:N0}", NdsValue).ToString();
            //        myDataRow["Column10"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", NdsValueSumm).ToString();
            //        myDataRow["Column11"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", current.TagPriceWithVAT).ToString();
            //        myDataRow["Column12"] = current.TnvedCode;
            //        myDataRow["Column13"] = current.Country;
            //        myDataRow["Column14"] = current.DeclarationNumber;
            //    }
            //    else
            //    {
            //        myDataRow["Column1"] = String.Concat(current.Model, ", ", current.ModelDescription, ", партия ", current.PartNumber);
            //        myDataRow["Column2"] = "";
            //        myDataRow["Column3"] = "";
            //        myDataRow["Column4"] = UnitDescription;
            //        myDataRow["Column5"] = String.Format(CultureInfo.InvariantCulture, "{0:N3}", current.Quantity).ToString();
            //        myDataRow["Column6"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", current.UnitPrice).ToString();
            //        myDataRow["Column7"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", SummWithOutNds).ToString();
            //        myDataRow["Column8"] = "";
            //        myDataRow["Column9"] = String.Format(CultureInfo.InvariantCulture, "{0:N0}", NdsValue).ToString();
            //        myDataRow["Column10"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", NdsValueSumm).ToString();
            //        myDataRow["Column11"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", current.TagPriceWithVAT).ToString();
            //        myDataRow["Column12"] = "";
            //        myDataRow["Column13"] = "";
            //        myDataRow["Column14"] = "";
            //    }

            //    documentDataTable.Rows.Add(myDataRow);
            //    numberDetails++;
            //}

            //myDataRow = documentDataTable.NewRow();
            //myDataRow["Column1"] = "Итого по накладной";
            //myDataRow["Column2"] = "";
            //myDataRow["Column3"] = "";
            //myDataRow["Column4"] = "";
            //myDataRow["Column5"] = "";
            //myDataRow["Column6"] = "";
            //myDataRow["Column7"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", FullSummWithOutNds).ToString();
            //myDataRow["Column8"] = "X";
            //myDataRow["Column9"] = "X";
            //myDataRow["Column10"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", NdsValueFullSumm).ToString();
            //myDataRow["Column11"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", documentPrint.SummaSumm).ToString();
            //myDataRow["Column12"] = "";
            //myDataRow["Column13"] = "";
            //myDataRow["Column14"] = "";
            //documentDataTable.Rows.Add(myDataRow);

            //reportData.documentDataTable = documentDataTable;

            //if (reportData.documentDataTable.Rows.Count > 5000)
            //    reportData.isMemoryStream = false;
            //else
            //    reportData.isMemoryStream = true;

            //reportData.documentNumber = documentPrint.TransferDocumentNumber.ToString();
            //if (documentPrint.SyncDate != null)
            //    reportData.documentDate = documentPrint.SyncDate?.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            //reportData.documentNumberChange = "";
            //reportData.documentDateChange = "";
            //reportData.documentRecieverShopNumber = documentPrint.ReceiverID.ToString();

            //if (_locnumeric.businessData.PowerFranchBoolean == true)
            //{
            //    if (isTransferDocument)
            //    {
            //        reportData.documentSenderOrganization = _locnumeric.requisites["FullCompanyName"];
            //        reportData.documentSenderAdress = String.Concat(_locnumeric.requisites["CompanyAddress"], ", (Магазин №", documentPrint.SenderID.ToString(), ")");
            //        reportData.documentSenderOrganizationAdress = String.Concat(_locnumeric.requisites["CompanyAddress"], ", (Магазин №", documentPrint.SenderID.ToString(), ")");
            //        reportData.documentSenderInnKpp = String.Concat(_locnumeric.requisites["INN"], "/", _locnumeric.requisites["KPP"]);
            //        reportData.documentSenderAccountant = _locnumeric.requisites["SeniorAccountant"];
            //        reportData.documentSenderGeneralDirector = _locnumeric.requisites["GeneralDirector"];
            //        reportData.documentSenderCurrency = String.Concat(_locnumeric.requisites["CurrencyName"], " (", _locnumeric.requisites["CourrencyCode"], ")");

            //        reportData.documentReceiverOrganization = _locnumeric.mainCompanyAttributes["FullCompanyName"];
            //        reportData.documentReceiverOrganizationAdress = String.Concat(_locnumeric.mainCompanyAttributes["FullCompanyName"], ", ", _locnumeric.mainCompanyAttributes["CompanyAddress"], ", (Магазин №", documentPrint.ReceiverID.ToString(), ")");
            //        reportData.documentReceiverAdress = String.Concat(_locnumeric.mainCompanyAttributes["CompanyAddress"], ", (Магазин №", documentPrint.ReceiverID.ToString(), ")");
            //        reportData.documentReceiverInnKpp = String.Concat(_locnumeric.mainCompanyAttributes["INN"], "/", _locnumeric.mainCompanyAttributes["KPP"]);                    
            //    }
            //    else 
            //    {
            //        reportData.documentSenderOrganization = _locnumeric.mainCompanyAttributes["FullCompanyName"];
            //        reportData.documentSenderAdress = String.Concat(_locnumeric.mainCompanyAttributes["CompanyAddress"], ", (Магазин №", documentPrint.SenderID.ToString(), ")");
            //        reportData.documentSenderOrganizationAdress = String.Concat(_locnumeric.mainCompanyAttributes["CompanyAddress"], ", (Магазин №", documentPrint.SenderID.ToString(), ")");
            //        reportData.documentSenderInnKpp = String.Concat(_locnumeric.mainCompanyAttributes["INN"], "/", _locnumeric.mainCompanyAttributes["KPP"]);
            //        reportData.documentSenderAccountant = _locnumeric.mainCompanyAttributes["SeniorAccountant"];
            //        reportData.documentSenderGeneralDirector = _locnumeric.mainCompanyAttributes["GeneralDirector"];
            //        reportData.documentSenderCurrency = String.Concat(_locnumeric.mainCompanyAttributes["CurrencyName"], " (", _locnumeric.mainCompanyAttributes["CourrencyCode"], ")");

            //        reportData.documentReceiverOrganization = _locnumeric.requisites["FullCompanyName"];
            //        reportData.documentReceiverOrganizationAdress = String.Concat(_locnumeric.requisites["FullCompanyName"], ", ", _locnumeric.requisites["CompanyAddress"], ", (Магазин №", documentPrint.ReceiverID.ToString(), ")");
            //        reportData.documentReceiverAdress = String.Concat(_locnumeric.requisites["CompanyAddress"], ", (Магазин №", documentPrint.ReceiverID.ToString(), ")");
            //        reportData.documentReceiverInnKpp = String.Concat(_locnumeric.requisites["INN"], "/", _locnumeric.requisites["KPP"]);                    
            //    }
                
                
            //}
            //else
            //{
            //    reportData.documentSenderOrganization = _locnumeric.anotherCompanyAttributes[companySenderNumber, "FullCompanyName"];
            //    reportData.documentSenderAdress = String.Concat(_locnumeric.anotherCompanyAttributes[companySenderNumber, "CompanyAddress"], ", (Магазин №", documentPrint.SenderID.ToString(), ")");
            //    reportData.documentSenderOrganizationAdress = String.Concat(_locnumeric.anotherCompanyAttributes[companySenderNumber, "CompanyAddress"], ", (Магазин №", documentPrint.SenderID.ToString(), ")");
            //    reportData.documentSenderInnKpp = String.Concat(_locnumeric.anotherCompanyAttributes[companySenderNumber, "INN"], "/", _locnumeric.anotherCompanyAttributes[companySenderNumber, "KPP"]);
            //    reportData.documentSenderCurrency = String.Concat(_locnumeric.anotherCompanyAttributes[companySenderNumber, "CurrencyName"], " (", _locnumeric.anotherCompanyAttributes[companySenderNumber, "CourrencyCode"], ")");                
            //    reportData.documentSenderAccountant = _locnumeric.anotherCompanyAttributes[companySenderNumber, "SeniorAccountant"];
            //    reportData.documentSenderGeneralDirector = _locnumeric.anotherCompanyAttributes[companySenderNumber, "GeneralDirector"];

            //    reportData.documentReceiverOrganization = _locnumeric.anotherCompanyAttributes[companyRecieverNumber, "FullCompanyName"];
            //    reportData.documentReceiverOrganizationAdress = String.Concat(_locnumeric.anotherCompanyAttributes[companyRecieverNumber, "FullCompanyName"], ", ", _locnumeric.anotherCompanyAttributes[companyRecieverNumber, "CompanyAddress"], ", (Магазин №", documentPrint.ReceiverID.ToString(), ")");
            //    reportData.documentReceiverAdress = String.Concat(_locnumeric.anotherCompanyAttributes[companyRecieverNumber, "CompanyAddress"], ", (Магазин №", documentPrint.ReceiverID.ToString(), ")");
            //    reportData.documentReceiverInnKpp = String.Concat(_locnumeric.anotherCompanyAttributes[companyRecieverNumber, "INN"], "/", _locnumeric.anotherCompanyAttributes[companyRecieverNumber, "KPP"]);           
            //}


            //reportData.documentNumberPayer = "";
            //reportData.documentDatePayer = "";
            //reportData.documentBase = documentPrint.Contract;

            //if(_locnumeric.businessData.menuOption["Contracts"].ToString().ToLower().Trim() == "true")
            //{
            //    reportData.documentContractNumber = documentPrint.ContractNumberInternalLong;
            //}
            //else
            //{
            //    reportData.documentContractNumber = "";
            //}

            //PrintSchetFactura1Document docWindow;
            //docWindow = new PrintSchetFactura1Document(reportData, progressBarData);
            //docWindow.CreateXps();
        }

        public void PrintDocumentSchetFactura2(FlexProgressBarData progressBarData)
        {
            //Report.SchetFactura2Document.ReportData reportData;
            //reportData = new Report.SchetFactura2Document.ReportData();

            //DataTable documentDataTable = new DataTable("documentDataTable");
            //for (Int32 i = 1; i <= 16; i++)
            //    documentDataTable.Columns.Add(String.Concat("Column", i.ToString()), typeof(string));

            //foreach (LocaleRow current in datalistdetails)
            //{
            //    NdsValue = (current.TagPriceWithVAT - current.TagPriceWithOutVAT) * 100 / current.TagPriceWithOutVAT;
            //    FullSummWithOutNds = FullSummWithOutNds + current.TagPriceWithOutVAT;
            //    NdsValueSumm = current.TagPriceWithVAT - current.TagPriceWithOutVAT;
            //    NdsValueFullSumm = NdsValueFullSumm + NdsValueSumm;

            //    myDataRow = documentDataTable.NewRow();

            //    if (isRelatedDocument == false)
            //    {
            //        GetStoneDescription(current.ID);
            //        myDataRow["Column1"] = numberDetails.ToString();
            //        myDataRow["Column2"] = current.ID;
            //        myDataRow["Column3"] = String.Concat(current.Type, String.IsNullOrEmpty(current.Model) ? "" : " ", current.Model, String.IsNullOrEmpty(fullStoneDescription) ? "" : " \r\n", fullStoneDescription);
            //        myDataRow["Column4"] = "--";
            //        myDataRow["Column5"] = current.KtIDShortDescription;
            //        myDataRow["Column6"] = UnitDescription;
            //        myDataRow["Column7"] = String.Format(CultureInfo.InvariantCulture, "{0:N3}", UnitQuantity).ToString();
            //        myDataRow["Column8"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", current.TagPriceWithVAT).ToString();
            //        myDataRow["Column9"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", current.TagPriceWithOutVAT).ToString();
            //        myDataRow["Column10"] = String.Concat("без", "\r\n", "акциза");
            //        myDataRow["Column11"] = String.Format(CultureInfo.InvariantCulture, "{0:N0}", NdsValue).ToString();
            //        myDataRow["Column12"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", NdsValueSumm).ToString();
            //        myDataRow["Column13"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", current.TagPriceWithVAT).ToString();
            //        myDataRow["Column14"] = current.TnvedCode;
            //        myDataRow["Column15"] = current.Country;
            //        myDataRow["Column16"] = current.DeclarationNumber;
            //    }
            //    else
            //    {
            //        myDataRow["Column1"] = numberDetails.ToString();
            //        myDataRow["Column2"] = current.Model;
            //        myDataRow["Column3"] = String.Concat(current.ModelDescription, ", партия ", current.PartNumber);
            //        myDataRow["Column4"] = "--";
            //        myDataRow["Column5"] = "";
            //        myDataRow["Column6"] = UnitDescription;
            //        myDataRow["Column7"] = String.Format(CultureInfo.InvariantCulture, "{0:N3}", current.Quantity).ToString();
            //        myDataRow["Column8"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", current.UnitPrice).ToString();
            //        myDataRow["Column9"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", SummWithOutNds).ToString();
            //        myDataRow["Column10"] = String.Concat("без", "\r\n", "акциза");
            //        myDataRow["Column11"] = String.Format(CultureInfo.InvariantCulture, "{0:N0}", NdsValue).ToString();
            //        myDataRow["Column12"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", NdsValueSumm).ToString();
            //        myDataRow["Column13"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", current.TagPriceWithVAT).ToString();
            //        myDataRow["Column14"] = current.TnvedCode;
            //        myDataRow["Column15"] = current.Country;
            //        myDataRow["Column16"] = current.DeclarationNumber;
            //    }

            //    documentDataTable.Rows.Add(myDataRow);
            //    numberDetails++;
            //}

            //myDataRow = documentDataTable.NewRow();
            //myDataRow["Column1"] = "";
            //myDataRow["Column2"] = "";
            //myDataRow["Column3"] = "Всего к оплате";
            //myDataRow["Column4"] = "";
            //myDataRow["Column5"] = "";
            //myDataRow["Column6"] = "";
            //myDataRow["Column7"] = ""; ;
            //myDataRow["Column8"] = "";
            //myDataRow["Column9"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", FullSummWithOutNds).ToString();
            //myDataRow["Column10"] = "X";
            //myDataRow["Column11"] = "X";
            //myDataRow["Column12"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", NdsValueFullSumm).ToString();
            //myDataRow["Column13"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", documentPrint.SummaSumm).ToString();
            //myDataRow["Column14"] = "";
            //myDataRow["Column15"] = "";
            //myDataRow["Column16"] = "";
            //documentDataTable.Rows.Add(myDataRow);

            //reportData.documentDataTable = documentDataTable;

            //if (reportData.documentDataTable.Rows.Count > 5000)
            //    reportData.isMemoryStream = false;
            //else
            //    reportData.isMemoryStream = true;

            //reportData.documentNumber = documentPrint.TransferDocumentNumber.ToString();

            //if (documentPrint.SyncDate != null)
            //    reportData.documentDate = documentPrint.SyncDate?.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            
            //reportData.documentBase = documentPrint.Contract;
            //reportData.documentRecieverShopNumber = documentPrint.ReceiverID.ToString();

            //if (_locnumeric.businessData.PowerFranchBoolean == true)
            //{
            //    if (isTransferDocument)
            //    {
            //        reportData.documentSenderOrganization = _locnumeric.requisites["FullCompanyName"];
            //        reportData.documentSenderAdress = String.Concat(_locnumeric.requisites["CompanyAddress"], ", (Магазин №", documentPrint.SenderID.ToString(), ")");
            //        reportData.documentSenderInnKpp = String.Concat(_locnumeric.requisites["INN"], "/", _locnumeric.requisites["KPP"]);
            //        reportData.documentSenderOrganizationAdress = "Он же";
            //        reportData.documentSenderAccountant = _locnumeric.requisites["SeniorAccountant"];
            //        reportData.documentSenderGeneralDirector = _locnumeric.requisites["GeneralDirector"];
            //        reportData.documentSenderCurrency = String.Concat(_locnumeric.requisites["CurrencyName"], " (", _locnumeric.requisites["CourrencyCode"], ")");
            //        reportData.documentSenderDescription = String.Concat(_locnumeric.requisites["FullCompanyName"],
            //            ", ИНН/КПП ", _locnumeric.requisites["INN"],
            //            "/", _locnumeric.requisites["KPP"]);

            //        reportData.documentReceiverOrganization = _locnumeric.mainCompanyAttributes["FullCompanyName"];
            //        reportData.documentReceiverAdress = _locnumeric.mainCompanyAttributes["CompanyAddress"];
            //        reportData.documentReceiverOrganizationAdress = String.Concat(_locnumeric.mainCompanyAttributes["FullCompanyName"], ", ", _locnumeric.mainCompanyAttributes["CompanyAddress"], ", (Магазин №", documentPrint.ReceiverID.ToString(), ")");
            //        reportData.documentReceiverInnKpp = String.Concat(_locnumeric.mainCompanyAttributes["INN"], "/", _locnumeric.mainCompanyAttributes["KPP"]);
            //        reportData.documentReceiverDescription = String.Concat(_locnumeric.mainCompanyAttributes["FullCompanyName"], ", ИНН/КПП ", _locnumeric.mainCompanyAttributes["INN"], "/", _locnumeric.mainCompanyAttributes["KPP"]);                
            //    }
            //    else
            //    {
            //        reportData.documentSenderOrganization = _locnumeric.mainCompanyAttributes["FullCompanyName"];
            //        reportData.documentSenderAdress = String.Concat(_locnumeric.mainCompanyAttributes["CompanyAddress"], ", (Магазин №", documentPrint.SenderID.ToString(), ")");
            //        reportData.documentSenderInnKpp = String.Concat(_locnumeric.mainCompanyAttributes["INN"], "/", _locnumeric.mainCompanyAttributes["KPP"]);
            //        reportData.documentSenderOrganizationAdress = "Он же";
            //        reportData.documentSenderAccountant = _locnumeric.mainCompanyAttributes["SeniorAccountant"];
            //        reportData.documentSenderGeneralDirector = _locnumeric.mainCompanyAttributes["GeneralDirector"];
            //        reportData.documentSenderCurrency = String.Concat(_locnumeric.mainCompanyAttributes["CurrencyName"], " (", _locnumeric.mainCompanyAttributes["CourrencyCode"], ")");
            //        reportData.documentSenderDescription = String.Concat(_locnumeric.mainCompanyAttributes["FullCompanyName"],
            //            ", ИНН/КПП ", _locnumeric.mainCompanyAttributes["INN"],
            //            "/", _locnumeric.mainCompanyAttributes["KPP"]);

            //        reportData.documentReceiverOrganization = _locnumeric.requisites["FullCompanyName"];
            //        reportData.documentReceiverAdress = _locnumeric.requisites["CompanyAddress"];
            //        reportData.documentReceiverOrganizationAdress = String.Concat(_locnumeric.requisites["FullCompanyName"], ", ", _locnumeric.requisites["CompanyAddress"], ", (Магазин №", documentPrint.ReceiverID.ToString(), ")");
            //        reportData.documentReceiverInnKpp = String.Concat(_locnumeric.requisites["INN"], "/", _locnumeric.requisites["KPP"]);
            //        reportData.documentReceiverDescription = String.Concat(_locnumeric.requisites["FullCompanyName"], ", ИНН/КПП ", _locnumeric.requisites["INN"], "/", _locnumeric.requisites["KPP"]);                  
            //    }
            //}
            //else
            //{
            //    reportData.documentSenderOrganization = _locnumeric.anotherCompanyAttributes[companySenderNumber, "FullCompanyName"];
            //    reportData.documentSenderAdress = String.Concat(_locnumeric.anotherCompanyAttributes[companySenderNumber, "CompanyAddress"], ", (Магазин №", documentPrint.SenderID.ToString(), ")");
            //    reportData.documentSenderInnKpp = String.Concat(_locnumeric.anotherCompanyAttributes[companySenderNumber, "INN"], "/", _locnumeric.anotherCompanyAttributes[companySenderNumber, "KPP"]);
            //    reportData.documentSenderOrganizationAdress = "Он же";
            //    reportData.documentSenderAccountant = _locnumeric.anotherCompanyAttributes[companySenderNumber, "SeniorAccountant"];
            //    reportData.documentSenderGeneralDirector = _locnumeric.anotherCompanyAttributes[companySenderNumber, "GeneralDirector"];
            //    reportData.documentSenderCurrency = String.Concat(_locnumeric.anotherCompanyAttributes[companySenderNumber, "CurrencyName"], " (", _locnumeric.anotherCompanyAttributes[companySenderNumber, "CourrencyCode"], ")");
            //    reportData.documentSenderDescription = String.Concat(_locnumeric.anotherCompanyAttributes[companySenderNumber, "FullCompanyName"],
            //        ", ИНН/КПП ", _locnumeric.anotherCompanyAttributes[companySenderNumber, "INN"],
            //        "/", _locnumeric.anotherCompanyAttributes[companySenderNumber, "KPP"]);


            //    reportData.documentReceiverOrganization = _locnumeric.anotherCompanyAttributes[companyRecieverNumber, "FullCompanyName"];
            //    reportData.documentReceiverAdress = _locnumeric.anotherCompanyAttributes[companyRecieverNumber, "CompanyAddress"];
            //    reportData.documentReceiverOrganizationAdress = String.Concat(_locnumeric.anotherCompanyAttributes[companyRecieverNumber, "FullCompanyName"], ", ", _locnumeric.anotherCompanyAttributes[companySenderNumber, "CompanyAddress"], ", (Магазин №", documentPrint.ReceiverID.ToString(), ")");
            //    reportData.documentReceiverInnKpp = String.Concat(_locnumeric.anotherCompanyAttributes[companyRecieverNumber, "INN"], "/", _locnumeric.anotherCompanyAttributes[companySenderNumber, "KPP"]);
            //    reportData.documentReceiverDescription = String.Concat(_locnumeric.anotherCompanyAttributes[companyRecieverNumber, "FullCompanyName"], ", ИНН/КПП ", _locnumeric.anotherCompanyAttributes[companySenderNumber, "INN"], "/", _locnumeric.anotherCompanyAttributes[companySenderNumber, "KPP"]);              
            //}

            //if (documentPrint.SyncDate != null)
            //    reportData.documentSenderDate = documentPrint.SyncDate?.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            //if (_locnumeric.businessData.menuOption["Contracts"].ToString().ToLower().Trim() == "true")
            //{
            //    reportData.documentContractNumber = documentPrint.ContractNumberInternalLong;
            //}
            //else
            //{
            //    reportData.documentContractNumber = "";
            //}

            //PrintSchetFactura2Document docWindow;
            //docWindow = new PrintSchetFactura2Document(reportData, progressBarData);
            //docWindow.CreateXps();
        }

        public void PrintDocumentTorg12(FlexProgressBarData progressBarData)
        {
           // Report.Torg12Document.ReportData reportData;
           // reportData = new Report.Torg12Document.ReportData();

           // DataTable documentDataTable = new DataTable("documentDataTable");
           // for (Int32 i = 1; i <= 16; i++)
           //     documentDataTable.Columns.Add(String.Concat("Column", i.ToString()), typeof(string));

           // foreach (LocaleRow current in datalistdetails)
           // {
           //     NdsValue = (current.TagPriceWithVAT - current.TagPriceWithOutVAT) * 100 / current.TagPriceWithOutVAT;
           //     FullSummWithOutNds = FullSummWithOutNds + current.TagPriceWithOutVAT;
           //     NdsValueSumm = current.TagPriceWithVAT - current.TagPriceWithOutVAT;
           //     NdsValueFullSumm = NdsValueFullSumm + NdsValueSumm;

           //     myDataRow = documentDataTable.NewRow();

           //     if (isRelatedDocument == false)
           //     {
           //         GetStoneDescription(current.ID);
           //         myDataRow["Column1"] = numberDetails.ToString();
           //         myDataRow["Column2"] = String.Concat(current.Type, " ", current.Model, String.IsNullOrEmpty(fullStoneDescription.Trim()) ? "" : "\r\n" + fullStoneDescription);
           //         myDataRow["Column3"] = current.KtIDShortDescription;
           //         if (String.IsNullOrEmpty(current.UIN))
           //         {
           //             myDataRow["Column4"] = String.Concat(current.ID);
           //         }
           //         else
           //         {
           //             current.UIN = current.UIN.Replace(","," ");
           //             myDataRow["Column4"] = String.Concat(current.ID, Environment.NewLine, current.UIN);
           //         }
           //         myDataRow["Column5"] = UnitDescription;
           //         myDataRow["Column6"] = "";
           //         myDataRow["Column7"] = "";
           //         myDataRow["Column8"] = "";
           //         myDataRow["Column9"] = String.Format(CultureInfo.InvariantCulture, "{0:N3}", UnitQuantity).ToString();
           //         myDataRow["Column10"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", current.Weight).ToString();
           //         myDataRow["Column11"] = "";
           //         myDataRow["Column12"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", current.TagPriceWithVAT).ToString();
           //         myDataRow["Column13"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", current.TagPriceWithOutVAT).ToString();
           //         documentPrint.QuantitySumm = documentPrint.QuantitySumm + 1;
           //         myDataRow["Column14"] = String.Format(CultureInfo.InvariantCulture, "{0:N0}", NdsValue).ToString();
           //         myDataRow["Column15"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", NdsValueSumm).ToString();
           //         myDataRow["Column16"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", current.TagPriceWithVAT).ToString();
           //     }
           //     else
           //     {
           //         myDataRow["Column1"] = numberDetails.ToString();
           //         myDataRow["Column2"] = String.Concat(current.Model, ", ", current.ModelDescription, ", партия ", current.PartNumber);
           //         myDataRow["Column3"] = "";
           //         myDataRow["Column4"] = current.Model;
           //         myDataRow["Column5"] = UnitDescription;
           //         myDataRow["Column6"] = "";
           //         myDataRow["Column7"] = "";
           //         myDataRow["Column8"] = "";
           //         documentPrint.QuantitySumm = documentPrint.QuantitySumm + current.Quantity;
           //         myDataRow["Column9"] = String.Format(CultureInfo.InvariantCulture, "{0:N3}", current.Quantity).ToString();
           //         myDataRow["Column10"] = "";
           //         myDataRow["Column11"] = "";
           //         myDataRow["Column12"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", current.UnitPrice).ToString();
           //         myDataRow["Column13"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", current.TagPriceWithOutVAT).ToString();
           //         myDataRow["Column14"] = String.Format(CultureInfo.InvariantCulture, "{0:N0}", NdsValue).ToString();
           //         myDataRow["Column15"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", NdsValueSumm).ToString();
           //         myDataRow["Column16"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", current.TagPriceWithVAT).ToString();
           //     }

           //     documentDataTable.Rows.Add(myDataRow);
           //     numberDetails++;
           // }

           // myDataRow = documentDataTable.NewRow();
           // myDataRow["Column1"] = "";
           // myDataRow["Column2"] = "Итого по накладной";
           // myDataRow["Column3"] = "";
           // myDataRow["Column4"] = "";
           // myDataRow["Column5"] = "";
           // myDataRow["Column6"] = "";
           // myDataRow["Column7"] = "";
           // myDataRow["Column8"] = "";
           // myDataRow["Column9"] = String.Format(CultureInfo.InvariantCulture, "{0:N3}", documentPrint.QuantitySumm).ToString();
           // myDataRow["Column10"] = documentPrint.WeightSumm == 0 ? "" : String.Format(CultureInfo.InvariantCulture, "{0:N2}", documentPrint.WeightSumm).ToString();
           // myDataRow["Column11"] = "";
           // myDataRow["Column12"] = "";
           // myDataRow["Column13"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", FullSummWithOutNds).ToString();
           // myDataRow["Column14"] = "";
           // myDataRow["Column15"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", NdsValueFullSumm).ToString();
           // myDataRow["Column16"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", documentPrint.SummaSumm).ToString();
           // documentDataTable.Rows.Add(myDataRow);

           // reportData.documentDataTable = documentDataTable;

           // if (reportData.documentDataTable.Rows.Count > 5000)
           //     reportData.isMemoryStream = false;
           // else
           //     reportData.isMemoryStream = true;

           // reportData.documentNumber = documentPrint.TransferDocumentNumber.ToString();
           // reportData.documentSenderContractNumber = "";

           // if (documentPrint.SyncDate != null)
           //     reportData.documentDate = documentPrint.SyncDate?.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

           // reportData.documentStructUnit = String.Concat(
           //    "Магазин №", documentPrint.SenderID.ToString(),
           //    ", ",
           //    shopSenderFullAddress
           //);

           // if (_locnumeric.businessData.PowerFranchBoolean == true)
           // {
           //     if (isTransferDocument)
           //     {
           //         reportData.documentSenderOrganizationAdress = String.Concat(
           //             _locnumeric.requisites["ShortCompanyName"], ", ",
           //             _locnumeric.requisites["BankAddress"], ", ",
           //             _locnumeric.requisites["CompanyAddress"], ", ",
           //             "тел. ", _locnumeric.requisites["Phone"], ", ",
           //             "ИНН/КПП ", _locnumeric.requisites["INN"], "/",
           //             _locnumeric.requisites["KPP"], ", ",
           //             "p/c ", _locnumeric.requisites["SettlementAccount"], ", ",
           //             "в ", _locnumeric.requisites["BankName"], ", ",
           //             " к/c ", _locnumeric.requisites["CorrespondentAccount"], ", ",
           //             " БИК ", _locnumeric.requisites["RCBIC"], "."
           //             );

           //         reportData.documentSenderAccountant = _locnumeric.requisites["SeniorAccountant"];
           //         reportData.documentSenderGeneralDirector = _locnumeric.requisites["GeneralDirector"];
           //         reportData.documentSenderCurrency = String.Concat(_locnumeric.requisites["CurrencyName"], " (", _locnumeric.requisites["CourrencyCode"], ")");

           //         reportData.documentSenderCodeOKUD = _locnumeric.requisites["Trade12OCUD"];
           //         reportData.documentSenderCodeOKPO1 = _locnumeric.requisites["Trade12OCUDOKPO"];
           //         reportData.documentSenderCodeOKPO2 = _locnumeric.requisites["Trade12OKDPOKPO"];
           //         reportData.documentSenderContractNumber = documentPrint.ContractNumberInternalLong;

           //         reportData.documentReceiverOrganizationAdress = String.Concat(
           //              _locnumeric.mainCompanyAttributes["ShortCompanyName"], ", ",
           //             _locnumeric.mainCompanyAttributes["BankAddress"], ", ",
           //             _locnumeric.mainCompanyAttributes["CompanyAddress"], ", ",
           //             "тел. ", _locnumeric.mainCompanyAttributes["Phone"], ", ",
           //             "ИНН/КПП ", _locnumeric.mainCompanyAttributes["INN"], "/",
           //             _locnumeric.mainCompanyAttributes["KPP"], ", ",
           //             "p/c ", _locnumeric.mainCompanyAttributes["SettlementAccount"], ", ",
           //             "в ", _locnumeric.mainCompanyAttributes["BankName"], ", ",
           //             " к/c ", _locnumeric.mainCompanyAttributes["CorrespondentAccount"], ", ",
           //             " БИК ", _locnumeric.mainCompanyAttributes["RCBIC"], "."
           //         );

           //         reportData.documentReceiverShopAddress = String.Concat("Магазин №", documentPrint.ReceiverID, ", ", shopRecieverFullAddress);

           //         reportData.documentReceiverPayer = String.Concat(_locnumeric.mainCompanyAttributes["ShortCompanyName"], ", ",
           //    _locnumeric.mainCompanyAttributes["BankAddress"], ", ",
           //    _locnumeric.mainCompanyAttributes["CompanyAddress"], ", ",
           //    "тел. ", _locnumeric.mainCompanyAttributes["Phone"], ", ",
           //    "ИНН/КПП ", _locnumeric.mainCompanyAttributes["INN"], "/",
           //    _locnumeric.mainCompanyAttributes["KPP"], ", ",
           //    "p/c ", _locnumeric.mainCompanyAttributes["SettlementAccount"], ", ",
           //    "в ", _locnumeric.mainCompanyAttributes["BankName"], ", ",
           //    " к/c ", _locnumeric.mainCompanyAttributes["CorrespondentAccount"], ", ",
           //    " БИК ", _locnumeric.mainCompanyAttributes["RCBIC"], ".");

           //     }
           //     else
           //     {
           //         reportData.documentSenderOrganizationAdress = String.Concat(
           //             _locnumeric.mainCompanyAttributes["ShortCompanyName"], ", ",
           //             _locnumeric.mainCompanyAttributes["BankAddress"], ", ",
           //             _locnumeric.mainCompanyAttributes["CompanyAddress"], ", ",
           //             "тел. ", _locnumeric.mainCompanyAttributes["Phone"], ", ",
           //             "ИНН/КПП ", _locnumeric.mainCompanyAttributes["INN"], "/",
           //             _locnumeric.mainCompanyAttributes["KPP"], ", ",
           //             "p/c ", _locnumeric.mainCompanyAttributes["SettlementAccount"], ", ",
           //             "в ", _locnumeric.mainCompanyAttributes["BankName"], ", ",
           //             " к/c ", _locnumeric.mainCompanyAttributes["CorrespondentAccount"], ", ",
           //             " БИК ", _locnumeric.mainCompanyAttributes["RCBIC"], "."
           //             );

           //         reportData.documentSenderAccountant = _locnumeric.mainCompanyAttributes["SeniorAccountant"];
           //         reportData.documentSenderGeneralDirector = _locnumeric.mainCompanyAttributes["GeneralDirector"];
           //         reportData.documentSenderCurrency = String.Concat(_locnumeric.mainCompanyAttributes["CurrencyName"], " (", _locnumeric.mainCompanyAttributes["CourrencyCode"], ")");

           //         reportData.documentSenderCodeOKUD = _locnumeric.mainCompanyAttributes["Trade12OCUD"];
           //         reportData.documentSenderCodeOKPO1 = _locnumeric.mainCompanyAttributes["Trade12OCUDOKPO"];
           //         reportData.documentSenderCodeOKPO2 = _locnumeric.mainCompanyAttributes["Trade12OKDPOKPO"];
           //         reportData.documentSenderContractNumber = documentPrint.ContractNumberInternalLong;

           //         reportData.documentReceiverOrganizationAdress = String.Concat(
           //              _locnumeric.requisites["ShortCompanyName"], ", ",
           //             _locnumeric.requisites["BankAddress"], ", ",
           //             _locnumeric.requisites["CompanyAddress"], ", ",
           //             "тел. ", _locnumeric.requisites["Phone"], ", ",
           //             "ИНН/КПП ", _locnumeric.requisites["INN"], "/",
           //             _locnumeric.requisites["KPP"], ", ",
           //             "p/c ", _locnumeric.requisites["SettlementAccount"], ", ",
           //             "в ", _locnumeric.requisites["BankName"], ", ",
           //             " к/c ", _locnumeric.requisites["CorrespondentAccount"], ", ",
           //             " БИК ", _locnumeric.requisites["RCBIC"], "."
           //         );

           //         reportData.documentReceiverShopAddress = String.Concat("Магазин №", documentPrint.ReceiverID, ", ", shopRecieverFullAddress);

           //         reportData.documentReceiverPayer = String.Concat(_locnumeric.requisites["ShortCompanyName"], ", ",
           //    _locnumeric.requisites["BankAddress"], ", ",
           //    _locnumeric.requisites["CompanyAddress"], ", ",
           //    "тел. ", _locnumeric.requisites["Phone"], ", ",
           //    "ИНН/КПП ", _locnumeric.requisites["INN"], "/",
           //    _locnumeric.requisites["KPP"], ", ",
           //    "p/c ", _locnumeric.requisites["SettlementAccount"], ", ",
           //    "в ", _locnumeric.requisites["BankName"], ", ",
           //    " к/c ", _locnumeric.requisites["CorrespondentAccount"], ", ",
           //    " БИК ", _locnumeric.requisites["RCBIC"], ".");

           //     }
           // }
           // else
           // {
           //     reportData.documentSenderOrganizationAdress = String.Concat(
           //             _locnumeric.anotherCompanyAttributes[companySenderNumber, "ShortCompanyName"], ", ",
           //             _locnumeric.anotherCompanyAttributes[companySenderNumber, "BankAddress"], ", ",
           //             _locnumeric.anotherCompanyAttributes[companySenderNumber, "CompanyAddress"], ", ",
           //             "тел. ", _locnumeric.anotherCompanyAttributes[companySenderNumber, "Phone"], ", ",
           //             "ИНН/КПП ", _locnumeric.anotherCompanyAttributes[companySenderNumber, "INN"], "/",
           //             _locnumeric.anotherCompanyAttributes[companySenderNumber, "KPP"], ", ",
           //             "p/c ", _locnumeric.anotherCompanyAttributes[companySenderNumber, "SettlementAccount"], ", ",
           //             "в ", _locnumeric.anotherCompanyAttributes[companySenderNumber, "BankName"], ", ",
           //             " к/c ", _locnumeric.anotherCompanyAttributes[companySenderNumber, "CorrespondentAccount"], ", ",
           //             " БИК ", _locnumeric.anotherCompanyAttributes[companySenderNumber, "RCBIC"], "."
           //             );

           //     reportData.documentSenderAccountant = _locnumeric.anotherCompanyAttributes[companySenderNumber, "SeniorAccountant"];
           //     reportData.documentSenderGeneralDirector = _locnumeric.anotherCompanyAttributes[companySenderNumber, "GeneralDirector"];
           //     reportData.documentSenderCurrency = String.Concat(_locnumeric.anotherCompanyAttributes[companySenderNumber, "CurrencyName"], " (", _locnumeric.anotherCompanyAttributes[companySenderNumber, "CourrencyCode"], ")");

           //     reportData.documentSenderCodeOKUD = _locnumeric.anotherCompanyAttributes[companySenderNumber, "Trade12OCUD"];
           //     reportData.documentSenderCodeOKPO1 = _locnumeric.anotherCompanyAttributes[companySenderNumber, "Trade12OCUDOKPO"];
           //     reportData.documentSenderCodeOKPO2 = _locnumeric.anotherCompanyAttributes[companySenderNumber, "Trade12OKDPOKPO"];
           //     reportData.documentSenderContractNumber = documentPrint.ContractNumberInternalLong;

           //     reportData.documentReceiverOrganizationAdress = String.Concat(
           //          _locnumeric.anotherCompanyAttributes[companyRecieverNumber, "ShortCompanyName"], ", ",
           //         _locnumeric.anotherCompanyAttributes[companyRecieverNumber, "BankAddress"], ", ",
           //         _locnumeric.anotherCompanyAttributes[companyRecieverNumber, "CompanyAddress"], ", ",
           //         "тел. ", _locnumeric.anotherCompanyAttributes[companyRecieverNumber, "Phone"], ", ",
           //         "ИНН/КПП ", _locnumeric.anotherCompanyAttributes[companyRecieverNumber, "INN"], "/", _locnumeric.anotherCompanyAttributes[companyRecieverNumber, "KPP"], ", ",
           //         "p/c ", _locnumeric.anotherCompanyAttributes[companyRecieverNumber, "SettlementAccount"], ", ",
           //         "в ", _locnumeric.anotherCompanyAttributes[companyRecieverNumber, "BankName"], ", ",
           //         " к/c ", _locnumeric.anotherCompanyAttributes[companyRecieverNumber, "CorrespondentAccount"], ", ",
           //         " БИК ", _locnumeric.anotherCompanyAttributes[companyRecieverNumber, "RCBIC"], "."
           //     );

           //     reportData.documentReceiverShopAddress = String.Concat("Магазин №", documentPrint.ReceiverID, ", ", shopRecieverFullAddress);

           //     reportData.documentReceiverPayer = String.Concat(_locnumeric.anotherCompanyAttributes[companyRecieverNumber, "ShortCompanyName"], ", ",
           //     _locnumeric.anotherCompanyAttributes[companyRecieverNumber, "BankAddress"], ", ",
           //     _locnumeric.anotherCompanyAttributes[companyRecieverNumber, "CompanyAddress"], ", ",
           //     "тел. ", _locnumeric.anotherCompanyAttributes[companyRecieverNumber, "Phone"], ", ",
           //     "ИНН/КПП ", _locnumeric.anotherCompanyAttributes[companyRecieverNumber, "INN"], "/", _locnumeric.anotherCompanyAttributes[companySenderNumber, "KPP"], ", ",
           //     "p/c ", _locnumeric.anotherCompanyAttributes[companyRecieverNumber, "SettlementAccount"], ", ",
           //     "в ", _locnumeric.anotherCompanyAttributes[companyRecieverNumber, "BankName"], ", ",
           //     " к/c ", _locnumeric.anotherCompanyAttributes[companyRecieverNumber, "CorrespondentAccount"], ", ",
           //     " БИК ", _locnumeric.anotherCompanyAttributes[companyRecieverNumber, "RCBIC"], ".");
           // }

           // reportData.recieverShopNumber = documentPrint.ReceiverID.ToString();
           // reportData.documentSupplier = "Он же";
           // reportData.documentBase = documentPrint.Contract;
            
            
           // reportData.documentSummaString = DigitalToString.GetStringCurrency(convertdata.FlexDataConvertToDouble(documentPrint.SummaSumm.ToString()));
           // PrintTorg12Document docWindow;
           // docWindow = new PrintTorg12Document(reportData, progressBarData);
           // docWindow.CreateXps();
        }

        public void PrintDocumentTorg13(FlexProgressBarData progressBarData)
        {
            //Report.Torg13Document.ReportData reportData;
            //reportData = new Report.Torg13Document.ReportData();
            //reportData = GenerateDocumentTorg13(progressBarData, reportData);

            //PrintTorg13Document docWindow;
            //docWindow = new PrintTorg13Document(reportData, progressBarData);
            //docWindow.CreateXps();
        }

        public byte[] GetPdfByteTorg13(FlexProgressBarData progressBarData)
        {
            //Report.Torg13Document.ReportData reportData;
            //reportData = new Report.Torg13Document.ReportData();
            //reportData = GenerateDocumentTorg13(progressBarData, reportData);

            //PrintTorg13Document docWindow;
            //docWindow = new PrintTorg13Document(reportData, progressBarData);
            //progressBarData.PbStatusStart = true;
            //docWindow.CreateXps();

            //XpsDocument xpsDocumentSource = progressBarData.XpsDoc;
            String pathPdf = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
            //using (MemoryStream memoryStream = new MemoryStream())
            //{
            //    System.IO.Packaging.Package package = System.IO.Packaging.Package.Open(memoryStream, FileMode.Create);
            //    XpsDocument xpsDocument = new XpsDocument(package);
            //    XpsDocumentWriter xpsDocumentWriter = XpsDocument.CreateXpsDocumentWriter(xpsDocument);

            //    xpsDocumentWriter.Write(xpsDocumentSource.GetFixedDocumentSequence());
            //    xpsDocument.Close();
            //    package.Close();

            //    var pdfXpsDoc = PdfSharp.Xps.XpsModel.XpsDocument.Open(memoryStream);
            //    XpsConverter.Convert(pdfXpsDoc, pathPdf, 0);
            //}

            return File.ReadAllBytes(pathPdf);
        }

        //public Report.Torg13Document.ReportData GenerateDocumentTorg13(FlexProgressBarData progressBarData, Report.Torg13Document.ReportData reportData)
        //{
        //    DataTable documentDataTable = new DataTable("documentDataTable");
        //    for (Int32 i = 1; i <= 13; i++)
        //        documentDataTable.Columns.Add(String.Concat("Column", i.ToString()), typeof(string));

        //    foreach (LocaleRow current in datalistdetails)
        //    {
        //        Double WeightDiff = 0;
        //        Double WeightNetto = 0;

        //        myDataRow = documentDataTable.NewRow();

        //        if (isRelatedDocument == false)
        //        {
        //            GetStoneDescription(current.ID);
        //            myDataRow["Column1"] = numberDetails.ToString();
        //            myDataRow["Column2"] = String.Concat(current.Type, " ", current.Model, String.IsNullOrEmpty(fullStoneDescription.Trim()) ? "" : "\r\n" + fullStoneDescription);
        //            myDataRow["Column3"] = current.ID;
        //            myDataRow["Column4"] = "";
        //            myDataRow["Column5"] = current.KtIDShortDescription;
        //            myDataRow["Column6"] = UnitDescription;
        //            myDataRow["Column7"] = "";
        //            myDataRow["Column8"] = "";
        //            documentPrint.QuantitySumm = documentPrint.QuantitySumm + 1;
        //            myDataRow["Column9"] = String.Format(CultureInfo.InvariantCulture, "{0:N3}", UnitQuantity).ToString();
        //            myDataRow["Column10"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", current.Weight).ToString();
        //            WeightNetto = current.Weight - WeightDiff;
        //            myDataRow["Column11"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", WeightNetto).ToString();
        //            WeightNettoSumm = WeightNettoSumm + WeightNetto;
        //            UnitPriceSumm = UnitPriceSumm + current.TagPriceWithVAT;
        //            myDataRow["Column12"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", current.TagPriceWithVAT).ToString();
        //            myDataRow["Column13"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", current.TagPriceWithVAT).ToString();
        //        }
        //        else
        //        {
        //            myDataRow["Column1"] = numberDetails.ToString();
        //            myDataRow["Column2"] = String.Concat(current.Model, ", ", current.ModelDescription, ", партия ", current.PartNumber);
        //            myDataRow["Column3"] = current.Model;
        //            myDataRow["Column4"] = "";
        //            myDataRow["Column5"] = "";
        //            myDataRow["Column6"] = UnitDescription;
        //            myDataRow["Column7"] = "";
        //            myDataRow["Column8"] = "";
        //            documentPrint.QuantitySumm = documentPrint.QuantitySumm + current.Quantity;
        //            myDataRow["Column9"] = String.Format(CultureInfo.InvariantCulture, "{0:N3}", current.Quantity).ToString();
        //            myDataRow["Column10"] = "";
        //            myDataRow["Column11"] = "";
        //            UnitPriceSumm = UnitPriceSumm + current.UnitPrice;
        //            myDataRow["Column12"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", current.UnitPrice).ToString();
        //            myDataRow["Column13"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", current.TagPriceWithVAT).ToString();
        //        }
        //        documentDataTable.Rows.Add(myDataRow);
        //        numberDetails++;
        //    }

        //    myDataRow = documentDataTable.NewRow();
        //    myDataRow["Column1"] = "";
        //    myDataRow["Column2"] = "Итого";
        //    myDataRow["Column3"] = "";
        //    myDataRow["Column4"] = "";
        //    myDataRow["Column5"] = "";
        //    myDataRow["Column6"] = "";
        //    myDataRow["Column7"] = "";
        //    myDataRow["Column8"] = "";
        //    myDataRow["Column9"] = String.Format(CultureInfo.InvariantCulture, "{0:N3}", documentPrint.QuantitySumm).ToString();
        //    myDataRow["Column10"] = documentPrint.WeightSumm == 0 ? "" : String.Format(CultureInfo.InvariantCulture, "{0:N2}", documentPrint.WeightSumm).ToString();
        //    myDataRow["Column11"] = documentPrint.WeightSumm == 0 ? "" : String.Format(CultureInfo.InvariantCulture, "{0:N2}", WeightNettoSumm).ToString();
        //    myDataRow["Column12"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", UnitPriceSumm).ToString();
        //    myDataRow["Column13"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", documentPrint.SummaSumm).ToString();
        //    documentDataTable.Rows.Add(myDataRow);

        //    myDataRow = documentDataTable.NewRow();
        //    myDataRow["Column1"] = "";
        //    myDataRow["Column2"] = "Итого по накладной";
        //    myDataRow["Column3"] = "";
        //    myDataRow["Column4"] = "";
        //    myDataRow["Column5"] = "";
        //    myDataRow["Column6"] = "";
        //    myDataRow["Column7"] = "";
        //    myDataRow["Column8"] = "";
        //    myDataRow["Column9"] = String.Format(CultureInfo.InvariantCulture, "{0:N3}", documentPrint.QuantitySumm).ToString();
        //    myDataRow["Column10"] = documentPrint.WeightSumm == 0 ? "" : String.Format(CultureInfo.InvariantCulture, "{0:N2}", documentPrint.WeightSumm).ToString();
        //    myDataRow["Column11"] = documentPrint.WeightSumm == 0 ? "" : String.Format(CultureInfo.InvariantCulture, "{0:N2}", WeightNettoSumm).ToString();
        //    myDataRow["Column12"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", UnitPriceSumm).ToString();
        //    myDataRow["Column13"] = String.Format(CultureInfo.InvariantCulture, "{0:N2}", documentPrint.SummaSumm).ToString();
        //    documentDataTable.Rows.Add(myDataRow);

        //    reportData.documentNumber = documentPrint.TransferDocumentNumber.ToString();
        //    reportData.documentDataTable = documentDataTable;

        //    if (reportData.documentDataTable.Rows.Count > 5000)
        //        reportData.isMemoryStream = false;
        //    else
        //        reportData.isMemoryStream = true;

        //    if (documentPrint.SyncDate != null)
        //        reportData.documentDate = documentPrint.SyncDate?.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

        //    reportData.documentSender = String.Concat("Магазин №", documentPrint.SenderID.ToString());
        //    reportData.documentReceiver = String.Concat("Магазин №", documentPrint.ReceiverID.ToString());
        //    reportData.recieverShopNumber = documentPrint.ReceiverID.ToString();
        //    reportData.documentRecieverAddress = shopRecieverFullAddress;
        //    reportData.documentSenderAddress = shopSenderFullAddress;

        //    String companySenderFullName;
        //    String companySenderАddress;

        //    if (_locnumeric.businessData.PowerFranchBoolean == true)
        //    {
        //        if (isTransferDocument)
        //        {
        //            companySenderFullName = _locnumeric.requisites["FullCompanyName"];
        //            companySenderАddress = _locnumeric.requisites["CompanyAddress"];

        //            reportData.documentSenderCurrency = String.Concat(_locnumeric.requisites["CurrencyName"], " (", _locnumeric.requisites["CourrencyCode"], ")");
        //            reportData.documentSenderCodeOKUD = _locnumeric.requisites["Trade13OKUD"];
        //            reportData.documentSenderCodeOKPO = _locnumeric.requisites["Trade13OKPO"];
        //            reportData.documentSenderCodeOKDP = _locnumeric.requisites["Trade13OKDP"];
        //        }
        //        else 
        //        {
        //            companySenderFullName = _locnumeric.mainCompanyAttributes["FullCompanyName"];
        //            companySenderАddress = _locnumeric.mainCompanyAttributes["CompanyAddress"];

        //            reportData.documentSenderCurrency = String.Concat(_locnumeric.mainCompanyAttributes["CurrencyName"], " (", _locnumeric.mainCompanyAttributes["CourrencyCode"], ")");
        //            reportData.documentSenderCodeOKUD = _locnumeric.mainCompanyAttributes["Trade13OKUD"];
        //            reportData.documentSenderCodeOKPO = _locnumeric.mainCompanyAttributes["Trade13OKPO"];
        //            reportData.documentSenderCodeOKDP = _locnumeric.mainCompanyAttributes["Trade13OKDP"];
        //        }
        //    }
        //    else
        //    {
        //        companySenderFullName = _locnumeric.anotherCompanyAttributes[companySenderNumber, "FullCompanyName"];
        //        companySenderАddress = _locnumeric.anotherCompanyAttributes[companySenderNumber, "CompanyAddress"];

        //        reportData.documentSenderCurrency = String.Concat(_locnumeric.anotherCompanyAttributes[companySenderNumber, "CurrencyName"], " (", _locnumeric.anotherCompanyAttributes[companySenderNumber, "CourrencyCode"], ")");
        //        reportData.documentSenderCodeOKUD = _locnumeric.anotherCompanyAttributes[companySenderNumber, "Trade13OKUD"];
        //        reportData.documentSenderCodeOKPO = _locnumeric.anotherCompanyAttributes[companySenderNumber, "Trade13OKPO"];
        //        reportData.documentSenderCodeOKDP = _locnumeric.anotherCompanyAttributes[companySenderNumber, "Trade13OKDP"];
        //    }

        //    reportData.documentSenderOrganizationAdress = String.Concat(companySenderFullName, ", ", companySenderАddress);
        //    reportData.documentSummaString = DigitalToString.GetStringCurrency(convertdata.FlexDataConvertToDouble(documentPrint.SummaSumm.ToString()));

        //    if (_locnumeric.businessData.menuOption["Contracts"].ToString().ToLower().Trim() == "true")
        //    {
        //        reportData.documentContractNumber = documentPrint.ContractNumberInternalLong;
        //    }
        //    else
        //    {
        //        reportData.documentContractNumber = "";
        //    }

        //    return reportData;
        //}
    
    }
}