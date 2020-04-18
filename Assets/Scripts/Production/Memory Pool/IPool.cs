using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
	//*singing* I like to iit iit iit IPools and baninis~
	//I like to iit iit iit IPools and baninis~ /endQuarantineWithToddler
	public interface IPool<T>
	{
		T Rent();
		void UnRent(T returningObject);
	}

	public class ObjectPool<T> : IPool<T>
	{
		private Stack<T> objs = new Stack<T>();
		GameObject prefab;
		Transform parent;

		public ObjectPool(GameObject prefab, Transform parent)
		{
			this.prefab = prefab;
			this.parent = parent;
		}

		public T Rent()
		{
			if (objs.Count == 0 || objs.Peek() == null)
			{
				GameObject tempGameObject = GameObject.Instantiate(prefab, parent) as GameObject;
				T objectOfType = tempGameObject.GetComponent<T>();
				objs.Push(objectOfType);
			}

			//null reference bug bandaid
			while (objs.Peek() == null)
			{
				objs.Pop();
			}

			return objs.Pop();
		}


		public void UnRent(T objectToReturn)
		{
			objs.Push(objectToReturn);
		}
	}
}