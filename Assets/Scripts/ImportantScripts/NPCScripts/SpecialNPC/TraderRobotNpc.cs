namespace ImportantScripts.NPCScripts
{
    public class TraderRobotNpc : Dialogue
    {
        public void OnBuySomeThing()
        {
            CurrentNode = 0;
            UpdateNodeAndAnswers();
        }
    }
}