using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyProfile.Personify
{
    class Class1
    {
    }
}

// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true)]
[System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
public partial class StoredProcedureOutput
{
    private string dataField;

    private object operationResultField;

    /// <remarks/>
    public string Data
    {
        get { return dataField; }
        set { dataField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElement(IsNullable = true)]
    public object operationResult
    {
        get { return operationResultField; }
        set { operationResultField = value; }
    }
}


// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class PsfyGeneralInfoContainer
{

    private DataNewDataSet newDataSetField;

    /// <remarks/>
    public DataNewDataSet NewDataSet
    {
        get { return newDataSetField; }
        set { newDataSetField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataNewDataSet
{

    private DataNewDataSetTable tableField;

    /// <remarks/>
    public DataNewDataSetTable Table
    {
        get { return tableField; }
        set { tableField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataNewDataSetTable
{

    private string business_HoursField;

    private ushort year_EstablishedField;

    private decimal sales_Volume_for_12_monthsField;

    private decimal advertising_Specialty_Sales_VolumeField;

    private string number_Of_EmployeesField;

    private object about_UsField;

    private object female_OwnedField;

    private object veteran_OwnedField;

    private object asian_OwnedField;

    private object hispanic_OwnedField;

    private object african_American_OwnedField;

    private object native_American_OwnedField;

    private object jewish_OwnedField;

    private object disabled_OwnedField;

    private object eSOPField;

    private object cert_AvailableField;

    private object small_DisadvantageField;

    private object lGBTQ_OwnedField;

    /// <remarks/>
    public string Business_Hours
    {
        get { return business_HoursField; }
        set { business_HoursField = value; }
    }

    /// <remarks/>
    public ushort Year_Established
    {
        get { return year_EstablishedField; }
        set { year_EstablishedField = value; }
    }

    /// <remarks/>
    public decimal Sales_Volume_for_12_months
    {
        get { return sales_Volume_for_12_monthsField; }
        set { sales_Volume_for_12_monthsField = value; }
    }

    /// <remarks/>
    public decimal Advertising_Specialty_Sales_Volume
    {
        get { return advertising_Specialty_Sales_VolumeField; }
        set { advertising_Specialty_Sales_VolumeField = value; }
    }

    /// <remarks/>
    public string Number_Of_Employees
    {
        get { return number_Of_EmployeesField; }
        set { number_Of_EmployeesField = value; }
    }

    /// <remarks/>
    public object About_Us
    {
        get { return about_UsField; }
        set { about_UsField = value; }
    }

    /// <remarks/>
    public object Female_Owned
    {
        get { return female_OwnedField; }
        set { female_OwnedField = value; }
    }

    /// <remarks/>
    public object Veteran_Owned
    {
        get { return veteran_OwnedField; }
        set { veteran_OwnedField = value; }
    }

    /// <remarks/>
    public object Asian_Owned
    {
        get { return asian_OwnedField; }
        set { asian_OwnedField = value; }
    }

    /// <remarks/>
    public object Hispanic_Owned
    {
        get { return hispanic_OwnedField; }
        set { hispanic_OwnedField = value; }
    }

    /// <remarks/>
    public object African_American_Owned
    {
        get { return african_American_OwnedField; }
        set { african_American_OwnedField = value; }
    }

    /// <remarks/>
    public object Native_American_Owned
    {
        get { return native_American_OwnedField; }
        set { native_American_OwnedField = value; }
    }

    /// <remarks/>
    public object Jewish_Owned
    {
        get { return jewish_OwnedField; }
        set { jewish_OwnedField = value; }
    }

    /// <remarks/>
    public object Disabled_Owned
    {
        get { return disabled_OwnedField; }
        set { disabled_OwnedField = value; }
    }

    /// <remarks/>
    public object ESOP
    {
        get { return eSOPField; }
        set { eSOPField = value; }
    }

    /// <remarks/>
    public object Cert_Available
    {
        get { return cert_AvailableField; }
        set { cert_AvailableField = value; }
    }

    /// <remarks/>
    public object Small_Disadvantage
    {
        get { return small_DisadvantageField; }
        set { small_DisadvantageField = value; }
    }

    /// <remarks/>
    public object LGBTQ_Owned
    {
        get { return lGBTQ_OwnedField; }
        set { lGBTQ_OwnedField = value; }
    }
}

