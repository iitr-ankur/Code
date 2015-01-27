using System;
using System.Threading.Tasks;


namespace TextMobileFunctions
{
	public interface IAccessMobile
	{
		Task<string> getcontacts ();
		void TakePhoto ();
		void PickPhoto ();
		void TakeVideo ();
		void PickVideo ();
	}
}

