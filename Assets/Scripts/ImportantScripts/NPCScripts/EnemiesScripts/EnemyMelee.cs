using System.Collections;
using ImportantScripts.CharScripts;
using UnityEngine;

namespace ImportantScripts.NPCScripts.EnemiesScripts
{
    public class EnemyMelee : Enemy
    {
        protected override IEnumerator Attack(GameObject other)
        {
            var charComp = other.GetComponent<Char>();
            print(charComp != null);

            while (EnemyState == EnemyStates.Attack)
            {
                agent.SetDestination(other.transform.position);

                if (Vector3.Distance(other.transform.position, gameObject.transform.position) < 3f)
                {
                    print("distance");

                    if (_canAttack)
                    { 
                        if (charComp != null)
                        {
                            _canAttack = false;
                            print(other);
                            print("Attack");
                            charComp.HpNow = charComp.HpNow - attackPower;
                        }

                        yield return new WaitForSeconds(reloadSpeed);
                        _canAttack = true;
                    }
                }
                
                if (Vector3.Distance(other.transform.position,transform.position) > radiusOfSee)
                {
                    ChangeStatesFun(EnemyStates.Searching,null);
                }
            }

           
            yield return null;
        }
    }
}