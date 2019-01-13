using UnityEngine;
using UnityEngine.AI;

namespace UselessScripts
{
	public class PlayerController : MonoBehaviour
	{

		public Camera MyCam;

		private NavMeshAgent _agent;
		
		void Start ()
		{
			_agent = GetComponent<NavMeshAgent>();
		}
	
	
		void Update () {
		
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				Ray myRay = MyCam.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
			
				if(Physics.Raycast(myRay, out hit, 5000, 1<<9))
				{
					_agent.SetDestination(hit.point);
					print(hit.transform.name);
				}
			}
		}
	}
}

