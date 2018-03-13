using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
//using Buncy = Org.BouncyCastle.X509;
using Buncy = Org.BouncyCastle.X509;
using MSC = System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Crypto;
using ThoughtWorks.QRCode.Codec;
using System.Drawing;
using CrystalDecisions.CrystalReports.Engine;

namespace RepoBox 
{
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/cfd/3")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://www.sat.gob.mx/cfd/3", IsNullable=false)]
    public partial class Comprobante 
    {
        [XmlIgnore]
        private string _Raiz = "C:\\Repobox\\Timbrado";
        [XmlIgnore]
        public string Observaciones { get; set; }
        [XmlIgnore]
        public string Banco { get; set; }
        public string SaveXmlERP(string xml, string ruta, string fileName)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                string UUID = "";
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                    if (node.Name == "cfdi:Complemento")
                        foreach (XmlNode nodeComplemento in node.ChildNodes)
                            if (nodeComplemento.Name == "tfd:TimbreFiscalDigital" || nodeComplemento.Name == "ns2:TimbreFiscalDigital")
                            {
                                UUID = nodeComplemento.Attributes["UUID"].Value;
                                break;
                            }

                try
                {
                    XmlElement addenda = doc.CreateElement("cfdi", "Addenda", "Addenda");
                    XmlElement AddRepo = doc.CreateElement("Repobox");
                    XmlElement AddDoc = doc.CreateElement("Documento");
                    XmlAttribute AddAtt = doc.CreateAttribute("Observaciones");
                    AddAtt.Value = this.Observaciones;
                    AddRepo.Attributes.Append(AddAtt);
                    AddAtt = doc.CreateAttribute("Banco");
                    AddAtt.Value = this.Banco;
                    AddRepo.Attributes.Append(AddAtt);

                    AddDoc.AppendChild(AddRepo);
                    addenda.AppendChild(AddDoc);
                    doc.DocumentElement.AppendChild(addenda);
                }
                catch (Exception)
                { }
                try
                {
                    if (!Directory.Exists(ruta))
                        Directory.CreateDirectory(ruta);
                }
                catch (Exception) { }
                //doc.Save(_Raiz + "\\" + Emisor.rfc + "\\CFDI\\RepoBox_" + Emisor.rfc + "_" + Receptor.rfc + "_" + this.fecha.Substring(0, 10) + "_" + this.folio + (this.serie == "" ? "" : "_" + this.serie) + ".XML");
                //return _Raiz + "\\" + Emisor.rfc + "\\CFDI\\RepoBox_" + Emisor.rfc + "_" + Receptor.rfc + "_" + this.fecha.Substring(0, 10) + "_" + this.folio + (this.serie == "" ? "" : "_" + this.serie) + ".XML";
                doc.Save(ruta + "\\" + fileName + ".XML");
                return ruta + "\\" + fileName + ".XML";
            }
            catch (Exception ex)
            { return ex.Message; }
        }
        public string SaveXml(string xml)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                string UUID="";
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                    if(node.Name == "cfdi:Complemento")
                        foreach (XmlNode nodeComplemento in node.ChildNodes)
                            if (nodeComplemento.Name == "tfd:TimbreFiscalDigital" || nodeComplemento.Name == "ns2:TimbreFiscalDigital")
                            {
                                UUID = nodeComplemento.Attributes["UUID"].Value;
                                break;
                            }

                try
                {
                    XmlElement addenda = doc.CreateElement("cfdi", "Addenda", "Addenda");
                    XmlElement AddRepo = doc.CreateElement("Repobox");
                    XmlElement AddDoc = doc.CreateElement("Documento");
                    XmlAttribute AddAtt = doc.CreateAttribute("Observaciones");
                    AddAtt.Value = this.Observaciones;
                    AddRepo.Attributes.Append(AddAtt);
                    AddAtt = doc.CreateAttribute("Banco");
                    AddAtt.Value = this.Banco;
                    AddRepo.Attributes.Append(AddAtt);

                    AddDoc.AppendChild(AddRepo);
                    addenda.AppendChild(AddDoc);
                    doc.DocumentElement.AppendChild(addenda);
                }
                catch (Exception)
                { }
                try
                {
                    if (!Directory.Exists(_Raiz + "\\" + Emisor.rfc + "\\CFDI"))
                        Directory.CreateDirectory(_Raiz + "\\" + Emisor.rfc + "\\CFDI");
                }
                catch (Exception) { }
                //doc.Save(_Raiz + "\\" + Emisor.rfc + "\\CFDI\\RepoBox_" + Emisor.rfc + "_" + Receptor.rfc + "_" + this.fecha.Substring(0, 10) + "_" + this.folio + (this.serie == "" ? "" : "_" + this.serie) + ".XML");
                //return _Raiz + "\\" + Emisor.rfc + "\\CFDI\\RepoBox_" + Emisor.rfc + "_" + Receptor.rfc + "_" + this.fecha.Substring(0, 10) + "_" + this.folio + (this.serie == "" ? "" : "_" + this.serie) + ".XML";
                doc.Save(_Raiz + "\\" + Emisor.rfc + "\\CFDI\\"+ UUID +".XML");
                return _Raiz + "\\" + Emisor.rfc + "\\CFDI\\" + UUID + ".XML";
            }
            catch (Exception ex)
            { return ex.Message; }
        }
        public string SaveXmlSEFIN(string xml)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                string UUID = "";
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                    if (node.Name == "cfdi:Complemento")
                        foreach (XmlNode nodeComplemento in node.ChildNodes)
                            if (nodeComplemento.Name == "tfd:TimbreFiscalDigital" || nodeComplemento.Name == "ns2:TimbreFiscalDigital")
                            {
                                UUID = nodeComplemento.Attributes["UUID"].Value;
                                break;
                            }

                try
                {
                    XmlElement addenda = doc.CreateElement("cfdi", "Addenda", "Addenda");
                    XmlElement AddRepo = doc.CreateElement("cfdi", "RepoBox", "RepoBox");
                    XmlAttribute AddAtt = doc.CreateAttribute("Observaciones");
                    AddAtt.Value = this.Observaciones;
                    AddRepo.Attributes.Append(AddAtt);
                    AddAtt = doc.CreateAttribute("Banco");
                    AddAtt.Value = this.Banco;
                    AddRepo.Attributes.Append(AddAtt);
                    addenda.AppendChild(AddRepo);
                    doc.DocumentElement.AppendChild(addenda);
                }
                catch (Exception)
                { }
                try
                {
                    if (!Directory.Exists(_Raiz + "\\" + Emisor.rfc + "\\CFDI"))
                        Directory.CreateDirectory(_Raiz + "\\" + Emisor.rfc + "\\CFDI");
                }
                catch (Exception) { }
                //doc.Save(_Raiz + "\\" + Emisor.rfc + "\\CFDI\\" + Receptor.rfc + "_" + this.folio + (this.serie == "" ? "" : "_" + this.serie) + ".XML");
                //return _Raiz + "\\" + Emisor.rfc + "\\CFDI\\" + Receptor.rfc + "_" + this.folio + (this.serie == "" ? "" : "_" + this.serie) + ".XML";
                doc.Save(_Raiz + "\\" + Emisor.rfc + "\\CFDI\\" + UUID + ".XML");
                return _Raiz + "\\" + Emisor.rfc + "\\CFDI\\" + UUID + ".XML";
            }
            catch (Exception ex)
            { return ex.Message; }
        }
        private string Validar()
        {
            try
            {
                if (string.IsNullOrEmpty(this.fecha))
                    return "RepoBox: Debe indicar la fecha del CFDI.";
                if(this.Fecha < DateTime.Now.AddHours(-70))
                    return "RepoBox: La fecha del CFDI está fuera del periodo permitido.";
                if (string.IsNullOrEmpty(this.formaDePago))
                    return "RepoBox: Debe indicar la forma de pago del CFDI.";
                if (this.subTotal == null)
                    return "RepoBox: Debe indicar el SubTotal del CFDI.";
                if (this.total == null)
                    return "RepoBox: Debe indicar el Total del CFDI.";
                if(this.tipoDeComprobante == null || this.tipoDeComprobante.ToString() == "")
                    return "RepoBox: Debe indicar el tipo de comprobante del CFDI.";
                if (string.IsNullOrEmpty(this.metodoDePago))
                    return "RepoBox: Debe indicar el método de pago del CFDI.";
                if (string.IsNullOrEmpty(this.LugarExpedicion))
                    return "RepoBox: Debe indicar el lugar de expedición del CFDI.";
                if(this.Emisor == null)
                    return "RepoBox: Debe indicar el emisor del CFDI.";
                if (string.IsNullOrEmpty(this.Emisor.rfc))
                    return "RepoBox: Debe indicar el RFC del emisor del CFDI.";
                else if (this.Emisor.rfc.Trim().Length < 12 || this.Emisor.rfc.Trim().Length > 13)
                    return "RepoBox: Verifique el RFC del Emisor, para personas fisicas debe ser 13 caracteres y para personas morales 12 caracteres.";
                if (this.Emisor.RegimenFiscal == null || this.Emisor.RegimenFiscal.Count == 0)
                    return "RepoBox: Debe indicar el Régimen Fiscal del emisor del CFDI.";
                if(this.Emisor.DomicilioFiscal == null)
                    return "RepoBox: Debe indicar la dirección fiscal del emisor del CFDI.";
                if(string.IsNullOrEmpty(this.Emisor.DomicilioFiscal.calle))
                    return "RepoBox: Debe indicar la calle en la dirección fiscal del emisor del CFDI.";
                if (string.IsNullOrEmpty(this.Emisor.DomicilioFiscal.municipio))
                    return "RepoBox: Debe indicar el municipio en la dirección fiscal del emisor del CFDI.";
                if (string.IsNullOrEmpty(this.Emisor.DomicilioFiscal.estado))
                    return "RepoBox: Debe indicar el estado en la dirección fiscal del emisor del CFDI.";
                if (string.IsNullOrEmpty(this.Emisor.DomicilioFiscal.pais))
                    return "RepoBox: Debe indicar el pais en la dirección fiscal del emisor del CFDI.";
                if (string.IsNullOrEmpty(this.Emisor.DomicilioFiscal.codigoPostal))
                    return "RepoBox: Debe indicar el codigo postal en la dirección fiscal del emisor del CFDI.";
                if(this.Emisor.ExpedidoEn != null)
                    if(string.IsNullOrEmpty(this.Emisor.ExpedidoEn.pais))
                        return "RepoBox: Debe indicar el pais en la dirección de expedición del emisor del CFDI.";
                if (this.Receptor == null)
                    return "RepoBox: Debe indicar el receptor del CFDI.";
                if (string.IsNullOrEmpty(this.Receptor.rfc))
                    return "RepoBox: Debe indicar el RFC del receptor del CFDI.";
                else if(this.Receptor.rfc.Trim().Length < 12 || this.Receptor.rfc.Trim().Length > 13)
                    return "RepoBox: Verifique el RFC del Receptor, para personas fisicas debe ser 13 caracteres y para personas morales 12 caracteres.";
                if (this.Receptor.Domicilio != null)
                    if (string.IsNullOrEmpty(this.Receptor.Domicilio.pais))
                        return "RepoBox: Debe indicar el pais en la dirección del receptor del CFDI.";
                if(this.Conceptos == null || this.Conceptos.Count == 0)
                    return "RepoBox: Debe indicar los conceptos del CFDI.";
                foreach (Concepto concepto in this.Conceptos)
                {
                    if (concepto.cantidad == null)
                        return "RepoBox: Debe indicar la cantidad en todos los conceptos del CFDI.";
                    if (string.IsNullOrEmpty(concepto.unidad))
                        return "RepoBox: Debe indicar la unidad en todos los conceptos del CFDI.";
                    if (string.IsNullOrEmpty(concepto.descripcion))
                        return "RepoBox: Debe indicar la descripción en todos los conceptos del CFDI.";
                    if (concepto.valorUnitario == null)
                        return "RepoBox: Debe indicar el valor unitario (precio) en todos los conceptos del CFDI.";
                    if (concepto.importe == null)
                        return "RepoBox: Debe indicar el importe en todos los conceptos del CFDI.";
                }
                if (this.Impuestos != null)
                {
                    if (this.Impuestos.Retenciones != null && this.Impuestos.Retenciones.Count > 0)
                    {
                        if (this.Impuestos.totalImpuestosRetenidos == null)
                            return "RepoBox: Debe indicar el total de impuestos retenidos en el CFDI.";
                        foreach (Retencion retencion in this.Impuestos.Retenciones)
                        {
                            if (retencion.impuesto == null)
                                return "RepoBox: Debe indicar el concepto de cada impuesto retenido agregado al CFDI";
                            if (retencion.importe == null)
                                return "RepoBox: Debe indicar el importe de cada impuesto retenido agregado al CFDI";
                        }
                    }
                    if (this.Impuestos.Traslados != null && this.Impuestos.Traslados.Count > 0)
                    {
                        if (this.Impuestos.totalImpuestosRetenidos == null)
                            return "RepoBox: Debe indicar el total de impuestos trasladados en el CFDI.";
                        foreach (Traslado traslado in this.Impuestos.Traslados)
                        {
                            if (traslado.impuesto == null)
                                return "RepoBox: Debe indicar el concepto de cada impuesto trasladado agregado al CFDI";
                            if (traslado.importe == null)
                                return "RepoBox: Debe indicar el importe de cada impuesto trasladado agregado al CFDI";
                            if(traslado.tasa == null)
                                return "RepoBox: Debe indicar la tasa de cada impuesto trasladado agregado al CFDI";
                        }
                    }
                }

                return "OK";
            }
            catch (Exception ex)
            { return "RepoBox: "+ex.Message; }
        }
        private string GetCertNumber()
        {
            MSC.X509Certificate cert = MSC.X509Certificate.CreateFromCertFile(this.Cer);

            Buncy.X509CertificateParser certParser = new Buncy.X509CertificateParser();
            Buncy.X509Certificate privateCertBouncy = certParser.ReadCertificate(cert.GetRawCertData());
            AsymmetricKeyParameter pubKey = privateCertBouncy.GetPublicKey();

            byte[] nSerie = privateCertBouncy.SerialNumber.ToByteArray();
            return Encoding.ASCII.GetString(nSerie);
        }
        [XmlIgnore]
        private string CerPath = "";
        [XmlIgnore]
        public string Cer
        {
            get { return CerPath; }
            set
            {
                this.CerPath = value;
                this.certificado = Convert.ToBase64String(File.ReadAllBytes(value));
                this.noCertificado = GetCertNumber();
            }
        }
        [XmlIgnore]
        public string Key { get; set; }
        [XmlIgnore]
        public string KeyPassword { get; set; }

        private string GetSignData(string originalData, string Llave, string KeyPass)
        {
            try
            {
                string s = originalData;
                if (!File.Exists(Llave))
                    throw new Exception("RepoBox: No existe el archivo .KEY Ruta: " + Llave);
                SecureString securePassword = new SecureString();
                securePassword.Clear();
                foreach (char ch in KeyPass.ToCharArray())
                    securePassword.AppendChar(ch);
                RSACryptoServiceProvider provider = RepoboxOpenSSL.DecodeEncryptedPrivateKeyInfo(File.ReadAllBytes(Llave), securePassword);
                SHA1CryptoServiceProvider halg = new SHA1CryptoServiceProvider();
                return Convert.ToBase64String(provider.SignData(Encoding.UTF8.GetBytes(s), halg));
            }
            catch (Exception ex)
            {
                if(ex.Message.StartsWith("RepoBox"))
                    throw new Exception(ex.Message);
                else
                    throw new Exception("Repobox: Contraseña del archivo KEY es incorrecta.");
            }
        }
        private string GenerarSello(XmlDocument doc)
        {
            try
            {
                StringWriter swriter = new StringWriter();
                XslCompiledTransform xsl = new XslCompiledTransform();
                xsl.Load(@"C:\Repobox\Timbrado\Base\cadenaoriginal_3_2.xslt");
                XmlTextWriter xmlwriter = new XmlTextWriter(swriter);
                xsl.Transform(doc.CreateNavigator(), null, xmlwriter);
                return swriter.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        public string GenerarCFD()
        {
            try
            {
                string validacion = this.Validar();
                if (validacion != "OK")
                    return validacion;
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

                // Cadena Original
                string selloplano = this.GenerarSello(document);
                selloplano = selloplano.Replace("&amp;", "&");
                this.sello = this.GetSignData(selloplano, this.Key, this.KeyPassword);
                if (this.sello == "")
                    throw new Exception("RepoBox: Sello No Generado");

                //x = new System.Xml.Serialization.XmlSerializer(this.GetType());
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
        [XmlNamespaceDeclarationsAttribute]
        public XmlSerializerNamespaces myNamespaces = new XmlSerializerNamespaces();

        [XmlAttribute("schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string SchemaLocation = "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd";

        private Emisor emisorField;
        
        private Receptor receptorField;
        
        private List<Concepto> conceptosField;
        
        private Impuestos impuestosField;
        
        private ComprobanteComplemento complementoField;
        
        private ComprobanteAddenda addendaField;
        
        private string versionField;
        
        private string serieField;
        
        private string folioField;
        
        //private System.DateTime fechaField;
        private string fechaField;
        private string selloField;
        
        private string formaDePagoField;
        
        private string noCertificadoField;
        
        private string certificadoField;
        
        private string condicionesDePagoField;
        
        private decimal subTotalField;
        
        private decimal descuentoField;
        
        private bool descuentoFieldSpecified;
        
        private string motivoDescuentoField;
        
        private string tipoCambioField;
        
        private string monedaField;
        
        private decimal totalField;
        
        private TipoDeComprobante tipoDeComprobanteField;
        
        private string metodoDePagoField;
        
        private string lugarExpedicionField;
        
        private string numCtaPagoField;
        
        private string folioFiscalOrigField;
        
        private string serieFolioFiscalOrigField;
        
        private System.DateTime fechaFolioFiscalOrigField;
        
        private bool fechaFolioFiscalOrigFieldSpecified;
        
        private decimal montoFolioFiscalOrigField;
        
        private bool montoFolioFiscalOrigFieldSpecified;
        
        public Comprobante(string cer, string key, string keyPassword) : this() {
           
            this.Cer = cer;
            //this.Cer = @"C:\Repobox\Timbrado\DEMO\CSD01_AAA010101AAA.cer";
            this.Key = key;
            //this.Key = @"C:\Repobox\Timbrado\DEMO\CSD01_AAA010101AAA.key";
            this.KeyPassword = keyPassword;
            //this.KeyPassword = "12345678a";
        }
        public Comprobante()
        {
            this.versionField = "3.2";
            this.myNamespaces.Add("cfdi", "http://www.sat.gob.mx/cfd/3");
        }
        /// <comentarios/>
        public Emisor Emisor {
            get {
                return this.emisorField;
            }
            set {
                this.emisorField = value;
            }
        }
        
        /// <comentarios/>
        public Receptor Receptor {
            get {
                return this.receptorField;
            }
            set {
                this.receptorField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Concepto", IsNullable=false)]
        //public ComprobanteConcepto[] Conceptos {
        public List<Concepto> Conceptos
        {
            get {
                return this.conceptosField;
            }
            set {
                this.conceptosField = value;
            }
        }
        
        /// <comentarios/>
        public Impuestos Impuestos {
            get {
                return this.impuestosField;
            }
            set {
                this.impuestosField = value;
            }
        }
        
        /// <comentarios/>
        public ComprobanteComplemento Complemento {
            get {
                return this.complementoField;
            }
            set {
                this.complementoField = value;
            }
        }
        
        /// <comentarios/>
        public ComprobanteAddenda Addenda {
            get {
                return this.addendaField;
            }
            set {
                this.addendaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string version {
            get {
                return this.versionField;
            }
            set {
                this.versionField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string serie {
            get {
                return this.serieField;
            }
            set {
                this.serieField = (value == "" ? null : value.ToUpper());
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string folio {
            get {
                return this.folioField;
            }
            set {
                this.folioField = (value == "" ? null : value.ToUpper());
            }
        }
        
        /// <comentarios/>
        [XmlIgnore]
        public System.DateTime Fecha {
            get {
                return Convert.ToDateTime(this.fecha);
            }
            set {
                this.fecha = (value.Year.ToString("0000")+"-"+value.Month.ToString("00")+"-"+value.Day.ToString("00")+"T"+
                    value.Hour.ToString("00")+":"+value.Minute.ToString("00")+":"+value.Second.ToString("00"));
            }
        }
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string fecha
        {
            get
            {
                return this.fechaField;
            }
            set
            {
                this.fechaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string sello {
            get {
                return this.selloField;
            }
            set {
                this.selloField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string formaDePago {
            get {
                return this.formaDePagoField;
            }
            set {
                this.formaDePagoField = (value == "" ? null : value.ToUpper());
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string noCertificado {
            get {
                return this.noCertificadoField;
            }
            set {
                this.noCertificadoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string certificado {
            get {
                return this.certificadoField;
            }
            set {
                this.certificadoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string condicionesDePago {
            get {
                return this.condicionesDePagoField;
            }
            set {
                this.condicionesDePagoField = (value == "" ? null : value.ToUpper());
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal subTotal {
            get {
                return this.subTotalField;
            }
            set {
                this.subTotalField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal descuento {
            get {
                return this.descuentoField;
            }
            set {
                this.descuentoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool descuentoSpecified {
            get {
                return this.descuentoFieldSpecified;
            }
            set {
                this.descuentoFieldSpecified = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string motivoDescuento {
            get {
                return this.motivoDescuentoField;
            }
            set {
                this.motivoDescuentoField = (value == "" ? null : value.ToUpper());
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TipoCambio {
            get {
                return this.tipoCambioField;
            }
            set {
                this.tipoCambioField = (value == "" ? null : value);
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Moneda {
            get {
                return this.monedaField;
            }
            set {
                this.monedaField = (value == "" ? null : value.ToUpper());
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal total {
            get {
                return this.totalField;
            }
            set {
                this.totalField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public TipoDeComprobante tipoDeComprobante {
            get {
                return this.tipoDeComprobanteField;
            }
            set {
                this.tipoDeComprobanteField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string metodoDePago {
            get {
                return this.metodoDePagoField;
            }
            set {
                this.metodoDePagoField = (value == "" ? null : value.ToUpper());
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LugarExpedicion {
            get {
                return this.lugarExpedicionField;
            }
            set {
                this.lugarExpedicionField = (value == "" ? null : value.ToUpper());
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NumCtaPago {
            get {
                return this.numCtaPagoField;
            }
            set {
                this.numCtaPagoField = (value == "" ? null : value);
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string FolioFiscalOrig {
            get {
                return this.folioFiscalOrigField;
            }
            set {
                this.folioFiscalOrigField = (value == "" ? null : value);
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SerieFolioFiscalOrig {
            get {
                return this.serieFolioFiscalOrigField;
            }
            set {
                this.serieFolioFiscalOrigField = (value == "" ? null : value);
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime FechaFolioFiscalOrig {
            get {
                return this.fechaFolioFiscalOrigField;
            }
            set {
                this.fechaFolioFiscalOrigField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FechaFolioFiscalOrigSpecified {
            get {
                return this.fechaFolioFiscalOrigFieldSpecified;
            }
            set {
                this.fechaFolioFiscalOrigFieldSpecified = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal MontoFolioFiscalOrig {
            get {
                return this.montoFolioFiscalOrigField;
            }
            set {
                this.montoFolioFiscalOrigField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MontoFolioFiscalOrigSpecified {
            get {
                return this.montoFolioFiscalOrigFieldSpecified;
            }
            set {
                this.montoFolioFiscalOrigFieldSpecified = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/cfd/3")]
    public partial class Emisor {
        
        private DomicilioFiscal domicilioFiscalField;
        
        private ExpedidoEn expedidoEnField;
        
        private List<RegimenFiscal> regimenFiscalField;
        
        private string rfcField;
        
        private string nombreField;
        
        /// <comentarios/>
        public DomicilioFiscal DomicilioFiscal {
            get {
                return this.domicilioFiscalField;
            }
            set {
                this.domicilioFiscalField = value;
            }
        }
        
        /// <comentarios/>
        public ExpedidoEn ExpedidoEn {
            get {
                return this.expedidoEnField;
            }
            set {
                this.expedidoEnField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("RegimenFiscal")]
        //public RegimenFiscal[] RegimenFiscal {
        public List<RegimenFiscal> RegimenFiscal
        {
            get {
                return this.regimenFiscalField;
            }
            set {
                this.regimenFiscalField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string rfc {
            get {
                return this.rfcField;
            }
            set {
                this.rfcField = (value == "" ? null : value.ToUpper());
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nombre {
            get {
                return this.nombreField;
            }
            set {
                this.nombreField = (value == "" ? null : value.ToUpper());
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.sat.gob.mx/cfd/3")]
    //public partial class t_UbicacionFiscal {
    public partial class DomicilioFiscal
    {        
        private string calleField;
        
        private string noExteriorField;
        
        private string noInteriorField;
        
        private string coloniaField;
        
        private string localidadField;
        
        private string referenciaField;
        
        private string municipioField;
        
        private string estadoField;
        
        private string paisField;
        
        private string codigoPostalField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string calle {
            get {
                return this.calleField;
            }
            set {
                this.calleField = (value == "" ? null : value);
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string noExterior {
            get {
                return this.noExteriorField;
            }
            set {
                this.noExteriorField = (value == "" ? null : value);
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string noInterior {
            get {
                return this.noInteriorField;
            }
            set {
                this.noInteriorField = (value == "" ? null : value);
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string colonia {
            get {
                return this.coloniaField;
            }
            set {
                this.coloniaField = (value == "" ? null : value);
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string localidad {
            get {
                return this.localidadField;
            }
            set {
                this.localidadField = (value == "" ? null : value);
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string referencia {
            get {
                return this.referenciaField;
            }
            set {
                this.referenciaField = (value == "" ? null : value);
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string municipio {
            get {
                return this.municipioField;
            }
            set {
                this.municipioField = (value == "" ? null : value);
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string estado {
            get {
                return this.estadoField;
            }
            set {
                this.estadoField = (value == "" ? null : value);
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string pais {
            get {
                return this.paisField;
            }
            set {
                this.paisField = (value == "" ? null : value);
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string codigoPostal {
            get {
                return this.codigoPostalField;
            }
            set {
                this.codigoPostalField = (value == "" ? null : value);
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.sat.gob.mx/cfd/3")]
    public partial class t_InformacionAduanera {
        
        private string numeroField;
        
        private System.DateTime fechaField;
        
        private string aduanaField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string numero {
            get {
                return this.numeroField;
            }
            set {
                this.numeroField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="date")]
        public System.DateTime fecha {
            get {
                return this.fechaField;
            }
            set {
                this.fechaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string aduana {
            get {
                return this.aduanaField;
            }
            set {
                this.aduanaField = value;
            }
        }
    }
    
    public partial class ExpedidoEn : t_Ubicacion
    {

    }
    public partial class Domicilio : t_Ubicacion
    {

    }
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class t_Ubicacion {
    
        private string calleField;

        private string noExteriorField;

        private string noInteriorField;

        private string coloniaField;

        private string localidadField;

        private string referenciaField;

        private string municipioField;

        private string estadoField;

        private string paisField;

        private string codigoPostalField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string calle
        {
            get
            {
                return this.calleField;
            }
            set
            {
                this.calleField = (value == "" ? null : value.ToUpper());
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string noExterior
        {
            get
            {
                return this.noExteriorField;
            }
            set
            {
                this.noExteriorField = (value == "" ? null : value.ToUpper());
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string noInterior
        {
            get
            {
                return this.noInteriorField;
            }
            set
            {
                this.noInteriorField = (value == "" ? null : value.ToUpper());
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string colonia
        {
            get
            {
                return this.coloniaField;
            }
            set
            {
                this.coloniaField = (value == "" ? null : value.ToUpper());
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string localidad
        {
            get
            {
                return this.localidadField;
            }
            set
            {
                this.localidadField = (value == "" ? null : value.ToUpper());
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string referencia
        {
            get
            {
                return this.referenciaField;
            }
            set
            {
                this.referenciaField = (value == "" ? null : value.ToUpper());
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string municipio
        {
            get
            {
                return this.municipioField;
            }
            set
            {
                this.municipioField = (value == "" ? null : value.ToUpper());
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string estado
        {
            get
            {
                return this.estadoField;
            }
            set
            {
                this.estadoField = (value == "" ? null : value.ToUpper());
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string pais
        {
            get
            {
                return this.paisField;
            }
            set
            {
                this.paisField = (value == "" ? null : value.ToUpper());
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string codigoPostal
        {
            get
            {
                return this.codigoPostalField;
            }
            set
            {
                this.codigoPostalField = (value == "" ? null : value.ToUpper());
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/cfd/3")]
    public partial class RegimenFiscal {
        
        private string regimenField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Regimen {
            get {
                return this.regimenField;
            }
            set {
                this.regimenField = (value == "" ? null : value.ToUpper());
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/cfd/3")]
    public partial class Receptor {
        
        private Domicilio domicilioField;
        
        private string rfcField;
        
        private string nombreField;
        
        /// <comentarios/>
        public Domicilio Domicilio
        {
            get {
                return this.domicilioField;
            }
            set {
                this.domicilioField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string rfc {
            get {
                return this.rfcField;
            }
            set {
                this.rfcField = (value == "" ? null : value.ToUpper());
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nombre {
            get {
                return this.nombreField;
            }
            set {
                this.nombreField = (value == "" ? null : value.ToUpper());
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/cfd/3")]
    //public partial class ComprobanteConcepto {
    public partial class Concepto
    {
        
        private object[] itemsField;
        
        private decimal cantidadField;
        
        private string unidadField;
        
        private string noIdentificacionField;
        
        private string descripcionField;
        
        private decimal valorUnitarioField;
        
        private decimal importeField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("ComplementoConcepto", typeof(ComprobanteConceptoComplementoConcepto))]
        [System.Xml.Serialization.XmlElementAttribute("CuentaPredial", typeof(ComprobanteConceptoCuentaPredial))]
        [System.Xml.Serialization.XmlElementAttribute("InformacionAduanera", typeof(t_InformacionAduanera))]
        [System.Xml.Serialization.XmlElementAttribute("Parte", typeof(ComprobanteConceptoParte))]
        private object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal cantidad {
            get {
                return this.cantidadField;
            }
            set {
                this.cantidadField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string unidad {
            get {
                return this.unidadField;
            }
            set {
                this.unidadField = (value == "" ? null : value.ToUpper());
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string noIdentificacion {
            get {
                return this.noIdentificacionField;
            }
            set {
                this.noIdentificacionField = (value == "" ? null : value.ToUpper());
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string descripcion {
            get {
                return this.descripcionField;
            }
            set {
                this.descripcionField = (value == "" ? null : value.ToUpper());
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal valorUnitario {
            get {
                return this.valorUnitarioField;
            }
            set {
                this.valorUnitarioField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal importe {
            get {
                return this.importeField;
            }
            set {
                this.importeField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteConceptoComplementoConcepto {
        
        private System.Xml.XmlElement[] anyField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlElement[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteConceptoCuentaPredial {
        
        private string numeroField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string numero {
            get {
                return this.numeroField;
            }
            set {
                this.numeroField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteConceptoParte {
        
        private t_InformacionAduanera[] informacionAduaneraField;
        
        private decimal cantidadField;
        
        private string unidadField;
        
        private string noIdentificacionField;
        
        private string descripcionField;
        
        private decimal valorUnitarioField;
        
        private bool valorUnitarioFieldSpecified;
        
        private decimal importeField;
        
        private bool importeFieldSpecified;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("InformacionAduanera")]
        public t_InformacionAduanera[] InformacionAduanera {
            get {
                return this.informacionAduaneraField;
            }
            set {
                this.informacionAduaneraField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal cantidad {
            get {
                return this.cantidadField;
            }
            set {
                this.cantidadField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string unidad {
            get {
                return this.unidadField;
            }
            set {
                this.unidadField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string noIdentificacion {
            get {
                return this.noIdentificacionField;
            }
            set {
                this.noIdentificacionField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string descripcion {
            get {
                return this.descripcionField;
            }
            set {
                this.descripcionField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal valorUnitario {
            get {
                return this.valorUnitarioField;
            }
            set {
                this.valorUnitarioField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool valorUnitarioSpecified {
            get {
                return this.valorUnitarioFieldSpecified;
            }
            set {
                this.valorUnitarioFieldSpecified = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal importe {
            get {
                return this.importeField;
            }
            set {
                this.importeField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool importeSpecified {
            get {
                return this.importeFieldSpecified;
            }
            set {
                this.importeFieldSpecified = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/cfd/3")]
    //public partial class ComprobanteImpuestos {
    public partial class Impuestos
    {
        private List<Retencion> retencionesField;
        
        private List<Traslado> trasladosField;
        
        private decimal totalImpuestosRetenidosField;
        
        private bool totalImpuestosRetenidosFieldSpecified;
        
        private decimal totalImpuestosTrasladadosField;
        
        private bool totalImpuestosTrasladadosFieldSpecified;
        public void CalcularTotales()
        {
            try
            {
                this.totalImpuestosRetenidos = 0;
                this.totalImpuestosTrasladados = 0;

                if (this.Retenciones != null)
                    foreach (Retencion retencion in this.Retenciones)
                        this.totalImpuestosRetenidos += retencion.importe;
                if (this.Traslados != null)
                    foreach (Traslado traslado in this.Traslados)
                        this.totalImpuestosTrasladados += traslado.importe;
                this.totalImpuestosTrasladadosSpecified = (this.totalImpuestosTrasladados > 0);
                this.totalImpuestosRetenidosSpecified = (this.totalImpuestosRetenidos > 0);
            }
            catch (Exception)
            { }
        }
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Retencion", IsNullable=false)]
        public List<Retencion> Retenciones
        {
            get {
                return this.retencionesField;
            }
            set {
                this.retencionesField = value;
                //CalcularTotales();
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Traslado", IsNullable=false)]
        public List<Traslado> Traslados {
            get {
                return this.trasladosField;
            }
            set {
                this.trasladosField = value;
                //CalcularTotales();       
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal totalImpuestosRetenidos {
            get {
                return this.totalImpuestosRetenidosField;
            }
            set {
                this.totalImpuestosRetenidosField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool totalImpuestosRetenidosSpecified {
            get {
                return this.totalImpuestosRetenidosFieldSpecified;
            }
            set {
                this.totalImpuestosRetenidosFieldSpecified = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal totalImpuestosTrasladados {
            get {
                return this.totalImpuestosTrasladadosField;
            }
            set {
                this.totalImpuestosTrasladadosField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool totalImpuestosTrasladadosSpecified {
            get {
                return this.totalImpuestosTrasladadosFieldSpecified;
            }
            set {
                this.totalImpuestosTrasladadosFieldSpecified = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/cfd/3")]
    //public partial class ComprobanteImpuestosRetencion {
    public partial class Retencion
    {
        private RetencionImpuesto impuestoField;
        
        private decimal importeField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public RetencionImpuesto impuesto {
            get {
                return this.impuestoField;
            }
            set {
                this.impuestoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal importe {
            get {
                return this.importeField;
            }
            set {
                this.importeField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/cfd/3")]
    public enum RetencionImpuesto {
        
        /// <comentarios/>
        ISR,
        
        /// <comentarios/>
        IVA,
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/cfd/3")]
    //public partial class ComprobanteImpuestosTraslado {
    public partial class Traslado
    {
        private TrasladoImpuesto impuestoField;
        
        private decimal tasaField;
        
        private decimal importeField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public TrasladoImpuesto impuesto {
            get {
                return this.impuestoField;
            }
            set {
                this.impuestoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal tasa {
            get {
                return this.tasaField;
            }
            set {
                this.tasaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal importe {
            get {
                return this.importeField;
            }
            set {
                this.importeField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/cfd/3")]
    public enum TrasladoImpuesto {
        
        /// <comentarios/>
        IVA,
        
        /// <comentarios/>
        IEPS,
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteComplemento {
        
        private System.Xml.XmlElement[] anyField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlElement[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteAddenda {
        
        private System.Xml.XmlElement[] anyField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlElement[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.sat.gob.mx/cfd/3")]
    public enum TipoDeComprobante {
        
        /// <comentarios/>
        ingreso,
        
        /// <comentarios/>
        egreso,
        
        /// <comentarios/>
        traslado,
    }
}
