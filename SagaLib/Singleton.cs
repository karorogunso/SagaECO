using System;
using System.Collections.Generic;
using System.Text;

namespace SagaLib
{
    [Serializable]
	/// <summary>
	/// Lazy-loading singleton
	/// </summary>
	/// <typeparam name="T">The type to have the singleton instance of</typeparam>
	public abstract class Singleton<T> where T : new()
	{
		/// <summary>
		/// Private constructor to avoid external instantiation. 
		/// </summary>
		/// <remarks>
		/// This is present to keep the compiler from providing a default public constructor
		/// </remarks>
		protected Singleton()
		{

		}

		/// <summary>
		/// Return an instance of <see cref="Singleton"/>
		/// </summary>
		public static T Instance
		{
			/// <summary>
			/// An instance of Singleton wont be created until the very first call to the SingletonHolder class.
			/// </summary>
			get { return SingletonHolder.instance; }
            set { SingletonHolder.instance = value; }
		}

		/// <summary>
		/// Sealed class to avoid any heritage from this helper class
		/// </summary>
		private sealed class SingletonHolder
		{
			internal static T instance = new T();

			/// <summary>
			/// Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
			/// </summary>
			static SingletonHolder()
			{
			}
		}
	}
}
