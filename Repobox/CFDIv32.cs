using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.IO;
using ThoughtWorks.QRCode.Codec;
using System.Drawing;
using CrystalDecisions.CrystalReports.Engine;
using RepoBox33.SATCatalogos;

namespace RepoBox
{
    public class CFDIv32
    {
        private string AcuseCancelacionTest(string rfc, string UUID, bool acuseBase64)
        {
            string acuse = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><Acuse Fecha=\"" + (DateTime.Now.Year.ToString("0000") + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Day.ToString("00") + "T" + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00") + ".000") + "\" RfcEmisor=\"" + rfc + "\" xmlns:ns2=\"http://www.w3.org/2000/09/xmldsig#\" xmlns:ns3=\"http://cancelacfd.sat.gob.mx\"><ns3:Folios><ns3:UUID>" + UUID + "</ns3:UUID><ns3:EstatusUUID>201</ns3:EstatusUUID></ns3:Folios><ns2:Signature Id=\"SelloSAT\"><ns2:SignedInfo><ns2:CanonicalizationMethod Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315\"/><ns2:SignatureMethod Algorithm=\"http://www.w3.org/2001/04/xmldsig-more#hmac-sha512\"/><ns2:Reference URI=\"\"><ns2:Transforms><ns2:Transform Algorithm=\"http://www.w3.org/TR/1999/REC-xpath-19991116\"><ns2:XPath>not(ancestor-or-self::*[local-name()='Signature'])</ns2:XPath></ns2:Transform></ns2:Transforms><ns2:DigestMethod Algorithm=\"http://www.w3.org/2001/04/xmlenc#sha512\"/><ns2:DigestValue>eC0KYeYw1UVBiFXflFMDlvOkwe4BN7OYGxn7vDce9BLaEy5rSimN/iQ7lUasi4ZLLPJmBgnGeNGUHr2PiU6sCQ==</ns2:DigestValue></ns2:Reference></ns2:SignedInfo><ns2:SignatureValue>FBOC4eYmjl1IS6nK0xteu3pt9wZ8IWQDTRyLyPe+4VsslGDJg97Dt2Ue7Q/Is53rN0oDAIf+T8/tR0cLVWZ5Gw==</ns2:SignatureValue><ns2:KeyInfo><ns2:KeyName>00001088888800000016</ns2:KeyName><ns2:KeyValue><ns2:RSAKeyValue><ns2:Modulus>xnL2zDPtH5jDsAZDTIfMqbKGrve+At8Kyx2EZvbfXbpK9uVExWS874oMelFzNq69/YqSReT3I7I8wr+joy5O7ouZH+4KWdIGp4Si6lHe0kntxzNmuuKyOPkJ9tMcntnFmQ4bfxFxlg/Ud2hCtuoy3j2xYkIXu5O4pGM98Nz8pAM=</ns2:Modulus><ns2:Exponent>AQAB</ns2:Exponent></ns2:RSAKeyValue></ns2:KeyValue></ns2:KeyInfo></ns2:Signature></Acuse>";
            if (acuseBase64)
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(acuse));
            else
                return acuse;
        }
        private string _Raiz = "C:\\Repobox\\Timbrado\\";
        public string Timbrar32(string xml, bool SAT, string PAC, string RFC)
        {
            if (PAC == "PADE")
                return TimbrarPADE(xml, SAT, RFC);
            else
                return TimbrarFINKOK(xml, SAT);
        }
        public string Timbrar33(string xml, bool SAT, string PAC, string RFC)
        {
            if (PAC == "PADE")
                return TimbrarPADE33(xml, SAT, RFC);
            else
                return TimbrarFINKOK(xml, SAT);
        }
        private string TimbrarFINKOK(string xml, bool SAT)
        {
            string xmlTimbrado = "";
            try
            {
                if (SAT)
                {
                    RepoWS.StampSOAP cfd = new RepoWS.StampSOAP();
                    RepoWS.stamp parametros = new RepoWS.stamp { username = "alicom@cepdi.mx", password = "@L1com,,", xml = XmlToByteArray(xml) };
                    RepoWS.stampResponse respuesta = cfd.stamp(parametros);
                    string errorTimbrado = "";
                    if (respuesta.stampResult.Incidencias.Length > 0)
                    {
                        foreach (RepoWS.Incidencia incidencia in respuesta.stampResult.Incidencias)
                            errorTimbrado += incidencia.MensajeIncidencia;
                        throw new Exception(errorTimbrado);
                    }
                    else
                        xmlTimbrado = respuesta.stampResult.xml;
                }
                else
                {
                    RepoWS_Demo.StampSOAP cfd = new RepoWS_Demo.StampSOAP();
                    RepoWS_Demo.stamp parametros = new RepoWS_Demo.stamp { username = "usuario_demo@mail.com", password = "u5u4r10.d3m0", xml = XmlToByteArray(xml) };
                    RepoWS_Demo.stampResponse respuesta = cfd.stamp(parametros);
                    string errorTimbrado = "";
                    if (respuesta.stampResult.Incidencias.Length > 0)
                    {
                        foreach (RepoWS_Demo.Incidencia incidencia in respuesta.stampResult.Incidencias)
                            errorTimbrado += incidencia.MensajeIncidencia;
                        throw new Exception(errorTimbrado);
                    }
                    else
                        xmlTimbrado = respuesta.stampResult.xml;
                }
                if (xmlTimbrado != "")
                    return xmlTimbrado;
                else
                    throw new Exception(xmlTimbrado);
            }
            catch (Exception ex)
            { return "Repobox: " + ErroresFINKOK(ex.Message, "Factura"); }
        }
        private string TimbrarPADE(string xml, bool SAT, string rfc)
        {
            RepoBoxWS.TimbradoService timbrado = new RepoBoxWS.TimbradoService();
            try
            {
                XmlDocument doc = new XmlDocument();
                if (rfc == "SFI950101DU2")
                {
                    // SFI950101DU2
                    if (SAT)
                        doc.LoadXml(timbrado.timbrado("3c6e5dc6-7586-4297-af58-fd916831d431", "Samuel.Arreola@repobox.com.mx", "Repobox.2017!", xml));
                    else
                        doc.LoadXml(timbrado.timbradoPrueba("3c6e5dc6-7586-4297-af58-fd916831d431", "Samuel.Arreola@repobox.com.mx", "Repobox.2017!", xml));
                }
                else if (rfc == "TGM110214QK0")
                {
                    // GPG (GrupoSARUWEB)
                    if (SAT)
                        doc.LoadXml(timbrado.timbrado("1094309f-b2cf-4592-9b77-c126117d36a3", "Samuel.Arreola@repobox.com.mx", "Repobox.2017!", xml));
                    else
                        doc.LoadXml(timbrado.timbradoPrueba("1094309f-b2cf-4592-9b77-c126117d36a3", "Samuel.Arreola@repobox.com.mx", "Repobox.2017!", xml));
                }
                else if (rfc == "CEA990831FI8")
                {
                    // CEA
                    //if (SAT)
                    //    doc.LoadXml(timbrado.timbrado("45cd141c-8cd8-41ea-8b2a-22152288ccf3", "Samuel.Arreola@repobox.com.mx", "Rp1234567890$-", xml));
                    //else
                    //    doc.LoadXml(timbrado.timbradoPrueba("45cd141c-8cd8-41ea-8b2a-22152288ccf3", "Samuel.Arreola@repobox.com.mx", "Rp1234567890$-", xml));
                    if (SAT)
                        doc.LoadXml(timbrado.timbrado("b37cd5b6-5c4e-4137-99aa-ed4f3f2d7d6d", "Samuel.Arreola@repobox.com.mx", "Repobox.2017!", xml));
                    else
                        doc.LoadXml(timbrado.timbradoPrueba("b37cd5b6-5c4e-4137-99aa-ed4f3f2d7d6d", "Samuel.Arreola@repobox.com.mx", "Repobox.2017!", xml));
                }
                else
                    return "RepoBox: RFC no permitido para timbrar.";
                DataSet set = new DataSet();
                using (MemoryStream stream = new MemoryStream())
                {
                    doc.Save(stream);
                    stream.Position = 0L;
                    set.ReadXml(stream);
                }
                if (set.Tables[0].Rows[0]["timbradoOk"].ToString() == "0" || set.Tables[0].Rows[0]["timbradoOk"].ToString().ToUpper() == "FALSE")
                    return "RepoBox: " + ErrorMSG(set.Tables[0].Rows[0]["mensaje"].ToString());
                else
                    return Encoding.UTF8.GetString(Convert.FromBase64String(set.Tables[0].Rows[0]["xmlBase64"].ToString()));
            }
            catch (Exception ex)
            { return "Repobox: " + ErrorMSG(ex.Message); }
        }
        private string TimbrarPADE33(string xml, bool SAT, string rfc)
        {
            RepoBoxWS33.Timbrado33 timbrado = new RepoBoxWS33.Timbrado33();
            try
            {
                XmlDocument doc = new XmlDocument();
                if (rfc == "SFI950101DU2")
                {
                    // SFI950101DU2
                    if (SAT)
                        doc.LoadXml(timbrado.timbrado("3c6e5dc6-7586-4297-af58-fd916831d431", "Samuel.Arreola@repobox.com.mx", "Repobox.2017!", xml, new string[] {}));
                    else
                        doc.LoadXml(timbrado.timbradoPrueba("3c6e5dc6-7586-4297-af58-fd916831d431", "Samuel.Arreola@repobox.com.mx", "Repobox.2017!", xml, new string[] { }));
                }
                else if (rfc == "TGM110214QK0")
                {
                    // GPG (GrupoSARUWEB)
                    if (SAT)
                        doc.LoadXml(timbrado.timbrado("1094309f-b2cf-4592-9b77-c126117d36a3", "Samuel.Arreola@repobox.com.mx", "Repobox.2017!", xml, new string[] { }));
                    else
                        doc.LoadXml(timbrado.timbradoPrueba("1094309f-b2cf-4592-9b77-c126117d36a3", "Samuel.Arreola@repobox.com.mx", "Repobox.2017!", xml, new string[] { }));
                }
                else if (rfc == "CEA990831FI8")
                {
                    // CEA
                    //if (SAT)
                    //    doc.LoadXml(timbrado.timbrado("45cd141c-8cd8-41ea-8b2a-22152288ccf3", "Samuel.Arreola@repobox.com.mx", "Rp1234567890$-", xml, new string[] { }));
                    //else
                    //    doc.LoadXml(timbrado.timbradoPrueba("45cd141c-8cd8-41ea-8b2a-22152288ccf3", "Samuel.Arreola@repobox.com.mx", "Rp1234567890$-", xml, new string[] { }));
                    if (SAT)
                        doc.LoadXml(timbrado.timbrado("b37cd5b6-5c4e-4137-99aa-ed4f3f2d7d6d", "Samuel.Arreola@repobox.com.mx", "Repobox.2017!", xml, new string[] { }));
                    else
                        doc.LoadXml(timbrado.timbradoPrueba("b37cd5b6-5c4e-4137-99aa-ed4f3f2d7d6d", "Samuel.Arreola@repobox.com.mx", "Repobox.2017!", xml, new string[] { }));
                }
                else
                    return "RepoBox: RFC no permitido para timbrar.";
                DataSet set = new DataSet();
                using (MemoryStream stream = new MemoryStream())
                {
                    doc.Save(stream);
                    stream.Position = 0L;
                    set.ReadXml(stream);
                }
                if (set.Tables[0].Rows[0]["timbradoOk"].ToString() == "0" || set.Tables[0].Rows[0]["timbradoOk"].ToString().ToUpper() == "FALSE")
                {
                    try
                    {
                        return "RepoBox: " + ErrorMSG(set.Tables[0].Rows[0]["codigo"].ToString() + " - " + set.Tables[0].Rows[0]["mensaje"].ToString());
                    }
                    catch (Exception)
                    {
                        return "RepoBox: " + ErrorMSG(set.Tables[0].Rows[0]["codigo"].ToString() + ".");
                    }
                }
                else
                    return Encoding.UTF8.GetString(Convert.FromBase64String(set.Tables[0].Rows[0]["xmlBase64"].ToString()));
            }
            catch (Exception ex)
            { return "Repobox: " + ErrorMSG(ex.Message); }
        }
        private string ErroresFINKOK(string error, string documento)
        {
            try
            {
                if (error.Contains("El usuario o contraseña son inválidos.") ||
                    error.Contains("Error Inesperado") ||
                    error.Contains("HTTP Error 50") ||
                    error.Contains("Internal Server Error") ||
                    error.Contains("HTTP 50") ||
                    error.Contains("Bad Gateway") ||
                    error.Contains("Type not found") ||
                    error.Contains("No services defined"))
                    return "El servicio de conexion al SAT está temporalmente suspendido. Por favor intente generar la " + documento + " un poco mas";
                if (error.Contains("No es posible conectar con el servidor remoto") || error.Contains("Se excedió el tiempo de espera de la operación"))
                    return "SAT: Por favor intente generar la " + documento + " nuevamente.";
                if (error.Contains("XML mal formado"))
                    return "Por favor verificar los datos ingresados en la " + documento + ". Mensaje SAT: " + error;
                //return "SAT: Por favor intente generar la " + documento + " mas tarde.";
                return "Mensaje SAT: " + error;
            }
            catch (Exception)
            {
                return "SAT: Por favor intente generar la " + documento + " mas tarde.";
            }
        }
        private string ErrorMSG(string msg)
        {
            try
            {
                int msgErrorINT = 0;
                int.TryParse(msg, out msgErrorINT);
                if (msg.Contains("t_RFC"))
                    return "Por favor verifique la estructura del RFC.";
                else if (msgErrorINT > 0)
                    return "Por favor verifique los datos ingresados en la factura y su conexión a internet.";
                else
                    return msg;
            }
            catch (Exception)
            {
                return msg;
            }
        }
        public string Cancelar32(bool SAT, string rfcEmisor, string UUID, string cerPath, string keyPath, string keyPassword, bool acuseBase64)
        {
            string respuesta = Cancelar32(SAT, "PADE", rfcEmisor, UUID, cerPath, keyPath, keyPassword, acuseBase64);
            if(respuesta.ToUpper().StartsWith("REPOBOX"))
                respuesta = Cancelar32(SAT, "FINKOK", rfcEmisor, UUID, cerPath, keyPath, keyPassword, acuseBase64);
            return respuesta;
        }
        private string Cancelar32(bool SAT, string PAC, string rfcEmisor, string UUID, string cerPath, string keyPath, string keyPassword, bool acuseBase64)
        {
            try
            {
                if (PAC == "PADE")
                {
                    string contrato = "", usuario = "", passwordPAC = "";
                    if (rfcEmisor == "SFI950101DU2")
                    {
                        // Secretaría de Finanzas Zacatecas.
                        contrato = "3c6e5dc6-7586-4297-af58-fd916831d431";
                        usuario = "Samuel.Arreola@repobox.com.mx";
                        passwordPAC = "Repobox.2017!";
                    }
                    else if (rfcEmisor == "TGM110214QK0")
                    {
                        // GPG (GrupoSARUWEB)
                        contrato = "1094309f-b2cf-4592-9b77-c126117d36a3";
                        usuario = "Samuel.Arreola@repobox.com.mx";
                        passwordPAC = "Repobox.2017!";
                    }
                    else if (rfcEmisor == "CEA990831FI8")
                    {
                        // CEA
                        //contrato = "45cd141c-8cd8-41ea-8b2a-22152288ccf3";
                        contrato = "b37cd5b6-5c4e-4137-99aa-ed4f3f2d7d6d";
                        usuario = "Samuel.Arreola@repobox.com.mx";
                        //passwordPAC = "Rp1234567890$-";
                        passwordPAC = "Repobox.2017!";
                    }
                    else
                        return "RepoBox: RFC no permitido para timbrar.";

                    if (SAT)
                    {
                        RepoBoxWS.TimbradoService timbrado = new RepoBoxWS.TimbradoService();
                        string acuse = timbrado.cancelar(contrato, usuario, passwordPAC, rfcEmisor, new string[1] { UUID },
                            File.ReadAllBytes(cerPath), File.ReadAllBytes(keyPath), keyPassword);
                        DataSet set = new DataSet();
                        XmlDocument document = new XmlDocument();
                        document.LoadXml(acuse);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            document.Save(stream);
                            stream.Position = 0L;
                            set.ReadXml(stream);
                        }
                        //
                        if (set.Tables["servicioCancel"].Rows[0]["statusOk"].ToString().ToUpper() == "TRUE")
                        {
                            if (acuseBase64)
                                return set.Tables["servicioCancel"].Rows[0]["acuseCancelBase64"].ToString();
                            else
                                return Encoding.UTF8.GetString(Convert.FromBase64String(set.Tables["servicioCancel"].Rows[0]["acuseCancelBase64"].ToString()));
                        }
                        else
                        {
                            if (set.Tables["servicioCancel"].Columns.Contains("mensaje"))
                                return "RepoBox: " + set.Tables["servicioCancel"].Rows[0]["mensaje"].ToString();
                            else if (set.Tables.Contains("cancelacion") && set.Tables["cancelacion"].Rows.Count > 0)
                                return "RepoBox: " + set.Tables["cancelacion"].Rows[0]["mensaje"].ToString();
                            else
                                return "RepoBox: No fué posible realizar la cancelación del CFDI, por favor intentelo de nuevo mas tarde.";
                        }
                    }
                    else
                    {
                        try
                        {
                            return AcuseCancelacionTest(rfcEmisor, UUID, acuseBase64);
                        }
                        catch (Exception)
                        {
                            return "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiIHN0YW5kYWxvbmU9InllcyI/Pgo8QWN1c2UgRmVjaGE9IjIwMTctMDEtMzFUMTA6NDU6MDcuNzExODY0NSIgUmZjRW1pc29yPSJBVUFHODQwODA2TFM5IiB4bWxuczpuczI9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvMDkveG1sZHNpZyMiIHhtbG5zOm5zMz0iaHR0cDovL2NhbmNlbGFjZmQuc2F0LmdvYi5teCI+CiAgICA8bnMzOkZvbGlvcz4KICAgICAgICA8bnMzOlVVSUQ+OEEzMjdDN0MtNTRGQS00RDQ2LUI2MEYtNDI3QTIzNzVBQkQ0PC9uczM6VVVJRD4KICAgICAgICA8bnMzOkVzdGF0dXNVVUlEPjIwMTwvbnMzOkVzdGF0dXNVVUlEPgogICAgPC9uczM6Rm9saW9zPgogICAgPG5zMjpTaWduYXR1cmUgSWQ9IlNlbGxvU0FUIj4KICAgICAgICA8bnMyOlNpZ25lZEluZm8+CiAgICAgICAgICAgIDxuczI6Q2Fub25pY2FsaXphdGlvbk1ldGhvZCBBbGdvcml0aG09Imh0dHA6Ly93d3cudzMub3JnL1RSLzIwMDEvUkVDLXhtbC1jMTRuLTIwMDEwMzE1Ii8+CiAgICAgICAgICAgIDxuczI6U2lnbmF0dXJlTWV0aG9kIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS8wNC94bWxkc2lnLW1vcmUjaG1hYy1zaGE1MTIiLz4KICAgICAgICAgICAgPG5zMjpSZWZlcmVuY2UgVVJJPSIiPgogICAgICAgICAgICAgICAgPG5zMjpUcmFuc2Zvcm1zPgogICAgICAgICAgICAgICAgICAgIDxuczI6VHJhbnNmb3JtIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvVFIvMTk5OS9SRUMteHBhdGgtMTk5OTExMTYiPgogICAgICAgICAgICAgICAgICAgICAgICA8bnMyOlhQYXRoPm5vdChhbmNlc3Rvci1vci1zZWxmOjoqW2xvY2FsLW5hbWUoKT0nU2lnbmF0dXJlJ10pPC9uczI6WFBhdGg+CiAgICAgICAgICAgICAgICAgICAgPC9uczI6VHJhbnNmb3JtPgogICAgICAgICAgICAgICAgPC9uczI6VHJhbnNmb3Jtcz4KICAgICAgICAgICAgICAgIDxuczI6RGlnZXN0TWV0aG9kIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS8wNC94bWxlbmMjc2hhNTEyIi8+CiAgICAgICAgICAgICAgICA8bnMyOkRpZ2VzdFZhbHVlPmVDMEtZZVl3MVVWQmlGWGZsRk1EbHZPa3dlNEJON09ZR3huN3ZEY2U5QkxhRXk1clNpbU4vaVE3bFVhc2k0WkxMUEptQmduR2VOR1VIcjJQaVU2c0NRPT08L25zMjpEaWdlc3RWYWx1ZT4KICAgICAgICAgICAgPC9uczI6UmVmZXJlbmNlPgogICAgICAgIDwvbnMyOlNpZ25lZEluZm8+CiAgICAgICAgPG5zMjpTaWduYXR1cmVWYWx1ZT5GQk9DNGVZbWpsMUlTNm5LMHh0ZXUzcHQ5d1o4SVdRRFRSeUx5UGUrNFZzc2xHREpnOTdEdDJVZTdRL0lzNTNyTjBvREFJZitUOC90UjBjTFZXWjVHdz09PC9uczI6U2lnbmF0dXJlVmFsdWU+CiAgICAgICAgPG5zMjpLZXlJbmZvPgogICAgICAgICAgICA8bnMyOktleU5hbWU+MDAwMDEwODg4ODg4MDAwMDAwMTY8L25zMjpLZXlOYW1lPgogICAgICAgICAgICA8bnMyOktleVZhbHVlPgogICAgICAgICAgICAgICAgPG5zMjpSU0FLZXlWYWx1ZT4KICAgICAgICAgICAgICAgICAgICA8bnMyOk1vZHVsdXM+eG5MMnpEUHRINWpEc0FaRFRJZk1xYktHcnZlK0F0OEt5eDJFWnZiZlhicEs5dVZFeFdTODc0b01lbEZ6TnE2OS9ZcVNSZVQzSTdJOHdyK2pveTVPN291WkgrNEtXZElHcDRTaTZsSGUwa250eHpObXV1S3lPUGtKOXRNY250bkZtUTRiZnhGeGxnL1VkMmhDdHVveTNqMnhZa0lYdTVPNHBHTTk4Tno4cEFNPTwvbnMyOk1vZHVsdXM+CiAgICAgICAgICAgICAgICAgICAgPG5zMjpFeHBvbmVudD5BUUFCPC9uczI6RXhwb25lbnQ+CiAgICAgICAgICAgICAgICA8L25zMjpSU0FLZXlWYWx1ZT4KICAgICAgICAgICAgPC9uczI6S2V5VmFsdWU+CiAgICAgICAgPC9uczI6S2V5SW5mbz4KICAgIDwvbnMyOlNpZ25hdHVyZT4KPC9BY3VzZT4K";
                        }
                    }
                }
                else
                {
                    if (SAT)
                    {

                        RepoWS_Cancel.CancelSOAP concel = new RepoWS_Cancel.CancelSOAP();
                        RepoWS_Cancel.cancel64Response respuesta = new RepoWS_Cancel.cancel64Response();
                        System.Text.ASCIIEncoding codificador = new ASCIIEncoding();
                        RepoWS_Cancel.cancel64 can = new RepoWS_Cancel.cancel64();
                        byte[] Keyby = System.IO.File.ReadAllBytes(keyPath);
                        can.key = (System.Convert.ToBase64String(Keyby));
                        byte[] Cerby = System.IO.File.ReadAllBytes(cerPath);
                        can.cer = (System.Convert.ToBase64String(Cerby));

                        RepoWS_Cancel.UUIDS uuid = new RepoWS_Cancel.UUIDS();
                        //String[] uuide = new String[] { "A81C0410-9AB9-408B-8998-96A3AF40508A" };
                        uuid.uuids = new string[] { UUID };
                        can.username = "alicom@cepdi.mx";
                        can.password = "@L1com,,";
                        can.taxpayer_id = rfcEmisor;
                        can.store_pending = true;
                        can.UUIDS = uuid;
                        can.passllave = Convert.ToBase64String(codificador.GetBytes(keyPassword));

                        respuesta = concel.cancel64(can);

                        if (respuesta.cancel64Result.Acuse != null)
                        {
                            if (acuseBase64)
                                return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(respuesta.cancel64Result.Acuse));
                            else
                                return respuesta.cancel64Result.Acuse;
                        }
                        else
                            return "RepoBox: " + respuesta.cancel64Result.CodEstatus;
                    }
                    else
                    {
                        try
                        {
                            return AcuseCancelacionTest(rfcEmisor, UUID, acuseBase64);
                        }
                        catch (Exception)
                        {
                            return "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiIHN0YW5kYWxvbmU9InllcyI/Pgo8QWN1c2UgRmVjaGE9IjIwMTctMDEtMzFUMTA6NDU6MDcuNzExODY0NSIgUmZjRW1pc29yPSJBVUFHODQwODA2TFM5IiB4bWxuczpuczI9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvMDkveG1sZHNpZyMiIHhtbG5zOm5zMz0iaHR0cDovL2NhbmNlbGFjZmQuc2F0LmdvYi5teCI+CiAgICA8bnMzOkZvbGlvcz4KICAgICAgICA8bnMzOlVVSUQ+OEEzMjdDN0MtNTRGQS00RDQ2LUI2MEYtNDI3QTIzNzVBQkQ0PC9uczM6VVVJRD4KICAgICAgICA8bnMzOkVzdGF0dXNVVUlEPjIwMTwvbnMzOkVzdGF0dXNVVUlEPgogICAgPC9uczM6Rm9saW9zPgogICAgPG5zMjpTaWduYXR1cmUgSWQ9IlNlbGxvU0FUIj4KICAgICAgICA8bnMyOlNpZ25lZEluZm8+CiAgICAgICAgICAgIDxuczI6Q2Fub25pY2FsaXphdGlvbk1ldGhvZCBBbGdvcml0aG09Imh0dHA6Ly93d3cudzMub3JnL1RSLzIwMDEvUkVDLXhtbC1jMTRuLTIwMDEwMzE1Ii8+CiAgICAgICAgICAgIDxuczI6U2lnbmF0dXJlTWV0aG9kIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS8wNC94bWxkc2lnLW1vcmUjaG1hYy1zaGE1MTIiLz4KICAgICAgICAgICAgPG5zMjpSZWZlcmVuY2UgVVJJPSIiPgogICAgICAgICAgICAgICAgPG5zMjpUcmFuc2Zvcm1zPgogICAgICAgICAgICAgICAgICAgIDxuczI6VHJhbnNmb3JtIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvVFIvMTk5OS9SRUMteHBhdGgtMTk5OTExMTYiPgogICAgICAgICAgICAgICAgICAgICAgICA8bnMyOlhQYXRoPm5vdChhbmNlc3Rvci1vci1zZWxmOjoqW2xvY2FsLW5hbWUoKT0nU2lnbmF0dXJlJ10pPC9uczI6WFBhdGg+CiAgICAgICAgICAgICAgICAgICAgPC9uczI6VHJhbnNmb3JtPgogICAgICAgICAgICAgICAgPC9uczI6VHJhbnNmb3Jtcz4KICAgICAgICAgICAgICAgIDxuczI6RGlnZXN0TWV0aG9kIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS8wNC94bWxlbmMjc2hhNTEyIi8+CiAgICAgICAgICAgICAgICA8bnMyOkRpZ2VzdFZhbHVlPmVDMEtZZVl3MVVWQmlGWGZsRk1EbHZPa3dlNEJON09ZR3huN3ZEY2U5QkxhRXk1clNpbU4vaVE3bFVhc2k0WkxMUEptQmduR2VOR1VIcjJQaVU2c0NRPT08L25zMjpEaWdlc3RWYWx1ZT4KICAgICAgICAgICAgPC9uczI6UmVmZXJlbmNlPgogICAgICAgIDwvbnMyOlNpZ25lZEluZm8+CiAgICAgICAgPG5zMjpTaWduYXR1cmVWYWx1ZT5GQk9DNGVZbWpsMUlTNm5LMHh0ZXUzcHQ5d1o4SVdRRFRSeUx5UGUrNFZzc2xHREpnOTdEdDJVZTdRL0lzNTNyTjBvREFJZitUOC90UjBjTFZXWjVHdz09PC9uczI6U2lnbmF0dXJlVmFsdWU+CiAgICAgICAgPG5zMjpLZXlJbmZvPgogICAgICAgICAgICA8bnMyOktleU5hbWU+MDAwMDEwODg4ODg4MDAwMDAwMTY8L25zMjpLZXlOYW1lPgogICAgICAgICAgICA8bnMyOktleVZhbHVlPgogICAgICAgICAgICAgICAgPG5zMjpSU0FLZXlWYWx1ZT4KICAgICAgICAgICAgICAgICAgICA8bnMyOk1vZHVsdXM+eG5MMnpEUHRINWpEc0FaRFRJZk1xYktHcnZlK0F0OEt5eDJFWnZiZlhicEs5dVZFeFdTODc0b01lbEZ6TnE2OS9ZcVNSZVQzSTdJOHdyK2pveTVPN291WkgrNEtXZElHcDRTaTZsSGUwa250eHpObXV1S3lPUGtKOXRNY250bkZtUTRiZnhGeGxnL1VkMmhDdHVveTNqMnhZa0lYdTVPNHBHTTk4Tno4cEFNPTwvbnMyOk1vZHVsdXM+CiAgICAgICAgICAgICAgICAgICAgPG5zMjpFeHBvbmVudD5BUUFCPC9uczI6RXhwb25lbnQ+CiAgICAgICAgICAgICAgICA8L25zMjpSU0FLZXlWYWx1ZT4KICAgICAgICAgICAgPC9uczI6S2V5VmFsdWU+CiAgICAgICAgPC9uczI6S2V5SW5mbz4KICAgIDwvbnMyOlNpZ25hdHVyZT4KPC9BY3VzZT4K";
                        }
                    }
                }
            }
            catch (Exception)
            {
                return "RepoBox: No fué posible realizar la cancelación del CFDI, por favor intentelo de nuevo mas tarde.";
            }
        }
        private void GenerarQR(string rfcEmisor, string rfcReceptor, string UUID, double total)
        {
            QRCodeEncoder qr = new QRCodeEncoder();
            Bitmap customImage;
            string varQR = "?\nre=" + rfcEmisor;
            varQR += "&rr=" + rfcReceptor;
            varQR += "&tt=" + total.ToString("N4");
            varQR += "&id=" + UUID;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Convert Image to byte[]
                    customImage = qr.Encode(varQR, Encoding.UTF8);
                    customImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();
                    System.IO.File.WriteAllBytes(_Raiz + "\\" + rfcEmisor + "\\CFDI\\" + UUID + ".png", imageBytes);
                }
            }
            catch (Exception)
            {
                throw new Exception("RepoBox: No fué posible generar el Sello Bidimensional.");
            }
        }
        public string GenerarPDF_PathFile(string xml, string pathLogo)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                if (!File.Exists(xml))
                    throw new Exception("RepoBox: No se pudo encontrar el archivo XML");
                doc.Load(xml);
                return this.GenerarPDF(doc.InnerXml, pathLogo, xml.Replace(".XML", ""));
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("RepoBox:"))
                    return ex.Message;
                else
                    throw new Exception("RepoBox: Verifique que exista el archivo XML en la ruta enviada.");
            }
        }
        public string GenerarPDF_PathFile33(string xml, string pathLogo)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                if (!File.Exists(xml))
                    throw new Exception("RepoBox: No se pudo encontrar el archivo XML");
                doc.Load(xml);
                return this.GenerarPDF33(doc.InnerXml, pathLogo, xml.Replace(".XML", ""));
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("RepoBox:"))
                    return ex.Message;
                else
                    throw new Exception("RepoBox: Verifique que exista el archivo XML en la ruta enviada.");
            }
        }
        public string GenerarPDF(string xml, string pathLogo, string pdfName)
        {
            try
            {
                DataSet set = new DataSet();
                // Datos
                set.Tables.Add(GetReportDS(xml));
                // Imagenes
                GenerarQR(set.Tables["CFDIv32"].Rows[0]["EmisorRFC"].ToString(), set.Tables["CFDIv32"].Rows[0]["ReceptorRFC"].ToString(),
                    set.Tables["CFDIv32"].Rows[0]["UUID"].ToString(), Convert.ToDouble(set.Tables["CFDIv32"].Rows[0]["Total"].ToString()));
                set.Tables.Add(GetImagenesDS(pathLogo, set.Tables["CFDIv32"].Rows[0]["EmisorRFC"].ToString(), set.Tables["CFDIv32"].Rows[0]["UUID"].ToString()));

                if (!File.Exists(_Raiz + "\\Base\\CFDIv32.rpt"))
                    throw new Exception("RepoBox: No se encontró el formato base.");
                ReportDocument m_Document = new ReportDocument();
                m_Document.Load(_Raiz + "\\Base\\CFDIv32.rpt");
                m_Document.SetDataSource(set);
                m_Document.Refresh();
                m_Document.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfName + ".PDF");
                return pdfName + ".PDF";
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("RepoBox:"))
                    return ex.Message;
                else
                    return "RepoBox: No fué posible generar el formato PDF.";
            }
        }
        public string GenerarPDF33(string xml, string pathLogo, string pdfName, SATCats satCats = null)
        {
            try
            {
                DataSet set = new DataSet();
                // Datos
                set.Tables.Add(GetReportDS33(xml, satCats));
                // Imagenes
                GenerarQR(set.Tables["CFDIv33"].Rows[0]["EmisorRFC"].ToString(), set.Tables["CFDIv33"].Rows[0]["ReceptorRFC"].ToString(),
                    set.Tables["CFDIv33"].Rows[0]["UUID"].ToString(), Convert.ToDouble(set.Tables["CFDIv33"].Rows[0]["Total"].ToString()));
                set.Tables.Add(GetImagenesDS(pathLogo, set.Tables["CFDIv33"].Rows[0]["EmisorRFC"].ToString(), set.Tables["CFDIv33"].Rows[0]["UUID"].ToString()));

                if (!File.Exists(_Raiz + "\\Base\\CFDIv33.rpt"))
                    throw new Exception("RepoBox: No se encontró el formato base.");
                ReportDocument m_Document = new ReportDocument();
                m_Document.Load(_Raiz + "\\Base\\CFDIv33.rpt");
                m_Document.SetDataSource(set);
                m_Document.Refresh();
                m_Document.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfName + ".PDF");
                return pdfName + ".PDF";
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("RepoBox:"))
                    return ex.Message;
                else
                    return "RepoBox: No fué posible generar el formato PDF.";
            }
        }
        private DataTable GetReportDS(string xml)
        {
            try
            {
                DataTable tabla = new DataTable("CFDIv32");
                tabla.Columns.AddRange(new DataColumn[] {
                                                           //Comprobante
                                                           new DataColumn("Version", typeof(string)),
                                                           new DataColumn("Serie", typeof(string)),
                                                           new DataColumn("Folio", typeof(string)),
                                                           new DataColumn("Fecha", typeof(string)),
                                                           new DataColumn("Sello", typeof(string)),
                                                           new DataColumn("FormaDePago", typeof(string)),
                                                           new DataColumn("NoCertificado", typeof(string)),
                                                           new DataColumn("CondicionesDePago", typeof(string)),
                                                           new DataColumn("SubTotal", typeof(decimal)),
                                                           new DataColumn("Descuento", typeof(decimal)),
                                                           new DataColumn("IEPS", typeof(decimal)),
                                                           new DataColumn("IVA", typeof(decimal)),
                                                           new DataColumn("IVARet", typeof(decimal)),
                                                           new DataColumn("ISRRet", typeof(decimal)),
                                                           new DataColumn("Total", typeof(decimal)),
                                                           new DataColumn("MotivoDescuento", typeof(string)),
                                                           new DataColumn("TipoDeCambio", typeof(string)),
                                                           new DataColumn("Moneda", typeof(string)),
                                                           new DataColumn("MetodoDePago", typeof(string)),
                                                           new DataColumn("LugarDeExpedicion", typeof(string)),
                                                           new DataColumn("NumCta", typeof(string)),
                                                           new DataColumn("TipoDeComprobante", typeof(string)),
                                                           // Emisor
                                                           new DataColumn("EmisorRFC", typeof(string)),
                                                           new DataColumn("EmisorRazonSocial", typeof(string)),
                                                           new DataColumn("EmisorRegimenFiscal", typeof(string)),
                                                           // Emisor Direccion Fiscal
                                                           new DataColumn("EmisorDireccionCalle", typeof(string)),
                                                           new DataColumn("EmisorDireccionNoExterior", typeof(string)),
                                                           new DataColumn("EmisorDireccionNoInterior", typeof(string)),
                                                           new DataColumn("EmisorDireccionReferencia", typeof(string)),
                                                           new DataColumn("EmisorDireccionColonia", typeof(string)),
                                                           new DataColumn("EmisorDireccionCiudad", typeof(string)),
                                                           new DataColumn("EmisorDireccionMunicipio", typeof(string)),
                                                           new DataColumn("EmisorDireccionEstado", typeof(string)),
                                                           new DataColumn("EmisorDireccionPais", typeof(string)),
                                                           new DataColumn("EmisorDireccionCodigoPostal", typeof(string)),
                                                           // Emisor Expedido En
                                                           new DataColumn("EmisorExpedidoEnCalle", typeof(string)),
                                                           new DataColumn("EmisorExpedidoEnNoExterior", typeof(string)),
                                                           new DataColumn("EmisorExpedidoEnNoInterior", typeof(string)),
                                                           new DataColumn("EmisorExpedidoEnReferencia", typeof(string)),
                                                           new DataColumn("EmisorExpedidoEnColonia", typeof(string)),
                                                           new DataColumn("EmisorExpedidoEnCiudad", typeof(string)),
                                                           new DataColumn("EmisorExpedidoEnMunicipio", typeof(string)),
                                                           new DataColumn("EmisorExpedidoEnEstado", typeof(string)),
                                                           new DataColumn("EmisorExpedidoEnPais", typeof(string)),
                                                           new DataColumn("EmisorExpedidoEnCodigoPostal", typeof(string)),
                                                           // Receptor
                                                           new DataColumn("ReceptorRFC", typeof(string)),
                                                           new DataColumn("ReceptorRazonSocial", typeof(string)),
                                                           // Receptor Direccion Fiscal
                                                           new DataColumn("ReceptorDireccionCalle", typeof(string)),
                                                           new DataColumn("ReceptorDireccionNoExterior", typeof(string)),
                                                           new DataColumn("ReceptorDireccionNoInterior", typeof(string)),
                                                           new DataColumn("ReceptorDireccionReferencia", typeof(string)),
                                                           new DataColumn("ReceptorDireccionColonia", typeof(string)),
                                                           new DataColumn("ReceptorDireccionCiudad", typeof(string)),
                                                           new DataColumn("ReceptorDireccionMunicipio", typeof(string)),
                                                           new DataColumn("ReceptorDireccionEstado", typeof(string)),
                                                           new DataColumn("ReceptorDireccionPais", typeof(string)),
                                                           new DataColumn("ReceptorDireccionCodigoPostal", typeof(string)),
                                                           // Conceptos 
                                                           new DataColumn("ConceptoCodigo", typeof(string)),
                                                           new DataColumn("ConceptoDescripcion", typeof(string)),
                                                           new DataColumn("ConceptoUnidad", typeof(string)),
                                                           new DataColumn("ConceptoCantidad", typeof(decimal)),
                                                           new DataColumn("ConceptoPrecio", typeof(decimal)),
                                                           new DataColumn("ConceptoImporte", typeof(decimal)),
                                                           // TimbreFiscalDigital
                                                           //noCertificadoSAT="20001000000300022323" selloCFD="" selloSAT=""
                                                           new DataColumn("TFDFecha", typeof(string)),
                                                           new DataColumn("UUID", typeof(string)),
                                                           new DataColumn("TFDNoCertificadoSAT", typeof(string)),
                                                           new DataColumn("TFDSelloCFD", typeof(string)),
                                                           new DataColumn("TFDSelloSAT", typeof(string)),
                                                           new DataColumn("Observaciones", typeof(string)),
                                                           new DataColumn("Banco", typeof(string))
                                                       });
                //tabla.WriteXmlSchema(@"C:\Repobox\Timbrado\schema.xsd");
                // Llenar tabla
                DataSet set = new DataSet();
                XmlDocument document = new XmlDocument();
                document.LoadXml(xml);
                using (MemoryStream stream = new MemoryStream())
                {
                    document.Save(stream);
                    stream.Position = 0L;
                    set.ReadXml(stream);
                }
                decimal IVA = 0, IEPS = 0, IVARet = 0, ISRRet = 0;
                string Observaciones = "", Banco = "";
                // Sacar impuestos

                if (set.Tables.Contains("Retencion"))
                    foreach (DataRow row in set.Tables["Retencion"].Rows)
                        if (set.Tables["Retencion"].Columns.Contains("impuesto"))
                            if (row["impuesto"].ToString() == "IVA")
                                IVARet += (set.Tables["Retencion"].Columns.Contains("importe") ? Convert.ToDecimal(row["importe"].ToString()) : 0);
                            else
                                ISRRet += (set.Tables["Retencion"].Columns.Contains("importe") ? Convert.ToDecimal(row["importe"].ToString()) : 0);
                if (set.Tables.Contains("Traslado"))
                    foreach (DataRow row in set.Tables["Traslado"].Rows)
                        if (set.Tables["Traslado"].Columns.Contains("impuesto"))
                            if (row["impuesto"].ToString() == "IVA")
                                IVA += (set.Tables["Traslado"].Columns.Contains("importe") ? Convert.ToDecimal(row["importe"].ToString()) : 0);
                            else
                                IEPS += (set.Tables["Traslado"].Columns.Contains("importe") ? Convert.ToDecimal(row["importe"].ToString()) : 0);
                foreach (XmlNode node in document.DocumentElement.ChildNodes)
                    if(node.Name.Contains("Addenda"))
                        foreach (XmlNode nodeAdd in node.ChildNodes)
                            if (nodeAdd.Name.Contains("RepoBox") || nodeAdd.Name.Contains("RepoBox"))
                            {
                                Observaciones = (nodeAdd.Attributes["Observaciones"] != null ? nodeAdd.Attributes["Observaciones"].Value : "");
                                Banco = (nodeAdd.Attributes["Banco"] != null ? nodeAdd.Attributes["Banco"].Value : "");
                                break;
                            }
                //if (set.Tables.Contains("RepoBox"))
                //    foreach (DataRow row in set.Tables["RepoBox"].Rows)
                //    {
                //        Observaciones = (set.Tables["RepoBox"].Columns.Contains("Observaciones") ? row["Observaciones"].ToString() : "");
                //        Banco = (set.Tables["RepoBox"].Columns.Contains("Banco") ? row["Banco"].ToString() : "");
                //        break;
                //    }
                if (set.Tables.Contains("Concepto"))
                    foreach (DataRow fila in set.Tables["Concepto"].Rows)
                    {
                        System.Data.DataRow row = tabla.NewRow();
                        row["Version"] = (set.Tables["Comprobante"].Columns.Contains("version") ? set.Tables["Comprobante"].Rows[0]["version"] : "");
                        row["Serie"] = (set.Tables["Comprobante"].Columns.Contains("serie") ? set.Tables["Comprobante"].Rows[0]["serie"] : "");
                        row["Folio"] = (set.Tables["Comprobante"].Columns.Contains("folio") ? set.Tables["Comprobante"].Rows[0]["folio"] : "");
                        row["Fecha"] = (set.Tables["Comprobante"].Columns.Contains("fecha") ? set.Tables["Comprobante"].Rows[0]["fecha"] : "");
                        row["Sello"] = (set.Tables["Comprobante"].Columns.Contains("sello") ? set.Tables["Comprobante"].Rows[0]["sello"] : "");
                        row["FormaDePago"] = (set.Tables["Comprobante"].Columns.Contains("formaDePago") ? set.Tables["Comprobante"].Rows[0]["formaDePago"] : "");
                        row["NoCertificado"] = (set.Tables["Comprobante"].Columns.Contains("noCertificado") ? set.Tables["Comprobante"].Rows[0]["noCertificado"] : "");
                        row["CondicionesDePago"] = (set.Tables["Comprobante"].Columns.Contains("condicionesDePago") ? set.Tables["Comprobante"].Rows[0]["condicionesDePago"] : "");
                        row["SubTotal"] = DecimalValue(set.Tables["Comprobante"].Rows[0]["subTotal"]);
                        row["Descuento"] = (set.Tables["Comprobante"].Columns.Contains("descuento") ? DecimalValue(set.Tables["Comprobante"].Rows[0]["descuento"]) : 0);
                        row["IEPS"] = IEPS;
                        row["IVA"] = IVA;
                        row["IVARet"] = IVARet;
                        row["ISRRet"] = ISRRet;
                        row["Total"] = DecimalValue(set.Tables["Comprobante"].Rows[0]["total"]);
                        row["MotivoDescuento"] = (set.Tables["Comprobante"].Columns.Contains("motivoDescuento") ? set.Tables["Comprobante"].Rows[0]["motivoDescuento"] : ""); ;
                        row["TipoDeCambio"] = (set.Tables["Comprobante"].Columns.Contains("TipoCambio") ? set.Tables["Comprobante"].Rows[0]["TipoCambio"] : "");
                        row["Moneda"] = (set.Tables["Comprobante"].Columns.Contains("Moneda") ? set.Tables["Comprobante"].Rows[0]["Moneda"] : "");
                        row["MetodoDePago"] = (set.Tables["Comprobante"].Columns.Contains("metodoDePago") ? set.Tables["Comprobante"].Rows[0]["metodoDePago"] : "");
                        row["LugarDeExpedicion"] = (set.Tables["Comprobante"].Columns.Contains("LugarExpedicion") ? set.Tables["Comprobante"].Rows[0]["LugarExpedicion"] : "");
                        row["NumCta"] = (set.Tables["Comprobante"].Columns.Contains("numCtaPago") ? set.Tables["Comprobante"].Rows[0]["numCtaPago"] : "");
                        row["TipoDeComprobante"] = (set.Tables["Comprobante"].Columns.Contains("tipoDeComprobante") ? set.Tables["Comprobante"].Rows[0]["tipoDeComprobante"] : "");
                        row["Observaciones"] = Observaciones;
                        row["Banco"] = Banco;
                        // Emisor
                        row["EmisorRFC"] = StringValue(set.Tables["Emisor"].Rows[0]["rfc"]);
                        row["EmisorRazonSocial"] = (set.Tables["Emisor"].Columns.Contains("nombre") ? set.Tables["Emisor"].Rows[0]["nombre"] : "");
                        if (set.Tables.Contains("RegimenFiscal"))
                            row["EmisorRegimenFiscal"] = (set.Tables["RegimenFiscal"].Columns.Contains("Regimen") ? set.Tables["RegimenFiscal"].Rows[0]["Regimen"] : "");
                        else
                            row["EmisorRegimenFiscal"] = "";
                        // Emisor Direccion Fiscal
                        if (set.Tables.Contains("DomicilioFiscal"))
                        {
                            row["EmisorDireccionCalle"] = (set.Tables["DomicilioFiscal"].Columns.Contains("calle") ? set.Tables["DomicilioFiscal"].Rows[0]["calle"] : "");
                            row["EmisorDireccionNoExterior"] = (set.Tables["DomicilioFiscal"].Columns.Contains("noExterior") ? set.Tables["DomicilioFiscal"].Rows[0]["noExterior"] : "");
                            row["EmisorDireccionNoInterior"] = (set.Tables["DomicilioFiscal"].Columns.Contains("noInterior") ? set.Tables["DomicilioFiscal"].Rows[0]["noInterior"] : "");
                            row["EmisorDireccionReferencia"] = (set.Tables["DomicilioFiscal"].Columns.Contains("referencia") ? set.Tables["DomicilioFiscal"].Rows[0]["referencia"] : "");
                            row["EmisorDireccionColonia"] = (set.Tables["DomicilioFiscal"].Columns.Contains("colonia") ? set.Tables["DomicilioFiscal"].Rows[0]["colonia"] : "");
                            row["EmisorDireccionCiudad"] = (set.Tables["DomicilioFiscal"].Columns.Contains("localidad") ? set.Tables["DomicilioFiscal"].Rows[0]["localidad"] : "");
                            row["EmisorDireccionMunicipio"] = (set.Tables["DomicilioFiscal"].Columns.Contains("municipio") ? set.Tables["DomicilioFiscal"].Rows[0]["municipio"] : "");
                            row["EmisorDireccionEstado"] = (set.Tables["DomicilioFiscal"].Columns.Contains("estado") ? set.Tables["DomicilioFiscal"].Rows[0]["estado"] : "");
                            row["EmisorDireccionPais"] = (set.Tables["DomicilioFiscal"].Columns.Contains("pais") ? set.Tables["DomicilioFiscal"].Rows[0]["pais"] : "");
                            row["EmisorDireccionCodigoPostal"] = (set.Tables["DomicilioFiscal"].Columns.Contains("codigoPostal") ? set.Tables["DomicilioFiscal"].Rows[0]["codigoPostal"] : "");
                        }
                        else
                        {
                            row["EmisorDireccionCalle"] = "";
                            row["EmisorDireccionNoExterior"] = "";
                            row["EmisorDireccionNoInterior"] = "";
                            row["EmisorDireccionReferencia"] = "";
                            row["EmisorDireccionColonia"] = "";
                            row["EmisorDireccionCiudad"] = "";
                            row["EmisorDireccionMunicipio"] = "";
                            row["EmisorDireccionEstado"] = "";
                            row["EmisorDireccionPais"] = "";
                            row["EmisorDireccionCodigoPostal"] = "";
                        }
                        // Emisor Expedido En
                        if (set.Tables.Contains("ExpedidoEn"))
                        {
                            row["EmisorExpedidoEnCalle"] = (set.Tables["ExpedidoEn"].Columns.Contains("calle") ? set.Tables["ExpedidoEn"].Rows[0]["calle"] : "");
                            row["EmisorExpedidoEnNoExterior"] = (set.Tables["ExpedidoEn"].Columns.Contains("noExterior") ? set.Tables["ExpedidoEn"].Rows[0]["noExterior"] : "");
                            row["EmisorExpedidoEnNoInterior"] = (set.Tables["ExpedidoEn"].Columns.Contains("noInterior") ? set.Tables["ExpedidoEn"].Rows[0]["noInterior"] : "");
                            row["EmisorExpedidoEnReferencia"] = (set.Tables["ExpedidoEn"].Columns.Contains("referencia") ? set.Tables["ExpedidoEn"].Rows[0]["referencia"] : "");
                            row["EmisorExpedidoEnColonia"] = (set.Tables["ExpedidoEn"].Columns.Contains("colonia") ? set.Tables["ExpedidoEn"].Rows[0]["colonia"] : "");
                            row["EmisorExpedidoEnCiudad"] = (set.Tables["ExpedidoEn"].Columns.Contains("localidad") ? set.Tables["ExpedidoEn"].Rows[0]["localidad"] : "");
                            row["EmisorExpedidoEnMunicipio"] = (set.Tables["ExpedidoEn"].Columns.Contains("municipio") ? set.Tables["ExpedidoEn"].Rows[0]["municipio"] : "");
                            row["EmisorExpedidoEnEstado"] = (set.Tables["ExpedidoEn"].Columns.Contains("estado") ? set.Tables["ExpedidoEn"].Rows[0]["estado"] : "");
                            row["EmisorExpedidoEnPais"] = (set.Tables["ExpedidoEn"].Columns.Contains("pais") ? set.Tables["ExpedidoEn"].Rows[0]["pais"] : "");
                            row["EmisorExpedidoEnCodigoPostal"] = (set.Tables["ExpedidoEn"].Columns.Contains("codigoPostal") ? set.Tables["ExpedidoEn"].Rows[0]["codigoPostal"] : "");
                        }
                        else
                        {
                            row["EmisorExpedidoEnCalle"] = "";
                            row["EmisorExpedidoEnNoExterior"] = "";
                            row["EmisorExpedidoEnNoInterior"] = "";
                            row["EmisorExpedidoEnReferencia"] = "";
                            row["EmisorExpedidoEnColonia"] = "";
                            row["EmisorExpedidoEnCiudad"] = "";
                            row["EmisorExpedidoEnMunicipio"] = "";
                            row["EmisorExpedidoEnEstado"] = "";
                            row["EmisorExpedidoEnPais"] = "";
                            row["EmisorExpedidoEnCodigoPostal"] = "";
                        }
                        // Receptor
                        row["ReceptorRFC"] = StringValue(set.Tables["Receptor"].Rows[0]["rfc"]);
                        row["ReceptorRazonSocial"] = (set.Tables["Receptor"].Columns.Contains("nombre") ? set.Tables["Receptor"].Rows[0]["nombre"] : "");
                        // Receptor Direccion Fiscal
                        if (set.Tables.Contains("Domicilio"))
                        {
                            row["ReceptorDireccionCalle"] = (set.Tables["Domicilio"].Columns.Contains("calle") ? set.Tables["Domicilio"].Rows[0]["calle"] : "");
                            row["ReceptorDireccionNoExterior"] = (set.Tables["Domicilio"].Columns.Contains("noExterior") ? set.Tables["Domicilio"].Rows[0]["noExterior"] : "");
                            row["ReceptorDireccionNoInterior"] = (set.Tables["Domicilio"].Columns.Contains("noInterior") ? set.Tables["Domicilio"].Rows[0]["noInterior"] : "");
                            row["ReceptorDireccionReferencia"] = (set.Tables["Domicilio"].Columns.Contains("referencia") ? set.Tables["Domicilio"].Rows[0]["referencia"] : "");
                            row["ReceptorDireccionColonia"] = (set.Tables["Domicilio"].Columns.Contains("colonia") ? set.Tables["Domicilio"].Rows[0]["colonia"] : "");
                            row["ReceptorDireccionCiudad"] = (set.Tables["Domicilio"].Columns.Contains("localidad") ? set.Tables["Domicilio"].Rows[0]["localidad"] : "");
                            row["ReceptorDireccionMunicipio"] = (set.Tables["Domicilio"].Columns.Contains("municipio") ? set.Tables["Domicilio"].Rows[0]["municipio"] : "");
                            row["ReceptorDireccionEstado"] = (set.Tables["Domicilio"].Columns.Contains("estado") ? set.Tables["Domicilio"].Rows[0]["estado"] : "");
                            row["ReceptorDireccionPais"] = (set.Tables["Domicilio"].Columns.Contains("pais") ? set.Tables["Domicilio"].Rows[0]["pais"] : "");
                            row["ReceptorDireccionCodigoPostal"] = (set.Tables["Domicilio"].Columns.Contains("codigoPostal") ? set.Tables["Domicilio"].Rows[0]["codigoPostal"] : "");
                        }
                        else
                        {
                            row["ReceptorDireccionCalle"] = "";
                            row["ReceptorDireccionNoExterior"] = "";
                            row["ReceptorDireccionNoInterior"] = "";
                            row["ReceptorDireccionReferencia"] = "";
                            row["ReceptorDireccionColonia"] = "";
                            row["ReceptorDireccionCiudad"] = "";
                            row["ReceptorDireccionMunicipio"] = "";
                            row["ReceptorDireccionEstado"] = "";
                            row["ReceptorDireccionPais"] = "";
                            row["ReceptorDireccionCodigoPostal"] = "";
                        }
                        // Conceptos 
                        row["ConceptoCodigo"] = (set.Tables["Concepto"].Columns.Contains("noIdentificacion") ? StringValue(fila["noIdentificacion"]) : "");
                        row["ConceptoDescripcion"] = (set.Tables["Concepto"].Columns.Contains("descripcion") ? StringValue(fila["descripcion"]) : "");
                        row["ConceptoUnidad"] = (set.Tables["Concepto"].Columns.Contains("unidad") ? StringValue(fila["unidad"]) : "");
                        row["ConceptoCantidad"] = DecimalValue(fila["cantidad"]);
                        row["ConceptoPrecio"] = DecimalValue(fila["valorUnitario"]);
                        row["ConceptoImporte"] = DecimalValue(fila["importe"]);
                        // TimbreFiscalDigital
                        //noCertificadoSAT="20001000000300022323" selloCFD="" selloSAT=""
                        row["TFDFecha"] = StringValue(set.Tables["TimbreFiscalDigital"].Rows[0]["FechaTimbrado"]);
                        row["UUID"] = StringValue(set.Tables["TimbreFiscalDigital"].Rows[0]["UUID"]);
                        row["TFDNoCertificadoSAT"] = StringValue(set.Tables["TimbreFiscalDigital"].Rows[0]["noCertificadoSAT"]);
                        row["TFDSelloCFD"] = StringValue(set.Tables["TimbreFiscalDigital"].Rows[0]["selloCFD"]);
                        row["TFDSelloSAT"] = StringValue(set.Tables["TimbreFiscalDigital"].Rows[0]["selloSAT"]);
                        tabla.Rows.Add(row);
                    }
                return tabla;
            }
            catch (Exception)
            {
                throw new Exception("RepoBox: No fué posible generar la fuente de datos del archivo PDF.");
            }
        }
        private DataTable GetReportDS33(string xml, SATCats satCats)
        {
            try
            {
                if (satCats == null)
                    satCats = new SATCats();
                if (satCats.UsosCFDI == null || satCats.UsosCFDI.Count == 0)
                    satCats.CargarCatalogos();
                DataTable tabla = new DataTable("CFDIv33");
                tabla.Columns.AddRange(new DataColumn[] {
                                                           //Comprobante
                                                           new DataColumn("Version", typeof(string)),
                                                           new DataColumn("Serie", typeof(string)),
                                                           new DataColumn("Folio", typeof(string)),
                                                           new DataColumn("Fecha", typeof(string)),
                                                           new DataColumn("Sello", typeof(string)),
                                                           new DataColumn("FormaDePago", typeof(string)),
                                                           new DataColumn("NoCertificado", typeof(string)),
                                                           new DataColumn("CondicionesDePago", typeof(string)),
                                                           new DataColumn("SubTotal", typeof(decimal)),
                                                           new DataColumn("Descuento", typeof(decimal)),
                                                           new DataColumn("IEPS", typeof(decimal)),
                                                           new DataColumn("IVA", typeof(decimal)),
                                                           new DataColumn("IVARet", typeof(decimal)),
                                                           new DataColumn("ISRRet", typeof(decimal)),
                                                           new DataColumn("Total", typeof(decimal)),
                                                           new DataColumn("MotivoDescuento", typeof(string)),
                                                           new DataColumn("TipoDeCambio", typeof(string)),
                                                           new DataColumn("Moneda", typeof(string)),
                                                           new DataColumn("MetodoDePago", typeof(string)),
                                                           new DataColumn("LugarDeExpedicion", typeof(string)),
                                                           
                                                           new DataColumn("TipoDeComprobante", typeof(string)),
                                                           // Emisor
                                                           new DataColumn("EmisorRFC", typeof(string)),
                                                           new DataColumn("EmisorRazonSocial", typeof(string)),
                                                           new DataColumn("EmisorRegimenFiscal", typeof(string)),
                                                           // Emisor Direccion Fiscal
                                                           //new DataColumn("EmisorDireccionCalle", typeof(string)),
                                                           //new DataColumn("EmisorDireccionNoExterior", typeof(string)),
                                                           //new DataColumn("EmisorDireccionNoInterior", typeof(string)),
                                                           //new DataColumn("EmisorDireccionReferencia", typeof(string)),
                                                           //new DataColumn("EmisorDireccionColonia", typeof(string)),
                                                           //new DataColumn("EmisorDireccionCiudad", typeof(string)),
                                                           //new DataColumn("EmisorDireccionMunicipio", typeof(string)),
                                                           //new DataColumn("EmisorDireccionEstado", typeof(string)),
                                                           //new DataColumn("EmisorDireccionPais", typeof(string)),
                                                           //new DataColumn("EmisorDireccionCodigoPostal", typeof(string)),
                                                           //// Emisor Expedido En
                                                           //new DataColumn("EmisorExpedidoEnCalle", typeof(string)),
                                                           //new DataColumn("EmisorExpedidoEnNoExterior", typeof(string)),
                                                           //new DataColumn("EmisorExpedidoEnNoInterior", typeof(string)),
                                                           //new DataColumn("EmisorExpedidoEnReferencia", typeof(string)),
                                                           //new DataColumn("EmisorExpedidoEnColonia", typeof(string)),
                                                           //new DataColumn("EmisorExpedidoEnCiudad", typeof(string)),
                                                           //new DataColumn("EmisorExpedidoEnMunicipio", typeof(string)),
                                                           //new DataColumn("EmisorExpedidoEnEstado", typeof(string)),
                                                           //new DataColumn("EmisorExpedidoEnPais", typeof(string)),
                                                           //new DataColumn("EmisorExpedidoEnCodigoPostal", typeof(string)),
                                                           // Receptor
                                                           new DataColumn("ReceptorRFC", typeof(string)),
                                                           new DataColumn("ReceptorRazonSocial", typeof(string)),
                                                           new DataColumn("ReceptorUsoCFDI", typeof(string)),
                                                           // Receptor Direccion Fiscal
                                                           new DataColumn("ReceptorDireccionCalle", typeof(string)),
                                                           new DataColumn("ReceptorDireccionNoExterior", typeof(string)),
                                                           new DataColumn("ReceptorDireccionNoInterior", typeof(string)),
                                                           new DataColumn("ReceptorDireccionReferencia", typeof(string)),
                                                           new DataColumn("ReceptorDireccionColonia", typeof(string)),
                                                           new DataColumn("ReceptorDireccionCiudad", typeof(string)),
                                                           new DataColumn("ReceptorDireccionMunicipio", typeof(string)),
                                                           new DataColumn("ReceptorDireccionEstado", typeof(string)),
                                                           new DataColumn("ReceptorDireccionPais", typeof(string)),
                                                           new DataColumn("ReceptorDireccionCodigoPostal", typeof(string)),
                                                           // Conceptos 
                                                           new DataColumn("ConceptoCodigo", typeof(string)),
                                                           new DataColumn("ConceptoClaveProductoServicio", typeof(string)),
                                                           new DataColumn("ConceptoClaveUnidad", typeof(string)),
                                                           new DataColumn("ConceptoDescripcion", typeof(string)),
                                                           new DataColumn("ConceptoUnidad", typeof(string)),
                                                           new DataColumn("ConceptoCantidad", typeof(decimal)),
                                                           new DataColumn("ConceptoPrecio", typeof(decimal)),
                                                           new DataColumn("ConceptoImporte", typeof(decimal)),
                                                           new DataColumn("ConceptoDescuento", typeof(decimal)),
                                                           // TimbreFiscalDigital
                                                           //noCertificadoSAT="20001000000300022323" selloCFD="" selloSAT=""
                                                           new DataColumn("TFDFecha", typeof(string)),
                                                           new DataColumn("UUID", typeof(string)),
                                                           new DataColumn("TFDNoCertificadoSAT", typeof(string)),
                                                           new DataColumn("TFDSelloCFD", typeof(string)),
                                                           new DataColumn("TFDSelloSAT", typeof(string)),
                                                           new DataColumn("Observaciones", typeof(string)),
                                                           new DataColumn("Banco", typeof(string)),
                                                           new DataColumn("NumCta", typeof(string)),
                                                           new DataColumn("CEA_NoServicio", typeof(string))
                                                       });
                //tabla.WriteXmlSchema(@"C:\Repobox\Timbrado\schema33.xsd");
                // Llenar tabla
                DataSet set = new DataSet();
                XmlDocument document = new XmlDocument();
                document.LoadXml(xml);
                using (MemoryStream stream = new MemoryStream())
                {
                    document.Save(stream);
                    stream.Position = 0L;
                    set.ReadXml(stream);
                }
                decimal IVA = 0, IEPS = 0, IVARet = 0, ISRRet = 0;
                string Observaciones = "", Banco = "", Cuenta = "", CEA_NoServicio = "";
                // Sacar impuestos
                XmlNodeList xmlImpuestos = null;
                    if(document.DocumentElement["cfdi:Impuestos"] != null)
                        xmlImpuestos = document.DocumentElement["cfdi:Impuestos"].ChildNodes;
                if (xmlImpuestos != null && xmlImpuestos.Count > 0)
                {
                    foreach (XmlNode node in xmlImpuestos)
                    {
                        if (node.Name == "cfdi:Traslados")
                        {
                            foreach (XmlNode nodeTraslados in node.ChildNodes)
                            {
                                if (nodeTraslados.Attributes["Impuesto"].Value == "002")
                                    IVA += Convert.ToDecimal(nodeTraslados.Attributes["Importe"].Value);
                                else
                                    IEPS += Convert.ToDecimal(nodeTraslados.Attributes["Importe"].Value);
                            }
                        }
                        else // Retenciones
                        {
                            foreach (XmlNode nodeRetenciones in node.ChildNodes)
                            {
                                if (nodeRetenciones.Attributes["Impuesto"].Value == "002")
                                    IVARet += Convert.ToDecimal(nodeRetenciones.Attributes["Importe"].Value);
                                else
                                    ISRRet += Convert.ToDecimal(nodeRetenciones.Attributes["Importe"].Value);
                            }
                        }
                    }
                }
                //if (set.Tables.Contains("Retencion"))
                //    foreach (DataRow row in set.Tables["Retencion"].Rows)
                //        if (set.Tables["Retencion"].Columns.Contains("impuesto"))
                //            if (row["impuesto"].ToString() == "IVA")
                //                IVARet += (set.Tables["Retencion"].Columns.Contains("importe") ? Convert.ToDecimal(row["importe"].ToString()) : 0);
                //            else
                //                ISRRet += (set.Tables["Retencion"].Columns.Contains("importe") ? Convert.ToDecimal(row["importe"].ToString()) : 0);
                //if (set.Tables.Contains("Traslado"))
                //    foreach (DataRow row in set.Tables["Traslado"].Rows)
                //        if (set.Tables["Traslado"].Columns.Contains("impuesto"))
                //            if (row["impuesto"].ToString() == "IVA")
                //                IVA += (set.Tables["Traslado"].Columns.Contains("importe") ? Convert.ToDecimal(row["importe"].ToString()) : 0);
                //            else
                //                IEPS += (set.Tables["Traslado"].Columns.Contains("importe") ? Convert.ToDecimal(row["importe"].ToString()) : 0);
                RepoBox.Domicilio domicilio = null;
                foreach (XmlNode node in document.DocumentElement.ChildNodes)
                    if (node.Name.Contains("Addenda"))
                        foreach (XmlNode nodeAdd in node.ChildNodes)
                            if(nodeAdd.Name.Contains("Documento"))
                                foreach (XmlNode nodeRepoBox in nodeAdd.ChildNodes)
                                    if (nodeRepoBox.Name.Contains("RepoBox") || nodeRepoBox.Name.Contains("RepoBox"))
                                    {
                                        Observaciones = (nodeRepoBox.Attributes["Observaciones"] != null ? nodeRepoBox.Attributes["Observaciones"].Value : "");
                                        Banco = (nodeRepoBox.Attributes["Banco"] != null ? nodeRepoBox.Attributes["Banco"].Value : "");
                                        Cuenta = (nodeRepoBox.Attributes["Cuenta"] != null ? nodeRepoBox.Attributes["Cuenta"].Value : "");
                                        CEA_NoServicio = (nodeRepoBox.Attributes["CEA_NoServicio"] != null ? nodeRepoBox.Attributes["CEA_NoServicio"].Value : "");
                                        if (nodeRepoBox.ChildNodes.Count > 0)
                                        {
                                            if (nodeRepoBox.ChildNodes[0].Name == "ReceptorDomicilio")
                                            {
                                                XmlAttributeCollection att = nodeRepoBox.ChildNodes[0].Attributes;
                                                domicilio = new Domicilio()
                                                {
                                                    calle = att["Calle"].Value,
                                                    noExterior = att["NoExterior"].Value,
                                                    noInterior = att["NoInterior"].Value,
                                                    colonia = att["Colonia"].Value,
                                                    referencia = att["Referencia"].Value,
                                                    localidad = att["Ciudad"].Value,
                                                    municipio = att["Municipio"].Value,
                                                    estado = att["Estado"].Value,
                                                    pais = att["Pais"].Value,
                                                    codigoPostal = att["CodigoPostal"].Value
                                                };
                                            }
                                        }
                                        break;
                                    }

                XmlNodeList xmlConceptos = document.DocumentElement["cfdi:Conceptos"].ChildNodes;
                if (xmlConceptos != null && xmlConceptos.Count > 0)
                    foreach (XmlNode concepto in xmlConceptos)
                    {
                        System.Data.DataRow row = tabla.NewRow();
                        row["Version"] = (document.DocumentElement.Attributes["Version"] != null ? document.DocumentElement.Attributes["Version"].Value : "");
                        //concepto.Attributes["ClaveProdServ"].Value
                        row["Serie"] = (document.DocumentElement.Attributes["Serie"] != null ? document.DocumentElement.Attributes["Serie"].Value : "");
                        row["Folio"] = (document.DocumentElement.Attributes["Folio"] != null ? document.DocumentElement.Attributes["Folio"].Value : "");
                        row["Fecha"] = (document.DocumentElement.Attributes["Fecha"] != null ? document.DocumentElement.Attributes["Fecha"].Value : "");
                        row["Sello"] = (document.DocumentElement.Attributes["Sello"] != null ? document.DocumentElement.Attributes["Sello"].Value : "");
                        try
                        {
                            RepoBox33.SATCatalogos.FormaPago formadePago = satCats.FormasDePago.Find(x => x.ID == document.DocumentElement.Attributes["FormaPago"].Value.Trim());
                            row["FormaDePago"] = (document.DocumentElement.Attributes["FormaPago"] != null ? document.DocumentElement.Attributes["FormaPago"].Value + " - " + formadePago.Descripcion.Trim() : "");
                        }
                        catch (Exception)
                        {
                            row["FormaDePago"] = (document.DocumentElement.Attributes["FormaPago"] != null ? document.DocumentElement.Attributes["FormaPago"].Value : "");
                        }
                        row["NoCertificado"] = (document.DocumentElement.Attributes["NoCertificado"] != null ? document.DocumentElement.Attributes["NoCertificado"].Value : "");
                        row["CondicionesDePago"] = (document.DocumentElement.Attributes["CondicionesDePago"] != null ? document.DocumentElement.Attributes["CondicionesDePago"].Value : "");
                        row["SubTotal"] = DecimalValue((document.DocumentElement.Attributes["SubTotal"] != null ? document.DocumentElement.Attributes["SubTotal"].Value : ""));
                        row["Descuento"] = DecimalValue((document.DocumentElement.Attributes["Descuento"] != null ? document.DocumentElement.Attributes["Descuento"].Value : ""));
                        row["IEPS"] = IEPS;
                        row["IVA"] = IVA;
                        row["IVARet"] = IVARet;
                        row["ISRRet"] = ISRRet;
                        row["Total"] = DecimalValue((document.DocumentElement.Attributes["Total"] != null ? document.DocumentElement.Attributes["Total"].Value : ""));
                        row["MotivoDescuento"] = "";
                        row["TipoDeCambio"] = DecimalValue((document.DocumentElement.Attributes["TipoCambio"] != null ? document.DocumentElement.Attributes["TipoCambio"].Value : "1"));
                        try
                        {
                            RepoBox33.SATCatalogos.Moneda moneda = satCats.Monedas.Find(x => x.ID == document.DocumentElement.Attributes["Moneda"].Value.Trim());
                            row["Moneda"] = (document.DocumentElement.Attributes["Moneda"] != null ? document.DocumentElement.Attributes["Moneda"].Value + " - " + moneda.Descripcion.Trim() : "");
                        }
                        catch (Exception)
                        {
                            row["Moneda"] = (document.DocumentElement.Attributes["Moneda"] != null ? document.DocumentElement.Attributes["Moneda"].Value : "");
                        }
                        try
                        {
                            RepoBox33.SATCatalogos.MetodoPago metododePago = satCats.MetodosPago.Find(x => x.ID == document.DocumentElement.Attributes["MetodoPago"].Value.Trim());
                            row["MetodoDePago"] = (document.DocumentElement.Attributes["MetodoPago"] != null ? document.DocumentElement.Attributes["MetodoPago"].Value + " - " + metododePago.Descripcion.Trim() : "");
                        }
                        catch (Exception)
                        {
                            row["MetodoDePago"] = (document.DocumentElement.Attributes["MetodoPago"] != null ? document.DocumentElement.Attributes["MetodoPago"].Value : "");
                        }
                        row["LugarDeExpedicion"] = (document.DocumentElement.Attributes["LugarExpedicion"] != null ? document.DocumentElement.Attributes["LugarExpedicion"].Value : "");
                        try
                        {
                            RepoBox33.SATCatalogos.TipoComprobante tipoComprobante = satCats.TiposComprobante.Find(x => x.ID == document.DocumentElement.Attributes["TipoDeComprobante"].Value.Trim());
                            row["TipoDeComprobante"] = (document.DocumentElement.Attributes["TipoDeComprobante"] != null ? document.DocumentElement.Attributes["TipoDeComprobante"].Value + " - " + tipoComprobante.Descripcion : "");
                        }
                        catch (Exception)
                        {
                            row["TipoDeComprobante"] = (document.DocumentElement.Attributes["TipoDeComprobante"] != null ? document.DocumentElement.Attributes["TipoDeComprobante"].Value : "");
                        }
                        
                        row["Observaciones"] = Observaciones;
                        row["Banco"] = Banco;
                        row["NumCta"] = Cuenta;
                        row["CEA_NoServicio"] = CEA_NoServicio;
                        // Emisor
                        row["EmisorRFC"] = StringValue((document.DocumentElement["cfdi:Emisor"].Attributes["Rfc"] != null ? document.DocumentElement["cfdi:Emisor"].Attributes["Rfc"].Value : ""));
                        row["EmisorRazonSocial"] = StringValue((document.DocumentElement["cfdi:Emisor"].Attributes["Nombre"] != null ? document.DocumentElement["cfdi:Emisor"].Attributes["Nombre"].Value : ""));
                        try
                        {
                            RepoBox33.SATCatalogos.RegimenFiscal regimenFiscal = satCats.RegimenesFiscales.Find(x => x.ID == Convert.ToInt32(document.DocumentElement["cfdi:Emisor"].Attributes["RegimenFiscal"].Value.Trim()));
                            row["EmisorRegimenFiscal"] = StringValue((document.DocumentElement["cfdi:Emisor"].Attributes["RegimenFiscal"] != null ? document.DocumentElement["cfdi:Emisor"].Attributes["RegimenFiscal"].Value + " - " + regimenFiscal.Descripcion : ""));
                        }
                        catch (Exception)
                        {
                            row["EmisorRegimenFiscal"] = StringValue((document.DocumentElement["cfdi:Emisor"].Attributes["RegimenFiscal"] != null ? document.DocumentElement["cfdi:Emisor"].Attributes["RegimenFiscal"].Value : ""));
                        }
                        
                        // Receptor
                        row["ReceptorRFC"] = StringValue((document.DocumentElement["cfdi:Receptor"].Attributes["Rfc"] != null ? document.DocumentElement["cfdi:Receptor"].Attributes["Rfc"].Value : ""));
                        row["ReceptorRazonSocial"] = StringValue((document.DocumentElement["cfdi:Receptor"].Attributes["Nombre"] != null ? document.DocumentElement["cfdi:Receptor"].Attributes["Nombre"].Value : ""));
                        try
                        {
                            RepoBox33.SATCatalogos.UsoCFDI usoCFDI = satCats.UsosCFDI.Find(x => x.ID == document.DocumentElement["cfdi:Receptor"].Attributes["UsoCFDI"].Value.Trim());
                            row["ReceptorUsoCFDI"] = StringValue((document.DocumentElement["cfdi:Receptor"].Attributes["UsoCFDI"] != null ? document.DocumentElement["cfdi:Receptor"].Attributes["UsoCFDI"].Value + " - " + usoCFDI.Descripcion : ""));
                        }
                        catch (Exception)
                        {
                            row["ReceptorUsoCFDI"] = StringValue((document.DocumentElement["cfdi:Receptor"].Attributes["UsoCFDI"] != null ? document.DocumentElement["cfdi:Receptor"].Attributes["UsoCFDI"].Value : ""));
                        }
                        
                        // Receptor Direccion Fiscal
                        if (domicilio != null)
                        {
                            row["ReceptorDireccionCalle"] = (!string.IsNullOrEmpty(domicilio.calle) ? domicilio.calle : "");
                            row["ReceptorDireccionNoExterior"] = (!string.IsNullOrEmpty(domicilio.noExterior) ? domicilio.noExterior : "");
                            row["ReceptorDireccionNoInterior"] = (!string.IsNullOrEmpty(domicilio.noInterior) ? domicilio.noInterior : "");
                            row["ReceptorDireccionReferencia"] = (!string.IsNullOrEmpty(domicilio.referencia) ? domicilio.referencia : "");
                            row["ReceptorDireccionColonia"] = (!string.IsNullOrEmpty(domicilio.colonia) ? domicilio.colonia : "");
                            row["ReceptorDireccionCiudad"] = (!string.IsNullOrEmpty(domicilio.localidad) ? domicilio.localidad : "");
                            row["ReceptorDireccionMunicipio"] = (!string.IsNullOrEmpty(domicilio.municipio) ? domicilio.municipio : "");
                            row["ReceptorDireccionEstado"] = (!string.IsNullOrEmpty(domicilio.estado) ? domicilio.estado : "");
                            row["ReceptorDireccionPais"] = (!string.IsNullOrEmpty(domicilio.pais) ? domicilio.pais : "");
                            row["ReceptorDireccionCodigoPostal"] = (!string.IsNullOrEmpty(domicilio.codigoPostal) ? domicilio.codigoPostal : "");
                        }
                        else
                        {
                            row["ReceptorDireccionCalle"] = "";
                            row["ReceptorDireccionNoExterior"] = "";
                            row["ReceptorDireccionNoInterior"] = "";
                            row["ReceptorDireccionReferencia"] = "";
                            row["ReceptorDireccionColonia"] = "";
                            row["ReceptorDireccionCiudad"] = "";
                            row["ReceptorDireccionMunicipio"] = "";
                            row["ReceptorDireccionEstado"] = "";
                            row["ReceptorDireccionPais"] = "";
                            row["ReceptorDireccionCodigoPostal"] = "";
                        }
                        // Conceptos
                        row["ConceptoClaveProductoServicio"] = StringValue((concepto.Attributes["ClaveProdServ"] != null ? concepto.Attributes["ClaveProdServ"].Value : ""));
                        row["ConceptoClaveUnidad"] = StringValue((concepto.Attributes["ClaveUnidad"] != null ? concepto.Attributes["ClaveUnidad"].Value : ""));
                        row["ConceptoCodigo"] = StringValue((concepto.Attributes["NoIdentificacion"] != null ? concepto.Attributes["NoIdentificacion"].Value : ""));
                        row["ConceptoDescripcion"] = StringValue((concepto.Attributes["Descripcion"] != null ? concepto.Attributes["Descripcion"].Value : ""));
                        row["ConceptoUnidad"] = StringValue((concepto.Attributes["Unidad"] != null ? concepto.Attributes["Unidad"].Value : ""));
                        row["ConceptoCantidad"] = DecimalValue((concepto.Attributes["Cantidad"] != null ? concepto.Attributes["Cantidad"].Value : ""));
                        row["ConceptoPrecio"] = DecimalValue((concepto.Attributes["ValorUnitario"] != null ? concepto.Attributes["ValorUnitario"].Value : ""));
                        row["ConceptoImporte"] = DecimalValue((concepto.Attributes["Importe"] != null ? concepto.Attributes["Importe"].Value : ""));
                        row["ConceptoDescuento"] = DecimalValue((concepto.Attributes["Descuento"] != null ? concepto.Attributes["Descuento"].Value : ""));
                        //// TimbreFiscalDigital
                        row["TFDFecha"] = StringValue((document.DocumentElement["cfdi:Complemento"]["tfd:TimbreFiscalDigital"].Attributes["FechaTimbrado"] != null ? document.DocumentElement["cfdi:Complemento"]["tfd:TimbreFiscalDigital"].Attributes["FechaTimbrado"].Value : ""));
                        row["UUID"] = StringValue((document.DocumentElement["cfdi:Complemento"]["tfd:TimbreFiscalDigital"].Attributes["UUID"] != null ? document.DocumentElement["cfdi:Complemento"]["tfd:TimbreFiscalDigital"].Attributes["UUID"].Value : ""));
                        row["TFDNoCertificadoSAT"] = StringValue((document.DocumentElement["cfdi:Complemento"]["tfd:TimbreFiscalDigital"].Attributes["NoCertificadoSAT"] != null ? document.DocumentElement["cfdi:Complemento"]["tfd:TimbreFiscalDigital"].Attributes["NoCertificadoSAT"].Value : ""));
                        row["TFDSelloCFD"] = StringValue((document.DocumentElement["cfdi:Complemento"]["tfd:TimbreFiscalDigital"].Attributes["SelloCFD"] != null ? document.DocumentElement["cfdi:Complemento"]["tfd:TimbreFiscalDigital"].Attributes["SelloCFD"].Value : ""));
                        row["TFDSelloSAT"] = StringValue((document.DocumentElement["cfdi:Complemento"]["tfd:TimbreFiscalDigital"].Attributes["SelloSAT"] != null ? document.DocumentElement["cfdi:Complemento"]["tfd:TimbreFiscalDigital"].Attributes["SelloSAT"].Value : ""));
                        tabla.Rows.Add(row);
                    }
                return tabla;
            }
            catch (Exception)
            {
                throw new Exception("RepoBox: No fué posible generar la fuente de datos del archivo PDF.");
            }
        }
        private string StringValue(object val)
        {
            try
            {
                return val.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        private decimal DecimalValue(object val)
        {
            try
            {
                return Convert.ToDecimal(val);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        private DataTable GetImagenesDS(string pathLogo, string rfc, string UUID)
        {
            try
            {
                DataTable tabla = new DataTable("Imagenes");
                tabla.Columns.AddRange(new DataColumn[] {
                                                           new DataColumn("Logo", typeof(byte[])),
                                                           new DataColumn("CodigoQR", typeof(byte[]))
                                                       });
                tabla.Rows.Clear();
                System.Data.DataRow row = tabla.NewRow();
                row["Logo"] = ConversionImagen(pathLogo);
                row["CodigoQR"] = ConversionImagen(_Raiz + "\\" + rfc + "\\CFDI\\" + UUID + ".png");
                tabla.Rows.Add(row);
                try
                {
                    File.Delete(_Raiz + "\\" + rfc + "\\CFDI\\" + UUID + ".png");
                }
                catch (Exception)
                { }
                return tabla;
            }
            catch (Exception)
            {
                throw new Exception("RepoBox: No fué posible asignar el Logo y/o Sello Bidimensional en el formato PDF.");
            }
        }
        private byte[] ConversionImagen(string RutaArchivo)
        {
            try
            {
                //Declaramos fs para poder abrir la imagen.
                FileStream fs = new FileStream(RutaArchivo, FileMode.Open);

                // Declaramos un lector binario para pasar la imagen
                // a bytes
                BinaryReader br = new BinaryReader(fs);
                byte[] imagen = new byte[(int)fs.Length];
                br.Read(imagen, 0, (int)fs.Length);
                br.Close();
                fs.Close();
                return imagen;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool EnviarEmail(string xmlPath, string pdfPath, EmailParameters parametros)
        {
            try
            {
                using (System.Net.Mail.MailMessage mensaje = new System.Net.Mail.MailMessage(parametros.Correo,
                                                                                        parametros.Destinatarios.Split(';')[0],
                                                                                        parametros.Asunto,
                                                                                        parametros.CuerpoMensaje))
                {
                    string[] destinatarios = parametros.Destinatarios.Split(';');
                    if (destinatarios.Length > 1)
                        foreach (string dest in destinatarios)
                            if (parametros.Destinatarios.Split(';')[0] != dest && dest.Trim() != "")
                                mensaje.CC.Add(new System.Net.Mail.MailAddress(dest));
                    mensaje.SubjectEncoding = Encoding.UTF8;
                    mensaje.IsBodyHtml = parametros.CuerpoHTML;
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(parametros.SMTP);
                    smtp.EnableSsl = parametros.SSL;
                    smtp.UseDefaultCredentials = false;
                    smtp.Host = parametros.SMTP;
                    smtp.Port = parametros.PuertoSMTP;
                    smtp.Credentials = new System.Net.NetworkCredential(parametros.Correo, parametros.Password);
                    System.Net.Mail.Attachment adjunto;
                    System.Net.Mime.ContentDisposition disposicion;
                    if (xmlPath != "" && File.Exists(xmlPath))
                    {
                        adjunto = new System.Net.Mail.Attachment(xmlPath);
                        disposicion = adjunto.ContentDisposition;
                        disposicion.CreationDate = System.IO.File.GetCreationTime(xmlPath);
                        disposicion.ModificationDate = System.IO.File.GetLastWriteTime(xmlPath);
                        disposicion.ReadDate = System.IO.File.GetLastAccessTime(xmlPath);
                        mensaje.Attachments.Add(adjunto);
                    }
                    if (pdfPath != "" && !pdfPath.StartsWith("RepoBox:") && File.Exists(pdfPath))
                    {
                        adjunto = new System.Net.Mail.Attachment(pdfPath);
                        disposicion = adjunto.ContentDisposition;
                        disposicion.CreationDate = System.IO.File.GetCreationTime(pdfPath);
                        disposicion.ModificationDate = System.IO.File.GetLastWriteTime(pdfPath);
                        disposicion.ReadDate = System.IO.File.GetLastAccessTime(pdfPath);
                        mensaje.Attachments.Add(adjunto);
                    }
                    smtp.Send(mensaje);
                    return true;
                }
            }
            catch (Exception)
            { return false; }
        }
        private byte[] XmlToByteArray(string string_)
        {
            return System.Convert.FromBase64String(System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(string_)));
        }
        public string GetXmlRFCReal(string xml, string rfc)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    if (node.Name == "cfdi:Emisor")
                    {
                        node.Attributes["rfc"].Value = rfc;
                        break;
                    }
                }
                return doc.InnerXml;
            }
            catch (Exception)
            {
                return xml;
            }
        }
    }
    public class EmailParameters
    {
        public int PuertoSMTP { get; set; }
        public string SMTP { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public bool SSL { get; set; }
        public string Destinatarios { get; set; }
        public string Asunto { get; set; }
        public string CuerpoMensaje { get; set; }
        public bool CuerpoHTML { get; set; }
    }
}
