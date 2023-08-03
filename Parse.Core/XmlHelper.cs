using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;

namespace Parse.Core
{
	public static class XmlHelper
	{
		public static XDocument GetXDocument(this XmlDocument document)
		{
			XDocument xDoc = new XDocument();
			using (XmlWriter xmlWriter = xDoc.CreateWriter())
			{
				document.WriteTo(xmlWriter);
			}
			XmlDeclaration decl = document.ChildNodes.OfType<XmlDeclaration>().FirstOrDefault<XmlDeclaration>();
			if (decl != null)
			{
				xDoc.Declaration = new XDeclaration(decl.Version, decl.Encoding, decl.Standalone);
			}
			return xDoc;
		}

		public static XmlDocument GetXmlDocument(XDocument document)
		{
			XmlDocument xmlDocument;
			using (XmlReader xmlReader = document.CreateReader())
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(xmlReader);
				if (document.Declaration != null)
				{
					XmlDeclaration dec = xmlDoc.CreateXmlDeclaration(document.Declaration.Version, document.Declaration.Encoding, document.Declaration.Standalone);
					xmlDoc.InsertBefore(dec, xmlDoc.FirstChild);
				}
				xmlDocument = xmlDoc;
			}
			return xmlDocument;
		}

		public static XDocument RemoveAttribte(XDocument xdoc)
		{
			foreach (XElement xElement in xdoc.Root.DescendantsAndSelf())
			{
				foreach (XElement ex in xElement.Descendants())
				{
					if (!ex.HasAttributes)
					{
						continue;
					}
					foreach (XAttribute xa in ex.Attributes())
					{
						if (xa.Name.LocalName != "nil")
						{
							continue;
						}
						xa.Remove();
					}
				}
			}
			return xdoc;
		}

		public static XDocument RemoveNamespace(XDocument xdoc)
		{
			foreach (XElement e in xdoc.Root.DescendantsAndSelf())
			{
				if (e.Name.Namespace != XNamespace.None)
				{
					e.Name = XNamespace.None.GetName(e.Name.LocalName);
				}
				if (!e.Attributes().Where<XAttribute>((XAttribute a) => {
					if (a.IsNamespaceDeclaration)
					{
						return true;
					}
					return a.Name.Namespace != XNamespace.None;
				}).Any<XAttribute>())
				{
					continue;
				}
				e.ReplaceAttributes(e.Attributes().Select<XAttribute, XAttribute>((XAttribute a) => {
					if (a.IsNamespaceDeclaration)
					{
						return null;
					}
					if (a.Name.Namespace == XNamespace.None)
					{
						return a;
					}
					return new XAttribute(XNamespace.None.GetName(a.Name.LocalName), a.Value);
				}));
			}
			return xdoc;
		}
	}
}