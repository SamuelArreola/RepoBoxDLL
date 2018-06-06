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
using System.Xml.XPath;

namespace RepoBox33
{

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.sat.gob.mx/cfd/3", IsNullable = false)]
    public partial class Comprobante
    {
        #region Copiado de la v3.2
        public C_Impuestos ListadoImpuestos()
        {
            try
            {
                C_Impuestos impuestos = new C_Impuestos();
                impuestos.Traslados = new List<CI_Traslado>();
                impuestos.Retenciones = new List<CI_Retencion>();
                impuestos.trasladoIEPSCuota = impuestos.trasladoIVACuota = impuestos.trasladoIEPS = impuestos.trasladoIVA = impuestos.retencionIVA = impuestos.retencionISR = 0;
                impuestos.ListadoIEPS = new List<CI_Traslado>();
                foreach (Concepto concepto in this.Conceptos)
                {
                    if (concepto.Impuestos != null && concepto.Impuestos.Traslados != null && concepto.Impuestos.Traslados.Count > 0)
                        impuestos.Traslados.AddRange(concepto.Impuestos.Traslados);
                    if (concepto.Impuestos != null && concepto.Impuestos.Retenciones != null && concepto.Impuestos.Retenciones.Count > 0)
                        impuestos.Retenciones.AddRange(concepto.Impuestos.Retenciones);
                }
                foreach (CI_Traslado traslado in impuestos.Traslados)
                {
                    if (traslado.Impuesto == "002")
                        if (traslado.TipoFactor == "Tasa")
                            impuestos.trasladoIVA += traslado.Importe;
                        else
                            impuestos.trasladoIVACuota += traslado.Importe;
                    else
                    {
                        if(traslado.TipoFactor == "Tasa")
                            impuestos.trasladoIEPS += traslado.Importe;
                        else
                            impuestos.trasladoIEPSCuota += traslado.Importe;
                        List<CI_Traslado> nlist = impuestos.ListadoIEPS.FindAll(x => x.TipoFactor == traslado.TipoFactor && x.TasaOCuota == traslado.TasaOCuota);
                        if (nlist.Count > 0)
                            nlist[0].Importe += traslado.Importe;
                        else
                            impuestos.ListadoIEPS.Add(traslado);
                    }
                }
                foreach (CI_Retencion retencion in impuestos.Retenciones)
                {
                    if (retencion.Impuesto == "002")
                        impuestos.retencionIVA += retencion.Importe;
                    else
                        impuestos.retencionISR += retencion.Importe;
                }
                return impuestos;
            }
            catch (Exception)
            {
                return null;
            }
        }
        [XmlIgnore]
        private string _Raiz = "C:\\Repobox\\Timbrado";
        [XmlIgnore]
        public string Observaciones { get; set; }
        [XmlIgnore]
        public string Banco { get; set; }
        [XmlIgnore]
        public string Cuenta { get; set; }
        [XmlIgnore]
        public RepoBox.Domicilio ReceptorDomicilio { get; set; }
        [XmlIgnore]
        public string CadenaOriginal { get; set; }
        [XmlIgnore]
        public AddendaCEA AddendaCEA { get; set; }
        [XmlIgnore]
        public RepoBox.Addendas.PEPSICO.RequestCFD PEPSICO { get; set; }
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

                //doc = this.AddendaAdds(doc);
                doc.LoadXml(doc.InnerXml.Replace("</cfdi:Comprobante>", "<cfdi:Addenda></cfdi:Addenda></cfdi:Comprobante>"));
                doc.DocumentElement["cfdi:Addenda"].AppendChild(CreateAddenda(doc));
                //if (PEPSICO != null)
                //    doc.DocumentElement["cfdi:Addenda"].AppendChild(PEPSICO.Create());
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
        
        public string SaveXml(ref string xml)
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

                //try
                //{
                //    XmlElement addenda = doc.CreateElement("cfdi", "Addenda", "Addenda");
                //    XmlElement AddRepo = doc.CreateElement("Repobox");
                //    XmlElement AddDoc = doc.CreateElement("Documento");
                //    XmlAttribute AddAtt = doc.CreateAttribute("Observaciones");
                //    AddAtt.Value = this.Observaciones;
                //    AddRepo.Attributes.Append(AddAtt);
                //    AddAtt = doc.CreateAttribute("Banco");
                //    AddAtt.Value = this.Banco;
                //    AddRepo.Attributes.Append(AddAtt);
                //    if (this.AddendaCEA != null)
                //    {
                //        AddAtt = doc.CreateAttribute("CEA_NoServicio");
                //        AddAtt.Value = (string.IsNullOrEmpty(this.AddendaCEA.NoServicio) ? "" : this.AddendaCEA.NoServicio);
                //        AddRepo.Attributes.Append(AddAtt);
                //    }
                //    AddDoc.AppendChild(AddRepo);

                //    if (this.ReceptorDomicilio == null)
                //    {
                //        this.ReceptorDomicilio = new RepoBox.Domicilio()
                //        {
                //            calle = "",
                //            noExterior = "",
                //            noInterior = "",
                //            referencia = "",
                //            colonia = "",
                //            localidad = "",
                //            municipio = "",
                //            estado = "",
                //            pais = "",
                //            codigoPostal = ""
                //        };
                //    }
                //    AddRepo = doc.CreateElement("ReceptorDomicilio");
                //    AddAtt = doc.CreateAttribute("Calle");
                //    AddAtt.Value = this.ReceptorDomicilio.calle;
                //    AddRepo.Attributes.Append(AddAtt);
                //    AddAtt = doc.CreateAttribute("NoExterior");
                //    AddAtt.Value = this.ReceptorDomicilio.noExterior;
                //    AddRepo.Attributes.Append(AddAtt);
                //    AddAtt = doc.CreateAttribute("NoInterior");
                //    AddAtt.Value = this.ReceptorDomicilio.noInterior;
                //    AddRepo.Attributes.Append(AddAtt);
                //    AddAtt = doc.CreateAttribute("Referencia");
                //    AddAtt.Value = this.ReceptorDomicilio.referencia;
                //    AddRepo.Attributes.Append(AddAtt);
                //    AddAtt = doc.CreateAttribute("Colonia");
                //    AddAtt.Value = this.ReceptorDomicilio.colonia;
                //    AddRepo.Attributes.Append(AddAtt);
                //    AddAtt = doc.CreateAttribute("Ciudad");
                //    AddAtt.Value = this.ReceptorDomicilio.localidad;
                //    AddRepo.Attributes.Append(AddAtt);
                //    AddAtt = doc.CreateAttribute("Municipio");
                //    AddAtt.Value = this.ReceptorDomicilio.municipio;
                //    AddRepo.Attributes.Append(AddAtt);
                //    AddAtt = doc.CreateAttribute("Estado");
                //    AddAtt.Value = this.ReceptorDomicilio.estado;
                //    AddRepo.Attributes.Append(AddAtt);
                //    AddAtt = doc.CreateAttribute("Pais");
                //    AddAtt.Value = this.ReceptorDomicilio.pais;
                //    AddRepo.Attributes.Append(AddAtt);
                //    AddAtt = doc.CreateAttribute("CodigoPostal");
                //    AddAtt.Value = this.ReceptorDomicilio.codigoPostal;
                //    AddRepo.Attributes.Append(AddAtt);
                //    AddDoc.AppendChild(AddRepo);

                //    addenda.AppendChild(AddDoc);
                //    doc.DocumentElement.AppendChild(addenda);
                //}
                //catch (Exception)
                //{ }
                doc.LoadXml(doc.InnerXml.Replace("</cfdi:Comprobante>", "<cfdi:Addenda></cfdi:Addenda></cfdi:Comprobante>"));
                doc.DocumentElement["cfdi:Addenda"].AppendChild(CreateAddenda(doc));
                if (PEPSICO != null)
                    doc.InnerXml = doc.InnerXml.Replace("</cfdi:Addenda></cfdi:Comprobante>", PEPSICO.Create().Replace("<?xml version=\"1.0\"?>", "").Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "").Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "").Replace("xmlns=\"http://www.fact.com.mx/schema/pepsico\"", "") + "</cfdi:Addenda></cfdi:Comprobante>");
                try
                {
                    if (!Directory.Exists(_Raiz + "\\" + Emisor.Rfc + "\\CFDI"))
                        Directory.CreateDirectory(_Raiz + "\\" + Emisor.Rfc + "\\CFDI");
                }
                catch (Exception) { }
                //doc.Save(_Raiz + "\\" + Emisor.rfc + "\\CFDI\\RepoBox_" + Emisor.rfc + "_" + Receptor.rfc + "_" + this.fecha.Substring(0, 10) + "_" + this.folio + (this.serie == "" ? "" : "_" + this.serie) + ".XML");
                //return _Raiz + "\\" + Emisor.rfc + "\\CFDI\\RepoBox_" + Emisor.rfc + "_" + Receptor.rfc + "_" + this.fecha.Substring(0, 10) + "_" + this.folio + (this.serie == "" ? "" : "_" + this.serie) + ".XML";
                xml = doc.InnerXml;
                doc.Save(_Raiz + "\\" + Emisor.Rfc + "\\CFDI\\" + UUID + ".XML");
                return _Raiz + "\\" + Emisor.Rfc + "\\CFDI\\" + UUID + ".XML";
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

                //try
                //{
                //    XmlElement addenda = doc.CreateElement("cfdi", "Addenda", "Addenda");
                //    XmlElement AddRepo = doc.CreateElement("cfdi", "RepoBox", "RepoBox");
                //    XmlAttribute AddAtt = doc.CreateAttribute("Observaciones");
                //    AddAtt.Value = this.Observaciones;
                //    AddRepo.Attributes.Append(AddAtt);
                //    AddAtt = doc.CreateAttribute("Banco");
                //    AddAtt.Value = this.Banco;
                //    AddRepo.Attributes.Append(AddAtt);
                //    addenda.AppendChild(AddRepo);
                //    doc.DocumentElement.AppendChild(addenda);
                //}
                //catch (Exception)
                //{ }
                //doc = this.AddendaAdds(doc);
                doc.LoadXml(doc.InnerXml.Replace("</cfdi:Comprobante>", "<cfdi:Addenda></cfdi:Addenda></cfdi:Comprobante>"));
                doc.DocumentElement["cfdi:Addenda"].AppendChild(CreateAddenda(doc));
                //if (PEPSICO != null)
                //    doc.DocumentElement["cfdi:Addenda"].AppendChild(PEPSICO.Create());
                try
                {
                    if (!Directory.Exists(_Raiz + "\\" + Emisor.Rfc + "\\CFDI"))
                        Directory.CreateDirectory(_Raiz + "\\" + Emisor.Rfc + "\\CFDI");
                }
                catch (Exception) { }
                //doc.Save(_Raiz + "\\" + Emisor.rfc + "\\CFDI\\" + Receptor.rfc + "_" + this.folio + (this.serie == "" ? "" : "_" + this.serie) + ".XML");
                //return _Raiz + "\\" + Emisor.rfc + "\\CFDI\\" + Receptor.rfc + "_" + this.folio + (this.serie == "" ? "" : "_" + this.serie) + ".XML";
                doc.Save(_Raiz + "\\" + Emisor.Rfc + "\\CFDI\\" + UUID + ".XML");
                return _Raiz + "\\" + Emisor.Rfc + "\\CFDI\\" + UUID + ".XML";
            }
            catch (Exception ex)
            { return ex.Message; }
        }
        [Obsolete]
        private XmlDocument AddendaAdds(XmlDocument doc)
        {
            try
            {
                XmlElement addenda = doc.CreateElement("cfdi", "Addenda", "Addenda");
                XmlElement AddRepo = doc.CreateElement("RepoBox");
                XmlElement AddDoc = doc.CreateElement("Documento");
                XmlAttribute AddAtt = doc.CreateAttribute("Observaciones");
                AddAtt.Value = this.Observaciones;
                AddRepo.Attributes.Append(AddAtt);
                AddAtt = doc.CreateAttribute("Banco");
                AddAtt.Value = this.Banco;
                AddRepo.Attributes.Append(AddAtt);
                AddAtt = doc.CreateAttribute("Cuenta");
                AddAtt.Value = (string.IsNullOrEmpty(this.Cuenta) ? "" : this.Cuenta);
                AddRepo.Attributes.Append(AddAtt);
                if (this.AddendaCEA != null)
                {
                    AddAtt = doc.CreateAttribute("CEA_NoServicio");
                    AddAtt.Value = (string.IsNullOrEmpty(this.AddendaCEA.NoServicio) ? "" : this.AddendaCEA.NoServicio);
                    AddRepo.Attributes.Append(AddAtt);
                }
                if (this.ReceptorDomicilio == null)
                {
                    this.ReceptorDomicilio = new RepoBox.Domicilio()
                    {
                        calle = "",
                        noExterior = "",
                        noInterior = "",
                        referencia = "",
                        colonia = "",
                        localidad = "",
                        municipio = "",
                        estado = "",
                        pais = "",
                        codigoPostal = ""
                    };
                }
                XmlElement AddDireccion = doc.CreateElement("ReceptorDomicilio");
                AddAtt = doc.CreateAttribute("Calle");
                AddAtt.Value = this.ReceptorDomicilio.calle;
                AddDireccion.Attributes.Append(AddAtt);
                AddAtt = doc.CreateAttribute("NoExterior");
                AddAtt.Value = this.ReceptorDomicilio.noExterior;
                AddDireccion.Attributes.Append(AddAtt);
                AddAtt = doc.CreateAttribute("NoInterior");
                AddAtt.Value = this.ReceptorDomicilio.noInterior;
                AddDireccion.Attributes.Append(AddAtt);
                AddAtt = doc.CreateAttribute("Referencia");
                AddAtt.Value = this.ReceptorDomicilio.referencia;
                AddDireccion.Attributes.Append(AddAtt);
                AddAtt = doc.CreateAttribute("Colonia");
                AddAtt.Value = this.ReceptorDomicilio.colonia;
                AddDireccion.Attributes.Append(AddAtt);
                AddAtt = doc.CreateAttribute("Ciudad");
                AddAtt.Value = this.ReceptorDomicilio.localidad;
                AddDireccion.Attributes.Append(AddAtt);
                AddAtt = doc.CreateAttribute("Municipio");
                AddAtt.Value = this.ReceptorDomicilio.municipio;
                AddDireccion.Attributes.Append(AddAtt);
                AddAtt = doc.CreateAttribute("Estado");
                AddAtt.Value = this.ReceptorDomicilio.estado;
                AddDireccion.Attributes.Append(AddAtt);
                AddAtt = doc.CreateAttribute("Pais");
                AddAtt.Value = this.ReceptorDomicilio.pais;
                AddDireccion.Attributes.Append(AddAtt);
                AddAtt = doc.CreateAttribute("CodigoPostal");
                AddAtt.Value = this.ReceptorDomicilio.codigoPostal;
                AddDireccion.Attributes.Append(AddAtt);
                AddRepo.AppendChild(AddDireccion);
                AddDoc.AppendChild(AddRepo);
                addenda.AppendChild(AddDoc);
                doc.DocumentElement.AppendChild(addenda);
                return doc;
            }
            catch (Exception)
            {
                return doc;
            }
        }
        private XmlElement CreateAddenda(XmlDocument doc)
        {
            XmlElement AddDoc = doc.CreateElement("Documento");
            try
            {
                //XmlElement addenda = doc.CreateElement("cfdi", "Addenda", "Addenda");
                XmlElement AddRepo = doc.CreateElement("RepoBox");
                
                XmlAttribute AddAtt = doc.CreateAttribute("Observaciones");
                AddAtt.Value = this.Observaciones;
                AddRepo.Attributes.Append(AddAtt);
                AddAtt = doc.CreateAttribute("Banco");
                AddAtt.Value = this.Banco;
                AddRepo.Attributes.Append(AddAtt);
                AddAtt = doc.CreateAttribute("Cuenta");
                AddAtt.Value = (string.IsNullOrEmpty(this.Cuenta) ? "" : this.Cuenta);
                AddRepo.Attributes.Append(AddAtt);
                if (this.AddendaCEA != null)
                {
                    AddAtt = doc.CreateAttribute("CEA_NoServicio");
                    AddAtt.Value = (string.IsNullOrEmpty(this.AddendaCEA.NoServicio) ? "" : this.AddendaCEA.NoServicio);
                    AddRepo.Attributes.Append(AddAtt);
                }
                if (this.ReceptorDomicilio == null)
                {
                    this.ReceptorDomicilio = new RepoBox.Domicilio()
                    {
                        calle = "",
                        noExterior = "",
                        noInterior = "",
                        referencia = "",
                        colonia = "",
                        localidad = "",
                        municipio = "",
                        estado = "",
                        pais = "",
                        codigoPostal = ""
                    };
                }
                XmlElement AddDireccion = doc.CreateElement("ReceptorDomicilio");
                AddAtt = doc.CreateAttribute("Calle");
                AddAtt.Value = this.ReceptorDomicilio.calle;
                AddDireccion.Attributes.Append(AddAtt);
                AddAtt = doc.CreateAttribute("NoExterior");
                AddAtt.Value = this.ReceptorDomicilio.noExterior;
                AddDireccion.Attributes.Append(AddAtt);
                AddAtt = doc.CreateAttribute("NoInterior");
                AddAtt.Value = this.ReceptorDomicilio.noInterior;
                AddDireccion.Attributes.Append(AddAtt);
                AddAtt = doc.CreateAttribute("Referencia");
                AddAtt.Value = this.ReceptorDomicilio.referencia;
                AddDireccion.Attributes.Append(AddAtt);
                AddAtt = doc.CreateAttribute("Colonia");
                AddAtt.Value = this.ReceptorDomicilio.colonia;
                AddDireccion.Attributes.Append(AddAtt);
                AddAtt = doc.CreateAttribute("Ciudad");
                AddAtt.Value = this.ReceptorDomicilio.localidad;
                AddDireccion.Attributes.Append(AddAtt);
                AddAtt = doc.CreateAttribute("Municipio");
                AddAtt.Value = this.ReceptorDomicilio.municipio;
                AddDireccion.Attributes.Append(AddAtt);
                AddAtt = doc.CreateAttribute("Estado");
                AddAtt.Value = this.ReceptorDomicilio.estado;
                AddDireccion.Attributes.Append(AddAtt);
                AddAtt = doc.CreateAttribute("Pais");
                AddAtt.Value = this.ReceptorDomicilio.pais;
                AddDireccion.Attributes.Append(AddAtt);
                AddAtt = doc.CreateAttribute("CodigoPostal");
                AddAtt.Value = this.ReceptorDomicilio.codigoPostal;
                AddDireccion.Attributes.Append(AddAtt);
                AddRepo.AppendChild(AddDireccion);
                AddDoc.AppendChild(AddRepo);
                //addenda.AppendChild(AddDoc);
                //doc.DocumentElement.AppendChild(addenda);
                return AddDoc;
            }
            catch (Exception)
            {
                return AddDoc;
            }
        }
        private string Validar()
        {
            try
            {
                if (string.IsNullOrEmpty(this.Fecha))
                    return "RepoBox: Debe indicar la fecha del CFDI.";
                if (this._Fecha < DateTime.Now.AddHours(-70))
                    return "RepoBox: La fecha del CFDI está fuera del periodo permitido.";
                if (string.IsNullOrEmpty(this.TipoDeComprobante))
                    return "RepoBox: Debe indicar el tipo de comprobante del CFDI.";
                if (this.TipoDeComprobante != "P")
                {
                    if (string.IsNullOrEmpty(this.FormaPago))
                        return "RepoBox: Debe indicar la forma de pago del CFDI.";
                    if (this.TipoDeComprobante == "I" && (this.Complemento == null || this.Complemento.Count == 0))
                    {
                        if (string.IsNullOrEmpty(this.MetodoPago))
                            return "RepoBox: Debe indicar el método de pago del CFDI.";
                    }
                }
                if (this.SubTotal == null)
                    return "RepoBox: Debe indicar el SubTotal del CFDI.";
                if (this.Total == null)
                    return "RepoBox: Debe indicar el Total del CFDI.";
                if (string.IsNullOrEmpty(this.LugarExpedicion))
                    return "RepoBox: Debe indicar el lugar de expedición del CFDI.";
                if (this.Emisor == null)
                    return "RepoBox: Debe indicar el emisor del CFDI.";
                if (string.IsNullOrEmpty(this.Emisor.Rfc))
                    return "RepoBox: Debe indicar el RFC del emisor del CFDI.";
                else if (this.Emisor.Rfc.Trim().Length < 12 || this.Emisor.Rfc.Trim().Length > 13)
                    return "RepoBox: Verifique el RFC del Emisor, para personas fisicas debe ser 13 caracteres y para personas morales 12 caracteres.";
                if (string.IsNullOrEmpty(this.Emisor.RegimenFiscal))
                    return "RepoBox: Debe indicar el Régimen Fiscal del emisor del CFDI.";
                if (this.Receptor == null)
                    return "RepoBox: Debe indicar el receptor del CFDI.";
                if (string.IsNullOrEmpty(this.Receptor.Rfc))
                    return "RepoBox: Debe indicar el RFC del receptor del CFDI.";
                else if (this.Receptor.Rfc.Trim().Length < 12 || this.Receptor.Rfc.Trim().Length > 13)
                    if (this.Receptor.Rfc.Trim().Replace("&amp;", "&").Length < 12 || this.Receptor.Rfc.Trim().Replace("&amp;", "&").Length > 13)
                        return "RepoBox: Verifique el RFC del Receptor, para personas fisicas debe ser 13 caracteres y para personas morales 12 caracteres.";
                if (this.Conceptos == null || this.Conceptos.Count == 0)
                    return "RepoBox: Debe indicar los conceptos del CFDI.";
                foreach (Concepto concepto in this.Conceptos)
                {
                    if (concepto.Cantidad == null)
                        return "RepoBox: Debe indicar la cantidad en todos los conceptos del CFDI.";
                    if (this.TipoDeComprobante != "P" && this.TipoDeComprobante != "N")
                    {
                        if (string.IsNullOrEmpty(concepto.Unidad))
                            return "RepoBox: Debe indicar la unidad en todos los conceptos del CFDI.";
                    }
                    if (string.IsNullOrEmpty(concepto.Descripcion))
                        return "RepoBox: Debe indicar la descripción en todos los conceptos del CFDI.";
                    if (concepto.ValorUnitario == null)
                        return "RepoBox: Debe indicar el valor unitario (precio) en todos los conceptos del CFDI.";
                    if (concepto.Importe == null)
                        return "RepoBox: Debe indicar el importe en todos los conceptos del CFDI.";
                    // Verificar impuestos.
                    RepoBox.SAT.Funciones funciones = new RepoBox.SAT.Funciones();
                    if (!funciones.Importes(concepto.Cantidad, concepto.ValorUnitario, concepto.Importe))
                        return "RepoBox: Debe verificar el importe del concepto '"+ concepto.Descripcion +"', está fuera de los parametros permitidos por el SAT para importes segun su cantidad y precio unitario.";
                    if (concepto.Impuestos != null)
                    {
                        if(concepto.Impuestos.Traslados != null && concepto.Impuestos.Traslados.Count > 0)
                            foreach (CI_Traslado traslado in concepto.Impuestos.Traslados)
                                if (!funciones.Impuestos(traslado.Base, traslado.TasaOCuota, traslado.Importe))
                                    return "RepoBox: Debe verificar el importe del impuesto trasladado '"+ (traslado.Impuesto == "002" ? "IVA" : "IEPS") +"' para el concepto '" + concepto.Descripcion + "', está fuera de los parametros permitidos por el SAT para importes segun su tasa o cuota y su base.";
                        if (concepto.Impuestos.Retenciones != null && concepto.Impuestos.Retenciones.Count > 0)
                            foreach (CI_Retencion retencion in concepto.Impuestos.Retenciones)
                                if (!funciones.Impuestos(retencion.Base, retencion.TasaOCuota, retencion.Importe))
                                    return "RepoBox: Debe verificar el importe del impuesto retenido '" + (retencion.Impuesto == "002" ? "IVA" : "ISR") + "' para el concepto '" + concepto.Descripcion + "', está fuera de los parametros permitidos por el SAT para importes segun su tasa o cuota y su base.";
                    }
                }
                if (this.Impuestos != null)
                {
                    if (this.Impuestos.Retenciones != null && this.Impuestos.Retenciones.Count > 0)
                    {
                        if (this.Impuestos.TotalImpuestosRetenidos == null)
                            return "RepoBox: Debe indicar el total de impuestos retenidos en el CFDI.";
                        foreach (Retencion retencion in this.Impuestos.Retenciones)
                        {
                            if (retencion.Impuesto == null)
                                return "RepoBox: Debe indicar el concepto de cada impuesto retenido agregado al CFDI";
                            if (retencion.Importe == null)
                                return "RepoBox: Debe indicar el importe de cada impuesto retenido agregado al CFDI";
                        }
                    }
                    if (this.Impuestos.Traslados != null && this.Impuestos.Traslados.Count > 0)
                    {
                        if (this.Impuestos.TotalImpuestosRetenidos == null)
                            return "RepoBox: Debe indicar el total de impuestos trasladados en el CFDI.";
                        foreach (Traslado traslado in this.Impuestos.Traslados)
                        {
                            if (traslado.Impuesto == null)
                                return "RepoBox: Debe indicar el concepto de cada impuesto trasladado agregado al CFDI";
                            if (traslado.Importe == null)
                                return "RepoBox: Debe indicar el importe de cada impuesto trasladado agregado al CFDI";
                            if (traslado.TasaOCuota == null)
                                return "RepoBox: Debe indicar la tasa de cada impuesto trasladado agregado al CFDI";
                        }
                    }
                }

                return "OK";
            }
            catch (Exception ex)
            { return "RepoBox: " + ex.Message; }
        }
        private string GetCertNumber()
        {
            return GetCertNumber(this.Cer);
            //MSC.X509Certificate cert = MSC.X509Certificate.CreateFromCertFile(this.Cer);

            //Buncy.X509CertificateParser certParser = new Buncy.X509CertificateParser();
            //Buncy.X509Certificate privateCertBouncy = certParser.ReadCertificate(cert.GetRawCertData());
            //AsymmetricKeyParameter pubKey = privateCertBouncy.GetPublicKey();

            //byte[] nSerie = privateCertBouncy.SerialNumber.ToByteArray();
            //return Encoding.ASCII.GetString(nSerie);
        }
        public string GetCertNumber(string cer)
        {
            try
            {
                MSC.X509Certificate cert = MSC.X509Certificate.CreateFromCertFile(cer);

                Buncy.X509CertificateParser certParser = new Buncy.X509CertificateParser();
                Buncy.X509Certificate privateCertBouncy = certParser.ReadCertificate(cert.GetRawCertData());
                AsymmetricKeyParameter pubKey = privateCertBouncy.GetPublicKey();

                byte[] nSerie = privateCertBouncy.SerialNumber.ToByteArray();
                return Encoding.ASCII.GetString(nSerie);
            }
            catch (Exception)
            {
                return "";
            }
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
                this.Certificado = Convert.ToBase64String(File.ReadAllBytes(value));
                this.NoCertificado = GetCertNumber();
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
                RSACryptoServiceProvider provider = RepoBox.RepoboxOpenSSL.DecodeEncryptedPrivateKeyInfo(File.ReadAllBytes(Llave), securePassword);
                SHA256CryptoServiceProvider halg = new SHA256CryptoServiceProvider();
                return Convert.ToBase64String(provider.SignData(Encoding.UTF8.GetBytes(s), halg));
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("RepoBox"))
                    throw new Exception(ex.Message);
                else
                    throw new Exception("Repobox: Contraseña del archivo KEY es incorrecta. Msg: "+ex.Message);
            }
        }
        private string GenerarSello(XmlDocument doc)
        {
            try
            {
                StringWriter swriter = new StringWriter();
                XslCompiledTransform xsl = new XslCompiledTransform();
                xsl.Load(@"C:\Repobox\Timbrado\Base\cadenaoriginal_3_3.xslt");
                XmlTextWriter xmlwriter = new XmlTextWriter(swriter);
                xsl.Transform(doc.CreateNavigator(), null, xmlwriter);
                return swriter.ToString();

                ////Cargar el XML
                ////StreamReader reader = new StreamReader(@"C:/SAT/ejemplo1cfdv3.xml");
                ////XPathDocument myXPathDoc = new XPathDocument(reader);

                ////Cargando el XSLT
                //XslCompiledTransform myXslTrans = new XslCompiledTransform();
                //myXslTrans.Load(@"C:\Repobox\Timbrado\Base\cadenaoriginal_3_3.xslt");

                //StringWriter str = new StringWriter();
                //XmlTextWriter myWriter = new XmlTextWriter(str);

                ////Aplicando transformacion
                //myXslTrans.Transform(doc.CreateNavigator(), null, myWriter);

                ////Resultado
                //string result = str.ToString();
                ////return result;
                //return "||3.3|H|131355|2017-10-17T13:13:55|01|30001000000300023708|PUE|10|MXN|11.6|I|PUE|85000|AAA010101AAA|SECRETARIA DE FINANZAS|603|AERS900616U25|SAMUEL ARMANDO ARREOLA ROMERO|P01|93161701|C1|1|E48|PZA|Producto 1|10|10|10|002|Tasa|0.16|1.60|002|Tasa|0.16|1.60|1.60||";
            }
            catch (Exception EX)
            {
                return "RepoBox: Error al generar Sello. Msg: "+EX.Message;
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
                this.CadenaOriginal = selloplano;
                this.Sello = this.GetSignData(selloplano, this.Key, this.KeyPassword);
                if (this.Sello == "")
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
        public string SchemaLocation = "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd";
        #endregion
        private CfdiRelacionados cfdiRelacionadosField;
        private Emisor emisorField;
        private Receptor receptorField;
        private List<Concepto> conceptosField;
        private Impuestos impuestosField;
        private List<ComprobanteComplemento> complementoField;
        private Addenda addendaField;
        private string versionField;
        private string serieField;
        private string folioField;
        private string fechaField;
        private string selloField;
        private string formaPagoField;
        private string noCertificadoField;
        private string certificadoField;
        private string condicionesDePagoField;
        private decimal subTotalField;
        private decimal descuentoField;
        private bool descuentoFieldSpecified;
        private string monedaField;
        private decimal tipoCambioField;
        private bool tipoCambioFieldSpecified;
        private decimal totalField;
        private string tipoDeComprobanteField;
        private string metodoPagoField;
        private string lugarExpedicionField;
        private string confirmacionField;

        public Comprobante()
        {
            this.versionField = "3.3";
            this.myNamespaces.Add("cfdi", "http://www.sat.gob.mx/cfd/3");
        }
        public Comprobante(string cer, string key, string keyPassword)
            : this()
        {

            this.Cer = cer;
            //this.Cer = @"C:\Repobox\Timbrado\DEMO\CSD01_AAA010101AAA.cer";
            this.Key = key;
            //this.Key = @"C:\Repobox\Timbrado\DEMO\CSD01_AAA010101AAA.key";
            this.KeyPassword = keyPassword;
            //this.KeyPassword = "12345678a";
        }
        /// <comentarios/>
        public CfdiRelacionados CfdiRelacionados
        {
            get
            {
                return this.cfdiRelacionadosField;
            }
            set
            {
                this.cfdiRelacionadosField = value;
            }
        }

        /// <comentarios/>
        public Emisor Emisor
        {
            get
            {
                return this.emisorField;
            }
            set
            {
                this.emisorField = value;
            }
        }

        /// <comentarios/>
        public Receptor Receptor
        {
            get
            {
                return this.receptorField;
            }
            set
            {
                this.receptorField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Concepto", IsNullable = false)]
        public List<Concepto> Conceptos
        {
            get
            {
                return this.conceptosField;
            }
            set
            {
                this.conceptosField = value;
            }
        }

        /// <comentarios/>
        public Impuestos Impuestos
        {
            get
            {
                return this.impuestosField;
            }
            set
            {
                this.impuestosField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("Complemento")]
        public List<ComprobanteComplemento> Complemento
        {
            get
            {
                return this.complementoField;
            }
            set
            {
                this.complementoField = value;
            }
        }

        /// <comentarios/>
        public Addenda Addenda
        {
            get
            {
                return this.addendaField;
            }
            set
            {
                this.addendaField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Serie
        {
            get
            {
                return this.serieField;
            }
            set
            {
                this.serieField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Folio
        {
            get
            {
                return this.folioField;
            }
            set
            {
                this.folioField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Fecha
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
        [XmlIgnore]
        public System.DateTime _Fecha
        {
            get
            {
                return Convert.ToDateTime(this.Fecha);
            }
            set
            {
                this.Fecha = (value.Year.ToString("0000") + "-" + value.Month.ToString("00") + "-" + value.Day.ToString("00") + "T" +
                    value.Hour.ToString("00") + ":" + value.Minute.ToString("00") + ":" + value.Second.ToString("00"));
            }
        }
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Sello
        {
            get
            {
                return this.selloField;
            }
            set
            {
                this.selloField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string FormaPago
        {
            get
            {
                return this.formaPagoField;
            }
            set
            {
                this.formaPagoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NoCertificado
        {
            get
            {
                return this.noCertificadoField;
            }
            set
            {
                this.noCertificadoField = value;
            }
        }

        /// <comentarios/>
        //[XmlIgnore]
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Certificado
        {
            get
            {
                return this.certificadoField;
            }
            set
            {
                this.certificadoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CondicionesDePago
        {
            get
            {
                return this.condicionesDePagoField;
            }
            set
            {
                this.condicionesDePagoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal SubTotal
        {
            get
            {
                return this.subTotalField;
            }
            set
            {
                this.subTotalField = (Moneda != "XXX" ? Convert.ToDecimal(value.ToString("N2")) : value);
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Descuento
        {
            get
            {
                return this.descuentoField;
            }
            set
            {
                this.descuentoField = Convert.ToDecimal(value.ToString("N2"));
                this.descuentoFieldSpecified = true;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DescuentoSpecified
        {
            get
            {
                return this.descuentoFieldSpecified;
            }
            set
            {
                this.descuentoFieldSpecified = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Moneda
        {
            get
            {
                return this.monedaField;
            }
            set
            {
                this.monedaField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal TipoCambio
        {
            get
            {
                return this.tipoCambioField;
            }
            set
            {
                this.tipoCambioField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TipoCambioSpecified
        {
            get
            {
                return this.tipoCambioFieldSpecified;
            }
            set
            {
                this.tipoCambioFieldSpecified = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Total
        {
            get
            {
                return this.totalField;
            }
            set
            {
                this.totalField = (Moneda != "XXX" ? Convert.ToDecimal(value.ToString("N2")) : value);
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TipoDeComprobante
        {
            get
            {
                return this.tipoDeComprobanteField;
            }
            set
            {
                this.tipoDeComprobanteField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MetodoPago
        {
            get
            {
                return this.metodoPagoField;
            }
            set
            {
                this.metodoPagoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LugarExpedicion
        {
            get
            {
                return this.lugarExpedicionField;
            }
            set
            {
                this.lugarExpedicionField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Confirmacion
        {
            get
            {
                return this.confirmacionField;
            }
            set
            {
                this.confirmacionField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class CfdiRelacionados
    {

        private List<CfdiRelacionado> cfdiRelacionadoField;

        private string tipoRelacionField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("CfdiRelacionado")]
        public List<CfdiRelacionado> CfdiRelacionado
        {
            get
            {
                return this.cfdiRelacionadoField;
            }
            set
            {
                this.cfdiRelacionadoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TipoRelacion
        {
            get
            {
                return this.tipoRelacionField;
            }
            set
            {
                this.tipoRelacionField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class CfdiRelacionado
    {

        private string uUIDField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string UUID
        {
            get
            {
                return this.uUIDField;
            }
            set
            {
                this.uUIDField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class Emisor
    {

        private string rfcField;

        private string nombreField;

        private string regimenFiscalField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Rfc
        {
            get
            {
                return this.rfcField;
            }
            set
            {
                this.rfcField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Nombre
        {
            get
            {
                return this.nombreField;
            }
            set
            {
                this.nombreField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RegimenFiscal
        {
            get
            {
                return this.regimenFiscalField;
            }
            set
            {
                this.regimenFiscalField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class Receptor
    {

        private string rfcField;

        private string nombreField;

        private string residenciaFiscalField;

        private string numRegIdTribField;

        private string usoCFDIField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Rfc
        {
            get
            {
                return this.rfcField;
            }
            set
            {
                this.rfcField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Nombre
        {
            get
            {
                return this.nombreField;
            }
            set
            {
                this.nombreField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ResidenciaFiscal
        {
            get
            {
                return this.residenciaFiscalField;
            }
            set
            {
                this.residenciaFiscalField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NumRegIdTrib
        {
            get
            {
                return this.numRegIdTribField;
            }
            set
            {
                this.numRegIdTribField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string UsoCFDI
        {
            get
            {
                return this.usoCFDIField;
            }
            set
            {
                this.usoCFDIField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class Concepto
    {

        private C_Impuestos impuestosField;

        private C_InformacionAduanera[] informacionAduaneraField;

        private C_CuentaPredial cuentaPredialField;

        private C_ComplementoConcepto complementoConceptoField;

        private Parte[] parteField;

        private string claveProdServField;

        private string noIdentificacionField;

        private decimal cantidadField;

        private string claveUnidadField;

        private string unidadField;

        private string descripcionField;

        private decimal valorUnitarioField;

        private decimal importeField;

        private decimal descuentoField;

        private bool descuentoFieldSpecified;

        /// <comentarios/>
        public C_Impuestos Impuestos
        {
            get
            {
                return this.impuestosField;
            }
            set
            {
                this.impuestosField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("InformacionAduanera")]
        public C_InformacionAduanera[] InformacionAduanera
        {
            get
            {
                return this.informacionAduaneraField;
            }
            set
            {
                this.informacionAduaneraField = value;
            }
        }

        /// <comentarios/>
        public C_CuentaPredial CuentaPredial
        {
            get
            {
                return this.cuentaPredialField;
            }
            set
            {
                this.cuentaPredialField = value;
            }
        }

        /// <comentarios/>
        public C_ComplementoConcepto ComplementoConcepto
        {
            get
            {
                return this.complementoConceptoField;
            }
            set
            {
                this.complementoConceptoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("Parte")]
        public Parte[] Parte
        {
            get
            {
                return this.parteField;
            }
            set
            {
                this.parteField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ClaveProdServ
        {
            get
            {
                return this.claveProdServField;
            }
            set
            {
                this.claveProdServField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NoIdentificacion
        {
            get
            {
                return this.noIdentificacionField;
            }
            set
            {
                this.noIdentificacionField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Cantidad
        {
            get
            {
                return this.cantidadField;
            }
            set
            {
                this.cantidadField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ClaveUnidad
        {
            get
            {
                return this.claveUnidadField;
            }
            set
            {
                this.claveUnidadField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Unidad
        {
            get
            {
                return this.unidadField;
            }
            set
            {
                this.unidadField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Descripcion
        {
            get
            {
                return this.descripcionField;
            }
            set
            {
                this.descripcionField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ValorUnitario
        {
            get
            {
                return this.valorUnitarioField;
            }
            set
            {
                this.valorUnitarioField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Importe
        {
            get
            {
                return this.importeField;
            }
            set
            {
                this.importeField = (Descripcion.ToUpper() != "PAGO" ? Convert.ToDecimal(value.ToString("N2")) : value);
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Descuento
        {
            get
            {
                return this.descuentoField;
            }
            set
            {
                this.descuentoField = value;
                this.descuentoFieldSpecified = true;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DescuentoSpecified
        {
            get
            {
                return this.descuentoFieldSpecified;
            }
            set
            {
                this.descuentoFieldSpecified = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class C_Impuestos
    {
        [XmlIgnore]
        public List<CI_Traslado> ListadoIEPS { get; set; }
        [XmlIgnore]
        public decimal trasladoIEPS { get; set; }
        [XmlIgnore]
        public decimal trasladoIEPSCuota { get; set; }
        [XmlIgnore]
        public decimal trasladoIVA { get; set; }
        [XmlIgnore]
        public decimal trasladoIVACuota { get; set; }

        [XmlIgnore]
        public decimal retencionIVA { get; set; }
        [XmlIgnore]
        public decimal retencionISR { get; set; }

        private List<CI_Traslado> trasladosField;
        private List<CI_Retencion> retencionesField;
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Traslado", IsNullable = false)]
        public List<CI_Traslado> Traslados
        {
            get
            {
                return this.trasladosField;
            }
            set
            {
                this.trasladosField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Retencion", IsNullable = false)]
        public List<CI_Retencion> Retenciones
        {
            get
            {
                return this.retencionesField;
            }
            set
            {
                this.retencionesField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class CI_Traslado
    {

        private decimal baseField;

        private string impuestoField;

        private string tipoFactorField;

        private decimal tasaOCuotaField;

        private bool tasaOCuotaFieldSpecified;

        private decimal importeField;

        private bool importeFieldSpecified;

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Base
        {
            get
            {
                return this.baseField;
            }
            set
            {
                this.baseField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Impuesto
        {
            get
            {
                return this.impuestoField;
            }
            set
            {
                this.impuestoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TipoFactor
        {
            get
            {
                return this.tipoFactorField;
            }
            set
            {
                this.tipoFactorField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal TasaOCuota
        {
            get
            {
                return this.tasaOCuotaField;
            }
            set
            {
                this.tasaOCuotaField = Convert.ToDecimal(value.ToString("N6"));
                this.tasaOCuotaFieldSpecified = true;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TasaOCuotaSpecified
        {
            get
            {
                return this.tasaOCuotaFieldSpecified;
            }
            set
            {
                this.tasaOCuotaFieldSpecified = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Importe
        {
            get
            {
                return this.importeField;
            }
            set
            {
                this.importeField = Convert.ToDecimal(value.ToString("N2"));
                this.importeFieldSpecified = true;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ImporteSpecified
        {
            get
            {
                return this.importeFieldSpecified;
            }
            set
            {
                this.importeFieldSpecified = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class CI_Retencion
    {

        private decimal baseField;

        private string impuestoField;

        private string tipoFactorField;

        private decimal tasaOCuotaField;

        private decimal importeField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Base
        {
            get
            {
                return this.baseField;
            }
            set
            {
                this.baseField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Impuesto
        {
            get
            {
                return this.impuestoField;
            }
            set
            {
                this.impuestoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TipoFactor
        {
            get
            {
                return this.tipoFactorField;
            }
            set
            {
                this.tipoFactorField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal TasaOCuota
        {
            get
            {
                return this.tasaOCuotaField;
            }
            set
            {
                this.tasaOCuotaField = Convert.ToDecimal(value.ToString("N6"));
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Importe
        {
            get
            {
                return this.importeField;
            }
            set
            {
                this.importeField = Convert.ToDecimal(value.ToString("N2"));
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class C_InformacionAduanera
    {

        private string numeroPedimentoField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NumeroPedimento
        {
            get
            {
                return this.numeroPedimentoField;
            }
            set
            {
                this.numeroPedimentoField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class C_CuentaPredial
    {

        private string numeroField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Numero
        {
            get
            {
                return this.numeroField;
            }
            set
            {
                this.numeroField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class C_ComplementoConcepto
    {

        private System.Xml.XmlElement[] anyField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlElement[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class Parte
    {

        private C_ParteInformacionAduanera[] informacionAduaneraField;

        private string claveProdServField;

        private string noIdentificacionField;

        private decimal cantidadField;

        private string unidadField;

        private string descripcionField;

        private decimal valorUnitarioField;

        private bool valorUnitarioFieldSpecified;

        private decimal importeField;

        private bool importeFieldSpecified;

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("InformacionAduanera")]
        public C_ParteInformacionAduanera[] InformacionAduanera
        {
            get
            {
                return this.informacionAduaneraField;
            }
            set
            {
                this.informacionAduaneraField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ClaveProdServ
        {
            get
            {
                return this.claveProdServField;
            }
            set
            {
                this.claveProdServField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NoIdentificacion
        {
            get
            {
                return this.noIdentificacionField;
            }
            set
            {
                this.noIdentificacionField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Cantidad
        {
            get
            {
                return this.cantidadField;
            }
            set
            {
                this.cantidadField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Unidad
        {
            get
            {
                return this.unidadField;
            }
            set
            {
                this.unidadField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Descripcion
        {
            get
            {
                return this.descripcionField;
            }
            set
            {
                this.descripcionField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ValorUnitario
        {
            get
            {
                return this.valorUnitarioField;
            }
            set
            {
                this.valorUnitarioField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorUnitarioSpecified
        {
            get
            {
                return this.valorUnitarioFieldSpecified;
            }
            set
            {
                this.valorUnitarioFieldSpecified = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Importe
        {
            get
            {
                return this.importeField;
            }
            set
            {
                this.importeField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ImporteSpecified
        {
            get
            {
                return this.importeFieldSpecified;
            }
            set
            {
                this.importeFieldSpecified = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class C_ParteInformacionAduanera
    {
        private string numeroPedimentoField;
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NumeroPedimento
        {
            get
            {
                return this.numeroPedimentoField;
            }
            set
            {
                this.numeroPedimentoField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class Impuestos
    {
        public void CalcularTotales()
        {
            try
            {
                this.TotalImpuestosRetenidos = 0;
                this.TotalImpuestosTrasladados = 0;

                if (this.Retenciones != null)
                    foreach (Retencion retencion in this.Retenciones)
                        this.TotalImpuestosRetenidos += retencion.Importe;
                if (this.Traslados != null)
                    foreach (Traslado traslado in this.Traslados)
                        this.TotalImpuestosTrasladados += traslado.Importe;

                this.TotalImpuestosRetenidos = Math.Round(this.TotalImpuestosRetenidos, 6);
                this.TotalImpuestosTrasladados = Math.Round(this.TotalImpuestosTrasladados, 6);
                this.TotalImpuestosTrasladadosSpecified = (this.Traslados.Count > 0);
                this.TotalImpuestosRetenidosSpecified = (this.TotalImpuestosRetenidos > 0);
            }
            catch (Exception)
            { }
        }
         
        private List<Retencion> retencionesField;
        private List<Traslado> trasladosField;
        private decimal totalImpuestosRetenidosField;
        private bool totalImpuestosRetenidosFieldSpecified;
        private decimal totalImpuestosTrasladadosField;
        private bool totalImpuestosTrasladadosFieldSpecified;
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Retencion", IsNullable = false)]
        public List<Retencion> Retenciones
        {
            get
            {
                return this.retencionesField;
            }
            set
            {
                this.retencionesField = value;
            }
        }
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Traslado", IsNullable = false)]
        public List<Traslado> Traslados
        {
            get
            {
                return this.trasladosField;
            }
            set
            {
                this.trasladosField = value;
            }
        }
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal TotalImpuestosRetenidos
        {
            get
            {
                return this.totalImpuestosRetenidosField;
            }
            set
            {
                this.totalImpuestosRetenidosField = value;
            }
        }
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalImpuestosRetenidosSpecified
        {
            get
            {
                return this.totalImpuestosRetenidosFieldSpecified;
            }
            set
            {
                this.totalImpuestosRetenidosFieldSpecified = value;
            }
        }
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal TotalImpuestosTrasladados
        {
            get
            {
                return this.totalImpuestosTrasladadosField;
            }
            set
            {
                this.totalImpuestosTrasladadosField = value;
            }
        }
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalImpuestosTrasladadosSpecified
        {
            get
            {
                return this.totalImpuestosTrasladadosFieldSpecified;
            }
            set
            {
                this.totalImpuestosTrasladadosFieldSpecified = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class Retencion
    {

        private string impuestoField;

        private decimal importeField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Impuesto
        {
            get
            {
                return this.impuestoField;
            }
            set
            {
                this.impuestoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Importe
        {
            get
            {
                return this.importeField;
            }
            set
            {
                this.importeField = Convert.ToDecimal(value.ToString("N2"));
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class Traslado
    {

        private string impuestoField;

        private string tipoFactorField;

        private decimal tasaOCuotaField;

        private decimal importeField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Impuesto
        {
            get
            {
                return this.impuestoField;
            }
            set
            {
                this.impuestoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TipoFactor
        {
            get
            {
                return this.tipoFactorField;
            }
            set
            {
                this.tipoFactorField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal TasaOCuota
        {
            get
            {
                return this.tasaOCuotaField;
            }
            set
            {
                this.tasaOCuotaField = Convert.ToDecimal(value.ToString("N6"));
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Importe
        {
            get
            {
                return this.importeField;
            }
            set
            {
                this.importeField = Convert.ToDecimal(value.ToString("N2"));
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteComplemento
    {

        private List<System.Xml.XmlElement> anyField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public List<System.Xml.XmlElement> Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class Addenda
    {
        [XmlNamespaceDeclarationsAttribute]
        public XmlSerializerNamespaces myNamespaces = new XmlSerializerNamespaces();
        private System.Xml.XmlElement[] anyField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlElement[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }

        public Addenda()
        {
            this.myNamespaces.Add("cfdi", "http://www.sat.gob.mx/cfd/3");
        }
    }
    [Serializable]
    public class AddendaCEA
    {
        public string NoServicio { get; set; }
    }
}