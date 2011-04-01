﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Xml.XPath;

namespace Roadkill.Core
{
	/// <summary>
	/// Represents a &lt;roadkill&gt; section inside a configuration file.
	/// </summary>
	public class RoadkillSection : ConfigurationSection
	{
		private static RoadkillSection _section;

		/// <summary>
		/// The current instance of the section. This is not a singleton but there is no requirement for this to be threadsafe.
		/// </summary>
		public static RoadkillSection Current
		{
			get
			{
				if (_section == null)
					_section = ConfigurationManager.GetSection("roadkill") as RoadkillSection;

				return _section;
			}
		}

		/// <summary>
		/// Gets or sets the name of the admin role.
		/// </summary>
		[ConfigurationProperty("adminRoleName", IsRequired = true)]
		public string AdminRoleName
		{
			get { return (string)this["adminRoleName"]; }
			set { this["adminRoleName"] = value; }
		}

		/// <summary>
		/// Gets or sets the attachments folder, which should begin with "~/".
		/// </summary>
		[ConfigurationProperty("attachmentsFolder", IsRequired = true)]
		public string AttachmentsFolder
		{
			get { return (string)this["attachmentsFolder"]; }
			set { this["attachmentsFolder"] = value; }
		}

		/// <summary>
		/// Indicates whether NHibernate 2nd level caching is enabled.
		/// </summary>
		[ConfigurationProperty("cacheEnabled", IsRequired = true)]
		public bool CacheEnabled
		{
			get { return (bool)this["cacheEnabled"]; }
			set { this["cacheEnabled"] = value; }
		}

		/// <summary>
		/// Indicates whether page content should be cached, IF <see cref="CacheEnabled"/> is true.
		/// </summary>
		[ConfigurationProperty("cacheText", IsRequired = true)]
		public bool CacheText
		{
			get { return (bool)this["cacheText"]; }
			set { this["cacheText"] = value; }
		}

		/// <summary>
		/// Gets or sets the name of the connection string in the connectionstrings section.
		/// </summary>
		[ConfigurationProperty("connectionStringName", IsRequired = true)]
		public string ConnectionStringName
		{
			get { return (string) this["connectionStringName"]; }
			set { this["connectionStringName"] = value; }
		}

		/// <summary>
		/// The database type for Roadkill. This can be: "sqlserver","sqlite","mysql"
		/// </summary>
		[ConfigurationProperty("databaseType", IsRequired = false)]
		public string DatabaseType
		{
			get { return (string)this["databaseType"]; }
			set { this["databaseType"] = value; }
		}


		/// <summary>
		/// Gets or sets the name of the editor role.
		/// </summary>
		[ConfigurationProperty("editorRoleName", IsRequired = true)]
		public string EditorRoleName
		{
			get { return (string)this["editorRoleName"]; }
			set { this["editorRoleName"] = value; }
		}


		/// <summary>
		/// Gets or sets whether this roadkill instance has been installed.
		/// </summary>
		[ConfigurationProperty("installed", IsRequired = true)]
		public bool Installed
		{
			get { return (bool)this["installed"]; }
			set { this["installed"] = value; }
		}


		/// <summary>
		/// Gets a value indicating whether the <see cref="T:System.Configuration.ConfigurationElement"/> object is read-only,
		/// and can therefore be saved back to disk.
		/// </summary>
		/// <returns>This returns true.</returns>
		public override bool IsReadOnly()
		{
			return false;
		}
	}
}
