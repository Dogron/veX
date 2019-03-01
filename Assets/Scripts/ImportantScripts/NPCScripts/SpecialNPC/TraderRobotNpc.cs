namespace ImportantScripts.NPCScripts.SpecialNPC
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