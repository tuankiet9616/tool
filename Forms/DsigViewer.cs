using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;

namespace Parse.Forms
{
	public class DsigViewer
	{
		public DsigViewer()
		{
		}

		public static string GetHtml(XmlDocument xdoc, string xsltData)
		{
			XmlDocument xmlDocTemplate = new XmlDocument();
			xmlDocTemplate.LoadXml(xsltData);
			return DsigViewer.TransformXMLToHTML(xdoc.InnerXml, xmlDocTemplate.InnerXml);
		}

		public static string GetHtml(string xmlData, out int products)
		{
			products = 1;
			XmlDocument xdoc = new XmlDocument()
			{
				PreserveWhitespace = true
			};
			xdoc.LoadXml(xmlData);
			XmlNodeList elemList = xdoc.GetElementsByTagName("Product");
			products = elemList.Count / 10 + 1;
			XmlProcessingInstruction instruction = xdoc.SelectSingleNode("processing-instruction('xml-stylesheet')") as XmlProcessingInstruction;
			if (instruction != null)
			{
				string tempName = "";
				string templatePath = UtilsViewer.getInvoiceFolder((instruction.OwnerDocument.ReadNode(XmlReader.Create(new StringReader(string.Concat("<pi ", instruction.Value, "/>")))) as XmlElement).GetAttribute("href"), out tempName);
				if (templatePath == null)
				{
					return null;
				}
				string xsltFile = string.Concat(templatePath, tempName, ".xslt");
				XmlDocument xmlDocTemplate = new XmlDocument();
				xmlDocTemplate.Load(xsltFile);
				if (xmlDocTemplate != null)
				{
					return DsigViewer.TransformXMLToHTML(xdoc.InnerXml, xmlDocTemplate.InnerXml);
				}
			}
			return null;
		}

		public static string TransformXMLToHTML(string inputXml, string xsltString)
		{
			XslCompiledTransform transform = new XslCompiledTransform();
			using (XmlReader reader = XmlReader.Create(new StringReader(xsltString)))
			{
				transform.Load(reader);
			}
			StringWriter results = new StringWriter();
			using (XmlReader reader2 = XmlReader.Create(new StringReader(inputXml)))
			{
				transform.Transform(reader2, null, results);
			}
			return results.ToString();
		}
	}
}