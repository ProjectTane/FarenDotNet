using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paraiba.Drawing.Surfaces;

namespace FarenDotNet.FileData
{
	public class FieldMap : IList<Area>
	{

		public readonly Surface Image;
		private readonly IList<Area> _areas;

		public FieldMap(Surface img, IList<Area> areas)
		{
			this.Image = img;
			_areas = areas;
		}

		#region IList<Area> および、それの上位インタフェイス メンバ

		// IList<Area>
		public int IndexOf(Area item) { return _areas.IndexOf(item); }
		public void Insert(int index, Area item) { _areas.Insert(index, item); }
		public void RemoveAt(int index) { _areas.RemoveAt(index); }
		public Area this[int index]
		{
			get { return _areas[index]; }
			set { _areas[index] = value; }
		}

		// ICollection<Area>
		public void Add(Area item) { _areas.Add(item); }
		public void Clear() { _areas.Clear(); }
		public bool Contains(Area item) { return _areas.Contains(item); }
		public void CopyTo(Area[] array, int arrayIndex) { _areas.CopyTo(array, arrayIndex); }
		public int Count { get { return _areas.Count; } }
		public bool IsReadOnly { get { return _areas.IsReadOnly; } }
		public bool Remove(Area item) { return _areas.Remove(item); }

		// IEnumerable<Area>
		public IEnumerator<Area> GetEnumerator() { return _areas.GetEnumerator();}

		// IEnumerable
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion
		
	}
}
