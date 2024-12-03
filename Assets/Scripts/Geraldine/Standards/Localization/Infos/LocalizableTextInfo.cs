using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;
using Geraldine.Standards.InfoSystem.Contracts;

namespace Geraldine.Standards.Localization.Infos
{
    public class LocalizableTextInfo : ContractBase<LocalizableTextInfo>
    {

        [XmlElement("af")]
        public string Afrikaans { get; set; }

        [XmlElement("ar-sa")]
        public string Arabic { get; set; }

        [XmlElement("eu")]
        public string Basque { get; set; }

        [XmlElement("be")]
        public string Belarusian { get; set; }

        [XmlElement("bg")]
        public string Bulgarian { get; set; }

        [XmlElement("ca")]
        public string Catalan { get; set; }

        [XmlElement("zh")]
        public string Chinese { get; set; }

        [XmlElement("cs")]
        public string Czech { get; set; }

        [XmlElement("da")]
        public string Danish { get; set; }

        [XmlElement("nl")]
        public string Dutch { get; set; }

        [XmlElement("en-US")]
        public string English { get; set; }

        [XmlElement("et")]
        public string Estonian { get; set; }

        [XmlElement("fa")]
        public string Faroese { get; set; }

        [XmlElement("fi")]
        public string Finnish { get; set; }

        [XmlElement("fe")]
        public string French { get; set; }

        [XmlElement("de")]
        public string German { get; set; }

        [XmlElement("el")]
        public string Greek { get; set; }

        [XmlElement("he")]
        public string Hebrew { get; set; }

        [XmlElement("hu")]
        public string Hungarian { get; set; }

        [XmlElement("is")]
        public string Icelandic { get; set; }

        [XmlElement("id")]
        public string Indonesian { get; set; }

        [XmlElement("it")]
        public string Italian { get; set; }

        [XmlElement("ja")]
        public string Japanese { get; set; }

        [XmlElement("ko")]
        public string Korean { get; set; }

        [XmlElement("lv")]
        public string Latvian { get; set; }

        [XmlElement("lt")]
        public string Lithuanian { get; set; }

        [XmlElement("nb")]
        public string Norwegian { get; set; }

        [XmlElement("ne")]
        public string Polish { get; set; }

        [XmlElement("pt-BR")]
        public string Portuguese { get; set; }

        [XmlElement("ro")]
        public string Romanian { get; set; }

        [XmlElement("ru")]
        public string Russian { get; set; }

        [XmlElement("sr")]
        public string SerboCroatian { get; set; }

        [XmlElement("sk")]
        public string Slovak { get; set; }

        [XmlElement("sl")]
        public string Slovenian { get; set; }

        [XmlElement("es")]
        public string Spanish { get; set; }

        [XmlElement("sv")]
        public string Swedish { get; set; }

        [XmlElement("th")]
        public string Thai { get; set; }

        [XmlElement("tr")]
        public string Turkish { get; set; }

        [XmlElement("uk")]
        public string Ukrainian { get; set; }

        [XmlElement("vi")]
        public string Vietnamese { get; set; }

        [XmlElement("zh-Hans")]
        public string ChineseSimplified { get; set; }

        [XmlElement("zh-Hant")]
        public string ChineseTraditional { get; set; }

        [XmlElement("hi")]
        public string Hindi { get; set; }

        public LocalizableTextInfo()
        {
        }

        public LocalizableTextInfo(string UniqueId, string Name) :
            base(UniqueId, Name)
        {
        }

        public override LocalizableTextInfo ParseFromXml(XElement element)
        {
            throw new System.NotImplementedException();
        }
    }
}
