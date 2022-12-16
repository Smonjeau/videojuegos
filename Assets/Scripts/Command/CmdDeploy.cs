using Strategy;
using UnityEngine;

namespace Command
{
    public class CmdDeploy:ICommand
    {
        private IDeployable _deployable;
        

        public CmdDeploy(IDeployable deployable)
        {
            _deployable = deployable;
        }

        public void Execute() 
        {
            Debug.LogWarning("executing deploy");
            
            _deployable.Deploy();
        } 
       
    }
}