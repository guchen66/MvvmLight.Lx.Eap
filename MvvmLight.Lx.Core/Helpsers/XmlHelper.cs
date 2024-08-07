using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MvvmLight.Lx.Core.Helpsers
{
    public class XmlHelper
    {
        public string DefaultFilePath { get; set; }
        private Dictionary<string, XmlNode> _cache = new Dictionary<string, XmlNode>();
        private string _currentSectionId;

        public XmlHelper(string defaultFilePath)
        {
            DefaultFilePath = defaultFilePath;
        }

        public int CcdCount()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(DefaultFilePath);
            // 使用XPath来获取所有的ccdinfo节点
            XmlNodeList ccdInfoNodes = doc.SelectNodes("//ccdinfo");

            // ccdInfoNodes.Count将给出ccdinfo的数量
            return ccdInfoNodes.Count;
        }

        public XmlHelper GetSection(string sectionId)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(DefaultFilePath);

            // 仅当缓存中没有相应的sectionId时，才执行查询并缓存结果
            if (!_cache.TryGetValue(sectionId, out XmlNode ccdInfoNode))
            {
                ccdInfoNode = doc.SelectSingleNode("//ccdinfo[@Id='" + sectionId + "']");
                _cache[sectionId] = ccdInfoNode; // 缓存结果
            }

            if (ccdInfoNode != null)
            {
                _currentSectionId = sectionId;
            }

            return this;
        }

        public string GetKey(string key)
        {
            if (string.IsNullOrEmpty(_currentSectionId))
            {
                throw new InvalidOperationException("No section selected.");
            }

            // 延迟查询，直到GetKey被调用时才执行
            XmlNode ccdInfoNode = _cache[_currentSectionId];
            if (ccdInfoNode != null)
            {
                XmlNode node = ccdInfoNode.SelectSingleNode(key); // 直接使用key作为XPath
                return node?.InnerText;
            }

            return null;
        }
    }
}
