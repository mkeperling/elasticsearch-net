﻿using System;
using System.Globalization;
using Nest.Resolvers;
using Elasticsearch.Net;

namespace Nest
{
	public interface IBulkDeleteOperation<T> : IBulkOperation
		where T : class
	{
		T Document { get; set; }
	}

	public class BulkDeleteOperation<T> : BulkOperationBase, IBulkDeleteOperation<T>
		where T : class
	{
		public BulkDeleteOperation(T document)
		{
			this.Document = document;
		} 
		
		public BulkDeleteOperation(string id) { this.Id = id; }
		public BulkDeleteOperation(double id) { this.Id = id.ToString(CultureInfo.InvariantCulture); }

		public override string Operation { get { return "delete"; } }

		public override Type ClrType { get { return typeof(T); } } 
		
		public override object GetBody()
		{
			return null;
		}

		public override string GetIdForOperation(ElasticInferrer inferrer)
		{
			return this.Id ?? inferrer.Id(this.Document);
		}

		public T Document { get; set; }
	}
	public class BulkDeleteDescriptor<T> : BulkOperationDescriptorBase, IBulkDeleteOperation<T>
		where T : class
	{
		private IBulkDeleteOperation<T> Self { get { return this; } } 

		protected override string _Operation { get { return "delete"; } }
		protected override Type _ClrType { get { return typeof(T); } }
		
		T IBulkDeleteOperation<T>.Document { get; set; }

		protected override object _GetBody()
		{
			return null;
		}
		
		protected override string GetIdForOperation(ElasticInferrer inferrer)
		{
			return Self.Id ?? inferrer.Id(Self.Document);
		}

		/// Manually set the index, default to the default index or the fixed index set on the bulk operation
		/// </summary>
		public BulkDeleteDescriptor<T> Index(string index)
		{
			index.ThrowIfNullOrEmpty("indices");
			Self.Index = index;
			return this;
		}
		/// <summary>
		/// Manualy set the type to get the object from, default to whatever
		/// T will be inferred to if not passed or the fixed type set on the parent bulk operation
		/// </summary>
		public BulkDeleteDescriptor<T> Type(string type)
		{
			type.ThrowIfNullOrEmpty("type");
			Self.Type = type;
			return this;
		}

		/// <summary>
		/// Manually set the type of which a typename will be inferred
		/// </summary>
		public BulkDeleteDescriptor<T> Type(Type type)
		{
			type.ThrowIfNull("type");
			Self.Type = type;
			return this;
		}

		/// <summary>
		/// Manually set the id for the newly created object
		/// </summary>
		public BulkDeleteDescriptor<T> Id(long id)
		{
			return this.Id(id.ToString(CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Manually set the id for the newly created object
		/// </summary>
		public BulkDeleteDescriptor<T> Id(string id)
		{
			Self.Id = id;
			return this;
		}

		/// <summary>
		/// The object to infer the id off, (if id is not passed using Id())
		/// </summary>
		public BulkDeleteDescriptor<T> Document(T @object)
		{
			Self.Document = @object;
			return this;
		}


		public BulkDeleteDescriptor<T> Version(string version)
		{
			Self.Version = version; 
			return this;
		}

		public BulkDeleteDescriptor<T> VersionType(VersionType versionType)
		{
			Self.VersionType = versionType;
			return this;
		}

		public BulkDeleteDescriptor<T> Routing(string routing)
		{
			Self.Routing = routing; 
			return this;
		}

		public BulkDeleteDescriptor<T> Parent(string parent)
		{
			Self.Parent = parent; 
			return this;
		}

		public BulkDeleteDescriptor<T> Timestamp(long timestamp)
		{
			Self.Timestamp = timestamp; 
			return this;
		}

		public BulkDeleteDescriptor<T> Ttl(string ttl)
		{
			Self.Ttl = ttl; 
			return this;
		}

	}
}