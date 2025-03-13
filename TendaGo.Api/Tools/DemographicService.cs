using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TendaGo
{
    public static class DemographicService
    {
        private static string UrlBase => ConfigurationManager.AppSettings["TendaGo:DemographicServiceUrl"];

        public static async Task<DemographicDto> GetDataAsync(string identificacion)
        {
            var encid = Encrypt(identificacion);

            var response = await new HttpClient()
                .GetAsync($"{UrlBase}/{encid}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<DemographicDto>();
            }

            return default;
        }

        public static byte[] IV = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };
        public static string EncryptionKey = "v5Jt8GfKuLgYjFF";
        //public static string EncryptionKey = "YjFFt8KuLgv5JGf"; 

        public static string Encrypt(string clearText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, IV);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;

        }

        public static string Decrypt(string cipherText)
        {
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, IV);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }

    public partial class DemographicDto
    {
        [JsonProperty("SOCIODEMOGRAFICO")]
        public List<DemographicEntityDto> Data { get; set; }

        [JsonProperty("CONTACTOS")]
        public List<DemographicContactEntityDto> DataContact { get; set; }

    }

    public partial class DemographicEntityDto
    {
        [JsonProperty("IDENTIFICACION")]
        public string Identificacion { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("COD_SEXO")]
        public string CodSexo { get; set; }

        [JsonProperty("DES_SEXO")]
        public string DesSexo { get; set; }

        [JsonProperty("COD_CIUDADANIA")]
        public string CodCiudadania { get; set; }

        [JsonProperty("DES_CIUDADANIA")]
        public string DesCiudadania { get; set; }

        [JsonProperty("FECH_NAC")]
        public string FechNac { get; set; }

        [JsonProperty("LUG_NAC")]
        public string LugNac { get; set; }

        [JsonProperty("Edad")]
        public string Edad { get; set; }

        [JsonProperty("COD_NACIONALIDAD")]
        public string CodNacionalidad { get; set; }

        [JsonProperty("DES_NACIONALIDAD")]
        public string DesNacionalidad { get; set; }

        [JsonProperty("COD_ESTAD_CIVIL")]
        public string CodEstadCivil { get; set; }

        [JsonProperty("DES_ESTADO_CIVIL")]
        public string DesEstadoCivil { get; set; }

        [JsonProperty("COD_NIV_ESTUD")]
        public string CodNivEstud { get; set; }

        [JsonProperty("DESC_NIV_EST")]
        public string DescNivEst { get; set; }

        [JsonProperty("COD_PROFESION")]
        public string CodProfesion { get; set; }

        [JsonProperty("DES_PROFESION")]
        public string DesProfesion { get; set; }

        [JsonProperty("NOM_CONYUG")]
        public string NomConyug { get; set; }

        [JsonProperty("CED_CONYUG")]
        public string CedConyug { get; set; }

        [JsonProperty("FECHA_MAT")]
        public string FechaMat { get; set; }

        [JsonProperty("LUG_MAT")]
        public string LugMat { get; set; }

        [JsonProperty("NOM_PAD")]
        public string NomPad { get; set; }

        [JsonProperty("NAC_PAD")]
        public string NacPad { get; set; }

        [JsonProperty("CED_PAD")]
        public string CedPad { get; set; }

        [JsonProperty("NOM_MAD")]
        public string NomMad { get; set; }

        [JsonProperty("NAC_MAD")]
        public string NacMad { get; set; }

        [JsonProperty("CED_MAD")]
        public string CedMad { get; set; }

        [JsonProperty("FECH_DEF")]
        public string FechDef { get; set; }

        [JsonProperty("LUG_DEF")]
        public string LugDef { get; set; }

        [JsonProperty("FECHA_INSC_DEFUNCION")]
        public string FechaInscDefuncion { get; set; }

        [JsonProperty("LUG_INSC")]
        public string LugInsc { get; set; }

        [JsonProperty("COD_DOMIC")]
        public string CodDomic { get; set; }

        [JsonProperty("CALLE")]
        public string Calle { get; set; }

        [JsonProperty("NUM")]
        public string Num { get; set; }

        [JsonProperty("FECHA_ACTUAL")]
        public string FechaActual { get; set; }

        [JsonProperty("FECHA_EXPED")]
        public string FechaExped { get; set; }

        [JsonProperty("CEDULA_MAGNA")]
        public string CedulaMagna { get; set; }

        [JsonProperty("INDIVIDUAL_DACTILAR")]
        public string IndividualDactilar { get; set; }

        [JsonProperty("COD_GENERO_LEY")]
        public string CodGeneroLey { get; set; }

        [JsonProperty("FECHA_INSC_GENERO")]
        public string FechaInscGenero { get; set; }

        [JsonProperty("COD_PARR_INSC_GENERO")]
        public string CodParrInscGenero { get; set; }

        [JsonProperty("NUMDEDO1")]
        public string Numdedo1 { get; set; }

        [JsonProperty("LUGAR_NAC")]
        public string LugarNac { get; set; }

        [JsonProperty("LUGAR_DOM")]
        public string LugarDom { get; set; }

        [JsonProperty("GENERO_LEY")]
        public string GeneroLey { get; set; }

        [JsonProperty("Alerta")]
        public string Alerta { get; set; }

        [JsonProperty("NOMBRE")]
        public string Nombre { get; set; }
    }
    public partial class DemographicContactEntityDto
    {
        [JsonProperty("TipoDato")]
        public string TipoDato { get; set; }

        [JsonProperty("Dato")]
        public string Dato { get; set; }

        [JsonProperty("ord")]
        public string ord { get; set; }

        [JsonProperty("IDENTIFICACION")]
        public string IDENTIFICACION { get; set; }
    }
}