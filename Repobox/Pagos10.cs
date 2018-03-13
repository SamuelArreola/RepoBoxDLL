using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.Data;
using System.IO;
using System.Drawing;
using System.Xml.Xsl;
using System.Security;
using System.Security.Cryptography;

namespace RepoBox.Pagos10 {
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/Pagos")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://www.sat.gob.mx/Pagos", IsNullable=false)]
    public partial class Pagos {
        [XmlNamespaceDeclarationsAttribute]
        public XmlSerializerNamespaces myNamespaces = new XmlSerializerNamespaces();

        public Pagos() {
            Version = "1.0";
            this.myNamespaces.Add("pago10", "http://www.sat.gob.mx/Pagos");
        }
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("Pago")]
        public List<Pago> Pago { get; set; }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Version { get; set; }
        public string GenerarCFD()
        {
            try
            {
                //string validacion = this.Validar();
                //if (validacion != "OK")
                //    return validacion;
                XmlDocument document = new XmlDocument();
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(this.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    x.Serialize(stream, this);
                    //electronicDocument.SaveToStream(stream);
                    stream.Position = 0L;
                    StreamReader reader = new StreamReader(stream);
                    string xml = reader.ReadToEnd();
                    reader.Close();
                    document.LoadXml(xml);
                }
                return document.InnerXml;
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("RepoBox"))
                    throw new Exception(ex.Message);
                else
                    throw new Exception("RepoBox: " + ex.Message);
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/Pagos")]
    public partial class Pago {

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("DoctoRelacionado")]
        public List<DoctoRelacionado> DoctoRelacionado { get; set; }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("Impuestos")]
        public List<RepoBox.Pagos10.Impuestos> Impuestos { get; set; }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string FechaPago { get; set; }
        [XmlIgnore]
        public DateTime _FechaPago
        {
            get { return Convert.ToDateTime(FechaPago); }
            set { this.FechaPago = value.Year.ToString("0000") + "-" + value.Month.ToString("00") + "-" + value.Day.ToString("00") + "T" + value.Hour.ToString("00") + ":" + value.Minute.ToString("00") + ":" + value.Second.ToString("00"); }
        }
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string FormaDePagoP { get; set; }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MonedaP { get; set; }

        /// <comentarios/>
        [XmlIgnore]
        private decimal tipoCambioP;
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal TipoCambioP { get { return tipoCambioP; } set { tipoCambioP = value; TipoCambioPSpecified = (value > 0); } }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TipoCambioPSpecified { get; set; }
        private decimal _Monto;
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Monto { get { return _Monto; }
            set { _Monto = Convert.ToDecimal(value.ToString("N2")); }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NumOperacion { get; set; }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RfcEmisorCtaOrd { get; set; }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NomBancoOrdExt { get; set; }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CtaOrdenante { get; set; }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RfcEmisorCtaBen { get; set; }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CtaBeneficiario { get; set; }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TipoCadPago { get; set; }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "base64Binary")]
        public byte[] CertPago { get; set; }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CadPago { get; set; }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "base64Binary")]
        public byte[] SelloPago { get; set; }

        //public string GenerarCFD(string Cer, string Key, string KeyPassword)
        //{
        //    try
        //    {
        //        CertPago = Convert.ToBase64String(File.ReadAllBytes(Cer));
        //        //string validacion = this.Validar();
        //        //if (validacion != "OK")
        //        //    return validacion;
        //        XmlDocument document = new XmlDocument();
        //        System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(this.GetType());
        //        using (MemoryStream stream = new MemoryStream())
        //        {
        //            x.Serialize(stream, this);
        //            //electronicDocument.SaveToStream(stream);
        //            stream.Position = 0L;
        //            StreamReader reader = new StreamReader(stream);
        //            string xml = reader.ReadToEnd();
        //            reader.Close();
        //            document.LoadXml(xml);
        //        }

        //        // Cadena Original
        //        string selloplano = this.GenerarSello(document);
        //        this.SelloPago = this.GetSignData(selloplano, Key, KeyPassword);
        //        if (this.SelloPago == "")
        //            throw new Exception("RepoBox: Sello No Generado");

        //        //x = new System.Xml.Serialization.XmlSerializer(this.GetType());
        //        using (MemoryStream stream = new MemoryStream())
        //        {
        //            x.Serialize(stream, this);
        //            //electronicDocument.SaveToStream(stream);
        //            stream.Position = 0L;
        //            StreamReader reader = new StreamReader(stream);
        //            string xml = reader.ReadToEnd();
        //            reader.Close();
        //            document.LoadXml(xml);
        //        }
        //        return document.InnerXml;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.StartsWith("RepoBox"))
        //            throw new Exception(ex.Message);
        //        else
        //            throw new Exception("RepoBox: " + ex.Message);
        //    }
        //}
        //private string GenerarSello(XmlDocument doc)
        //{
        //    try
        //    {
        //        StringWriter swriter = new StringWriter();
        //        XslCompiledTransform xsl = new XslCompiledTransform();
        //        xsl.Load(@"C:\Repobox\Timbrado\Base\Pagos10.xslt");
        //        XmlTextWriter xmlwriter = new XmlTextWriter(swriter);
        //        xsl.Transform(doc.CreateNavigator(), null, xmlwriter);
        //        return swriter.ToString();
        //    }
        //    catch (Exception)
        //    {
        //        return "";
        //    }
        //}
        //private string GetSignData(string originalData, string Llave, string KeyPass)
        //{
        //    try
        //    {
        //        string s = originalData;
        //        if (!File.Exists(Llave))
        //            throw new Exception("RepoBox: No existe el archivo .KEY Ruta: " + Llave);
        //        SecureString securePassword = new SecureString();
        //        securePassword.Clear();
        //        foreach (char ch in KeyPass.ToCharArray())
        //            securePassword.AppendChar(ch);
        //        RSACryptoServiceProvider provider = RepoBox.RepoboxOpenSSL.DecodeEncryptedPrivateKeyInfo(File.ReadAllBytes(Llave), securePassword);
        //        SHA256CryptoServiceProvider halg = new SHA256CryptoServiceProvider();
        //        return Convert.ToBase64String(provider.SignData(Encoding.UTF8.GetBytes(s), halg));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.StartsWith("RepoBox"))
        //            throw new Exception(ex.Message);
        //        else
        //            throw new Exception("Repobox: Contraseña del archivo KEY es incorrecta.");
        //    }
        //}
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/Pagos")]
    public partial class DoctoRelacionado {
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string IdDocumento { get; set; }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Serie { get; set; }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Folio { get; set; }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MonedaDR { get; set; }

        /// <comentarios/>
        [XmlIgnore]
        private decimal tipoCambioDR;
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal TipoCambioDR { get { return tipoCambioDR; } set { tipoCambioDR = value; TipoCambioDRSpecified = (value > 0); } }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TipoCambioDRSpecified { get; set; }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MetodoDePagoDR { get; set; }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "integer")]
        public string NumParcialidad { get; set; }

        /// <comentarios/>
        [XmlIgnore]
        private decimal impSaldoAnt;
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ImpSaldoAnt { get { return impSaldoAnt; } set { impSaldoAnt = Convert.ToDecimal(value.ToString("N2")); ImpSaldoAntSpecified = (value > 0); } }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ImpSaldoAntSpecified { get; set; }

        /// <comentarios/>
        [XmlIgnore]
        private decimal impPagado;
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ImpPagado { get { return impPagado; } set { impPagado = Convert.ToDecimal(value.ToString("N2")); ImpPagadoSpecified = (value > 0); } }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ImpPagadoSpecified { get; set; }

        /// <comentarios/>
        [XmlIgnore]
        private decimal impSaldoInsoluto;
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ImpSaldoInsoluto { get { return impSaldoInsoluto; } set { impSaldoInsoluto = Convert.ToDecimal(value.ToString("N2")); ImpSaldoInsolutoSpecified = (value > 0); } }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ImpSaldoInsolutoSpecified { get; set; }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/Pagos")]
    public partial class Impuestos {
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Retencion", IsNullable=false)]
        public List<Retencion> Retenciones { get; set; }
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Traslado", IsNullable=false)]
        public List<Traslado> Traslados { get; set; }
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal TotalImpuestosRetenidos { get; set; }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalImpuestosRetenidosSpecified { get; set; }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal TotalImpuestosTrasladados { get; set; }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalImpuestosTrasladadosSpecified { get; set; }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/Pagos")]
    public partial class Retencion {

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Impuesto { get; set; }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Importe { get; set; }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/Pagos")]
    public partial class Traslado {
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Impuesto { get; set; }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TipoFactor { get; set; }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal TasaOCuota { get; set; }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Importe { get; set; }
    }
}
